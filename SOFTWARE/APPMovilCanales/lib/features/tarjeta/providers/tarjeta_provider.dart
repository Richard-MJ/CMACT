import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/tipo_tarjeta.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/compras_internet/models/afiliacion.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/tarjeta/models/confirmar_cvv_dinamico_response.dart';
import 'package:caja_tacna_app/features/tarjeta/models/obtener_cvv_dinamico_response.dart';
import 'package:caja_tacna_app/features/tarjeta/services/tarjeta_service.dart';
import 'package:caja_tacna_app/features/tarjeta/widgets/dialog_estado_servicio_cvv_dinamico.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final tarjetaProvider = StateNotifierProvider<TarjetaNotifier, TarjetaState>(
    (ref) => TarjetaNotifier(ref));

class TarjetaNotifier extends StateNotifier<TarjetaState> {
  TarjetaNotifier(this.ref) : super(TarjetaState());

  final Ref ref;

  inicializarDatos() {
    ref.read(timerProvider.notifier).cancelTimer();
    state = state.copyWith(
        numeroTarjeta: null,
        datosOfuscados: true,
        datosCargados: false,
        mostrarTokenDigital: false,
        afiliacionComprasInternet: () => null,
        cvv: '***',
        tipoTarjeta: ref.read(homeProvider).datosCliente?.codigoTipoTarjeta ?? TipoTarjeta.debitoVisa,
        fechaVencimiento: '**/**');
  }

  Future<void> obtenerDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    final tarjeta = CtUtils.formatNumeroTarjeta(
        numeroCuenta: ref.read(loginProvider).numeroTarjeta.value, hash: true);

    try {
      if (state.tipoTarjeta == "3") {
        var afiliacionComprasInternet =
            await TarjetaService.obtenerAfiliacion();
        state = state.copyWith(
          afiliacionComprasInternet: () => afiliacionComprasInternet,
        );
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    } finally {
      state = state.copyWith(
        numeroTarjeta: tarjeta,
        datosCargados: true,
      );
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  Future<void> confirmarMostrarDatos(BuildContext context) async {
    ref.read(timerProvider.notifier).cancelTimer();
    if (state.datosOfuscados) {
      resetToken();
      ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
      try {
        final estadoServicio = await TarjetaService.obtenerEstadoServicio(
            numeroTarjeta: ref.read(loginProvider).numeroTarjeta.value);
        if (estadoServicio.estadoServicio == "N") {
          ref.read(loaderProvider.notifier).dismissLoader();
          await showDialog(
              context: context,
              builder: (BuildContext context) {
                return DialogEstadoServicioCvvDinamico(
                    mensajeAlerta: estadoServicio.mensajeAlerta);
              });
          return;
        }

        final response = await TarjetaService.confirmarCvvDinamico(
            identificadorDispositivo: ref
                .read(dispositivoProvider.notifier)
                .getIdentificadorDispositivo());

        final tokenDigital =
            await CoreService.desencriptarToken(response.codigoSolicitado);
        state = state.copyWith(
            confirmarCvvDinamicoResponse: () => response,
            tokenDigital: tokenDigital,
            mostrarTokenDigital: true);
        initTimer();
      } on ServiceException catch (e) {
        ref
            .read(snackbarProvider.notifier)
            .showSnackbar(e.message, SnackbarType.error);
        ref.read(timerProvider.notifier).cancelTimer();
      }

      ref.read(loaderProvider.notifier).dismissLoader();
      return;
    }

    ocultarDatos();
  }

  void ocultarDatos() {
    state = state.copyWith(
        numeroTarjeta: CtUtils.formatNumeroTarjeta(
            numeroCuenta: ref.read(loginProvider).numeroTarjeta.value,
            hash: true),
        cvv: '***',
        fechaVencimiento: '**/**',
        datosOfuscados: true,
        mostrarTokenDigital: false,
        obtenerCvvDinamicoResponse: () => null);
  }

  Future<void> mostrarDatos(BuildContext context) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final response = await TarjetaService.obtenerCvvDinamico(
          codigoAutorizacion: state.tokenDigital,
          numeroTarjeta: ref.read(loginProvider).numeroTarjeta.value);
      final fechaVencimientoTarjeta = CtUtils.formatearVencimientoTarjeta(
          fechaVencimientoTarjeta: response.fechaVencimientoTarjeta);

      state = state.copyWith(
          numeroTarjeta: CtUtils.formatNumeroTarjeta(
              numeroCuenta: response.numeroTarjeta, hash: false),
          cvv: response.cvvDinamico,
          fechaVencimiento: fechaVencimientoTarjeta,
          datosOfuscados: false,
          mostrarTokenDigital: false,
          obtenerCvvDinamicoResponse: () => response);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
      initTimerCvvDinamico(context);
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    } finally {
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  irComprasPorInternet() {
    ref.read(appRouterProvider).push('/compras-internet/configurar-afiliacion');
  }

  irAnularTarjeta() {
    if (!state.datosOfuscados) {
      ref.read(timerProvider.notifier).cancelTimer();
      ocultarDatos();
    }
    ref.read(appRouterProvider).push('/anulacion-tarjetas/anular');
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: resetToken,
          initDate: state.confirmarCvvDinamicoResponse?.fechaSistema,
          date: state.confirmarCvvDinamicoResponse?.fechaVencimiento,
        );
  }

  initTimerCvvDinamico(BuildContext context) {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () => confirmarMostrarDatos(context),
          initDate: state.obtenerCvvDinamicoResponse?.fechaGeneracionCvv,
          date: state.obtenerCvvDinamicoResponse?.fechaVencimientoCvv,
        );
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  copiarNumeroTarjeta() async {
    await Clipboard.setData(ClipboardData(text: state.numeroTarjeta));
    ref
        .read(snackbarProvider.notifier)
        .showSnackbar("Número de tarjeta copiado", SnackbarType.info);
  }
}

class TarjetaState {
  final String numeroTarjeta;
  final String fechaVencimiento;
  final String cvv;
  final bool datosOfuscados;
  final bool datosCargados;
  final bool mostrarTokenDigital;
  final String tokenDigital;
  final String tipoTarjeta;
  final ConfirmarCvvDinamicoResponse? confirmarCvvDinamicoResponse;
  final Afiliacion? afiliacionComprasInternet;
  final ObtenerCvvDinamicoResponse? obtenerCvvDinamicoResponse;

  TarjetaState(
      {this.numeroTarjeta = '',
      this.fechaVencimiento = '**/**',
      this.cvv = '***',
      this.datosOfuscados = false,
      this.datosCargados = false,
      this.mostrarTokenDigital = false,
      this.tokenDigital = '',
      this.tipoTarjeta = '',
      this.afiliacionComprasInternet,
      this.confirmarCvvDinamicoResponse,
      this.obtenerCvvDinamicoResponse});

  TarjetaState copyWith(
          {String? numeroTarjeta,
          String? fechaVencimiento,
          String? cvv,
          bool? datosOfuscados,
          bool? datosCargados,
          bool? mostrarTokenDigital,
          String? tokenDigital,
          String? tipoTarjeta,
          ValueGetter<Afiliacion?>? afiliacionComprasInternet,
          ValueGetter<ConfirmarCvvDinamicoResponse?>?
              confirmarCvvDinamicoResponse,
          ValueGetter<ObtenerCvvDinamicoResponse?>?
              obtenerCvvDinamicoResponse}) =>
      TarjetaState(
          numeroTarjeta: numeroTarjeta ?? this.numeroTarjeta,
          fechaVencimiento: fechaVencimiento ?? this.fechaVencimiento,
          cvv: cvv ?? this.cvv,
          datosOfuscados: datosOfuscados ?? this.datosOfuscados,
          datosCargados: datosCargados ?? this.datosCargados,
          mostrarTokenDigital: mostrarTokenDigital ?? this.mostrarTokenDigital,
          tokenDigital: tokenDigital ?? this.tokenDigital,
          tipoTarjeta: tipoTarjeta ?? this.tipoTarjeta,
          afiliacionComprasInternet: afiliacionComprasInternet != null
              ? afiliacionComprasInternet()
              : this.afiliacionComprasInternet,
          confirmarCvvDinamicoResponse: confirmarCvvDinamicoResponse != null
              ? confirmarCvvDinamicoResponse()
              : this.confirmarCvvDinamicoResponse,
          obtenerCvvDinamicoResponse: obtenerCvvDinamicoResponse != null
              ? obtenerCvvDinamicoResponse()
              : this.obtenerCvvDinamicoResponse);
}

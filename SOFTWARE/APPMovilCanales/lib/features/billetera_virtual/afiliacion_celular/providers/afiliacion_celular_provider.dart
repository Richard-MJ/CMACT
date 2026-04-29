import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/datos_afiliacion_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/services/afiliacion_celular_service.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/states/afiliacion_celular_state.dart';
import 'package:caja_tacna_app/features/billetera_virtual/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/billetera_virtual/widgets/dialog_afiliar_compras_internet.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';
import 'package:formz/formz.dart';

final afiliacionCelularProvider =
    NotifierProvider<AfiliacionCelularNotifier, AfiliacionCelularState>(
        () => AfiliacionCelularNotifier());

class AfiliacionCelularNotifier extends Notifier<AfiliacionCelularState> {
  @override
  AfiliacionCelularState build() {
    return AfiliacionCelularState();
  }

  initDatos() {
    state = state.copyWith(
      tokenDigital: '',
      esAfiliada: false,
      esAfiliadaSimple: false,
      tokenResponse: () => null,
      datosAfiliacion: () => null,
      confirmacionResponse: null,
      esModificacion: false,
      numeroCelular: const NumeroCelular.pure(''),
    );
  }

  goAfiliacionCelular(BuildContext context) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      initDatos();
      await getDatosIniciales(context);
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  getAfiliacionBilleteraVirtual() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final bool afiliacionBilleteraVirtual =
          await AfiliacionCelularService.obtenerAfiliacionBilleteraVirtual();

      state = state.copyWith(
        esAfiliadaSimple: afiliacionBilleteraVirtual,
      );
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  getDatosIniciales(BuildContext context) async {
    final DatosValidacionResponse datosValidacionResponse =
        await AfiliacionCelularService.obtenerDatosAfiliacion();

    if (!datosValidacionResponse.indicadorComprasPorInternet) {
      ref.read(loaderProvider.notifier).dismissLoader();
      if (!context.mounted) return;
      bool? continuar = await showDialog(
        context: context,
        builder: (BuildContext context) {
          return const DialogAfiliarComprarInternet();
        },
      );
      if (continuar == null || !continuar) {
        ref.read(appRouterProvider).pop();
        return;
      }
      ref
          .read(appRouterProvider)
          .replace("/compras-internet/configurar-afiliacion");
      return;
    }

    state = state.copyWith(
      datosAfiliacion: () => datosValidacionResponse.datosAfiliacion,
      numeroCelular: NumeroCelular.pure(
          datosValidacionResponse.datosAfiliacion!.numeroCelular),
      esAfiliada:
          datosValidacionResponse.datosAfiliacion!.indicadorAfiliacionCCE,
      esAfiliadaSimple:
          datosValidacionResponse.datosAfiliacion!.indicadorAfiliacionCCE,
      notificarOperacionesEnviadas:
          datosValidacionResponse.datosAfiliacion!.notificarOperacionesEnviadas,
      notificarOperacionesRecibidas: datosValidacionResponse
          .datosAfiliacion!.notificarOperacionesRecibidas,
    );
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetTokenDigital();
          },
          initDate: state.tokenResponse?.fechaSistema,
          date: state.tokenResponse?.fechaVencimiento,
        );
  }

  afiliar({required bool withPush}) async {
    final datosAfiliacion = state.datosAfiliacion;
    final numeroCelular = NumeroCelular.dirty(state.numeroCelular.value);

    if (!Formz.validate([numeroCelular])) return;
    resetTokenDigital();

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      if (!state.datosAfiliacion!.indicadorAfiliacionCCE) {
        state = state.copyWith(
            notificarOperacionesRecibidas: true,
            notificarOperacionesEnviadas: true);
      }

      final tokenResponse = await AfiliacionCelularService.enviarAfiliacion(
        codigoMonedaCuenta: datosAfiliacion?.codigoMonedaCuenta,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
          tokenResponse: () => tokenResponse,
          tokenDigital: await CoreService.desencriptarToken(
              tokenResponse.codigoSolicitado));

      initTimer();
      if (withPush) {
        ref
            .read(appRouterProvider)
            .push('/billetera-virtual/afiliacion/verificar-numero');
      }
    } on ServiceException catch (e) {
      resetTokenDigital();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }

    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmar() async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.tokenDigital.isEmpty) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Ingrese su token digital', SnackbarType.error);
      return;
    }
    if (state.tokenDigital.length != 6) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('El token debe tener 6 dígitos', SnackbarType.error);
      return;
    }

    state.datosAfiliacion?.codigoAutorizacion = state.tokenDigital;
    final datosAfiliacion = state.datosAfiliacion;

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    ref.read(timerProvider.notifier).cancelTimer();

    try {
      if (state.esAfiliada &&
          state.esModificacion &&
          state.datosAfiliacion!.indicadorAfiliacionCCE) {
        final configuracionNotificacionResponse =
            await AfiliacionCelularService.configurarNotificacionesBilletera(
          codigoAutorizacion: state.tokenDigital,
          numeroCuenta: datosAfiliacion!.numeroCuentaAfiliada,
          notificarOperacionesEnviadas: state.notificarOperacionesEnviadas,
          notificarOperacionesRecibidas: state.notificarOperacionesRecibidas,
        );

        state = state.copyWith(
          configuracionNotificacionResponse: configuracionNotificacionResponse,
        );

        ref
            .read(appRouterProvider)
            .go('/billetera-virtual/afiliacion/configuracion-exitosa');
      } else {
        await AfiliacionCelularService.validarToken(
            numeroVerificacion: state.tokenDigital);

        final ConfirmarResponse confirmacionResponse = state.esAfiliada
            ? await AfiliacionCelularService.enviarAfiliacionCCE(
                datos: datosAfiliacion,
                notificarOperacionesEnviadas:
                    state.notificarOperacionesEnviadas,
                notificarOperacionesRecibidas:
                    state.notificarOperacionesRecibidas)
            : await AfiliacionCelularService.enviarDesfiliacionCCE(
                datosAfiliacion);

        state = state.copyWith(confirmacionResponse: confirmacionResponse);

        ref
            .read(appRouterProvider)
            .push('/billetera-virtual/afiliacion/operacion-exitosa');
      }
    } on ServiceException catch (e) {
      resetTokenDigital();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  resetTokenDigital() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  selectCuenta(bool afiliada) {
    state = state.copyWith(esAfiliada: !afiliada);

    if (!state.esAfiliada) {
      state = state.copyWith(
        notificarOperacionesEnviadas: false,
        notificarOperacionesRecibidas: false,
      );
    } else {
      state = state.copyWith(
        notificarOperacionesEnviadas:
            state.datosAfiliacion!.notificarOperacionesEnviadas,
        notificarOperacionesRecibidas:
            state.datosAfiliacion!.notificarOperacionesRecibidas,
      );
    }
    comprobarModificacion();
  }

  changeNumeroCelular(NumeroCelular numeroCelular) {
    state = state.copyWith(
      numeroCelular: numeroCelular,
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  changeNotificarOperacionesEnviadas(bool notificarOperacionesEnviadas) {
    state = state.copyWith(
      notificarOperacionesEnviadas: notificarOperacionesEnviadas,
    );
    comprobarModificacion();
  }

  changeNotificarOperacionesRecibidas(bool notificarOperacionesRecibidas) {
    state = state.copyWith(
      notificarOperacionesRecibidas: notificarOperacionesRecibidas,
    );
    comprobarModificacion();
  }

  comprobarModificacion() {
    state = state.copyWith(
        esModificacion: state.notificarOperacionesEnviadas !=
                state.datosAfiliacion!.notificarOperacionesEnviadas ||
            state.notificarOperacionesRecibidas !=
                state.datosAfiliacion!.notificarOperacionesRecibidas);
  }
}

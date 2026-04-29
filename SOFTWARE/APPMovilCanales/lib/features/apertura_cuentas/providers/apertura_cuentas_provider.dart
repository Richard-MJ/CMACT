import 'dart:async';

import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/inputs/dias_dpf.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/inputs/monto.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/cuenta_origen.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/services/apertura_cuentas_service.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/states/apertura_cuentas_state.dart';
import 'package:caja_tacna_app/features/emision_giros/models/agencia.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

final aperturaCuentasProvider =
    NotifierProvider<AperturaCuentasNotifier, AperturaCuentasState>(
        () => AperturaCuentasNotifier());

class AperturaCuentasNotifier extends Notifier<AperturaCuentasState> {
  @override
  AperturaCuentasState build() {
    return AperturaCuentasState();
  }

  initDatos() {
    state = state.copyWith(
      cuentasOrigen: [],
      cuentaOrigen: () => null,
      monto: const MontoApertura.pure(''),
      tokenDigital: '',
      correoElectronicoDestinatario: const Email.pure(''),
      confirmarResponse: () => null,
      aperturarResponse: () => null,
      aceptarCartilla: false,
      aceptarClausulas: false,
      aceptarTdp: false,
      agencia: () => null,
      agencias: [],
      tipoCuenta: () => null,
      tiposCuenta: [],
      calculoDpfResponse: () => null,
      diasDpf: const DiasDpf.pure(''),
      gestionTdp: () => null,
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final verificacion = await AperturaCuentasService.validarApertura();

      if (verificacion.validacionDatosCliente.validacionCorreoElectronico !=
              "Correcto" ||
          verificacion.validacionDatosCliente.validacionDatosCliente !=
              "Correcto" ||
          verificacion.validacionDatosCliente.validacionPolitica !=
              "Correcto" ||
          verificacion.validacionDatosCliente.validacionListas != "Correcto" ||
          verificacion.validacionDatosCliente.validacionResidente !=
              "Correcto") {
        ref.read(appRouterProvider).pop();
        ref.read(snackbarProvider.notifier).showSnackbar(
            'Por favor, acérquese a nuestras agencias para aperturar una cuenta.',
            SnackbarType.error);
      } else {
        final DatosInicialesResponse response =
            await AperturaCuentasService.obtenerDatosIniciales();

        state = state.copyWith(
          cuentasOrigen: response.productosDebito,
          tiposCuenta: response.productosApertura,
          gestionTdp: () => response.gestionTdp,
        );

        final agenciasResponse = await AperturaCuentasService.obtenerAgencias();
        agenciasResponse.agencias
            .sort((a, b) => a.nombreAgencia.compareTo(b.nombreAgencia));
        state = state.copyWith(agencias: agenciasResponse.agencias);
      }
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  abrirCuenta({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.tipoCuenta == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione el tipo de cuenta', SnackbarType.error);
      return;
    }

    if (state.cuentaOrigen == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }

    if (state.agencia == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione una agencia', SnackbarType.error);
      return;
    }

    final monto = MontoApertura.dirty(state.monto.value);

    state = state.copyWith(
      monto: monto,
    );

    if (!Formz.validate([
      monto,
    ])) return;
    resetToken();
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final confirmacionInicial =
          await AperturaCuentasService.confirmacionInicial(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        codigoAgencia: state.agencia?.codigoAgencia,
        codigoProducto: state.tipoCuenta?.codigoProducto,
        codigoSistema: state.tipoCuenta?.codigoSistema,
        montoApertura: state.monto.value,
        codigoMoneda: state.tipoCuenta?.codigoMoneda,
        diasDpf:
            state.tipoCuenta?.codigoSistema == 'DP' ? state.diasDpf.value : '',
      );

      if (confirmacionInicial) {
        final aperturarResponse = await AperturaCuentasService.aperturar(
          numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
          codigoAgencia: state.agencia?.codigoAgencia,
          codigoProducto: state.tipoCuenta?.codigoProducto,
          codigoSistema: state.tipoCuenta?.codigoSistema,
          montoApertura: state.monto.value,
          codigoMoneda: state.tipoCuenta?.codigoMoneda,
          identificadorDispositivo: ref
              .read(dispositivoProvider.notifier)
              .getIdentificadorDispositivo(),
        );

        state = state.copyWith(
          aperturarResponse: () => aperturarResponse,
          tokenDigital: await CoreService.desencriptarToken(
            aperturarResponse.codigoSolicitado,
          ),
        );
        initTimer();
        if (withPush) {
          ref.read(appRouterProvider).push('/apertura-cuentas/confirmar');
        }
      }
    } on ServiceException catch (e) {
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
          .showSnackbar('Ingrese su Token Digital', SnackbarType.error);
      return;
    }
    if (state.tokenDigital.length != 6) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('El token debe tener 6 dígitos', SnackbarType.error);
      return;
    }
    if (!state.aceptarCartilla) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Acepte las cartillas de información.', SnackbarType.error);
      return;
    }

    if (!state.aceptarClausulas) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Acepte las clausulas contractuales.', SnackbarType.error);
      return;
    }

    if (!state.aceptarTdp) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Acepte el tratamiento de datos personales.', SnackbarType.error);
      return;
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final confirmarResponse = await AperturaCuentasService.confirmar(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        codigoMoneda: state.tipoCuenta?.codigoMoneda,
        tokenDigital: state.tokenDigital,
        codigoAgencia: state.agencia?.codigoAgencia,
        codigoProducto: state.tipoCuenta?.codigoProducto,
        codigoSistema: state.tipoCuenta?.codigoSistema,
        montoApertura: state.monto.value,
        email: ref.read(homeProvider).datosCliente?.correoElectronico,
        conocimientoTdp: state.gestionTdp?.indicadorConocimientoDatosPersonales,
        consentimientoTdp: state.gestionTdp?.indicadorUsoDatosPersonales,
        diasDpf:
            state.tipoCuenta?.codigoSistema == 'DP' ? state.diasDpf.value : '',
      );

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );

      ref.read(homeProvider.notifier).getCuentas();
      ref.read(appRouterProvider).push('/apertura-cuentas/apertura-exitosa');
    } on ServiceException catch (e) {
      resetToken();
      ref.read(timerProvider.notifier).cancelTimer();

      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.aperturarResponse?.fechaSistema,
          date: state.aperturarResponse?.fechaVencimiento,
        );
  }

  changeTipoCuenta(ProductoApertura tipoCuenta) {
    state = state.copyWith(
      tipoCuenta: () => tipoCuenta,
      diasDpf: const DiasDpf.pure(''),
      calculoDpfResponse: () => null,
    );
  }

  changeCuentaOrien(CuentaOrigenApertura cuentaOrigen) {
    state = state.copyWith(
      cuentaOrigen: () => cuentaOrigen,
    );
  }

  changeAgencia(Agencia agencia) {
    state = state.copyWith(
      agencia: () => agencia,
    );
    calculoDpf();
  }

  changeMonto(MontoApertura monto) {
    if (monto != state.monto) {
      state = state.copyWith(
        monto: monto,
      );
      calculoDpf();
    }
  }

  toggleAceptarCartilla() {
    state = state.copyWith(
      aceptarCartilla: !state.aceptarCartilla,
    );
  }

  toggleAceptarClausulas() {
    state = state.copyWith(
      aceptarClausulas: !state.aceptarClausulas,
    );
  }

  toggleAceptarTdp() {
    state = state.copyWith(
      aceptarTdp: !state.aceptarTdp,
    );
  }

  changeCorreoDestinatario(Email correo) {
    state = state.copyWith(
      correoElectronicoDestinatario: correo,
    );
  }

  changeCuentaOrigen(CuentaOrigenApertura cuenta) {
    state = state.copyWith(
      cuentaOrigen: () => cuenta,
    );
    calculoDpf();
  }

  calculoDpf() async {
    if (state.cuentaOrigen == null ||
        state.agencia == null ||
        state.monto.value == '' ||
        state.diasDpf.value == '' ||
        state.tipoCuenta?.codigoSistema != 'DP') return;

    final numeroCuenta = state.cuentaOrigen?.numeroProducto;
    final codigoAgencia = state.agencia?.codigoAgencia;
    final monto = state.monto.value;
    final diasDpf = state.diasDpf.value;
    final codigoTipoCuenta = state.tipoCuenta?.codigoProducto;

    if (_debounceTimer?.isActive ?? false) _debounceTimer?.cancel();
    _debounceTimer = Timer(
      const Duration(milliseconds: 2000),
      () async {
        if (numeroCuenta != state.cuentaOrigen?.numeroProducto ||
            codigoAgencia != state.agencia?.codigoAgencia ||
            monto != state.monto.value ||
            diasDpf != state.diasDpf.value ||
            codigoTipoCuenta != state.tipoCuenta?.codigoProducto) return;
        ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

        try {
          final calculoDpfResponse = await AperturaCuentasService.calculoDpf(
            numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
            codigoAgencia: state.agencia?.codigoAgencia,
            codigoProducto: state.tipoCuenta?.codigoProducto,
            montoApertura: state.monto.value,
            codigoMoneda: state.tipoCuenta?.codigoMoneda,
            diasDpf: state.diasDpf.value,
          );
          if (!(numeroCuenta != state.cuentaOrigen?.numeroProducto ||
              codigoAgencia != state.agencia?.codigoAgencia ||
              monto != state.monto.value ||
              diasDpf != state.diasDpf.value ||
              codigoTipoCuenta != state.tipoCuenta?.codigoProducto)) {
            state = state.copyWith(
              calculoDpfResponse: () => calculoDpfResponse,
            );
          }
        } on ServiceException catch (e) {
          if (!(numeroCuenta != state.cuentaOrigen?.numeroProducto ||
              codigoAgencia != state.agencia?.codigoAgencia ||
              monto != state.monto.value ||
              diasDpf != state.diasDpf.value ||
              codigoTipoCuenta != state.tipoCuenta?.codigoProducto)) {
            ref
                .read(snackbarProvider.notifier)
                .showSnackbar(e.message, SnackbarType.error);
          }
        }

        if (!(numeroCuenta != state.cuentaOrigen?.numeroProducto ||
            codigoAgencia != state.agencia?.codigoAgencia ||
            monto != state.monto.value ||
            diasDpf != state.diasDpf.value ||
            codigoTipoCuenta != state.tipoCuenta?.codigoProducto)) {
          ref.read(loaderProvider.notifier).dismissLoader();
        }
      },
    );
  }

  reenviarComprobante() async {
    FocusManager.instance.primaryFocus?.unfocus();

    final correoElectronicoDestinatario =
        Email.dirty(state.correoElectronicoDestinatario.value);
    state = state.copyWith(
      correoElectronicoDestinatario: correoElectronicoDestinatario,
    );

    if (!Formz.validate([correoElectronicoDestinatario])) return;

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await SharedService.reenviarComprobante(
        tipoOperacion: "10",
        correoElectronicoDestinatario:
            state.correoElectronicoDestinatario.value,
        idOperacionTts:
            int.parse(state.confirmarResponse?.idOperacionTts ?? '0'),
      );

      ref.read(snackbarProvider.notifier).showSnackbar(
            'Correo enviado con éxito',
            SnackbarType.floating,
          );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  changeDiasDpf(DiasDpf diasDpf) {
    state = state.copyWith(
      diasDpf: diasDpf,
      calculoDpfResponse: () => null,
    );
    calculoDpf();
  }

  Timer? _debounceTimer;
}

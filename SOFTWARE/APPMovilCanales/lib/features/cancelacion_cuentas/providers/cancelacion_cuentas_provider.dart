import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/tipo_transferencia.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/services/cancelacion_cuentas_service.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/states/cancelacion_cuentas_state.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/cuenta_destino.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/services/configurar_cuentas_service.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_documento.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_cci.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_tercero.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/tipo_documento.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

const double _saldoCero = 0;

final cancelacionCuentasProvider =
    NotifierProvider<CancelacionCuentasNotifier, CancelacionCuentasState>(
        () => CancelacionCuentasNotifier());

class CancelacionCuentasNotifier
    extends Notifier<CancelacionCuentasState> {
  
  @override
  CancelacionCuentasState build() {
    return CancelacionCuentasState();
  }

  initDatos() {
    state = state.copyWith(
      tipoTransferencia: () => null,
      numeroCuentaTercero: const NumeroCuentaTercero.pure(''),
      cuentaDestinoPropia: () => null,
      cancelarInternaResponse: () => null,
      cancelarSaldoCeroResponse: () => null,
      confirmarInternaResponse: () => null,
      confirmarSaldoCeroResponse: () => null,
      cuentaTercero: () => null,
      tokenDigital: '',
      cancelarInterbancariaResponse: () => null,
      confirmarInterbancariaResponse: () => null,
      cuentaDestinoCci: const NumeroCuentaCci.pure(''),
      esTitular: false,
      tipoDocumento: () => null,
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      numeroDocumento: const NumeroDocumento.pure(''),
      tiposDocumento: [],
    );
  }

  selectCuentaAhorro(CuentaAhorro cuentaAhorro) {
    state = state.copyWith(
      cuentaAhorro: () => cuentaAhorro,
    );

    //verifica si es saldo cero o no
    if (state.cuentaAhorro?.saldoDisponible == _saldoCero &&
        state.cuentaAhorro?.codigoTipo != 'DP') {
      cancelarCuenta(withPush: true);
    } else {
      ref.read(appRouterProvider).push('/cancelacion-cuentas/configurar-cancelacion');
    }
  }

  changeTipoTransferencia(TipoTransferencia tipoTransferencia) {
    state = state.copyWith(
      tipoTransferencia: () => tipoTransferencia,
      cuentaDestinoPropia: () => null,
      numeroCuentaTercero: const NumeroCuentaTercero.pure(''),
      cuentaDestinoCci: const NumeroCuentaCci.pure(''),
      esTitular: false,
      tipoDocumento: () => state.tiposDocumento[0],
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      numeroDocumento: const NumeroDocumento.pure(''),
    );
  }

  changeCuentaDestinoPropia(CuentaDestinoCancCuenta cuentaDestinoPropia) {
    state = state.copyWith(
      cuentaDestinoPropia: () => cuentaDestinoPropia,
    );
  }

  cancelarCuenta({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();
    resetToken();

    //cancelar con trans propia
    if (state.tipoTransferencia?.id == 1) {
      cancelarTransPropia(withPush: withPush);
      return;
    }

    //cancelar con trans tercero
    if (state.tipoTransferencia?.id == 2) {
      cancelarTransTercero(withPush: withPush);
      return;
    }

    //cancelar con saldo cero
    if (state.tipoTransferencia?.id == null &&
        state.cuentaAhorro?.saldoDisponible == _saldoCero) {
      cancelarSaldoCero(withPush: withPush);
      return;
    }

    //cancelar con transferencia interbancaria
    if (state.tipoTransferencia?.id == 3) {
      cancelarTransInterbancaria(withPush: withPush);
      return;
    }
  }

  confirmar() async {
    FocusManager.instance.primaryFocus?.unfocus();

    //confirmar con trans propia o tercero
    if (state.tipoTransferencia?.id == 1 || state.tipoTransferencia?.id == 2) {
      confirmarTransInterna();
      return;
    }

    //confirmar con saldo cero
    if (state.tipoTransferencia?.id == null &&
        state.cuentaAhorro?.saldoDisponible == _saldoCero) {
      confirmarSaldoCero();
      return;
    }

    //confirmar con transferencia interbancaria
    if (state.tipoTransferencia?.id == 3) {
      confirmarTransInterbancaria();
      return;
    }
  }

  cancelarTransPropia({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final cancelarInternaResponse =
          await CancelacionCuentasService.cancelarTransInterna(
        numeroCuentaDestino: state.cuentaDestinoPropia?.numeroProducto,
        tipoCancelacion: 'propia',
        numeroCuentaAhorro: state.cuentaAhorro?.identificador,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
        codigoTipo: state.cuentaAhorro?.codigoTipo,
      );

      state = state.copyWith(
        cancelarInternaResponse: () => cancelarInternaResponse,
        tokenDigital: await CoreService.desencriptarToken(
          cancelarInternaResponse.datosAutorizacion.codigoSolicitado,
        ),
      );
      initTimer(
          initDate: state.cancelarInternaResponse?.datosAutorizacion.fechaSistema,
          date:state.cancelarInternaResponse?.datosAutorizacion.fechaVencimiento);
      if (withPush) {
        ref.read(appRouterProvider).push('/cancelacion-cuentas/confirmar-interna');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  cancelarTransTercero({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final cuentaTercero =
          await CancelacionCuentasService.obtenerCuentaTercero(
        numCuenta: state.numeroCuentaTercero.value,
      );

      state = state.copyWith(
        cuentaTercero: () => cuentaTercero,
      );

      final cancelarInternaResponse =
          await CancelacionCuentasService.cancelarTransInterna(
        numeroCuentaDestino: state.numeroCuentaTercero.value,
        tipoCancelacion: 'terceros',
        numeroCuentaAhorro: state.cuentaAhorro?.identificador,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
        codigoTipo: state.cuentaAhorro?.codigoTipo,
      );

      state = state.copyWith(
        cancelarInternaResponse: () => cancelarInternaResponse,
        tokenDigital: await CoreService.desencriptarToken(
          cancelarInternaResponse.datosAutorizacion.codigoSolicitado,
        ),
      );
      initTimer(
          initDate: state
              .cancelarInternaResponse?.datosAutorizacion.fechaSistema,
          date: state
              .cancelarInternaResponse?.datosAutorizacion.fechaVencimiento);
      if (withPush) {
        ref.read(appRouterProvider).push('/cancelacion-cuentas/confirmar-interna');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  //para confirmar transferencia propia o tercero
  confirmarTransInterna() async {
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
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final confirmarResponse =
          await CancelacionCuentasService.confirmarTransInterna(
              numeroCuentaDestino: state.tipoTransferencia?.id == 1
                  ? state.cuentaDestinoPropia?.numeroProducto
                  : state.numeroCuentaTercero.value,
              montoCancelar: state.cancelarInternaResponse?.montoCancelacion,
              tokenDigital: state.tokenDigital,
              numeroCuentaAhorro: state.cuentaAhorro?.identificador,
              codigoTipo: state.cuentaAhorro?.codigoTipo,
              cancelacionAnticipada:
                  state.cancelarInternaResponse?.cancelacionAnticipada,
              interesCancelacion:
                  state.cancelarInternaResponse?.interesCancelacion,
              codigoAgencia: state.cuentaAhorro?.codigoAgencia);

      state = state.copyWith(
        confirmarInternaResponse: () => confirmarResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();
      ref.read(appRouterProvider).push('/cancelacion-cuentas/cancelacion-exitosa-interna');
    } on ServiceException catch (e) {
      resetToken();
      ref.read(timerProvider.notifier).cancelTimer();

      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  //cancelacion saldo cero

  cancelarSaldoCero({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final cancelarSaldoCero =
          await CancelacionCuentasService.cancelarSaldoCero(
        numeroCuentaAhorro: state.cuentaAhorro?.identificador,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        cancelarSaldoCeroResponse: () => cancelarSaldoCero,
        tokenDigital: await CoreService.desencriptarToken(
          cancelarSaldoCero.codigoSolicitado,
        ),
      );
      initTimer(
          initDate: state.cancelarSaldoCeroResponse?.fechaSistema,
          date: state.cancelarSaldoCeroResponse?.fechaVencimiento);
      if (withPush) {
        ref.read(appRouterProvider).push('/cancelacion-cuentas/confirmar-interna');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmarSaldoCero() async {
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
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final confirmarSaldoCero =
          await CancelacionCuentasService.confirmarSaldoCero(
        tokenDigital: state.tokenDigital,
        numeroCuentaAhorro: state.cuentaAhorro?.identificador,
      );

      state = state.copyWith(
        confirmarSaldoCeroResponse: () => confirmarSaldoCero,
      );
      ref.read(homeProvider.notifier).getCuentas();

      ref.read(appRouterProvider).push('/cancelacion-cuentas/cancelacion-exitosa-saldo-cero');
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

  initTimer({required DateTime? initDate, required DateTime? date}) {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: initDate,
          date: date,
        );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  changeCuentaTercero(NumeroCuentaTercero numeroCuentaTercero) {
    state = state.copyWith(
      numeroCuentaTercero: numeroCuentaTercero,
    );
  }

  //transf interbancaria

  initDataCancelacion() async {
    initDatos();
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final datosInicialesCceResponse =
          await CancelacionCuentasService.obtenerDatosInicialesCce();

      state = state.copyWith(
        tiposDocumento: datosInicialesCceResponse.tiposDocumento,
        tipoDocumento: () => datosInicialesCceResponse.tiposDocumento[0],
      );

      final datosInicialesResponse =
          await ConfigurarCuentasService.obtenerDatosIniciales();

      state = state.copyWith(
        cuentasDestinoPropias: datosInicialesResponse.productosCredito,
      );
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  cancelarTransInterbancaria({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final response =
          await CancelacionCuentasService.cancelarTransInterbancaria(
        numeroCuentaOrigen: state.cuentaAhorro?.identificador,
        numeroCuentaDestino: state.cuentaDestinoCci.value,
        codigoMoneda: state.cuentaAhorro?.codigoMoneda,
        idTipoDocumento: state.tipoDocumento?.idTipoDocumento,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroDocumento: state.numeroDocumento.value,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        cancelarInterbancariaResponse: () => response,
        tokenDigital: await CoreService.desencriptarToken(
          response.datosAutorizacion.codigoSolicitado,
        ),
      );
      initTimer(
          initDate: response.datosAutorizacion.fechaSistema,
          date: response.datosAutorizacion.fechaVencimiento);
      if (withPush) {
        ref.read(appRouterProvider).push('/cancelacion-cuentas/confirmar-interbancaria');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmarTransInterbancaria() async {
    FocusManager.instance.primaryFocus?.unfocus();

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final response =
          await CancelacionCuentasService.confirmarTransInterbancaria(
        numeroCuentaOrigen: state.cuentaAhorro?.identificador,
        numeroCuentaDestino: state.cuentaDestinoCci.value,
        montoCancelar: state.cuentaAhorro?.saldoDisponible,
        codigoMoneda: state.cuentaAhorro?.codigoMoneda,
        tokenDigital: state.tokenDigital,
        esPersonaNatural: state.cancelarInterbancariaResponse?.esPersonaNatural,
        idTipoDocumento: state.tipoDocumento?.idTipoDocumento,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroDocumento: state.numeroDocumento.value,
      );

      state = state.copyWith(
        confirmarInterbancariaResponse: () => response,
      );
      ref.read(homeProvider.notifier).getCuentas();
      ref.read(appRouterProvider).push('/cancelacion-cuentas/cancelacion-exitosa-interbancaria');
    } on ServiceException catch (e) {
      resetToken();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  changeCuentaDestinoCci(NumeroCuentaCci cuentaDestinoCci) {
    state = state.copyWith(
      cuentaDestinoCci: cuentaDestinoCci,
    );
  }

  changeNombreBeneficiario(NombreBeneficiario nombreBeneficiario) {
    state = state.copyWith(
      nombreBeneficiario: nombreBeneficiario,
    );
  }

  changeDocumento(TipoDocumentoTransInter documento) {
    state = state.copyWith(
      tipoDocumento: () => documento,
      numeroDocumento: const NumeroDocumento.pure(''),
    );
  }

  changeNumeroDocumento(NumeroDocumento numeroDocumento) {
    state = state.copyWith(
      numeroDocumento: numeroDocumento,
    );
  }

  toggleEsTitular() {
    final datosCliente = ref.read(homeProvider).datosCliente;
    final newValue = !state.esTitular;
    state = state.copyWith(
      esTitular: newValue,
      nombreBeneficiario: newValue
          ? NombreBeneficiario.pure('${datosCliente?.nombreCompleto}')
          : const NombreBeneficiario.pure(''),
      numeroDocumento: newValue
          ? NumeroDocumento.pure(datosCliente?.dni ?? '')
          : const NumeroDocumento.pure(''),
    );
  }
}

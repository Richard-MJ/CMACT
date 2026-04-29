import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/monto.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_tercero.dart';
import 'package:caja_tacna_app/features/transferencias/services/transferencia_entre_mis_cuentas_controller.dart';
import 'package:caja_tacna_app/features/transferencias/models/cuenta_transferencia.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_terceros/states/transferencia_terceros_state.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

final transferenciaTercerosProvider =
    NotifierProvider<TransferenciaTercerosNotifier, TransferenciaTercerosState>(
        () => TransferenciaTercerosNotifier());

class TransferenciaTercerosNotifier
    extends Notifier<TransferenciaTercerosState> {
  late final TransferenciaEntreCuentasController _controller;

  @override
  TransferenciaTercerosState build() {
    _controller = TransferenciaEntreCuentasController(ref);
    return TransferenciaTercerosState();
  }

  initDatos() {
    state = TransferenciaTercerosState();
  }

  Future<void> getDatosIniciales() async {
    final response = await _controller.getDatosIniciales();
    if (response == null) {
      ref.read(appRouterProvider).pop();
      return;
    }

    state = state.copyWith(
      cuentasOrigen: response.productosDebito,
    );

    _autoCompletarOpFrecuente();
  }

  _autoCompletarOpFrecuente() {
    try {
      final operacionFrecuente =
          ref.read(operacionesFrecuentesProvider).operacionSeleccionada;
      if (operacionFrecuente != null) {
        final indexCuentaOrigen = state.cuentasOrigen.indexWhere((cuenta) =>
            cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

        if (indexCuentaOrigen >= 0) {
          state = state.copyWith(
            cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
          );
        }

        state = state.copyWith(
          numeroCuentaDestino: NumeroCuentaTercero.dirty(operacionFrecuente
                  .operacionesFrecuenteDetalle.numeroCuentaCredito ??
              ''),
        );

        ref.read(operacionesFrecuentesProvider.notifier).resetOperacion();
      }
    } catch (e) {
      throw ServiceException('Ocurrió un error al cargar la operación');
    }
  }

  Future<void> transferir({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.cuentaOrigen == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }

    final monto = MontoTrans.dirty(state.monto.value);
    final numeroCuentaDestino =
        NumeroCuentaTercero.dirty(state.numeroCuentaDestino.value);

    state = state.copyWith(
      monto: monto,
      numeroCuentaDestino: numeroCuentaDestino,
    );

    if (!Formz.validate([monto, numeroCuentaDestino])) return;
    _resetToken();

    final dispositivo =
        ref.read(dispositivoProvider.notifier).getIdentificadorDispositivo();

    final response = await _controller.transferir(
      cuentaOrigen: state.cuentaOrigen!.numeroProducto,
      cuentaDestino: state.numeroCuentaDestino.value,
      monto: state.monto.value,
      identificadorDispositivo: dispositivo,
    );

    if (response != null) {
      state = state.copyWith(
        transferirResponse: () => response,
        tokenDigital: await CoreService.desencriptarToken(
          response.datosAutorizacion?.codigoSolicitado ?? '',
        ),
      );

      _initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/transferencias/terceros/confirmar');
      }
    }
  }

  _initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            _resetToken();
          },
          initDate: state.transferirResponse?.datosAutorizacion?.fechaSistema,
          date: state.transferirResponse?.datosAutorizacion?.fechaVencimiento,
        );
  }

  _resetToken() => state = state.copyWith(
        tokenDigital: '',
      );

  Future<void> confirmar() async {
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

    final response = await _controller.confirmar(
      cuentaOrigen: state.cuentaOrigen!.numeroProducto,
      cuentaDestino: state.numeroCuentaDestino.value,
      monto: state.monto.value,
      tokenDigital: state.tokenDigital,
      codigoMoneda: state.cuentaOrigen?.codigoMonedaProducto,
    );

    if (response == null) {
      _resetToken();
      ref.read(timerProvider.notifier).cancelTimer();
      return;
    }
    
    state = state.copyWith(
      confirmarResponse: () => response,
    );
    ref.read(homeProvider.notifier).getCuentas();

    if (state.operacionFrecuente) {
      await _controller.agregarOperacionFrecuenteTerceros(
        cuentaOrigen: state.cuentaOrigen?.numeroProducto,
        cuentaDestino: state.numeroCuentaDestino.value,
        nombreOperacionFrecuente: state.nombreOperacionFrecuente,
      );
    }
    ref.read(appRouterProvider).push('/transferencias/terceros/transferencia-exitosa');
  }

  Future<void> reenviarComprobante() async {
    FocusManager.instance.primaryFocus?.unfocus();

    final correoElectronicoDestinatario =
        Email.dirty(state.correoElectronicoDestinatario.value);
    state = state.copyWith(
      correoElectronicoDestinatario: correoElectronicoDestinatario,
    );

    if (!Formz.validate([correoElectronicoDestinatario])) return;

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await _controller.reenviarComprobante(
        tipoOperacion: "1",
        correo: state.correoElectronicoDestinatario.value,
        idOperacionTts: state.confirmarResponse?.idOperacionTts,
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

  toggleOperacionFrecuente() => state = state.copyWith(
        operacionFrecuente: !state.operacionFrecuente,
        nombreOperacionFrecuente: '',
      );

  changeCuentaOrigen(CuentaTransferencia producto) => state = state.copyWith(
        cuentaOrigen: () => producto,
      );

  changeCuentaDestino(NumeroCuentaTercero numeroCuenta) =>
      state = state.copyWith(
        numeroCuentaDestino: numeroCuenta,
      );

  changeMonto(MontoTrans monto) => state = state.copyWith(
        monto: monto,
      );

  changeMotivo(String motivo) => state = state.copyWith(
        motivo: motivo,
      );

  changeToken(String tokenDigital) => state = state.copyWith(
        tokenDigital: tokenDigital,
      );

  changeNombreOperacionFrecuente(String nombreOperacionFrecuente) =>
      state = state.copyWith(
        nombreOperacionFrecuente: nombreOperacionFrecuente,
      );

  changeCorreoDestinatario(Email correo) => state = state.copyWith(
        correoElectronicoDestinatario: correo,
      );
}

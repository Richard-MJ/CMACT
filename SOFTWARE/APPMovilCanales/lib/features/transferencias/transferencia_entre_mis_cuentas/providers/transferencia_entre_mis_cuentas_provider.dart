import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/tipo_operacion_reenvio.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/monto.dart';
import 'package:caja_tacna_app/features/transferencias/models/cuenta_transferencia.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/transferencias/services/transferencia_entre_mis_cuentas_controller.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/states/transferencia_entre_mis_cuentas_state.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

final transferenciaEntreMisCuentasProvider = NotifierProvider<
        TransferenciaEntreMisCuentasNotifier,
        TransferenciaEntreMisCuentasState>(
    () => TransferenciaEntreMisCuentasNotifier());

class TransferenciaEntreMisCuentasNotifier
    extends Notifier<TransferenciaEntreMisCuentasState> {
  late final TransferenciaEntreCuentasController _controller;

  @override
  TransferenciaEntreMisCuentasState build() {
    _controller = TransferenciaEntreCuentasController(ref);
    return TransferenciaEntreMisCuentasState();
  }

  initDatos() {
    state = TransferenciaEntreMisCuentasState();
  }

  Future<void> getDatosIniciales() async {
    final response = await _controller.getDatosIniciales();
    if (response == null) {
      ref.read(appRouterProvider).pop();
      return;
    }
    state = state.copyWith(
      cuentasOrigen: response.productosDebito,
      cuentasDestino: response.productosCredito,
    );

    _autoCompletarOpFrecuente();
  }

  _autoCompletarOpFrecuente() {
    try {
      final operacionFrecuente =
          ref.read(operacionesFrecuentesProvider).operacionSeleccionada;

      if (operacionFrecuente == null) return;

      final indexCuentaOrigen = state.cuentasOrigen.indexWhere(
          (cuenta) => cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

      if (indexCuentaOrigen >= 0) {
        state = state.copyWith(
          cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
        );
      }

      final indexCuentaDestino = state.cuentasDestino.indexWhere((cuenta) =>
          cuenta.numeroProducto ==
          operacionFrecuente.operacionesFrecuenteDetalle.numeroCuentaCredito);

      if (indexCuentaDestino >= 0) {
        state = state.copyWith(
          cuentaDestino: () => state.cuentasDestino[indexCuentaDestino],
        );
      }

      ref.read(operacionesFrecuentesProvider.notifier).resetOperacion();
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
    if (state.cuentaDestino == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de destino', SnackbarType.error);
      return;
    }

    final monto = MontoTrans.dirty(state.monto.value);
    state = state.copyWith(
      monto: monto,
    );

    if (!Formz.validate([monto])) return;

    final dispositivo =
        ref.read(dispositivoProvider.notifier).getIdentificadorDispositivo();

    final response = await _controller.transferir(
      cuentaOrigen: state.cuentaOrigen!.numeroProducto,
      cuentaDestino: state.cuentaDestino!.numeroProducto,
      monto: state.monto.value,
      identificadorDispositivo: dispositivo,
      codigoSistema: state.cuentaDestino!.codigoSistema,
    );

    if (response != null) {
      state = state.copyWith(transferirResponse: () => response);
      if (withPush) {
        ref.read(appRouterProvider).push('/transferencias/entre-mis-cuentas/confirmar');
      }
    }
  }

  Future<void> confirmar() async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.cuentaOrigen == null || state.cuentaDestino == null) return;

    final response = await _controller.confirmar(
      cuentaOrigen: state.cuentaOrigen!.numeroProducto,
      cuentaDestino: state.cuentaDestino!.numeroProducto,
      monto: state.monto.value,
      codigoMoneda: state.cuentaDestino?.codigoMonedaProducto,
      tokenDigital: '',
      codigoSistema: state.cuentaDestino?.codigoSistema,
    );

    if (response != null) {
      state = state.copyWith(confirmarResponse: () => response);
      ref.read(homeProvider.notifier).getCuentas();

      if (state.operacionFrecuente) {
        await _controller.agregarOperacionFrecuentePropia(
          cuentaOrigen: state.cuentaOrigen!,
          cuentaDestino: state.cuentaDestino!,
          nombreOperacionFrecuente: state.nombreOperacionFrecuente,
        );
      }

      ref.read(appRouterProvider).push('/transferencias/entre-mis-cuentas/transferencia-exitosa');
    }
  }

  Future<void> reenviarComprobante() async {
    FocusManager.instance.primaryFocus?.unfocus();

    final correoElectronicoDestinatario =
        Email.dirty(state.correoElectronicoDestinatario.value);
    state = state.copyWith(
      correoElectronicoDestinatario: correoElectronicoDestinatario,
    );

    if (!Formz.validate([correoElectronicoDestinatario])) return;

    try {
      await _controller.reenviarComprobante(
        tipoOperacion: state.cuentaDestino?.codigoSistema == "DP"
          ? TipoOperacionReenvio.transferenciaDPF
          : TipoOperacionReenvio.transferencia,
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
  }

  toggleOperacionFrecuente() => state = state.copyWith(
        operacionFrecuente: !state.operacionFrecuente,
        nombreOperacionFrecuente: '',
      );

  changeCuentaOrigen(CuentaTransferencia producto) => state = state.copyWith(
        cuentaOrigen: () => producto,
      );

  changeCuentaDestino(CuentaTransferencia producto) {
    state = state.copyWith(
      cuentaDestino: () => producto,
    );
    if (state.cuentaDestino != null &&
        state.cuentaDestino?.codigoSistema == 'DP') {
      state = state.copyWith(
        monto: MontoTrans.pure(CtUtils.formatStringWithTwoDecimals(
            state.cuentaDestino!.montoCuota.toString())),
      );
    } else {
      state = state.copyWith(
        monto: const MontoTrans.pure(''),
      );
    }
  }

  changeMonto(MontoTrans monto) => state = state.copyWith(
        monto: monto,
      );

  changeMotivo(String motivo) => state = state.copyWith(
        motivo: motivo,
      );

  changeNombreOperacionFrecuente(String nombreOperacionFrecuente) =>
      state = state.copyWith(
        nombreOperacionFrecuente: nombreOperacionFrecuente,
      );

  changeCorreoDestinatario(Email correo) => state = state.copyWith(
        correoElectronicoDestinatario: correo,
      );
}

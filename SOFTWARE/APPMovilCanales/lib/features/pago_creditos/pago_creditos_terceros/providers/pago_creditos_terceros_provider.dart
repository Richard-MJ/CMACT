import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/tipo_operacion_reenvio.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/credito_abonar.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/opcion_pago_credito.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/producto_debito.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/index.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/services/pago_creditos_propios_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/services/pago_creditos_terceros_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/providers/pago_creditos_anticipo_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/inputs/monto_abonar.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

enum TipoPago { aplicativo, cip }

final pagoCreditoTercerosProvider =
    NotifierProvider<PagoCreditoTercerosNotifier, PagoCreditoTercerosState>(
        () => PagoCreditoTercerosNotifier());

class PagoCreditoTercerosNotifier extends Notifier<PagoCreditoTercerosState> {
  @override
  PagoCreditoTercerosState build() {
    return PagoCreditoTercerosState();
  }

  initDatos() {
    state = state.copyWith(
      cuentaOrigen: () => null,
      numeroCredito: '',
      creditoAbonar: () => null,
      monto: const MontoAbonar.pure(''),
      tokenDigital: '',
      confirmarAppResponse: () => null,
      correoElectronicoDestinatario: const Email.pure(''),
      cuentasOrigen: [],
      pagarAppResponse: () => null,
      nombreOperacionFrecuente: '',
      operacionFrecuente: false,
      tipoPagoCredito: TipoPagoCredito.abono,
      tipoAnticipo: TipoAnticipo.defecto,
    );
    ref.read(pagoCreditoAnticipoProvider.notifier).initDatos();
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final DatosInicialesResponse datosInicialesResponse =
          await PagoCreditosPropiosService.obtenerDatosIniciales();

      state = state.copyWith(
        cuentasOrigen: datosInicialesResponse.productosDebito,
      );
      await autoCompletarOpFrecuente();
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  autoCompletarOpFrecuente() async {
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

      state = state.copyWith(
        numeroCredito:
            operacionFrecuente.operacionesFrecuenteDetalle.numeroCredito,
      );

      await getDatosCredito();

      ref.read(operacionesFrecuentesProvider.notifier).resetOperacion();
    } catch (e) {
      throw ServiceException('Ocurrió un error al cargar la operación');
    }
  }

  Future<void> getDatosCredito() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final currentValue = state.numeroCredito;

      final CreditoAbonar creditoAbonarResponse =
          await PagoCreditosTercerosService.obtenerDatosCreditoTercero(
        numeroCredito: currentValue,
      );

      if (currentValue == state.numeroCredito) {
        state = state.copyWith(
          creditoAbonar: () => creditoAbonarResponse,
        );
        changeTipoPagoCredito(TipoPagoCredito.abono);
        FocusManager.instance.primaryFocus?.unfocus();
      }
    } on ServiceException catch (e) {
      state = state.copyWith(
          creditoAbonar: () => null,
          monto: const MontoAbonar.pure(''),
          tipoPagoCredito: TipoPagoCredito.abono);
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  pagar({required bool withPush}) async {
    if (state.cuentaOrigen == null && state.tipoPago == TipoPago.aplicativo) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }

    if (!state.tipoPagoCredito.esAnticipo()) {
      final monto =
          state.monto.copyWith(value: state.monto.value, isPure: false);

      state = state.copyWith(
        monto: monto,
      );

      if (!Formz.validate([monto])) return;
    } else {
      if (!ref
          .read(pagoCreditoAnticipoProvider.notifier)
          .validarMontoAnticipo()) return;
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    // if (!context.mounted) return;
    if (state.tipoPago == TipoPago.aplicativo) {
      await pagarApp(withPush: withPush);
    } else {
      await generarCip();
    }

    ref.read(loaderProvider.notifier).dismissLoader();
  }

  pagarApp({required bool withPush}) async {
    resetToken();
    try {
      if (!state.tipoPagoCredito.esAnticipo()) {
        final pagarAppResponse = await PagoCreditosPropiosService.pagar(
          numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
          numeroCreditoDestino: state.creditoAbonar?.numeroCredito,
          monto: state.monto.value,
          cancelacionCredito:
              state.tipoPagoCredito == TipoPagoCredito.cancelacion,
          codigoMonedaOrigen: state.cuentaOrigen?.codigoMonedaProducto,
          codigoMonedaDestino: state.creditoAbonar?.codigoMoneda,
          identificadorDispositivo: ref
              .read(dispositivoProvider.notifier)
              .getIdentificadorDispositivo(),
        );

        state = state.copyWith(
          pagarAppResponse: () => pagarAppResponse,
          tokenDigital: await CoreService.desencriptarToken(
            pagarAppResponse.codigoSolicitado,
          ),
        );
      } else {
        final pagarAppAnticipoResponse = await ref
            .read(pagoCreditoAnticipoProvider.notifier)
            .pagarAnticipo(
                numeroProducto: state.cuentaOrigen?.numeroProducto,
                numeroCredito: state.creditoAbonar?.numeroCredito,
                tipoPago: state.tipoPagoCredito,
                tipoAnticipo: state.tipoAnticipo,
                tipoSolicitante: TipoSolicitante.tercero.obtenerIdentificador(),
                codigoMonedaProducto: state.cuentaOrigen?.codigoMonedaProducto,
                withPush: withPush);

        state = state.copyWith(
          pagarAppResponse: () =>
              pagarAppAnticipoResponse.datosTokenDigital,
          tokenDigital: await CoreService.desencriptarToken(
            pagarAppAnticipoResponse.datosTokenDigital.codigoSolicitado,
          ),
        );
      }

      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push(
          '/pago-creditos/creditos-terceros/confirmar',
        );
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  generarCip() async {
    try {
      final generarCipResponse = await PagoCreditosPropiosService.generarCip(
        monto: state.monto.value,
        cancelacionCredito:
            state.tipoPagoCredito == TipoPagoCredito.cancelacion,
        email: ref.read(homeProvider).datosCliente?.correoElectronico,
        numeroCredito: state.creditoAbonar?.numeroCredito,
        numeroCuota: state.creditoAbonar?.cuotaActual,
        numeroTelefono: ref.read(homeProvider).datosCliente?.numeroTelefonoSms,
      );

      state = state.copyWith(
        generarCipResponse: () => generarCipResponse,
      );

      ref.read(appRouterProvider).push(
        '/pago-creditos/creditos-terceros/pago-exitoso-cip',
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  confirmar() async {
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
      if (!state.tipoPagoCredito.esAnticipo()) {
        final confirmarResponse = await PagoCreditosPropiosService.confirmar(
          numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
          numeroCreditoDestino: state.creditoAbonar?.numeroCredito,
          monto: state.monto.value,
          cancelacionCredito:
              state.tipoPagoCredito == TipoPagoCredito.cancelacion,
          codigoMonedaOrigen: state.cuentaOrigen?.codigoMonedaProducto,
          codigoMonedaDestino: state.creditoAbonar?.codigoMoneda,
          tokenDigital: state.tokenDigital,
        );

        state = state.copyWith(confirmarAppResponse: () => confirmarResponse);

        ref.read(homeProvider.notifier).getCuentas();
        agregarOperacionFrecuente();
      } else {
        var confirmarAppResponse = await ref
            .read(pagoCreditoAnticipoProvider.notifier)
            .confirmarAnticipo(
              numeroProducto: state.cuentaOrigen?.numeroProducto,
              numeroCredito: state.creditoAbonar?.numeroCredito,
              tipoAnticipo: state.tipoAnticipo,
              tipoPago: state.tipoPagoCredito,
              tipoSolicitante: TipoSolicitante.tercero.obtenerIdentificador(),
              codigoMonedaProducto: state.cuentaOrigen?.codigoMonedaProducto,
              codigoAutorizacion: state.tokenDigital,
            );

        state = state.copyWith(
          confirmarAppResponse: () => confirmarAppResponse,
        );
      }

      ref.read(appRouterProvider).push(
        '/pago-creditos/creditos-terceros/pago-exitoso',
      );
    } on ServiceException catch (e) {
      resetToken();
      ref.read(timerProvider.notifier).cancelTimer();

      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
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
        tipoOperacion: state.tipoPagoCredito.esAnticipo()
          ? TipoOperacionReenvio.anticipoPagoCredito
          : TipoOperacionReenvio.pagoCredito,
        correoElectronicoDestinatario:
            state.correoElectronicoDestinatario.value,
        idOperacionTts: state.confirmarAppResponse?.idOperacionTts,
        codigoTipoAnticipo: state.tipoAnticipo.obtenerIdentificador(),
        codigoTipoPago: state.tipoPagoCredito.obtenerIdentificador(),
        codigoTipoSolicitante: TipoSolicitante.tercero.obtenerIdentificador()
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

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.pagarAppResponse?.fechaSistema,
          date: state.pagarAppResponse?.fechaVencimiento,
        );
  }

  changeCuentaOrigen(ProductoDebito producto) {
    state = state.copyWith(
      cuentaOrigen: () => producto,
    );
  }

  changeMonto(MontoAbonar monto) {
    state = state.copyWith(
      monto: monto,
    );
  }

  setTipoPago(TipoPago tipoPago) {
    state = state.copyWith(
      tipoPago: tipoPago,
    );
    ref.read(appRouterProvider).push('/pago-creditos/creditos-terceros/pagar');
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeTipoPagoCredito(TipoPagoCredito tipoPagoCredito) {
    state = state.copyWith(
      tipoPagoCredito: tipoPagoCredito,
      monto: state.monto.copyWith(
        value: tipoPagoCredito == TipoPagoCredito.cancelacion
            ? state.creditoAbonar?.montoSaldoCancelacion.toString()
            : CtUtils.formatStringWithTwoDecimals(
                state.creditoAbonar?.montoTotalPago.toString() ?? '',
              ),
        montoCuota: tipoPagoCredito == TipoPagoCredito.cancelacion
            ? state.creditoAbonar?.montoSaldoCancelacion
            : state.creditoAbonar?.montoTotalPago,
        isPure: true,
        simboloMoneda: state.creditoAbonar?.simboloMoneda,
      ),
    );

    ref.read(pagoCreditoAnticipoProvider.notifier).setMontoAnticipo(
          state.creditoAbonar!,
        );
  }

  changeToken(String tokenDigital) {
    if (tokenDigital.length == 6) {
      FocusManager.instance.primaryFocus?.unfocus();
    }
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  changeNumeroCredito(String numeroCredito) {
    state = state.copyWith(
      numeroCredito: numeroCredito,
      creditoAbonar: () => null,
    );
  }

  changeCorreoDestinatario(Email correo) {
    state = state.copyWith(
      correoElectronicoDestinatario: correo,
    );
  }

  agregarOperacionFrecuente() async {
    try {
      if (!state.operacionFrecuente) return;
      await OperacionesFrecuentesService.agregarPagoCredito(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        nombreOperacionFrecuente: state.nombreOperacionFrecuente,
        numeroCredito: state.creditoAbonar?.numeroCredito,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  toggleOperacionFrecuente() {
    state = state.copyWith(
      operacionFrecuente: !state.operacionFrecuente,
      nombreOperacionFrecuente: '',
    );
  }

  changeNombreOperacionFrecuente(String nombreOperacionFrecuente) {
    state = state.copyWith(
      nombreOperacionFrecuente: nombreOperacionFrecuente,
    );
  }

  changeReduccionAnticipo(TipoAnticipo tipoAnticipo) {
    state = state.copyWith(tipoAnticipo: tipoAnticipo);
  }
}

class PagoCreditoTercerosState {
  final List<ProductoDebito> cuentasOrigen;
  final String numeroCredito;
  final ProductoDebito? cuentaOrigen;
  final CreditoAbonar? creditoAbonar;
  final MontoAbonar monto;
  final String tokenDigital;
  final TipoPago tipoPago;
  final Email correoElectronicoDestinatario;
  final PagarAppResponse? pagarAppResponse;
  final ConfirmarAppResponse? confirmarAppResponse;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final GenerarCipResponse? generarCipResponse;
  final TipoPagoCredito tipoPagoCredito;
  final TipoAnticipo tipoAnticipo;

  PagoCreditoTercerosState({
    this.cuentasOrigen = const [],
    this.cuentaOrigen,
    this.numeroCredito = '',
    this.creditoAbonar,
    this.monto = const MontoAbonar.pure(''),
    this.tokenDigital = '',
    this.tipoPago = TipoPago.aplicativo,
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.pagarAppResponse,
    this.confirmarAppResponse,
    this.operacionFrecuente = false,
    this.nombreOperacionFrecuente = '',
    this.generarCipResponse,
    this.tipoPagoCredito = TipoPagoCredito.abono,
    this.tipoAnticipo = TipoAnticipo.monto,
  });

  PagoCreditoTercerosState copyWith({
    List<ProductoDebito>? cuentasOrigen,
    ValueGetter<ProductoDebito?>? cuentaOrigen,
    String? numeroCredito,
    ValueGetter<CreditoAbonar?>? creditoAbonar,
    MontoAbonar? monto,
    String? tokenDigital,
    TipoPago? tipoPago,
    Email? correoElectronicoDestinatario,
    ValueGetter<PagarAppResponse?>? pagarAppResponse,
    ValueGetter<ConfirmarAppResponse?>? confirmarAppResponse,
    ValueGetter<GenerarCipResponse?>? generarCipResponse,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    TipoPagoCredito? tipoPagoCredito,
    TipoAnticipo? tipoAnticipo,
  }) =>
      PagoCreditoTercerosState(
          cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
          cuentaOrigen:
              cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
          numeroCredito: numeroCredito ?? this.numeroCredito,
          creditoAbonar:
              creditoAbonar != null ? creditoAbonar() : this.creditoAbonar,
          monto: monto ?? this.monto,
          tokenDigital: tokenDigital ?? this.tokenDigital,
          tipoPago: tipoPago ?? this.tipoPago,
          correoElectronicoDestinatario: correoElectronicoDestinatario ??
              this.correoElectronicoDestinatario,
          pagarAppResponse: pagarAppResponse != null
              ? pagarAppResponse()
              : this.pagarAppResponse,
          confirmarAppResponse: confirmarAppResponse != null
              ? confirmarAppResponse()
              : this.confirmarAppResponse,
          generarCipResponse: generarCipResponse != null
              ? generarCipResponse()
              : this.generarCipResponse,
          operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
          nombreOperacionFrecuente:
              nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,
          tipoPagoCredito: tipoPagoCredito ?? this.tipoPagoCredito,
          tipoAnticipo: tipoAnticipo ?? this.tipoAnticipo);
}

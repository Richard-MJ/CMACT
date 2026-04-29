import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/services/pago_tarjetas_credito_inmediata_service.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/models/datos_operacion_exitosa_response.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/models/detalle_transferencia_response.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/models/montos_totales_response.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/models/token_response.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/models/tipo_documento.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inputs/numero_tarjeta_credito.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/providers/pago_tarjetas_credito_provider.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/monto_transferir.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_documento.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/features/home/models/configuracion.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:caja_tacna_app/api/api.dart';
import 'package:flutter/material.dart';
import 'package:formz/formz.dart';


final pagoTarjetasCreditoInmediataProvider = StateNotifierProvider<
    PagoTarjetasCreditoInmediataNotifier, PagoTarjetasCreditoInmediataState>((ref) {
  return PagoTarjetasCreditoInmediataNotifier(ref);
});

class PagoTarjetasCreditoInmediataNotifier
    extends StateNotifier<PagoTarjetasCreditoInmediataState> {
  PagoTarjetasCreditoInmediataNotifier(this.ref) : super(PagoTarjetasCreditoInmediataState());

  final api = Api();
  final Ref ref;

  String codigoAppMovil = "91";
  String codigoEntidadCMACT = "813";
  String codigoTipoPagoTarjetaCredito = "325";
  String codigoDocumentoTerminos = "COD_01";
  int tipoOperacionPagoTarjeta = 11;

  initDatos() {
    state = state.copyWith(
      cuentasOrigen: [],
      cuentaOrigen: () => null,
      numeroTarjetaCredito: const NumeroTarjetaCredito.pure(''),
      aceptarTerminos: false,
      monto: const MontoTransferir.pure(''),
      tokenDigital: '',
      motivo: '',
      operacionFrecuente: false,
      correoElectronicoDestinatario: const Email.pure(''),
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      numeroDocumento: const NumeroDocumento.pure(''),
      esTitular: false,
      nombreOperacionFrecuente: '',
      datosOperacionesExitosaResponse: () => null,
      tiposDocumento: [],
      tipoDocumento: () => null,
      entidadesFinancieras: [],
      entidadFinanciera: () => null,
      limitesTransferencias: [],
      tokenResponse: () => null,
      documentoTermino:() => null,
      detalleTransferenciaResponse: () => null,
      montosTotales: () => null
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final DatosInicialesResponse datosInicialesResponse =
          await PagoTarjetasCreditoInmediataService.obtenerDatosIniciales();

      final documentoTermino = ref.read(homeProvider).configuracion?.enlacesDocumentos
        .firstWhere((x) => x.codigoDocumento == codigoDocumentoTerminos);
      documentoTermino!.dataDocumento = await SharedService.convertirPdfAsBytes(documentoTermino.urlDocumento);

      final entidadesFiltradas = datosInicialesResponse.entidadesFinancieras
        .where((x) => x.oficinaPagoTarjeta.isNotEmpty)
        .toList();

      state = state.copyWith(
        cuentasOrigen: datosInicialesResponse.productosDebito,
        tiposDocumento: datosInicialesResponse.tiposDocumentos,
        entidadesFinancieras: entidadesFiltradas,
        limitesTransferencias: datosInicialesResponse.limitesTransferencias,
        tipoDocumento: () => datosInicialesResponse.tiposDocumentos[0],
        documentoTermino: () => documentoTermino
      );

      await autoCompletarOpFrecuente();

      ref.read(loaderProvider.notifier).dismissLoader();
    } on ServiceException catch (e) {
      ref.read(tipoPagoTarjetaCreditoTransferenciaProvider.notifier).state = TipoTransferencia.diferida;
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  bool isLoadingNumeroTarjeta = false;
  changeNumeroTarjetaCredito(NumeroTarjetaCredito numeroTarjetaCredito) async {
    state = state.copyWith(
      numeroTarjetaCredito: numeroTarjetaCredito,
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      numeroDocumento: const NumeroDocumento.pure(''),
    );

    if (!Formz.validate([
      numeroTarjetaCredito,
    ])) {
      return;
    }

    await Future.delayed(Duration(milliseconds: 50));

    if (isLoadingNumeroTarjeta) return;
    isLoadingNumeroTarjeta = true;

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {

      final detalleTransferenciaResponse = await PagoTarjetasCreditoInmediataService
        .obtenerDatosCuentaReceptor(
          codigoTipoTransferencia: codigoTipoPagoTarjetaCredito,
          numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
          numeroTarjetaCredito: state.numeroTarjetaCredito.value,
          codigoEntidadReceptora: state.entidadFinanciera!.codigoEntidad,
          codigoCanalCCE: codigoAppMovil);

      if(detalleTransferenciaResponse.tipoDocumentoReceptor != "0")
      {
        state = state.copyWith(
          tipoDocumento:() => state.tiposDocumento.firstWhere(
            (d) => d.codigoTipoDocumentoCamaraCompensacion == int.tryParse(detalleTransferenciaResponse.tipoDocumentoReceptor),
          )
        );
      }

      state = state.copyWith(
        detalleTransferenciaResponse: () => detalleTransferenciaResponse,
        numeroDocumento: NumeroDocumento.pure(detalleTransferenciaResponse.numeroIdentidadReceptor),
        nombreBeneficiario: NombreBeneficiario.pure(detalleTransferenciaResponse.nombreReceptor),
        esTitular: detalleTransferenciaResponse.mismoTitular == "M"
      );
      
    } on ServiceException catch (e) {
      ref.read(snackbarProvider.notifier).showSnackbar(e.message, SnackbarType.error);
    }

    isLoadingNumeroTarjeta = false;
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  ingresarMonto() {
    FocusManager.instance.primaryFocus?.unfocus();
    final numeroTarjetaCredito =
        NumeroTarjetaCredito.dirty(state.numeroTarjetaCredito.value);
    final numeroDocumento = NumeroDocumento.dirty(state.numeroDocumento.value);
    final nombreBeneficiario =
        NombreBeneficiario.dirty(state.nombreBeneficiario.value);
    state = state.copyWith(
      numeroTarjetaCredito: numeroTarjetaCredito,
      nombreBeneficiario: nombreBeneficiario,
      numeroDocumento: numeroDocumento,
    );
    if (!Formz.validate([
      numeroTarjetaCredito,
      nombreBeneficiario,
    ])) {
      return;
    }

    ref.read(appRouterProvider).push('/pago-tarjetas-credito/ingreso-monto-pagar-inmediata');
  }

  pagarTarjeta({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.cuentaOrigen == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }

    final monto = MontoTransferir.dirty(state.monto.value);
    final numeroTarjetaCredito = NumeroTarjetaCredito.dirty(state.numeroTarjetaCredito.value);
    final nombreBeneficiario = NombreBeneficiario.dirty(state.nombreBeneficiario.value);

    state = state.copyWith(
      monto: monto,
      numeroTarjetaCredito: numeroTarjetaCredito,
      nombreBeneficiario: nombreBeneficiario,
    );

    if (!Formz.validate([
      monto,
      numeroTarjetaCredito,
      nombreBeneficiario,
    ])) {
      return;
    }

    resetToken();
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final MontosTotales montosTotales = await PagoTarjetasCreditoInmediataService.calcularMontosTotales(
        numeroCuenta: state.cuentaOrigen?.numeroProducto,
        mismoTitular: state.detalleTransferenciaResponse?.mismoTitular, 
        saldoActual: state.cuentaOrigen?.montoSaldo,
        montoOperacion: state.monto.value,
        montoMinimoCuenta: state.cuentaOrigen?.montoMinimo,
        esExoneradaItf: state.cuentaOrigen?.esExoneradaItf,
        esCuentaSueldo: state.cuentaOrigen?.esCuentaSueldo,
        comision: state.detalleTransferenciaResponse?.comision,
        esExoneradoComision: state.detalleTransferenciaResponse?.esExoneradoComision
      );

      final tokenResponse = await PagoTarjetasCreditoInmediataService.obtenerTokenDigital(
          codigoMonedaCuenta: state.detalleTransferenciaResponse?.codigoMoneda,
          identificadorDispositivo: ref
              .read(dispositivoProvider.notifier)
              .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        montosTotales: () => montosTotales,
        tokenResponse: () => tokenResponse,
        tokenDigital: await CoreService.desencriptarToken(tokenResponse.codigoSolicitado),
      );
      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/pago-tarjetas-credito/confirmar-inmediata');
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
    if (!state.aceptarTerminos) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Acepte los terminos y condiciones.', SnackbarType.error);
      return;
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await PagoTarjetasCreditoInmediataService
        .validarToken(numeroVerificacion: state.tokenDigital);

      final datosOperacionResponse = await PagoTarjetasCreditoInmediataService
        .confirmarTransferencia(        
          numeroCuenta: state.cuentaOrigen?.numeroProducto,
          controlMonto: state.montosTotales?.controlMonto,
          detalleTransferencia: state.detalleTransferenciaResponse,
          motivo: state.motivo,
          nombreTerminos: state.documentoTermino!.nombreDocumento,
          documentoTerminos: state.documentoTermino!.dataDocumento
        );

      state = state.copyWith(
        datosOperacionesExitosaResponse: () => datosOperacionResponse,
      );

      ref.read(homeProvider.notifier).getCuentas();
      agregarOperacionFrecuente();
      ref.read(appRouterProvider).push('/pago-tarjetas-credito/pago-inmediata-exitoso');
    } on ServiceException catch (e) {
      resetToken();
      ref.read(timerProvider.notifier).cancelTimer();
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

      if (operacionFrecuente == null || operacionFrecuente.numeroTipoOperacionFrecuente != tipoOperacionPagoTarjeta) 
      {
        ref.read(loaderProvider.notifier).dismissLoader();
        return;
      }

      final indexEntidadReceptor = state.entidadesFinancieras.indexWhere(
          (cuenta) => cuenta.idEntidadCce == int.tryParse(operacionFrecuente.operacionesFrecuenteDetalle.tipoDocumento!));

      final indexCuentaOrigen = state.cuentasOrigen.indexWhere(
          (cuenta) => cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

      if (indexEntidadReceptor < 0 || indexCuentaOrigen < 0)
      {
        ref.read(loaderProvider.notifier).dismissLoader();
        return;
      }

      state = state.copyWith(
        entidadFinanciera: () => state.entidadesFinancieras[indexEntidadReceptor],
        cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
      );

      var numeroTarjetaCredito = NumeroTarjetaCredito
        .pure(operacionFrecuente.operacionesFrecuenteDetalle.cuentaDestinoCci ?? '');

      await changeNumeroTarjetaCredito(numeroTarjetaCredito);

      ref.read(operacionesFrecuentesProvider.notifier).resetOperacion();
    } catch (e) {
      throw ServiceException('Ocurrió un error al cargar la operación');
    }
  }

  agregarOperacionFrecuente() async {
    try {
      if (!state.operacionFrecuente) return;
      await OperacionesFrecuentesService.agregarTransInterbancariaInmediata(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        nombreOperacionFrecuente: state.nombreOperacionFrecuente,
        esTitular: state.esTitular,
        idTipoDocumentoCompensacion: state.entidadFinanciera?.idEntidadCce,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroCuentaReceptor: state.numeroTarjetaCredito.value,
        numeroDocumento: state.numeroDocumento.value,
        tipoOperacionFrecuente: tipoOperacionPagoTarjeta
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  reenviarComprobante() async {
    FocusManager.instance.primaryFocus?.unfocus();

    final correoElectronicoDestinatario = Email.dirty(state.correoElectronicoDestinatario.value);
    state = state.copyWith(
      correoElectronicoDestinatario: correoElectronicoDestinatario,
    );

    if (!Formz.validate([correoElectronicoDestinatario])) return;

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await PagoTarjetasCreditoInmediataService.enviarCorreoElectronicoTransferencia(
          numeroMovimiento: state.datosOperacionesExitosaResponse?.numeroOperacion,
          correoElectronicoDestinatario: state.correoElectronicoDestinatario.value,
          nombreTerminos: state.documentoTermino!.nombreDocumento,
          documentoTerminos: state.documentoTermino!.dataDocumento
      );

      ref.read(snackbarProvider.notifier)
        .showSnackbar('Correo enviado con éxito', SnackbarType.floating);
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
      initDate: state.tokenResponse?.fechaSistema,
      date: state.tokenResponse?.fechaVencimiento,
    );
  }

  changeEntidadFinanciera(EntidadFinanciera entidadFinanciera) {
    state = state.copyWith(
      entidadFinanciera: () => entidadFinanciera,
    );
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  toggleAceptarTerminos() {
    state = state.copyWith(
      aceptarTerminos: !state.aceptarTerminos,
    );
  }

  toggleOperacionFrecuente() {
    state = state.copyWith(
      operacionFrecuente: !state.operacionFrecuente,
      nombreOperacionFrecuente: '',
    );
  }

  changeCuentaOrigen(CuentaEfectivo producto) {
    var limiteTransferencia = state.limitesTransferencias.firstWhere((d) => 
        d.codigoTipoTransferencia == codigoTipoPagoTarjetaCredito && 
        d.codigoMoneda == producto.codigoMoneda
    );
    
    state = state.copyWith(
      cuentaOrigen: () => producto,
      monto: MontoTransferir.pure('',
        montoMaximo: limiteTransferencia.montoMaximo,
        montoMinimo: limiteTransferencia.montoMinimo),
      numeroTarjetaCredito: const NumeroTarjetaCredito.pure(''),
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      numeroDocumento: const NumeroDocumento.pure(''),
    );
  }

  changeMonto(MontoTransferir monto) {
    state = state.copyWith(
      monto: monto,
    );
  }

  changeMotivo(String motivo) {
    state = state.copyWith(
      motivo: motivo,
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  changeNombreOperacionFrecuente(String nombreOperacionFrecuente) {
    state = state.copyWith(
      nombreOperacionFrecuente: nombreOperacionFrecuente,
    );
  }

  changeCorreoDestinatario(Email correo) {
    state = state.copyWith(
      correoElectronicoDestinatario: correo,
    );
  }
}

class PagoTarjetasCreditoInmediataState {
  final List<CuentaEfectivo> cuentasOrigen;
  final List<TipoDocumentoTransInter> tiposDocumento;
  final List<LimitesTransferencias> limitesTransferencias;  
  final List<EntidadFinanciera> entidadesFinancieras;
  final CuentaEfectivo? cuentaOrigen;
  final NumeroTarjetaCredito numeroTarjetaCredito;
  final MontoTransferir monto;
  final String tokenDigital;
  final bool aceptarTerminos;
  final NombreBeneficiario nombreBeneficiario;
  final EntidadFinanciera? entidadFinanciera;
  final TipoDocumentoTransInter? tipoDocumento;
  final NumeroDocumento numeroDocumento;
  final bool esTitular;
  final String motivo;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final TokenResponse? tokenResponse;
  final EnlaceDocumento? documentoTermino;
  final MontosTotales? montosTotales;
  final Email correoElectronicoDestinatario;
  final DetalleTransferenciaResponse? detalleTransferenciaResponse;
  final DatosOperacionExitosaResponse? datosOperacionesExitosaResponse;

  PagoTarjetasCreditoInmediataState({
    this.cuentasOrigen = const [],
    this.tiposDocumento = const [],
    this.limitesTransferencias = const [],
    this.entidadesFinancieras = const [],
    this.cuentaOrigen,
    this.numeroTarjetaCredito = const NumeroTarjetaCredito.pure(''),
    this.monto = const MontoTransferir.pure(''),
    this.tokenDigital = '',
    this.aceptarTerminos = false,
    this.motivo = '',
    this.nombreBeneficiario = const NombreBeneficiario.pure(''),
    this.entidadFinanciera,
    this.tipoDocumento,
    this.numeroDocumento = const NumeroDocumento.pure(''),
    this.esTitular = false,
    this.operacionFrecuente = false,
    this.nombreOperacionFrecuente = '',
    this.tokenResponse,
    this.montosTotales,
    this.documentoTermino,
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.detalleTransferenciaResponse,
    this.datosOperacionesExitosaResponse,
  });

  PagoTarjetasCreditoInmediataState copyWith({
    List<CuentaEfectivo>? cuentasOrigen,
    List<TipoDocumentoTransInter>? tiposDocumento,
    List<LimitesTransferencias>? limitesTransferencias,
    List<EntidadFinanciera>? entidadesFinancieras,
    ValueGetter<CuentaEfectivo?>? cuentaOrigen,era,
    NumeroTarjetaCredito? numeroTarjetaCredito,
    MontoTransferir? monto,
    String? tokenDigital,
    bool? aceptarTerminos,
    String? motivo,
    NombreBeneficiario? nombreBeneficiario,
    ValueGetter<TipoDocumentoTransInter?>? tipoDocumento,
    ValueGetter<EntidadFinanciera?>? entidadFinanciera,
    ValueGetter<EnlaceDocumento?>? documentoTermino,     
    NumeroDocumento? numeroDocumento,
    bool? esTitular,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    ValueGetter<TokenResponse?>? tokenResponse,    
    ValueGetter<MontosTotales?>? montosTotales,
    Email? correoElectronicoDestinatario,
    ValueGetter<DetalleTransferenciaResponse?>? detalleTransferenciaResponse,    
    ValueGetter<DatosOperacionExitosaResponse?>? datosOperacionesExitosaResponse,
  }) =>
      PagoTarjetasCreditoInmediataState(
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        tiposDocumento: tiposDocumento ?? this.tiposDocumento,
        limitesTransferencias: limitesTransferencias ?? this.limitesTransferencias,
        entidadesFinancieras: entidadesFinancieras ?? this.entidadesFinancieras,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        numeroTarjetaCredito: numeroTarjetaCredito ?? this.numeroTarjetaCredito,
        monto: monto ?? this.monto,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        motivo: motivo ?? this.motivo,
        aceptarTerminos: aceptarTerminos ?? this.aceptarTerminos,
        nombreBeneficiario: nombreBeneficiario ?? this.nombreBeneficiario,
        tipoDocumento: tipoDocumento != null ? tipoDocumento() : this.tipoDocumento,
        entidadFinanciera: entidadFinanciera != null ? entidadFinanciera() : this.entidadFinanciera,
        documentoTermino: documentoTermino != null ? documentoTermino() : this.documentoTermino,
        numeroDocumento: numeroDocumento ?? this.numeroDocumento,
        esTitular: esTitular ?? this.esTitular,
        operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
        correoElectronicoDestinatario:
            correoElectronicoDestinatario ?? this.correoElectronicoDestinatario,
        nombreOperacionFrecuente:
            nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,    
        tokenResponse: tokenResponse != null
            ? tokenResponse()
            : this.tokenResponse,
        detalleTransferenciaResponse: detalleTransferenciaResponse != null
            ? detalleTransferenciaResponse()
            : this.detalleTransferenciaResponse,
        montosTotales: montosTotales != null
            ? montosTotales()
            : this.montosTotales,
        datosOperacionesExitosaResponse: datosOperacionesExitosaResponse != null
            ? datosOperacionesExitosaResponse()
            : this.datosOperacionesExitosaResponse,
      );
}

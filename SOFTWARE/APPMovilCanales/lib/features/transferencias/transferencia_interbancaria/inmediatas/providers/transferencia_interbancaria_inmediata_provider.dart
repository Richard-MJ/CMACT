import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/services/transferencia_interbancaria_inmediata_service.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/datos_operacion_exitosa_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/detalle_transferencia_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/montos_totales_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/token_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/tipo_documento.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_cci.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/monto_transferir.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
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
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/providers/transferencia_interbancaria_provider.dart';
import 'package:formz/formz.dart';

final transferenciaInterbancariaInmediataProvider = StateNotifierProvider<
    TransferenciaInterbancariaInmediataNotifier, TransferenciaInterbancariaInmediataState>((ref) {
  return TransferenciaInterbancariaInmediataNotifier(ref);
});

class TransferenciaInterbancariaInmediataNotifier
    extends StateNotifier<TransferenciaInterbancariaInmediataState> {
  TransferenciaInterbancariaInmediataNotifier(this.ref)
      : super(TransferenciaInterbancariaInmediataState());

  final api = Api();
  final Ref ref;

  String codigoAppMovil = "91";
  String codigoEntidadCMACT = "813";
  String codigoTipoTransferenciaOrdinaria = "320";
  String codigoDocumentoTerminos = "COD_01";
  int tipoOperacionTransferenciaInmediata = 10;
  bool isLoadingCuentaDestino = false;

  initDatos() {
    state = state.copyWith(
      cuentasOrigen: [],
      cuentaOrigen: () => null,
      cuentaDestino: const NumeroCuentaCci.pure(''),
      aceptarTerminos: false,
      monto: const MontoTransferir.pure(''),
      correoElectronicoDestinatario: const Email.pure(''),
      nombreEntidadBeneficiaria: '',
      tokenDigital: '',
      motivo: '',
      operacionFrecuente: false,
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      tipoDocumento: () => null,
      numeroDocumento: const NumeroDocumento.pure(''),
      esTitular: false,
      nombreOperacionFrecuente: '',
      datosOperacionesExitosaResponse: () => null,
      tiposDocumento: [],
      limitesTransferencias: [],
      entidadesFinancieras: [],
      tokenResponse: () => null,
      documentoTermino:() => null,
      detalleTransferenciaResponse: () => null,
      montosTotales: () => null
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final documentoTermino = ref.read(homeProvider).configuracion?.enlacesDocumentos
        .firstWhere((x) => x.codigoDocumento == codigoDocumentoTerminos);
      documentoTermino!.dataDocumento = await SharedService.convertirPdfAsBytes(documentoTermino.urlDocumento);

      final DatosInicialesResponse datosInicialesResponse =
          await TransferenciaInterbancariaInmediataService.obtenerDatosIniciales();

      state = state.copyWith(
        cuentasOrigen: datosInicialesResponse.productosDebito,
        tiposDocumento: datosInicialesResponse.tiposDocumentos,
        entidadesFinancieras: datosInicialesResponse.entidadesFinancieras,
        limitesTransferencias: datosInicialesResponse.limitesTransferencias,
        tipoDocumento: () => datosInicialesResponse.tiposDocumentos[0],
        documentoTermino: () => documentoTermino
      );

      await autoCompletarOpFrecuente();
      
      ref.read(loaderProvider.notifier).dismissLoader();
    } on ServiceException catch (e) {
      ref.read(tipoTransferenciaProvider.notifier).state = TipoTransferencia.diferida;
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  changeCuentaDestino(NumeroCuentaCci numeroCuentaCci) async {
    state = state.copyWith(
      cuentaDestino: numeroCuentaCci,
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      numeroDocumento: const NumeroDocumento.pure(''),
    );

    if (!Formz.validate([
      numeroCuentaCci,
    ])) {
      return;
    }

    await Future.delayed(Duration(milliseconds: 50));

    if (isLoadingCuentaDestino) return;
    isLoadingCuentaDestino = true;

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      if (numeroCuentaCci.value.startsWith(codigoEntidadCMACT)) {
        throw ServiceException("Estimado cliente no se puede realizar transferencias Interbancarias entre cuentas de la Caja");
      }

      final detalleTransferenciaResponse = await TransferenciaInterbancariaInmediataService
          .obtenerDatosCuentaReceptor(
            codigoTipoTransferencia: codigoTipoTransferenciaOrdinaria,
            numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
            codigoCuentaInterbancariaReceptor: state.cuentaDestino.value,
            codigoCanalCCE: codigoAppMovil);

      if (detalleTransferenciaResponse.tipoDocumentoReceptor != "0") {
        state = state.copyWith(
          tipoDocumento: () => state.tiposDocumento.firstWhere(
            (d) => d.codigoTipoDocumentoCamaraCompensacion ==
                int.tryParse(detalleTransferenciaResponse.tipoDocumentoReceptor),
          ),
        );
      }

      var nombreEntidadBeneficiaria = state.entidadesFinancieras.firstWhere(
        (d) => d?.codigoEntidad == detalleTransferenciaResponse.codigoEntidadReceptora,
      );

      if (nombreEntidadBeneficiaria == null) {
        throw ServiceException("Estimado cliente, no es posible realizar transferencias a la entidad seleccionada en este momento");
      }

      state = state.copyWith(
        detalleTransferenciaResponse: () => detalleTransferenciaResponse,
        numeroDocumento: NumeroDocumento.pure(detalleTransferenciaResponse.numeroIdentidadReceptor),
        nombreBeneficiario: NombreBeneficiario.pure(detalleTransferenciaResponse.nombreReceptor),
        nombreEntidadBeneficiaria: nombreEntidadBeneficiaria.nombreEntidadCce,
        esTitular: detalleTransferenciaResponse.mismoTitular == "M",
      );
    } on ServiceException catch (e) {
      ref.read(snackbarProvider.notifier).showSnackbar(e.message, SnackbarType.error);
    }

    isLoadingCuentaDestino = false;
    ref.read(loaderProvider.notifier).dismissLoader();
  }


  transferir({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.cuentaOrigen == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }

    final monto = MontoTransferir.dirty(state.monto.value);
    final cuentaDestino = NumeroCuentaCci.dirty(state.cuentaDestino.value);
    final nombreBeneficiario = NombreBeneficiario.dirty(state.nombreBeneficiario.value);

    state = state.copyWith(
      monto: monto,
      cuentaDestino: cuentaDestino,
      nombreBeneficiario: nombreBeneficiario,
    );

    if (!Formz.validate([
      monto,
      cuentaDestino,
      nombreBeneficiario,
    ])) {
      return;
    }

    resetToken();
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final MontosTotales montosTotales = await TransferenciaInterbancariaInmediataService.calcularMontosTotales(
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

      final tokenResponse = await TransferenciaInterbancariaInmediataService.obtenerTokenDigital(
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
        ref.read(appRouterProvider).push('/transferencias/interbancaria/confirmar-inmediata');
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
      await TransferenciaInterbancariaInmediataService
        .validarToken(numeroVerificacion: state.tokenDigital);

      final datosOperacionResponse = await TransferenciaInterbancariaInmediataService
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
      ref.read(appRouterProvider).push('/transferencias/interbancaria/transferencia-inmediata-exitosa');
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

      if (operacionFrecuente == null || operacionFrecuente.numeroTipoOperacionFrecuente != tipoOperacionTransferenciaInmediata) 
      {
        ref.read(loaderProvider.notifier).dismissLoader();
        return;
      }

      final indexCuentaOrigen = state.cuentasOrigen.indexWhere(
          (cuenta) => cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

      if (indexCuentaOrigen < 0)
      {
        ref.read(loaderProvider.notifier).dismissLoader();
        return;
      }   
      
      state = state.copyWith(
        cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
      );

      var codigoCuentaInterbancarioDestino = NumeroCuentaCci
        .pure(operacionFrecuente.operacionesFrecuenteDetalle.cuentaDestinoCci ?? '');

      await changeCuentaDestino(codigoCuentaInterbancarioDestino);

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
        idTipoDocumentoCompensacion: state.tipoDocumento?.codigoTipoDocumentoCamaraCompensacion,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroCuentaReceptor: state.cuentaDestino.value,
        numeroDocumento: state.numeroDocumento.value,
        tipoOperacionFrecuente: tipoOperacionTransferenciaInmediata
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
      await TransferenciaInterbancariaInmediataService.enviarCorreoElectronicoTransferencia(
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
    var limiteTransferencia = state.limitesTransferencias.firstWhere(
        (d) => d.codigoTipoTransferencia == codigoTipoTransferenciaOrdinaria &&
        d.codigoMoneda == producto.codigoMoneda
    );
    
    state = state.copyWith(
      cuentaOrigen: () => producto,
      monto: MontoTransferir.pure('',
        montoMaximo: limiteTransferencia.montoMaximo,
        montoMinimo: limiteTransferencia.montoMinimo),
      cuentaDestino: const NumeroCuentaCci.pure(''),
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

class TransferenciaInterbancariaInmediataState {
  final List<CuentaEfectivo> cuentasOrigen;
  final List<TipoDocumentoTransInter> tiposDocumento;
  final List<LimitesTransferencias> limitesTransferencias;  
  final List<EntidadesFinancieras?> entidadesFinancieras;
  final CuentaEfectivo? cuentaOrigen;
  final NumeroCuentaCci cuentaDestino;
  final MontoTransferir monto;
  final String tokenDigital;
  final String nombreEntidadBeneficiaria;
  final bool aceptarTerminos;
  final String motivo;
  final NombreBeneficiario nombreBeneficiario;
  final TipoDocumentoTransInter? tipoDocumento;
  final NumeroDocumento numeroDocumento;
  final bool esTitular;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final TokenResponse? tokenResponse;
  final MontosTotales? montosTotales; 
  final Email correoElectronicoDestinatario;
  final EnlaceDocumento? documentoTermino;
  final DetalleTransferenciaResponse? detalleTransferenciaResponse;
  final DatosOperacionExitosaResponse? datosOperacionesExitosaResponse;

  TransferenciaInterbancariaInmediataState({
    this.cuentasOrigen = const [],
    this.tiposDocumento = const [],
    this.limitesTransferencias = const [],
    this.entidadesFinancieras = const [],
    this.cuentaOrigen,
    this.cuentaDestino = const NumeroCuentaCci.pure(''),
    this.monto = const MontoTransferir.pure(''),
    this.tokenDigital = '',
    this.nombreEntidadBeneficiaria = '',
    this.aceptarTerminos = false,
    this.motivo = '',
    this.nombreBeneficiario = const NombreBeneficiario.pure(''),
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

  TransferenciaInterbancariaInmediataState copyWith({
    List<CuentaEfectivo>? cuentasOrigen,
    List<TipoDocumentoTransInter>? tiposDocumento,
    List<LimitesTransferencias>? limitesTransferencias,
    List<EntidadesFinancieras>? entidadesFinancieras,
    ValueGetter<CuentaEfectivo?>? cuentaOrigen,
    NumeroCuentaCci? cuentaDestino,
    MontoTransferir? monto,
    String? tokenDigital,
    String? nombreEntidadBeneficiaria,
    bool? aceptarTerminos,
    String? motivo,
    NombreBeneficiario? nombreBeneficiario,
    ValueGetter<TipoDocumentoTransInter?>? tipoDocumento,
    ValueGetter<EnlaceDocumento?>? documentoTermino,      
    NumeroDocumento? numeroDocumento,
    bool? esTitular,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    Email? correoElectronicoDestinatario,
    ValueGetter<TokenResponse?>? tokenResponse,   
    ValueGetter<MontosTotales?>? montosTotales,    
    ValueGetter<DetalleTransferenciaResponse?>? detalleTransferenciaResponse,    
    ValueGetter<DatosOperacionExitosaResponse?>? datosOperacionesExitosaResponse,
  }) =>
      TransferenciaInterbancariaInmediataState(
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        tiposDocumento: tiposDocumento ?? this.tiposDocumento,
        limitesTransferencias: limitesTransferencias ?? this.limitesTransferencias,
        entidadesFinancieras: entidadesFinancieras ?? this.entidadesFinancieras,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        cuentaDestino: cuentaDestino ?? this.cuentaDestino,
        monto: monto ?? this.monto,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        documentoTermino: documentoTermino != null ? documentoTermino() : this.documentoTermino,
        nombreEntidadBeneficiaria: nombreEntidadBeneficiaria ?? this.nombreEntidadBeneficiaria,
        aceptarTerminos: aceptarTerminos ?? this.aceptarTerminos,
        motivo: motivo ?? this.motivo,
        nombreBeneficiario: nombreBeneficiario ?? this.nombreBeneficiario,
        tipoDocumento:
            tipoDocumento != null ? tipoDocumento() : this.tipoDocumento,
        numeroDocumento: numeroDocumento ?? this.numeroDocumento,
        esTitular: esTitular ?? this.esTitular,
        operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
        nombreOperacionFrecuente:
            nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,    
        tokenResponse: tokenResponse != null
            ? tokenResponse()
            : this.tokenResponse,
        correoElectronicoDestinatario:
            correoElectronicoDestinatario ?? this.correoElectronicoDestinatario,
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

import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/services/pago_tarjetas_credito_diferida_service.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/entidad_financiera.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/producto_debito.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/pagar_response.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/tipo_documento.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inputs/numero_tarjeta_credito.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_documento.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/features/home/models/configuracion.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/inputs/monto.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:caja_tacna_app/api/api.dart';
import 'package:flutter/material.dart';
import 'package:formz/formz.dart';


final pagoTarjetasCreditoDiferidaProvider = StateNotifierProvider<
    PagoTarjetasCreditoDiferidaNotifier, PagoTarjetasCreditoDiferidaState>((ref) {
  return PagoTarjetasCreditoDiferidaNotifier(ref);
});

class PagoTarjetasCreditoDiferidaNotifier
    extends StateNotifier<PagoTarjetasCreditoDiferidaState> {
  PagoTarjetasCreditoDiferidaNotifier(this.ref) : super(PagoTarjetasCreditoDiferidaState());

  final api = Api();
  final Ref ref;

  String codigoDocumentoTerminos = "COD_01";

  initDatos() {
    state = state.copyWith(
      cuentasOrigen: [],
      cuentaOrigen: () => null,
      entidadesFinancieras: [],
      numeroTarjetaCredito: const NumeroTarjetaCredito.pure(''),
      aceptarTerminos: false,
      monto: const Monto.pure(''),
      tokenDigital: '',
      motivo: '',
      operacionFrecuente: false,
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      tipoDocumento: null,
      numeroDocumento: const NumeroDocumento.pure(''),
      esTitular: false,
      correoElectronicoDestinatario: const Email.pure(''),
      nombreOperacionFrecuente: '',
      entidadFinanciera: () => null,
      tiposDocumento: [],
      confirmarResponse: () => null,
      documentoTermino:() => null,
      pagarResponse: () => null,
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final documentoTermino = ref.read(homeProvider).configuracion?.enlacesDocumentos
        .firstWhere((x) => x.codigoDocumento == codigoDocumentoTerminos);

      final DatosInicialesResponse datosInicialesResponse =
          await PagoTarjetasCreditoDiferidaService.obtenerDatosIniciales();

      state = state.copyWith(
        cuentasOrigen: datosInicialesResponse.productosDebito,
        entidadesFinancieras: datosInicialesResponse.entidadesFinancieras,
        tiposDocumento: datosInicialesResponse.tiposDocumento,
        tipoDocumento: () => datosInicialesResponse.tiposDocumento[0],
        documentoTermino: () => documentoTermino
        
      );
      autoCompletarOpFrecuente();
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  autoCompletarOpFrecuente() {
    try {
      final operacionFrecuente =
          ref.read(operacionesFrecuentesProvider).operacionSeleccionada;

      if (operacionFrecuente == null)      
      {
        ref.read(loaderProvider.notifier).dismissLoader();
        return;
      }

      if(operacionFrecuente.numeroTipoOperacionFrecuente != 7) return;

      final indexCuentaOrigen = state.cuentasOrigen.indexWhere(
          (cuenta) => cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

      if (indexCuentaOrigen >= 0) {
        state = state.copyWith(
          cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
        );
      }

      state = state.copyWith(
        numeroTarjetaCredito: NumeroTarjetaCredito.pure(
            operacionFrecuente.operacionesFrecuenteDetalle.cuentaDestinoCci ??
                ''),
      );

      if (operacionFrecuente
              .operacionesFrecuenteDetalle.mismoTitularEnDestino ==
          'True') {
        toggleEsTitular(value: true);
      } else {
        toggleEsTitular(value: false);

        final int indexTipoDocumento = state.tiposDocumento.indexWhere(
            (element) =>
                element.codigoTipoDocumentoCamaraCompensacion.toString() ==
                operacionFrecuente.operacionesFrecuenteDetalle.tipoDocumento);

        TipoDocumentoPagoTarjeta newTipoDocumento = state.tiposDocumento[0];

        if (indexTipoDocumento >= 0) {
          newTipoDocumento = state.tiposDocumento[indexTipoDocumento];
        }

        state = state.copyWith(
          nombreBeneficiario: NombreBeneficiario.pure(
              operacionFrecuente.operacionesFrecuenteDetalle.nombreDestino ??
                  ''),
          numeroDocumento: NumeroDocumento.pure(
              operacionFrecuente.operacionesFrecuenteDetalle.numeroDocumento ??
                  ''),
          tipoDocumento: () => newTipoDocumento,
        );
      }

      ref.read(operacionesFrecuentesProvider.notifier).resetOperacion();
    } catch (e) {
      throw ServiceException('Ocurrió un error al cargar la operación');
    }
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
      numeroDocumento,
      nombreBeneficiario,
    ])) {
      return;
    }

    ref.read(appRouterProvider).push('/pago-tarjetas-credito/ingreso-monto-pagar-diferida');
  }

  pagar({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.cuentaOrigen == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }

    final monto = Monto.dirty(state.monto.value);
    final numeroTarjetaCredito =
        NumeroTarjetaCredito.dirty(state.numeroTarjetaCredito.value);

    state = state.copyWith(
      monto: monto,
      numeroTarjetaCredito: numeroTarjetaCredito,
    );

    if (!Formz.validate([monto, numeroTarjetaCredito])) return;
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    resetToken();

    try {
      final PagarResponse pagarResponse =
          await PagoTarjetasCreditoDiferidaService.pagar(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        numeroTarjetaCredito: state.numeroTarjetaCredito.value,
        monto: state.monto.value,
        codigoMoneda: state.cuentaOrigen?.codigoMonedaProducto,
        esTitular: state.esTitular,
        idTipoDocumentoCompensacion:
            state.tipoDocumento?.codigoTipoDocumentoCamaraCompensacion,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroDocumento: state.numeroDocumento.value,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
        idEntidad: state.entidadFinanciera?.idEntidadCce,
      );

      state = state.copyWith(
        pagarResponse: () => pagarResponse,
        tokenDigital: await CoreService.desencriptarToken(
          pagarResponse.datosAutorizacion.codigoSolicitado,
        ),
      );
      initTimer();

      if (withPush) {
        ref.read(appRouterProvider).push('/pago-tarjetas-credito/confirmar-diferida');
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
      final ConfirmarResponse confirmarResponse =
          await PagoTarjetasCreditoDiferidaService.confirmar(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        numeroTarjetaCredito: state.numeroTarjetaCredito.value,
        monto: state.monto.value,
        codigoMoneda: state.cuentaOrigen?.codigoMonedaProducto,
        tokenDigital: state.tokenDigital,
        codigoComision: state.pagarResponse?.codigoTarifarioComision,
        esPersonaNatural: state.pagarResponse?.esPersonaNatural,
        esTitular: state.esTitular,
        idTipoDocumentoCompensacion:
            state.tipoDocumento?.codigoTipoDocumentoCamaraCompensacion,
        montoComision: state.pagarResponse?.montoComision,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroDocumento: state.numeroDocumento.value,
        idEntidadFinancieraCce: state.pagarResponse?.idEntidadFinancieraCce,
      );

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();

      agregarOperacionFrecuente();

      ref.read(appRouterProvider).push('/pago-tarjetas-credito/pago-diferida-exitoso');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
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
        tipoOperacion: "4",
        correoElectronicoDestinatario:
            state.correoElectronicoDestinatario.value,
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

  changeCuentaOrigen(ProductoDebito producto) {
    state = state.copyWith(
      cuentaOrigen: () => producto,
    );
  }

  changeMonto(Monto monto) {
    state = state.copyWith(
      monto: monto,
    );
  }

  changeNumeroTarjetaCredito(NumeroTarjetaCredito numeroTarjetaCredito) {
    state = state.copyWith(
      numeroTarjetaCredito: numeroTarjetaCredito,
    );
  }

  changeNombreBeneficiario(NombreBeneficiario nombreBeneficiario) {
    state = state.copyWith(
      nombreBeneficiario: nombreBeneficiario,
    );
  }

  changeCorreoDestinatario(Email correo) {
    state = state.copyWith(
      correoElectronicoDestinatario: correo,
    );
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.pagarResponse?.datosAutorizacion.fechaSistema,
          date: state.pagarResponse?.datosAutorizacion.fechaVencimiento,
        );
  }

  changeEntidadFinanciera(EntidadFinanciera entidadFinanciera) {
    state = state.copyWith(
      entidadFinanciera: () => entidadFinanciera,
    );
  }

  toggleEsTitular({bool? value}) {
    final datosCliente = ref.read(homeProvider).datosCliente;
    final newValue = value ?? !state.esTitular;

    final int indexTipoDocumento = state.tiposDocumento.indexWhere((element) =>
        element.idTipoDocumento ==
        ref.read(loginProvider).documento?.idTipoDocumento);

    TipoDocumentoPagoTarjeta newTipoDocumento = state.tiposDocumento[0];

    if (indexTipoDocumento >= 0 && newValue) {
      newTipoDocumento = state.tiposDocumento[indexTipoDocumento];
    }

    state = state.copyWith(
      esTitular: newValue,
      nombreBeneficiario: newValue
          ? NombreBeneficiario.pure('${datosCliente?.nombreCompleto}')
          : const NombreBeneficiario.pure(''),
      numeroDocumento: newValue
          ? NumeroDocumento.pure(datosCliente?.dni ?? '')
          : const NumeroDocumento.pure(''),
      tipoDocumento: () => newTipoDocumento,
    );
  }

  changeTipoDocumento(TipoDocumentoPagoTarjeta tipoDocumento) {
    state = state.copyWith(
      tipoDocumento: () => tipoDocumento,
    );
  }

  changeNumeroDocumento(NumeroDocumento numeroDocumento) {
    state = state.copyWith(
      numeroDocumento: numeroDocumento,
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  toggleAceptarTerminos() {
    state = state.copyWith(
      aceptarTerminos: !state.aceptarTerminos,
    );
  }

  agregarOperacionFrecuente() async {
    try {
      if (!state.operacionFrecuente) return;
      await OperacionesFrecuentesService.agregarPagoTarjetaCredito(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        nombreOperacionFrecuente: state.nombreOperacionFrecuente,
        esTitular: state.esTitular,
        idTipoDocumentoCompensacion:
            state.tipoDocumento?.codigoTipoDocumentoCamaraCompensacion,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroCuentaCci: state.numeroTarjetaCredito.value,
        numeroDocumento: state.numeroDocumento.value,
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

  changeMotivo(String motivo) {
    state = state.copyWith(
      motivo: motivo,
    );
  }
}

class PagoTarjetasCreditoDiferidaState {
  final List<ProductoDebito> cuentasOrigen;
  final List<TipoDocumentoPagoTarjeta> tiposDocumento;
  final List<EntidadFinanciera> entidadesFinancieras;
  final EntidadFinanciera? entidadFinanciera;
  final ProductoDebito? cuentaOrigen;
  final NumeroTarjetaCredito numeroTarjetaCredito;
  final Monto monto;
  final String motivo;
  final String tokenDigital;
  final EnlaceDocumento? documentoTermino;
  final bool aceptarTerminos;
  final bool operacionFrecuente;
  final NombreBeneficiario nombreBeneficiario;
  final TipoDocumentoPagoTarjeta? tipoDocumento;
  final NumeroDocumento numeroDocumento;
  final bool esTitular;
  final String nombreOperacionFrecuente;
  final Email correoElectronicoDestinatario;

  final PagarResponse? pagarResponse;
  final ConfirmarResponse? confirmarResponse;

  PagoTarjetasCreditoDiferidaState({
    this.cuentasOrigen = const [],
    this.tiposDocumento = const [],
    this.entidadesFinancieras = const [],
    this.cuentaOrigen,
    this.entidadFinanciera,
    this.numeroTarjetaCredito = const NumeroTarjetaCredito.pure(''),
    this.monto = const Monto.pure(''),
    this.tokenDigital = '',
    this.documentoTermino,
    this.motivo = '',
    this.aceptarTerminos = false,
    this.operacionFrecuente = false,
    this.nombreBeneficiario = const NombreBeneficiario.pure(''),
    this.tipoDocumento,
    this.numeroDocumento = const NumeroDocumento.pure(''),
    this.esTitular = false,
    this.nombreOperacionFrecuente = '',
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.pagarResponse,
    this.confirmarResponse,
  });

  PagoTarjetasCreditoDiferidaState copyWith({
    List<ProductoDebito>? cuentasOrigen,
    List<TipoDocumentoPagoTarjeta>? tiposDocumento,
    List<EntidadFinanciera>? entidadesFinancieras,
    ValueGetter<ProductoDebito?>? cuentaOrigen,
    ValueGetter<EntidadFinanciera?>? entidadFinanciera,
    NumeroTarjetaCredito? numeroTarjetaCredito,
    Monto? monto,
    String? tokenDigital,
    ValueGetter<EnlaceDocumento?>? documentoTermino,
    String? motivo,
    bool? aceptarTerminos,
    bool? operacionFrecuente,
    NombreBeneficiario? nombreBeneficiario,
    ValueGetter<TipoDocumentoPagoTarjeta?>? tipoDocumento,
    NumeroDocumento? numeroDocumento,
    bool? esTitular,
    String? nombreOperacionFrecuente,
    Email? correoElectronicoDestinatario,
    ValueGetter<PagarResponse?>? pagarResponse,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
  }) =>
      PagoTarjetasCreditoDiferidaState(
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        tiposDocumento: tiposDocumento ?? this.tiposDocumento,
        entidadesFinancieras: entidadesFinancieras ?? this.entidadesFinancieras,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        entidadFinanciera: entidadFinanciera != null
            ? entidadFinanciera()
            : this.entidadFinanciera,
        numeroTarjetaCredito: numeroTarjetaCredito ?? this.numeroTarjetaCredito,
        monto: monto ?? this.monto,
        motivo: motivo ?? this.motivo,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        documentoTermino: documentoTermino != null ? documentoTermino() : this.documentoTermino,
        aceptarTerminos: aceptarTerminos ?? this.aceptarTerminos,
        operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
        nombreBeneficiario: nombreBeneficiario ?? this.nombreBeneficiario,
        tipoDocumento:
            tipoDocumento != null ? tipoDocumento() : this.tipoDocumento,
        numeroDocumento: numeroDocumento ?? this.numeroDocumento,
        esTitular: esTitular ?? this.esTitular,
        nombreOperacionFrecuente:
            nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,
        correoElectronicoDestinatario:
            correoElectronicoDestinatario ?? this.correoElectronicoDestinatario,
        pagarResponse:
            pagarResponse != null ? pagarResponse() : this.pagarResponse,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
      );
}
import 'package:caja_tacna_app/features/home/models/configuracion.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/services/transferencia_interbancaria_diferida_service.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/transferir_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/tipo_documento.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/transferencias/models/cuenta_transferencia.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_cci.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_documento.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/monto.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:caja_tacna_app/api/api.dart';
import 'package:flutter/material.dart';
import 'package:formz/formz.dart';

final transferenciaInterbancariaDiferidaProvider = StateNotifierProvider<
    TransferenciaInterbancariaDiferidaNotifier, TransferenciaInterbancariaDiferidaState>((ref) {
  return TransferenciaInterbancariaDiferidaNotifier(ref);
});

class TransferenciaInterbancariaDiferidaNotifier
    extends StateNotifier<TransferenciaInterbancariaDiferidaState> {
  TransferenciaInterbancariaDiferidaNotifier(this.ref)
      : super(TransferenciaInterbancariaDiferidaState());

  final api = Api();
  final Ref ref;

  String codigoDocumentoTerminos = "COD_01";

  initDatos() {
    state = state.copyWith(
      cuentasOrigen: [],
      cuentaOrigen: () => null,
      cuentaDestino: const NumeroCuentaCci.pure(''),
      aceptarTerminos: false,
      monto: const MontoTrans.pure(''),
      tokenDigital: '',
      motivo: '',
      operacionFrecuente: false,
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      tipoDocumento: () => null,
      numeroDocumento: const NumeroDocumento.pure(''),
      esTitular: false,
      correoElectronicoDestinatario: const Email.pure(''),
      nombreOperacionFrecuente: '',
      documentoTermino:() => null,
      confirmarResponse: () => null,
      tiposDocumento: [],
      transferirResponse: () => null,
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {

      final documentoTermino = ref.read(homeProvider).configuracion?.enlacesDocumentos
        .firstWhere((x) => x.codigoDocumento == codigoDocumentoTerminos);

      final DatosInicialesResponse datosInicialesResponse =
          await TransferenciaInterbancariaDiferidaService.obtenerDatosIniciales();

      state = state.copyWith(
        cuentasOrigen: datosInicialesResponse.productosDebito,
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

      if(operacionFrecuente.numeroTipoOperacionFrecuente != 4) return;

      final indexCuentaOrigen = state.cuentasOrigen.indexWhere(
          (cuenta) => cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

      if (indexCuentaOrigen < 0) return;

      state = state.copyWith(
        cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
        cuentaDestino: NumeroCuentaCci.pure(operacionFrecuente.operacionesFrecuenteDetalle.cuentaDestinoCci ?? '')
      );

      if (operacionFrecuente.operacionesFrecuenteDetalle.mismoTitularEnDestino == 'True') {
        toggleEsTitular(value: true);
      } else {
        toggleEsTitular(value: false);

        final int indexTipoDocumento = state.tiposDocumento.indexWhere(
            (element) =>
                element.codigoTipoDocumentoCamaraCompensacion.toString() ==
                operacionFrecuente.operacionesFrecuenteDetalle.tipoDocumento);

        TipoDocumentoTransInter newTipoDocumento = state.tiposDocumento[0];

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
    if (state.numeroDocumento.value.isEmpty) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Ingrese el número de documento', SnackbarType.error);
      return;
    }

    final numeroDocumento = NumeroDocumento.dirty(state.numeroDocumento.value);
    final monto = MontoTrans.dirty(state.monto.value);
    final cuentaDestino = NumeroCuentaCci.dirty(state.cuentaDestino.value);
    final nombreBeneficiario =
        NombreBeneficiario.dirty(state.nombreBeneficiario.value);

    state = state.copyWith(
      monto: monto,
      cuentaDestino: cuentaDestino,
      numeroDocumento: numeroDocumento,
      nombreBeneficiario: nombreBeneficiario,
    );

    if (!Formz.validate([
      monto,
      cuentaDestino,
      numeroDocumento,
      nombreBeneficiario,
    ])) {
      return;
    }
    resetToken();
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final TransferirResponse transferirResponse =
          await TransferenciaInterbancariaDiferidaService.transferir(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        numeroCuentaDestino: state.cuentaDestino.value,
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
      );

      state = state.copyWith(
        transferirResponse: () => transferirResponse,
        tokenDigital: await CoreService.desencriptarToken(
          transferirResponse.datosAutorizacion.codigoSolicitado,
        ),
      );
      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/transferencias/interbancaria/confirmar-diferida');
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
          await TransferenciaInterbancariaDiferidaService.confirmar(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        numeroCuentaDestino: state.cuentaDestino.value,
        monto: state.monto.value,
        codigoMoneda: state.cuentaOrigen?.codigoMonedaProducto,
        tokenDigital: state.tokenDigital,
        codigoComision: state.transferirResponse?.codigoTarifarioComision,
        esPersonaNatural: state.transferirResponse?.esPersonaNatural,
        esTitular: state.esTitular,
        idTipoDocumentoCompensacion:
            state.tipoDocumento?.codigoTipoDocumentoCamaraCompensacion,
        montoComision: state.transferirResponse?.montoComision,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroDocumento: state.numeroDocumento.value,
        idEntidadFinancieraCce:
            state.transferirResponse?.idEntidadFinancieraCce,
      );

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();
      agregarOperacionFrecuente();
      ref.read(appRouterProvider).push('/transferencias/interbancaria/transferencia-diferida-exitosa');
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

  agregarOperacionFrecuente() async {
    try {
      if (!state.operacionFrecuente) return;
      await OperacionesFrecuentesService.agregarTransInterbancaria(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        nombreOperacionFrecuente: state.nombreOperacionFrecuente,
        esTitular: state.esTitular,
        idTipoDocumentoCompensacion:
            state.tipoDocumento?.codigoTipoDocumentoCamaraCompensacion,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroCuentaCci: state.cuentaDestino.value,
        numeroDocumento: state.numeroDocumento.value,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
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
        tipoOperacion: "2",
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

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.transferirResponse?.datosAutorizacion.fechaSistema,
          date: state.transferirResponse?.datosAutorizacion.fechaVencimiento,
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

  toggleEsTitular({bool? value}) {
    final datosCliente = ref.read(homeProvider).datosCliente;
    final newValue = value ?? !state.esTitular;

    final int indexTipoDocumento = state.tiposDocumento.indexWhere((element) =>
        element.idTipoDocumento ==
        ref.read(loginProvider).documento?.idTipoDocumento);

    TipoDocumentoTransInter newTipoDocumento = state.tiposDocumento[0];

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

  changeCuentaOrigen(CuentaTransferencia producto) {
    state = state.copyWith(
      cuentaOrigen: () => producto,
    );
  }

  changeCuentaDestino(NumeroCuentaCci numeroCuenta) {
    state = state.copyWith(
      cuentaDestino: numeroCuenta,
    );
  }

  changeNombreBeneficiario(NombreBeneficiario nombreBeneficiario) {
    state = state.copyWith(
      nombreBeneficiario: nombreBeneficiario,
    );
  }

  changeMonto(MontoTrans monto) {
    state = state.copyWith(
      monto: monto,
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

class TransferenciaInterbancariaDiferidaState {
  final List<CuentaTransferencia> cuentasOrigen;
  final List<TipoDocumentoTransInter> tiposDocumento;
  final CuentaTransferencia? cuentaOrigen;
  final NumeroCuentaCci cuentaDestino;
  final MontoTrans monto;
  final String tokenDigital;
  final bool aceptarTerminos;
  final String motivo;
  final NombreBeneficiario nombreBeneficiario;
  final TipoDocumentoTransInter? tipoDocumento;
  final NumeroDocumento numeroDocumento;
  final bool esTitular;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final EnlaceDocumento? documentoTermino;
  final Email correoElectronicoDestinatario;
  final TransferirResponse? transferirResponse;
  final ConfirmarResponse? confirmarResponse;

  TransferenciaInterbancariaDiferidaState({
    this.cuentasOrigen = const [],
    this.tiposDocumento = const [],
    this.cuentaOrigen,
    this.cuentaDestino = const NumeroCuentaCci.pure(''),
    this.monto = const MontoTrans.pure(''),
    this.tokenDigital = '',
    this.aceptarTerminos = false,
    this.motivo = '',
    this.nombreBeneficiario = const NombreBeneficiario.pure(''),
    this.tipoDocumento,
    this.numeroDocumento = const NumeroDocumento.pure(''),
    this.esTitular = false,
    this.operacionFrecuente = false,
    this.nombreOperacionFrecuente = '',
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.documentoTermino,
    this.transferirResponse,
    this.confirmarResponse,
  });

  TransferenciaInterbancariaDiferidaState copyWith({
    List<CuentaTransferencia>? cuentasOrigen,
    List<TipoDocumentoTransInter>? tiposDocumento,
    ValueGetter<CuentaTransferencia?>? cuentaOrigen,
    NumeroCuentaCci? cuentaDestino,
    MontoTrans? monto,
    String? tokenDigital,
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
    ValueGetter<TransferirResponse?>? transferirResponse,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
  }) =>
      TransferenciaInterbancariaDiferidaState(
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        tiposDocumento: tiposDocumento ?? this.tiposDocumento,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        cuentaDestino: cuentaDestino ?? this.cuentaDestino,
        monto: monto ?? this.monto,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        aceptarTerminos: aceptarTerminos ?? this.aceptarTerminos,
        motivo: motivo ?? this.motivo,
        nombreBeneficiario: nombreBeneficiario ?? this.nombreBeneficiario,
        tipoDocumento:
            tipoDocumento != null ? tipoDocumento() : this.tipoDocumento,
        numeroDocumento: numeroDocumento ?? this.numeroDocumento,
        esTitular: esTitular ?? this.esTitular,
        operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
        documentoTermino: documentoTermino != null ? documentoTermino() : this.documentoTermino,
        nombreOperacionFrecuente:
            nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,
        correoElectronicoDestinatario:
            correoElectronicoDestinatario ?? this.correoElectronicoDestinatario,
        transferirResponse: transferirResponse != null
            ? transferirResponse()
            : this.transferirResponse,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
      );
}

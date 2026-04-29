import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/emision_giros/inputs/monto_giro.dart';
import 'package:caja_tacna_app/features/emision_giros/models/agencia.dart';
import 'package:caja_tacna_app/features/emision_giros/models/comision.dart';
import 'package:caja_tacna_app/features/emision_giros/models/confirmar_giro_response.dart';
import 'package:caja_tacna_app/features/emision_giros/models/departamento.dart';
import 'package:caja_tacna_app/features/emision_giros/models/nacionalidad.dart';
import 'package:caja_tacna_app/features/emision_giros/models/pagar_response.dart';
import 'package:caja_tacna_app/features/emision_giros/models/tipo_documento.dart';
import 'package:caja_tacna_app/features/emision_giros/models/vinculos_motivos_response.dart';
import 'package:caja_tacna_app/features/emision_giros/services/emision_giros_service.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/shared/inputs/direccion.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/inputs/nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_documento.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/transferencias/models/cuenta_transferencia.dart';
import 'package:caja_tacna_app/features/transferencias/services/transferencia_entre_mis_cuentas_service.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

final emisionGirosProvider =
    StateNotifierProvider<EmisionGirosNotifier, EmisionGirosState>((ref) {
  return EmisionGirosNotifier(ref);
});

class EmisionGirosNotifier extends StateNotifier<EmisionGirosState> {
  EmisionGirosNotifier(this.ref) : super(EmisionGirosState());

  final Ref ref;

  initDatos() {
    state = state.copyWith(
      cuentasOrigen: [],
      agencias: [],
      departamentos: [],
      motivos: [],
      nacionalidades: [],
      tiposDocumentos: [],
      vinculos: [],
      agencia: () => null,
      departamento: () => null,
      motivo: () => null,
      nacionalidad: () => null,
      cuentaOrigen: () => null,
      correoElectronicoDestinatario: const Email.pure(''),
      tokenDigital: '',
      nombreBeneficiario: const NombreBeneficiario.pure(''),
      direccion: const Direccion.pure(''),
      montoGiro: const MontoGiro.pure(''),
      numeroDocumento: const NumeroDocumento.pure(''),
      tipoDocumento: () => null,
      vinculo: () => null,
      operacionFrecuente: false,
      nombreOperacionFrecuente: '',
      confirmarResponse: () => null,
      pagarResponse: () => null,
      comision: () => null,
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await Future.wait([
        getCuentasOrigen(),
        getDepartamentos(),
        getNacionalidades(),
        getTiposDocumentos(),
        getVinculosMotivos(),
      ]);
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

      final int indexCuentaOrigen = state.cuentasOrigen.indexWhere(
          (cuenta) => cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

      if (indexCuentaOrigen >= 0) {
        state = state.copyWith(
          cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
        );
      }

      final int indexTipoDocumento = state.tiposDocumentos.indexWhere(
          (element) =>
              element.codigoTipoDocumento.toString() ==
              operacionFrecuente.operacionesFrecuenteDetalle.tipoDocumento);

      TipoDocumentoGiro newTipoDocumento = state.tiposDocumentos[0];

      if (indexTipoDocumento >= 0) {
        newTipoDocumento = state.tiposDocumentos[indexTipoDocumento];
      }

      final int indexNacionalidad = state.nacionalidades.indexWhere(
          (nacionalidad) =>
              nacionalidad.codigoPais ==
              operacionFrecuente.operacionesFrecuenteDetalle.codigoPais);

      if (indexNacionalidad >= 0) {
        state = state.copyWith(
          nacionalidad: () => state.nacionalidades[indexNacionalidad],
        );
      }

      final int indexDepartamento = state.departamentos.indexWhere(
          (departamento) =>
              departamento.codigoDepartamento ==
              operacionFrecuente
                  .operacionesFrecuenteDetalle.codigoDepartamento);

      if (indexDepartamento >= 0) {
        state = state.copyWith(
          departamento: () => state.departamentos[indexDepartamento],
        );
      }

      await getAgencias();

      final int indexAgencia = state.agencias.indexWhere((agencia) =>
          agencia.codigoAgencia ==
          operacionFrecuente.operacionesFrecuenteDetalle.codigoAgencia);

      if (indexAgencia >= 0) {
        state = state.copyWith(
          agencia: () => state.agencias[indexAgencia],
        );
      }

      final int indexMotivo = state.motivos.indexWhere((motivo) =>
          motivo.idVinculoMotivo.toString() ==
          operacionFrecuente.operacionesFrecuenteDetalle.idMotivo);

      if (indexMotivo >= 0) {
        state = state.copyWith(
          motivo: () => state.motivos[indexMotivo],
        );
      }

      final int indexVinculo = state.vinculos.indexWhere((vinculo) =>
          vinculo.idVinculoMotivo.toString() ==
          operacionFrecuente.operacionesFrecuenteDetalle.idVinculo);

      if (indexVinculo >= 0) {
        state = state.copyWith(
          vinculo: () => state.vinculos[indexVinculo],
        );
      }

      state = state.copyWith(
        nombreBeneficiario: NombreBeneficiario.pure(
            operacionFrecuente.operacionesFrecuenteDetalle.nombreApellido ??
                ''),
        tipoDocumento: () => newTipoDocumento,
        numeroDocumento: NumeroDocumento.pure(
            operacionFrecuente.operacionesFrecuenteDetalle.numeroDocumento ??
                ''),
        direccion: Direccion.pure(
            operacionFrecuente.operacionesFrecuenteDetalle.direccion ?? ''),
      );

      ref.read(operacionesFrecuentesProvider.notifier).resetOperacion();
    } catch (e) {
      throw ServiceException('Ocurrió un error al cargar la operación');
    }
  }

  Future<void> getCuentasOrigen() async {
    try {
      final DatosInicialesResponse datosInicialesResponse =
          await TransferenciaEntreCuentasService.obtenerDatosIniciales();

      state = state.copyWith(
        cuentasOrigen: datosInicialesResponse.productosDebito,
      );
    } on ServiceException catch (e) {
      throw ServiceException(e.message);
    }
  }

  Future<void> getTiposDocumentos() async {
    try {
      final tiposDocumentos =
          await EmisionGirosService.obtenerTiposDocumentos();

      state = state.copyWith(
        tiposDocumentos: tiposDocumentos,
        tipoDocumento: () => tiposDocumentos[0],
      );
    } on ServiceException catch (e) {
      throw ServiceException(e.message);
    }
  }

  Future<void> getNacionalidades() async {
    try {
      final nacionalidades = await EmisionGirosService.obtenerNacionalidades();

      state = state.copyWith(
        nacionalidades: nacionalidades,
      );

      final int indexNacionalidad = state.nacionalidades
          .indexWhere((nacionalidad) => nacionalidad.codigoPais == '4028');

      if (indexNacionalidad >= 0) {
        state = state.copyWith(
          nacionalidad: () => state.nacionalidades[indexNacionalidad],
        );
      }
    } on ServiceException catch (e) {
      throw ServiceException(e.message);
    }
  }

  Future<void> getDepartamentos() async {
    try {
      final departamentos = await EmisionGirosService.obtenerDepartamentos();

      state = state.copyWith(
        departamentos: departamentos,
      );
    } on ServiceException catch (e) {
      throw ServiceException(e.message);
    }
  }

  Future<void> getVinculosMotivos() async {
    try {
      final response = await EmisionGirosService.obtenerVinculosMotivos();

      state = state.copyWith(
        vinculos: response.vinculos,
        motivos: response.motivos,
      );
    } on ServiceException catch (e) {
      throw ServiceException(e.message);
    }
  }

  Future<void> getAgencias() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final agencias = await EmisionGirosService.obtenerAgenciasPorDepartamento(
        codigoDepartamento: state.departamento?.codigoDepartamento,
      );

      state = state.copyWith(
        agencias: agencias,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  ingresarMontoSubmit() {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.cuentaOrigen == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }

    final montoGiro = MontoGiro.dirty(state.montoGiro.value);
    state = state.copyWith(
      montoGiro: montoGiro,
    );

    if (!Formz.validate([montoGiro])) return;

    ref.read(appRouterProvider).push('/emision-giros/datos-beneficiario');
  }

  datosBeneficiarioSubmit() {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.nacionalidad == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la nacionalidad', SnackbarType.error);
      return;
    }

    if (state.nacionalidad == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione el vinculo', SnackbarType.error);
      return;
    }

    final numeroDocumento = NumeroDocumento.dirty(state.numeroDocumento.value);
    final nombreBeneficiario =
        NombreBeneficiario.dirty(state.nombreBeneficiario.value);

    state = state.copyWith(
      numeroDocumento: numeroDocumento,
      nombreBeneficiario: nombreBeneficiario,
    );

    if (!Formz.validate([numeroDocumento, nombreBeneficiario])) return;

    ref.read(appRouterProvider).push('/emision-giros/direccion-beneficiario');
  }

  pagar({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.departamento == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione el departamento', SnackbarType.error);
      return;
    }

    if (state.agencia == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la agencia', SnackbarType.error);
      return;
    }

    if (state.motivo == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione el motivo', SnackbarType.error);
      return;
    }

    final direccion = Direccion.dirty(state.direccion.value);

    state = state.copyWith(
      direccion: direccion,
    );

    if (!Formz.validate([direccion])) return;
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {
      final comision = await EmisionGirosService.obtenerComision(
        codigoMoneda: state.cuentaOrigen?.codigoMonedaProducto,
        codigoAgencia: state.agencia?.codigoAgencia,
        montoOperacion: state.montoGiro.value,
        numeroCuenta: state.cuentaOrigen?.numeroProducto,
      );

      state = state.copyWith(
        comision: () => comision,
      );

      final pagarResponse = await EmisionGirosService.pagarGiro(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        codigoMoneda: state.cuentaOrigen?.codigoMonedaProducto,
        montoGiro: state.montoGiro.value,
        numeroDocumentoBeneficiario: state.numeroDocumento.value,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        pagarResponse: () => pagarResponse,
        tokenDigital: await CoreService.desencriptarToken(
          pagarResponse.codigoSolicitado,
        ),
      );
      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/emision-giros/confirmar');
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

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final confirmarResponse = await EmisionGirosService.confirmarGiro(
        tokenDigital: state.tokenDigital,
        codigoAgencia: state.agencia?.codigoAgencia,
        codigoMoneda: state.cuentaOrigen?.codigoMonedaProducto,
        direccionBeneficiario: state.direccion.value,
        idMotivo: state.motivo?.idVinculoMotivo,
        idNacionalidad: state.nacionalidad?.codigoPais,
        idVerificacion: state.pagarResponse?.idVerificacion,
        idVinculo: state.vinculo?.idVinculoMotivo,
        montoGiro: state.montoGiro.value,
        montoTotal: double.parse(state.montoGiro.value) +
            (state.comision?.montoComision ?? 0) +
            (state.comision?.montoItf ?? 0),
        nombreBeneficiario: state.nombreBeneficiario.value,
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        numeroDocumentoBeneficiario: state.numeroDocumento.value,
        tipoDocumentoBenef: state.tipoDocumento?.codigoTipoDocumento,
      );

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();
      agregarOperacionFrecuente();
      ref.read(appRouterProvider).push('/emision-giros/giro-exitoso');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  changeCuentaOrigen(CuentaTransferencia cuenta) {
    state = state.copyWith(
      cuentaOrigen: () => cuenta,
    );
  }

  changeTipoDocumento(TipoDocumentoGiro tipoDocumento) {
    state = state.copyWith(
      tipoDocumento: () => tipoDocumento,
      numeroDocumento: const NumeroDocumento.pure(''),
    );
  }

  changeNumeroDocumento(NumeroDocumento numeroDocumento) {
    state = state.copyWith(
      numeroDocumento: numeroDocumento,
    );
  }

  changeDireccion(Direccion direccion) {
    state = state.copyWith(
      direccion: direccion,
    );
  }

  changeNacionalidad(Nacionalidad nacionalidad) {
    state = state.copyWith(
      nacionalidad: () => nacionalidad,
    );
  }

  changeVinculo(Vinculo vinculo) {
    state = state.copyWith(
      vinculo: () => vinculo,
    );
  }

  changeDepartamento(Departamento departamento) {
    state = state.copyWith(
      departamento: () => departamento,
      agencia: () => null,
      agencias: [],
    );
    getAgencias();
  }

  changeAgencia(Agencia agencia) {
    state = state.copyWith(
      agencia: () => agencia,
    );
  }

  changeMotivo(Motivo motivo) {
    state = state.copyWith(
      motivo: () => motivo,
    );
  }

  changeMontoGiro(MontoGiro montoGiro) {
    state = state.copyWith(
      montoGiro: montoGiro,
    );
  }

  changeNombreBeneficiario(NombreBeneficiario nombreBeneficiario) {
    state = state.copyWith(
      nombreBeneficiario: nombreBeneficiario,
    );
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.pagarResponse?.fechaSistema,
          date: state.pagarResponse?.fechaVencimiento,
        );
  }

  agregarOperacionFrecuente() async {
    try {
      if (!state.operacionFrecuente) return;
      await OperacionesFrecuentesService.agregarGiro(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        nombreOperacionFrecuente: state.nombreOperacionFrecuente,
        nombreReceptor: state.nombreBeneficiario.value,
        numeroDocumento: state.numeroDocumento.value,
        codigoAgencia: state.agencia?.codigoAgencia,
        codigoDepartamento: state.departamento?.codigoDepartamento,
        codigoPais: state.nacionalidad?.codigoPais,
        direccion: state.direccion.value,
        idMotivo: state.motivo?.idVinculoMotivo,
        idVinculo: state.vinculo?.idVinculoMotivo,
        tipoDocumento: state.tipoDocumento?.idTipoDocumento,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeNombreOperacionFrecuente(String nombreOperacionFrecuente) {
    state = state.copyWith(
      nombreOperacionFrecuente: nombreOperacionFrecuente,
    );
  }

  toggleOperacionFrecuente() {
    state = state.copyWith(
      operacionFrecuente: !state.operacionFrecuente,
      nombreOperacionFrecuente: '',
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  changeCorreoDestinatario(Email correo) {
    state = state.copyWith(
      correoElectronicoDestinatario: correo,
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
        tipoOperacion: "22",
        correoElectronicoDestinatario:
            state.correoElectronicoDestinatario.value,
        idOperacionTts:
            int.parse(state.confirmarResponse?.numeroOperacion ?? '0'),
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

  toggleAceptarTerminos() {
    state = state.copyWith(
      aceptarTerminos: !state.aceptarTerminos,
    );
  }
}

class EmisionGirosState {
  final List<CuentaTransferencia> cuentasOrigen;
  final List<TipoDocumentoGiro> tiposDocumentos;
  final List<Nacionalidad> nacionalidades;
  final List<Vinculo> vinculos;
  final List<Departamento> departamentos;
  final List<Agencia> agencias;
  final List<Motivo> motivos;

  final CuentaTransferencia? cuentaOrigen;
  final TipoDocumentoGiro? tipoDocumento;
  final Nacionalidad? nacionalidad;
  final Vinculo? vinculo;
  final Departamento? departamento;
  final Agencia? agencia;
  final Motivo? motivo;

  final MontoGiro montoGiro;
  final NumeroDocumento numeroDocumento;
  final NombreBeneficiario nombreBeneficiario;
  final Direccion direccion;

  //data vista confirmar
  final String tokenDigital;
  final PagarResponse? pagarResponse;
  final Comision? comision;

  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;

  //data vista pago exitoso
  final Email correoElectronicoDestinatario;
  final ConfirmarGiroResponse? confirmarResponse;

  final bool aceptarTerminos;

  EmisionGirosState({
    this.cuentasOrigen = const [],
    this.tiposDocumentos = const [],
    this.nacionalidades = const [],
    this.vinculos = const [],
    this.departamentos = const [],
    this.agencias = const [],
    this.motivos = const [],
    this.cuentaOrigen,
    this.tipoDocumento,
    this.nacionalidad,
    this.vinculo,
    this.departamento,
    this.agencia,
    this.motivo,
    this.montoGiro = const MontoGiro.pure(''),
    this.direccion = const Direccion.pure(''),
    this.nombreBeneficiario = const NombreBeneficiario.pure(''),
    this.numeroDocumento = const NumeroDocumento.pure(''),
    this.tokenDigital = '',
    this.operacionFrecuente = false,
    this.nombreOperacionFrecuente = '',
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.pagarResponse,
    this.confirmarResponse,
    this.comision,
    this.aceptarTerminos = false,
  });

  EmisionGirosState copyWith({
    List<CuentaTransferencia>? cuentasOrigen,
    List<TipoDocumentoGiro>? tiposDocumentos,
    List<Nacionalidad>? nacionalidades,
    List<Vinculo>? vinculos,
    List<Departamento>? departamentos,
    List<Agencia>? agencias,
    List<Motivo>? motivos,
    ValueGetter<CuentaTransferencia?>? cuentaOrigen,
    ValueGetter<TipoDocumentoGiro?>? tipoDocumento,
    ValueGetter<Nacionalidad?>? nacionalidad,
    ValueGetter<Vinculo?>? vinculo,
    ValueGetter<Departamento?>? departamento,
    ValueGetter<Agencia?>? agencia,
    ValueGetter<Motivo?>? motivo,
    MontoGiro? montoGiro,
    Direccion? direccion,
    NombreBeneficiario? nombreBeneficiario,
    NumeroDocumento? numeroDocumento,
    String? tokenDigital,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    Email? correoElectronicoDestinatario,
    ValueGetter<PagarResponse?>? pagarResponse,
    ValueGetter<ConfirmarGiroResponse?>? confirmarResponse,
    ValueGetter<Comision?>? comision,
    bool? aceptarTerminos,
  }) =>
      EmisionGirosState(
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        tiposDocumentos: tiposDocumentos ?? this.tiposDocumentos,
        nacionalidades: nacionalidades ?? this.nacionalidades,
        vinculos: vinculos ?? this.vinculos,
        departamentos: departamentos ?? this.departamentos,
        agencias: agencias ?? this.agencias,
        motivos: motivos ?? this.motivos,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        tipoDocumento:
            tipoDocumento != null ? tipoDocumento() : this.tipoDocumento,
        nacionalidad: nacionalidad != null ? nacionalidad() : this.nacionalidad,
        vinculo: vinculo != null ? vinculo() : this.vinculo,
        departamento: departamento != null ? departamento() : this.departamento,
        agencia: agencia != null ? agencia() : this.agencia,
        motivo: motivo != null ? motivo() : this.motivo,
        montoGiro: montoGiro ?? this.montoGiro,
        nombreBeneficiario: nombreBeneficiario ?? this.nombreBeneficiario,
        numeroDocumento: numeroDocumento ?? this.numeroDocumento,
        direccion: direccion ?? this.direccion,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
        nombreOperacionFrecuente:
            nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,
        correoElectronicoDestinatario:
            correoElectronicoDestinatario ?? this.correoElectronicoDestinatario,
        pagarResponse:
            pagarResponse != null ? pagarResponse() : this.pagarResponse,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
        comision: comision != null ? comision() : this.comision,
        aceptarTerminos: aceptarTerminos ?? this.aceptarTerminos,
      );
}

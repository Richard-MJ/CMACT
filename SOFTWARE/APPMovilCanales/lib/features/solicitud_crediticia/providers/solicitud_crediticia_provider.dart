import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/emision_giros/models/agencia.dart';
import 'package:caja_tacna_app/features/emision_giros/models/departamento.dart';
import 'package:caja_tacna_app/features/emision_giros/services/emision_giros_service.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/inputs/cuotas.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/inputs/ingreso_mensual.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/inputs/monto_deseado.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/destino_credito.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/ingresar_solicitud_response.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/tipo_ingreso.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/tipo_moneda.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/services/solicitud_crediticia_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:formz/formz.dart';

final solicitudCrediticiaProvider = StateNotifierProvider<
    SolicitudCrediticiaNotifier, SolicitudCrediticiaState>((ref) {
  return SolicitudCrediticiaNotifier(ref);
});

class SolicitudCrediticiaNotifier
    extends StateNotifier<SolicitudCrediticiaState> {
  SolicitudCrediticiaNotifier(this.ref) : super(SolicitudCrediticiaState());

  final Ref ref;

  void initDatos() {
    state = state.copyWith(
        tiposIngreso: [],
        tiposMoneda: [],
        destinosCredito: [],
        departamentos: [],
        agencias: [],
        tipoIngreso: () => null,
        tipoMoneda: () => null,
        destinoCredito: () => null,
        departamento: () => null,
        agencia: () => null,
        aceptarPoliticaTratamientoDatos: false,
        aceptarPublicidad: false,
        ingresarSolicitudResponse: null,
        ingresoMensual: const IngresoMensual.pure(''),
        montoDeseado: const MontoDeseado.pure('',null),
        cuotas: const Cuotas.pure(''));
  }

  Future<void> getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final datosIniciales =
          await SolicitudCrediticiaService.obtenerDatosIniciales();
      state = state.copyWith(
          tiposIngreso: datosIniciales.tiposIngreso,
          tiposMoneda: datosIniciales.tiposMoneda,
          departamentos: datosIniciales.departamentos);
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    } finally {
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  void changeTipoIngreso(TipoIngreso tipoIngreso) {
    state = state.copyWith(
        tipoIngreso: () => tipoIngreso,
        destinosCredito: tipoIngreso.destinos,
        destinoCredito: () => tipoIngreso.destinos.firstOrNull);
  }

  void changeTipoMoneda(TipoMoneda tipoMoneda) {
    state = state.copyWith(
        tipoMoneda: () => tipoMoneda,
        montoDeseado: const MontoDeseado.dirty('',null));
  }

  void changeDestinoCredito(DestinoCredito destinoCredito) {
    state = state.copyWith(destinoCredito: () => destinoCredito);
  }

  void changeAgencia(Agencia agencia) {
    state = state.copyWith(agencia: () => agencia);
  }

  void changeIngresoMensual(IngresoMensual ingresoMensual) {
    state = state.copyWith(ingresoMensual: ingresoMensual);
  }

  void changeMontoDeseado(MontoDeseado montoDeseado) {
    state = state.copyWith(montoDeseado: montoDeseado);
  }

  void changeCuotas(Cuotas cuotas) {
    state = state.copyWith(cuotas: cuotas);
  }

  ingresoMensualSubmit() {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.tipoIngreso == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione el tipo de ingreso', SnackbarType.error);
      return;
    }

    final ingresoMensual = IngresoMensual.dirty(state.ingresoMensual.value);
    state = state.copyWith(
      ingresoMensual: ingresoMensual,
    );

    if (!Formz.validate([ingresoMensual])) return;

    ref.read(appRouterProvider).push('/solicitud-crediticia/datos-credito');
  }

  ingresoDatosCreditoSubmit() {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.tipoMoneda == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione el tipo de moneda', SnackbarType.error);
      return;
    }

    if (state.destinoCredito == null) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Seleccione el destino del crédito', SnackbarType.error);
      return;
    }

    final ingresoMensual = IngresoMensual.dirty(state.ingresoMensual.value);
    final cuotas = Cuotas.dirty(state.cuotas.value);

    state = state.copyWith(ingresoMensual: ingresoMensual, cuotas: cuotas);

    if (!Formz.validate([ingresoMensual, cuotas])) return;

    ref.read(appRouterProvider).push('/solicitud-crediticia/datos-atencion');
  }

  Future<void> ingresoDatosAtencionSubmit() async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.agencia == null) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Seleccione la agencia de atención', SnackbarType.error);
      return;
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final solicitudCrediticiaResponse =
          await SolicitudCrediticiaService.ingresarSolicitudCrediticia(
              codigoTipoIngreso: state.tipoIngreso?.codigoTipo,
              ingresoMensual: state.ingresoMensual.value,
              codigoTipoMoneda: state.tipoMoneda?.codigoMoneda,
              montoDeseado: state.montoDeseado.value,
              cuotas: state.cuotas.value,
              destinoCredito: state.destinoCredito?.descripcionDestino,
              codigoAgenciaAtencion: state.agencia?.codigoAgencia,
              indicadorPublicidad: state.aceptarPublicidad);

      state = state.copyWith(
          ingresarSolicitudResponse: solicitudCrediticiaResponse);

      ref.read(appRouterProvider).push('/solicitud-crediticia/solicitud-exitosa');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    } finally {
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  toggleAceptarPoliticaTratamientoDatos() {
    state = state.copyWith(
      aceptarPoliticaTratamientoDatos: !state.aceptarPoliticaTratamientoDatos,
    );
  }

  toggleAceptarPublicidad() {
    state = state.copyWith(
      aceptarPublicidad: !state.aceptarPublicidad,
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
}

class SolicitudCrediticiaState {
  final List<TipoIngreso> tiposIngreso;
  final List<TipoMoneda> tiposMoneda;
  final List<DestinoCredito> destinosCredito;
  final List<Departamento> departamentos;
  final List<Agencia> agencias;
  final TipoIngreso? tipoIngreso;
  final TipoMoneda? tipoMoneda;
  final DestinoCredito? destinoCredito;
  final Departamento? departamento;
  final Agencia? agencia;
  final bool aceptarPoliticaTratamientoDatos;
  final bool aceptarPublicidad;
  final IngresarSolicitudResponse? ingresarSolicitudResponse;

  final IngresoMensual ingresoMensual;
  final MontoDeseado montoDeseado;
  final Cuotas cuotas;

  SolicitudCrediticiaState({
    this.tiposIngreso = const [],
    this.tiposMoneda = const [],
    this.destinosCredito = const [],
    this.departamentos = const [],
    this.agencias = const [],
    this.tipoIngreso,
    this.tipoMoneda,
    this.destinoCredito,
    this.departamento,
    this.agencia,
    this.aceptarPoliticaTratamientoDatos = false,
    this.aceptarPublicidad = false,
    this.ingresarSolicitudResponse,
    this.ingresoMensual = const IngresoMensual.pure(''),
    this.montoDeseado = const MontoDeseado.pure('',null),
    this.cuotas = const Cuotas.pure(''),
  });

  SolicitudCrediticiaState copyWith({
    List<TipoIngreso>? tiposIngreso,
    List<TipoMoneda>? tiposMoneda,
    List<DestinoCredito>? destinosCredito,
    List<Departamento>? departamentos,
    List<Agencia>? agencias,
    ValueGetter<TipoIngreso?>? tipoIngreso,
    ValueGetter<TipoMoneda?>? tipoMoneda,
    ValueGetter<DestinoCredito?>? destinoCredito,
    ValueGetter<Departamento?>? departamento,
    ValueGetter<Agencia?>? agencia,
    bool? aceptarPoliticaTratamientoDatos,
    bool? aceptarPublicidad,
    IngresarSolicitudResponse? ingresarSolicitudResponse,
    IngresoMensual? ingresoMensual,
    MontoDeseado? montoDeseado,
    Cuotas? cuotas,
  }) {
    return SolicitudCrediticiaState(
        tiposIngreso: tiposIngreso ?? this.tiposIngreso,
        tiposMoneda: tiposMoneda ?? this.tiposMoneda,
        destinosCredito: destinosCredito ?? this.destinosCredito,
        departamentos: departamentos ?? this.departamentos,
        agencias: agencias ?? this.agencias,
        tipoIngreso: tipoIngreso != null ? tipoIngreso() : this.tipoIngreso,
        tipoMoneda: tipoMoneda != null ? tipoMoneda() : this.tipoMoneda,
        destinoCredito:
            destinoCredito != null ? destinoCredito() : this.destinoCredito,
        departamento: departamento != null ? departamento() : this.departamento,
        agencia: agencia != null ? agencia() : this.agencia,
        aceptarPoliticaTratamientoDatos: aceptarPoliticaTratamientoDatos ??
            this.aceptarPoliticaTratamientoDatos,
        aceptarPublicidad: aceptarPublicidad ?? this.aceptarPublicidad,
        ingresarSolicitudResponse:
            ingresarSolicitudResponse ?? this.ingresarSolicitudResponse,
        ingresoMensual: ingresoMensual ?? this.ingresoMensual,
        montoDeseado: montoDeseado ?? this.montoDeseado,
        cuotas: cuotas ?? this.cuotas);
  }
}

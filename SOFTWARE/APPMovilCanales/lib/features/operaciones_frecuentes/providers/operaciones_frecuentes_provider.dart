import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/inputs/alias.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/models/listar_operaciones_response.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/providers/pago_creditos_propios_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/providers/pago_creditos_terceros_provider.dart';
import 'package:caja_tacna_app/features/pago_servicios/providers/pago_servicios_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final operacionesFrecuentesProvider = StateNotifierProvider<
    OperacionesFrecuentesNotifier, OperacionesFrecuentesState>((ref) {
  return OperacionesFrecuentesNotifier(ref);
});

class OperacionesFrecuentesNotifier
    extends StateNotifier<OperacionesFrecuentesState> {
  OperacionesFrecuentesNotifier(this.ref) : super(OperacionesFrecuentesState());

  final Ref ref;

  initData() {
    state = state.copyWith(
      categorias: [],
      operacionesFrecuentes: [],
      search: '',
      categoria: () => null,
      detalleOperacion: () => null,
      operacionSeleccionada: () => null,
      nuevoAlias: const AliasOpFrecuente.pure(''),
    );
  }

  listarOperaciones() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final response = await OperacionesFrecuentesService.listarOperaciones();

      state = state.copyWith(
        categorias: response.categorias,
        operacionesFrecuentes: response.operacionesFrecuentes,
      );
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  changeSearch(String search) {
    state = state.copyWith(
      search: search,
    );
  }

  selectOperacion(OperacionFrecuente operacion) async {
    state = state.copyWith(
      operacionSeleccionada: () => operacion,
    );

    //recarga virtual
    if (operacion.numeroTipoOperacionFrecuente == 1) {
      ref.read(appRouterProvider).push('/recarga-virtual/recargar');
    }

    //transferencia propia
    if (operacion.numeroTipoOperacionFrecuente == 2) {
      ref.read(appRouterProvider).push('/transferencias/entre-mis-cuentas/transferir');
    }

    //transferencia a terceros
    if (operacion.numeroTipoOperacionFrecuente == 3) {
      ref.read(appRouterProvider).push('/transferencias/terceros/transferir');
    }

    //transferencia interbancaria
    if ([4, 10].contains(operacion.numeroTipoOperacionFrecuente)) {
      ref.read(appRouterProvider).push('/transferencias/interbancaria/transferir');
    }

    //pago creditos propios
    if (operacion.numeroTipoOperacionFrecuente == 5) {
      ref
          .read(pagoCreditoPropioProvider.notifier)
          .setTipoPago(TipoPago.aplicativo);

      ref.read(pagoCreditoPropioProvider.notifier).initDatosCuenta();
      await ref.read(pagoCreditoPropioProvider.notifier).getDatosIniciales();

      final indexCredito = ref
          .read(pagoCreditoPropioProvider)
          .creditos
          .indexWhere((c) =>
              c.numeroCredito.toString() ==
              state.operacionSeleccionada?.operacionesFrecuenteDetalle
                  .numeroCredito);

      if (indexCredito >= 0) {
        final credito =
            ref.read(pagoCreditoPropioProvider).creditos[indexCredito];
        await ref
            .read(pagoCreditoPropioProvider.notifier)
            .setCreditoAbonar(credito);
      } else {
        ref.read(snackbarProvider.notifier).showSnackbar(
            'No se encontró el crédito seleccionado.', SnackbarType.error);
      }
    }

    //pago creditos terceros
    if (operacion.numeroTipoOperacionFrecuente == 6) {
      ref.read(appRouterProvider).push('/pago-creditos/creditos-terceros/pagar');
    }

    //transferencia interbancaria
    if ([7, 11].contains(operacion.numeroTipoOperacionFrecuente)) {
      ref.read(appRouterProvider).push('/pago-tarjetas-credito/ingreso-datos-tarjeta');
    }

    //transferencia interbancaria
    if (operacion.numeroTipoOperacionFrecuente == 8) {
      ref.read(appRouterProvider).push('/emision-giros/ingreso-monto');
    }

    //pago servicios
    if (operacion.numeroTipoOperacionFrecuente == 9) {
      ref.read(pagoServiciosProvider.notifier).seleccionarOperacionFrecuente();
    }
  }

  detalleOperacion(OperacionFrecuente operacion) {
    state = state.copyWith(
      detalleOperacion: () => operacion,
    );
    ref.read(appRouterProvider).push('/operaciones-frecuentes/detalle-operacion');
  }

  eliminarOperacion() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await OperacionesFrecuentesService.eliminarOperacionFrecuente(
        numeroOperacionFrecuente:
            state.detalleOperacion?.numeroOperacionFrecuente,
      );
      ref.read(appRouterProvider).pop();
      initData();
      listarOperaciones();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  editarAliasOperacion() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await OperacionesFrecuentesService.editarAliasOperacionFrecuente(
        nombreOperacion: state.nuevoAlias.value,
        numeroOperacionFrecuente:
            state.detalleOperacion?.numeroOperacionFrecuente,
      );
      ref.read(appRouterProvider).pop();
      ref.read(appRouterProvider).pop();
      initData();
      listarOperaciones();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  resetOperacion() {
    state = state.copyWith(
      operacionSeleccionada: () => null,
    );
  }

  selectCategoria(CategoriaOperacionFrecuente? categoria) {
    state = state.copyWith(
      categoria: () => categoria,
    );
  }

  goEditarAlias() {
    state = state.copyWith(
      nuevoAlias: const AliasOpFrecuente.pure(''),
    );
    ref.read(appRouterProvider).push('/operaciones-frecuentes/editar-alias');
  }

  changeNuevoAlias(AliasOpFrecuente alias) {
    state = state.copyWith(
      nuevoAlias: alias,
    );
  }
}

class OperacionesFrecuentesState {
  final List<CategoriaOperacionFrecuente> categorias;
  final List<OperacionFrecuente> operacionesFrecuentes;
  final String search;
  final OperacionFrecuente? operacionSeleccionada;
  final OperacionFrecuente? detalleOperacion;
  final CategoriaOperacionFrecuente? categoria;
  final AliasOpFrecuente nuevoAlias;

  OperacionesFrecuentesState({
    this.categorias = const [],
    this.operacionesFrecuentes = const [],
    this.search = '',
    this.operacionSeleccionada,
    this.detalleOperacion,
    this.categoria,
    this.nuevoAlias = const AliasOpFrecuente.pure(''),
  });

  get btnAliasDisabled {
    return nuevoAlias.isNotValid;
  }

  OperacionesFrecuentesState copyWith({
    List<CategoriaOperacionFrecuente>? categorias,
    List<OperacionFrecuente>? operacionesFrecuentes,
    String? search,
    ValueGetter<OperacionFrecuente?>? operacionSeleccionada,
    ValueGetter<OperacionFrecuente?>? detalleOperacion,
    ValueGetter<CategoriaOperacionFrecuente?>? categoria,
    AliasOpFrecuente? nuevoAlias,
  }) =>
      OperacionesFrecuentesState(
        categorias: categorias ?? this.categorias,
        operacionesFrecuentes:
            operacionesFrecuentes ?? this.operacionesFrecuentes,
        search: search ?? this.search,
        operacionSeleccionada: operacionSeleccionada != null
            ? operacionSeleccionada()
            : this.operacionSeleccionada,
        detalleOperacion: detalleOperacion != null
            ? detalleOperacion()
            : this.detalleOperacion,
        categoria: categoria != null ? categoria() : this.categoria,
        nuevoAlias: nuevoAlias ?? this.nuevoAlias,
      );
}

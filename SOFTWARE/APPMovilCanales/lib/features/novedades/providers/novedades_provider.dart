import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/novedades/models/categoria_tipo_aviso.dart';
import 'package:caja_tacna_app/features/novedades/models/novedad.dart';
import 'package:caja_tacna_app/features/novedades/services/novedades_service.dart';
import 'package:caja_tacna_app/features/novedades/services/parametros_service.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/providers/sesion_canal_electronico_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final novedadesProvider =
    StateNotifierProvider<NovedadesNotifier, NovedadesState>((ref) {
  return NovedadesNotifier(ref);
});

class NovedadesNotifier extends StateNotifier<NovedadesState> {
  NovedadesNotifier(this.ref) : super(NovedadesState());

  final Ref ref;

  initData() {
    state = state.copyWith(
      tabs: [],
      novedades: [],
      tabSeleccionado: () => null,
    );
  }

  obtenerDatos() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final response =
          await ParametrosService.obtenerCategoriasPorTipoAviso(tipoAviso: 2);

      state = state.copyWith(
        tabs: response,
      );
      await obtenerNovedades(state.tabs[0].id);
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  obtenerNovedades(int idCategoria) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    state = state.copyWith(
        tabSeleccionado: () => idCategoria,
        categoriaSeleccionada: () =>
            state.tabs.firstWhere((x) => x.id == idCategoria));

    try {
      if (state.categoriaSeleccionada?.nombre != "ALERTAS") {
        final response = await NovedadesService.obtenerNovedadesPorCategoria(
            categoria: idCategoria);

        state = state.copyWith(
          novedades: response,
        );
      } else {
        await ref
            .read(sesionCanalElectronicoProvider.notifier)
            .obtenerSesionesCanalesElectronicos();
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  selectNovedad(Novedad novedad) {
    state = state.copyWith(
      novedad: () => novedad,
    );

    ref.read(appRouterProvider).push('/novedades/novedad');
  }
}

class NovedadesState {
  final List<CategoriaTipoAviso> tabs;
  final int? tabSeleccionado;
  final List<Novedad> novedades;
  final Novedad? novedad;
  final CategoriaTipoAviso? categoriaSeleccionada;

  NovedadesState(
      {this.tabs = const [],
      this.tabSeleccionado,
      this.novedades = const [],
      this.novedad,
      this.categoriaSeleccionada});

  NovedadesState copyWith(
          {List<CategoriaTipoAviso>? tabs,
          ValueGetter<int?>? tabSeleccionado,
          List<Novedad>? novedades,
          ValueGetter<Novedad?>? novedad,
          ValueGetter<CategoriaTipoAviso?>? categoriaSeleccionada}) =>
      NovedadesState(
          tabs: tabs ?? this.tabs,
          tabSeleccionado: tabSeleccionado != null
              ? tabSeleccionado()
              : this.tabSeleccionado,
          novedades: novedades ?? this.novedades,
          novedad: novedad != null ? novedad() : this.novedad,
          categoriaSeleccionada: categoriaSeleccionada != null
              ? categoriaSeleccionada()
              : this.categoriaSeleccionada);
}

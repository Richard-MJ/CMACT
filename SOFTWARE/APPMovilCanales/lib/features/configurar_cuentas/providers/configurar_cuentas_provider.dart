import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/inputs/alias.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/services/configurar_cuentas_service.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final configurarCuentasProvider =
    StateNotifierProvider<ConfigurarCuentasNotifier, ConfigurarCuentasState>(
        (ref) {
  return ConfigurarCuentasNotifier(ref);
});

class ConfigurarCuentasNotifier extends StateNotifier<ConfigurarCuentasState> {
  ConfigurarCuentasNotifier(this.ref) : super(ConfigurarCuentasState());

  final Ref ref;

  initDatos() {
    state = state.copyWith(
      cuentasAhorro: [],
      cuentaAhorro: () => null,
      alias: const AliasCuentaAhorro.pure(''),
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await ref.read(homeProvider.notifier).getCuentas();

      state = state.copyWith(
        cuentasAhorro: ref.read(homeProvider).cuentasAhorro,
      );
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  selectCuentaAhorro(CuentaAhorro cuentaAhorro) {
    state = state.copyWith(
      cuentaAhorro: () => cuentaAhorro,
    );
    ref.read(appRouterProvider).push('/configurar-cuentas/cuenta-ahorro');
  }

  actualizarAlias() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await ConfigurarCuentasService.actualizarAlias(
        alias: state.alias.value,
        codigoSistema: state.cuentaAhorro?.codigoTipo,
        numeroProducto: state.cuentaAhorro?.identificador,
      );

      await ref.read(homeProvider.notifier).getCuentas();

      state = state.copyWith(
        cuentasAhorro: ref.read(homeProvider).cuentasAhorro,
      );

      final indexCuenta = state.cuentasAhorro.indexWhere((cuenta) =>
          cuenta.identificador == state.cuentaAhorro?.identificador);
      if (indexCuenta >= 0) {
        state = state.copyWith(
          cuentaAhorro: () => state.cuentasAhorro[indexCuenta],
          alias: const AliasCuentaAhorro.pure(''),
        );
      }
      ref.read(appRouterProvider).pop();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  changeAlias(AliasCuentaAhorro alias) {
    state = state.copyWith(
      alias: alias,
    );
  }
}

class ConfigurarCuentasState {
  final List<CuentaAhorro> cuentasAhorro;
  final CuentaAhorro? cuentaAhorro;
  final AliasCuentaAhorro alias;

  ConfigurarCuentasState({
    this.cuentasAhorro = const [],
    this.cuentaAhorro,
    this.alias = const AliasCuentaAhorro.pure(''),
  });

  get btnAliasDisabled {
    return alias.isNotValid;
  }

  ConfigurarCuentasState copyWith({
    List<CuentaAhorro>? cuentasAhorro,
    ValueGetter<CuentaAhorro?>? cuentaAhorro,
    AliasCuentaAhorro? alias,
  }) =>
      ConfigurarCuentasState(
        cuentasAhorro: cuentasAhorro ?? this.cuentasAhorro,
        cuentaAhorro: cuentaAhorro != null ? cuentaAhorro() : this.cuentaAhorro,
        alias: alias ?? this.alias,
      );
}

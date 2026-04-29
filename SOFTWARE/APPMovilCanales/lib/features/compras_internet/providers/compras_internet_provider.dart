import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/compras_internet/models/afiliacion.dart';
import 'package:caja_tacna_app/features/compras_internet/models/afiliar_response.dart';
import 'package:caja_tacna_app/features/compras_internet/models/confirmar_afiliacion_response.dart';
import 'package:caja_tacna_app/features/compras_internet/models/confirmar_desafiliacion_response.dart';
import 'package:caja_tacna_app/features/compras_internet/models/cuenta_afiliacion.dart';
import 'package:caja_tacna_app/features/compras_internet/models/desafiliar_response.dart';
import 'package:caja_tacna_app/features/compras_internet/services/compras_internet_service.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final comprasInternetProvider =
    StateNotifierProvider<ComprasInternetNotifier, ComprasInternetState>((ref) {
  return ComprasInternetNotifier(ref);
});

class ComprasInternetNotifier extends StateNotifier<ComprasInternetState> {
  ComprasInternetNotifier(this.ref) : super(ComprasInternetState());

  final Ref ref;

  initDatos() {
    state = state.copyWith(
      cuentasAfiliacion: [],
      cuentaAfiliacion: () => null,
      afiliacion: () => null,
      confirmarAfiliacionResponse: () => null,
      afiliarResponse: () => null,
      tokenDigital: '',
      confirmarDesafiliacionResponse: () => null,
      desafiliarResponse: () => null,
    );
  }

  getCuentasAfiliacion() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final cuentasAfiliacion = await ComprasInternetService.obtenerCuentas();

      state = state.copyWith(
        cuentasAfiliacion: cuentasAfiliacion,
      );

      await obtenerAfiliacion();
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  obtenerAfiliacion() async {
    try {
      final afiliacion = await ComprasInternetService.obtenerAfiliacion();
      final cuentaAfiliacion = state.cuentasAfiliacion.firstWhere(
          (cuentaAfiliacion) =>
              cuentaAfiliacion.numeroCuenta == afiliacion.numeroCuentaAfiliada);

      state = state.copyWith(
        afiliacion: () => afiliacion,
        cuentaAfiliacion: () => cuentaAfiliacion,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  submit({required bool withPush}) {
    //si está afiliado
    if (state.afiliacion != null) {
      //si ninguna cuenta esta seleccionada
      if (state.cuentaAfiliacion == null) {
        desafiliar(withPush: withPush);
      } else {
        if (state.afiliacion?.numeroCuentaAfiliada !=
            state.cuentaAfiliacion?.numeroCuenta) {
          ref
              .read(snackbarProvider.notifier)
              .showSnackbar('Primero debe desafiliarse.', SnackbarType.error);
        }
      }
    } else {
      if (state.cuentaAfiliacion != null) {
        afiliar(withPush: withPush);
      }
    }
  }

  afiliar({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {
      final afiliarResponse = await ComprasInternetService.afiliar(
        codigoMoneda: state.cuentaAfiliacion?.codigoMoneda,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        afiliarResponse: () => afiliarResponse,
        metodoAfiliacion: MetodoAfiliacion.afiliacion,
        tokenDigital: await CoreService.desencriptarToken(
          afiliarResponse.codigoSolicitado,
        ),
      );
      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/compras-internet/confirmar');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmarAfiliacion() async {
    try {
      final confirmarAfiliacionResponse =
          await ComprasInternetService.confirmarAfiliacion(
        tokenDigital: state.tokenDigital,
        codigoMoneda: state.cuentaAfiliacion?.codigoMoneda,
        numeroCuenta: state.cuentaAfiliacion?.numeroCuenta,
      );

      state = state.copyWith(
        confirmarAfiliacionResponse: () => confirmarAfiliacionResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();
      ref.read(appRouterProvider).push('/compras-internet/afiliacion-exitosa');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
  }

  desafiliar({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {
      final desafiliarResponse = await ComprasInternetService.desafiliar(
        codigoMoneda: state.cuentaAfiliacion?.codigoMoneda,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        desafiliarResponse: () => desafiliarResponse,
        metodoAfiliacion: MetodoAfiliacion.desafiliacion,
        tokenDigital: await CoreService.desencriptarToken(
          desafiliarResponse.codigoSolicitado,
        ),
      );
      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/compras-internet/confirmar');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmarDesafiliacion() async {
    try {
      final confirmarDesafiliacionResponse =
          await ComprasInternetService.confirmarDesafiliacion(
        tokenDigital: state.tokenDigital,
        codigoMoneda: state.cuentaAfiliacion?.codigoMoneda,
      );

      state = state.copyWith(
        confirmarDesafiliacionResponse: () => confirmarDesafiliacionResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();
      ref.read(appRouterProvider).push('/compras-internet/afiliacion-exitosa');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
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
    if (state.metodoAfiliacion == MetodoAfiliacion.afiliacion) {
      await confirmarAfiliacion();
    } else {
      await confirmarDesafiliacion();
    }

    ref.read(loaderProvider.notifier).dismissLoader();
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.metodoAfiliacion == MetodoAfiliacion.afiliacion
              ? state.afiliarResponse?.fechaSistema
              : state.desafiliarResponse?.fechaSistema,
          date: state.metodoAfiliacion == MetodoAfiliacion.afiliacion
              ? state.afiliarResponse?.fechaVencimiento
              : state.desafiliarResponse?.fechaVencimiento,
        );
  }

  selectCuenta(CuentaAfiliacion cuentaAfiliacion) {
    if (cuentaAfiliacion.numeroCuenta == state.cuentaAfiliacion?.numeroCuenta) {
      state = state.copyWith(
        cuentaAfiliacion: () => null,
      );
      return;
    }

    state = state.copyWith(
      cuentaAfiliacion: () => cuentaAfiliacion,
    );
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }
}

enum MetodoAfiliacion { afiliacion, desafiliacion }

class ComprasInternetState {
  final List<CuentaAfiliacion> cuentasAfiliacion;
  final CuentaAfiliacion? cuentaAfiliacion;
  final Afiliacion? afiliacion;
  final AfiliarResponse? afiliarResponse;
  final DesafiliarResponse? desafiliarResponse;

  final String tokenDigital;
  final ConfirmarAfiliacionResponse? confirmarAfiliacionResponse;
  final ConfirmarDesafiliacionResponse? confirmarDesafiliacionResponse;

  final MetodoAfiliacion metodoAfiliacion;

  ComprasInternetState({
    this.cuentasAfiliacion = const [],
    this.cuentaAfiliacion,
    this.afiliacion,
    this.afiliarResponse,
    this.desafiliarResponse,
    this.tokenDigital = '',
    this.confirmarAfiliacionResponse,
    this.confirmarDesafiliacionResponse,
    this.metodoAfiliacion = MetodoAfiliacion.afiliacion,
  });

  ComprasInternetState copyWith({
    List<CuentaAfiliacion>? cuentasAfiliacion,
    ValueGetter<CuentaAfiliacion?>? cuentaAfiliacion,
    ValueGetter<Afiliacion?>? afiliacion,
    ValueGetter<AfiliarResponse?>? afiliarResponse,
    ValueGetter<DesafiliarResponse?>? desafiliarResponse,
    String? tokenDigital,
    ValueGetter<ConfirmarAfiliacionResponse?>? confirmarAfiliacionResponse,
    ValueGetter<ConfirmarDesafiliacionResponse?>?
        confirmarDesafiliacionResponse,
    MetodoAfiliacion? metodoAfiliacion,
  }) =>
      ComprasInternetState(
        cuentasAfiliacion: cuentasAfiliacion ?? this.cuentasAfiliacion,
        cuentaAfiliacion: cuentaAfiliacion != null
            ? cuentaAfiliacion()
            : this.cuentaAfiliacion,
        afiliacion: afiliacion != null ? afiliacion() : this.afiliacion,
        afiliarResponse:
            afiliarResponse != null ? afiliarResponse() : this.afiliarResponse,
        desafiliarResponse: desafiliarResponse != null
            ? desafiliarResponse()
            : this.desafiliarResponse,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        confirmarAfiliacionResponse: confirmarAfiliacionResponse != null
            ? confirmarAfiliacionResponse()
            : this.confirmarAfiliacionResponse,
        confirmarDesafiliacionResponse: confirmarDesafiliacionResponse != null
            ? confirmarDesafiliacionResponse()
            : this.confirmarDesafiliacionResponse,
        metodoAfiliacion: metodoAfiliacion ?? this.metodoAfiliacion,
      );
}

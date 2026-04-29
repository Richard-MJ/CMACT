import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/tipo_limite.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/actualizar_limite_response.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/confirmar_limite_response.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/limites_response.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/providers/configurar_cuentas_provider.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/services/limites_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final limitesProvider =
    StateNotifierProvider<LimitesNotifier, LimitesState>((ref) {
  return LimitesNotifier(ref);
});

class LimitesNotifier extends StateNotifier<LimitesState> {
  LimitesNotifier(this.ref) : super(LimitesState());

  final Ref ref;

  initDatos() {
    state = state.copyWith(
      limites: () => null,
      limiteTransacciones: '0.00',
      limiteOperaciones: '0',
      limiteSemanal: '0.00',
      actualizarResponse: () => null,
      confirmarResponse: () => null,
      tokenDigital: '',
    );
  }

  obtenerLimites() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final response = await LimitesService.obtenerLimites(
        numeroCuenta:
            ref.read(configurarCuentasProvider).cuentaAhorro?.identificador,
      );

      state = state.copyWith(
        limites: () => response,
        limiteTransacciones: CtUtils.formatStringWithTwoDecimals(
            response.montoMaximoOperacionLimiteCliente.toString()),
        limiteOperaciones:
            response.numeroMaximoOperacionesLimiteCliente.toInt().toString(),
        limiteSemanal: CtUtils.formatStringWithTwoDecimals(
            response.montoMaximoOperacionLimiteSemanalCliente.toString()),
      );
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  actualizar({required bool withPush, required int codigoTipoLimite}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {

      final valorLimite = switch (codigoTipoLimite) {
        TipoLimite.numeroTransacciones => state.limiteOperaciones,
        TipoLimite.montoPorTransaccion => state.limiteTransacciones,
        TipoLimite.montoSemanal => state.limiteSemanal,
        _ => '0.00'
      };

      final response = await LimitesService.actualizarLimite(
        valorLimite: valorLimite,
        numeroCuenta:
            ref.read(configurarCuentasProvider).cuentaAhorro?.identificador,
        codigoTipoLimite: codigoTipoLimite,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        actualizarResponse: () => response,
        tokenDigital: await CoreService.desencriptarToken(
          response.codigoSolicitado,
        ),
      );
      initTimer();
      if (withPush) {
        if (codigoTipoLimite == TipoLimite.numeroTransacciones) {
          ref.read(appRouterProvider).push('/configurar-cuentas/confirmar-limite-operaciones');
        }
        if(codigoTipoLimite == TipoLimite.montoPorTransaccion) {
          ref.read(appRouterProvider).push('/configurar-cuentas/confirmar-limite-transacciones');
        }
        if(codigoTipoLimite == TipoLimite.montoSemanal) {
          ref.read(appRouterProvider).push('/configurar-cuentas/confirmar-limite-transacciones-semanal');
        }
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmar({required int codigoTipoLimite}) async {
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
      final valorLimite = switch (codigoTipoLimite) {
        TipoLimite.numeroTransacciones => state.limiteOperaciones,
        TipoLimite.montoPorTransaccion => state.limiteTransacciones,
        TipoLimite.montoSemanal => state.limiteSemanal,
        _ => '0.00'
      };

      final confirmarResponse = await LimitesService.confirmarLimite(
        tokenDigital: state.tokenDigital,
        valorLimite: valorLimite,
        numeroCuenta:
            ref.read(configurarCuentasProvider).cuentaAhorro?.identificador,
        codigoTipoLimite: codigoTipoLimite,
      );

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );
      if (codigoTipoLimite == TipoLimite.numeroTransacciones) {
        ref.read(appRouterProvider).push(
            '/configurar-cuentas/actualizacion-exitosa-limite-operaciones');
      }
      if(codigoTipoLimite == TipoLimite.montoPorTransaccion) {
        ref.read(appRouterProvider).push(
            '/configurar-cuentas/actualizacion-exitosa-limite-transacciones');
      }
      if(codigoTipoLimite == TipoLimite.montoSemanal) {
        ref.read(appRouterProvider).push(
            '/configurar-cuentas/actualizacion-exitosa-limite-transacciones-semanal');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.actualizarResponse?.fechaSistema,
          date: state.actualizarResponse?.fechaVencimiento,
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

  changeLimiteTransacciones(double limiteTransacciones) {
    state = state.copyWith(
      limiteTransacciones:
          CtUtils.formatStringWithTwoDecimals(limiteTransacciones.toString()),
    );
  }

  changeLimiteSemanal(double limiteSemanal) {
    state = state.copyWith(
      limiteSemanal:
          CtUtils.formatStringWithTwoDecimals(limiteSemanal.toString()),
    );
  }

  changeLimiteOperaciones(double limiteOperaciones) {
    state = state.copyWith(
      limiteOperaciones: limiteOperaciones.toInt().toString(),
    );
  }

  changeLimiteOperacionesInput(String limiteOperaciones) {
    if (limiteOperaciones.isEmpty) {
      state = state.copyWith(
        limiteOperaciones: limiteOperaciones,
      );
      return;
    }

    if (int.parse(limiteOperaciones) < 0 ||
        int.parse(limiteOperaciones) >
            (state.limites?.numeroMaximoOperacionesDefecto ?? 0)) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'El valor ingresado no se encuentra dentro de los límites mínimo y máximo',
          SnackbarType.error);
      return;
    }

    state = state.copyWith(
      limiteOperaciones: limiteOperaciones,
    );
  }

  changeLimiteTransaccionesInput(String limiteTransacciones) {
    if (limiteTransacciones.isEmpty) {
      state = state.copyWith(
        limiteTransacciones: limiteTransacciones,
      );
      return;
    }

    if (double.parse(limiteTransacciones) <
            (state.limites?.montoMinimoOperacionDefecto ?? 0) ||
        double.parse(limiteTransacciones) >
            (state.limites?.montoMaximoOperacionDefecto ?? 0)) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'El valor ingresado no se encuentra dentro de los límites mínimo y máximo',
          SnackbarType.error);
      return;
    }

    state = state.copyWith(
      limiteTransacciones: limiteTransacciones,
    );
  }

  changeLimiteSemanalInput(String limiteSemanal) {
    if (limiteSemanal.isEmpty) {
      state = state.copyWith(
        limiteSemanal: limiteSemanal,
      );
      return;
    }

    if (double.parse(limiteSemanal) <
            (state.limites?.montoMinimoOperacionDefecto ?? 0) ||
        double.parse(limiteSemanal) >
            (state.limites?.montoMaximoOperacionLimiteSemanalDefecto ?? 0)) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'El valor ingresado no se encuentra dentro de los límites mínimo y máximo',
          SnackbarType.error);
      return;
    }

    state = state.copyWith(
      limiteSemanal: limiteSemanal,
    );
  }
}

class LimitesState {
  final LimitesResponse? limites;
  final String limiteTransacciones;
  final String limiteOperaciones;
  final String limiteSemanal;
  final ActualizarLimiteResponse? actualizarResponse;
  final String tokenDigital;
  final ConfirmarLimiteResponse? confirmarResponse;

  LimitesState({
    this.limites,
    this.limiteTransacciones = '0.00',
    this.limiteOperaciones = '0',
    this.limiteSemanal = '0.00',
    this.actualizarResponse,
    this.tokenDigital = '',
    this.confirmarResponse,
  });

  LimitesState copyWith({
    ValueGetter<LimitesResponse?>? limites,
    String? limiteTransacciones,
    String? limiteOperaciones,
    String? limiteSemanal,
    ValueGetter<ActualizarLimiteResponse?>? actualizarResponse,
    String? tokenDigital,
    ValueGetter<ConfirmarLimiteResponse?>? confirmarResponse,
  }) =>
      LimitesState(
        limites: limites != null ? limites() : this.limites,
        limiteTransacciones: limiteTransacciones ?? this.limiteTransacciones,
        limiteOperaciones: limiteOperaciones ?? this.limiteOperaciones,
        limiteSemanal: limiteSemanal ?? this.limiteSemanal,
        actualizarResponse: actualizarResponse != null
            ? actualizarResponse()
            : this.actualizarResponse,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
      );
}

import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/dispositivo_seguro.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/remover_dispositivo_seguro_confirmar_response.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/remover_dispositivo_seguro_response.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/sesion_canal_electronico.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/services/sesion_canal_electronico_service.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';

final sesionCanalElectronicoProvider = StateNotifierProvider<
    SesionCanalElectronicoNotifier, SesionCanalEletronicoState>((ref) {
  return SesionCanalElectronicoNotifier(ref);
});

class SesionCanalElectronicoNotifier
    extends StateNotifier<SesionCanalEletronicoState> {
  SesionCanalElectronicoNotifier(this.ref)
      : super(SesionCanalEletronicoState());

  final Ref ref;

  initDatos() {
    state = state.copyWith(
        dispositivosSeguros: [],
        sesionesCanalElectronico: [],
        confirmarResponse: () => null,
        removerResponse: () => null,
        dispositivoActual: () => null,
        dispositivoSeleccionados: [],
        tokenDigital: '',
        otrosDispositivosSeguros: false);
  }

  Future<void> obtenerDispositivosSeguros(BuildContext context) async {
    try {
      final dispositivosSeguros =
          await SesionCanalElectronicoService.obtenerDispositivosSeguros();
      final dispositivoId = ref.watch(loginProvider).guid;
      final dispositivoActual = dispositivosSeguros.firstWhere(
          (dispositivo) => dispositivo.dispositivoId == dispositivoId);
      dispositivosSeguros.remove(dispositivoActual);

      state = state.copyWith(
          dispositivosSeguros: dispositivosSeguros,
          dispositivoActual: () => dispositivoActual);
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  Future<void> obtenerSesionesCanalesElectronicos() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final sesionesCanal = await SesionCanalElectronicoService
          .obtenerSesionesCanalesElectronicos();
      state = state.copyWith(sesionesCanalElectronico: sesionesCanal);
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  Future<void> removerDispositivosSeguros({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final confirmarResponse = await SesionCanalElectronicoService
          .removerDispositivosSegurosConfirmacion(
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );
      state = state.copyWith(
          confirmarResponse: () => confirmarResponse,
          tokenDigital: await CoreService.desencriptarToken(
              confirmarResponse.codigoSolicitado));

      initTimer();

      if (withPush) {
        ref.read(appRouterProvider).push('/sesion-canal-electronico/remover/confirmar');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  Future<void> confirmar() async {
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
      final removerResponse =
          await SesionCanalElectronicoService.removerDispositivosSeguros(
        codigoAutorizacion: state.tokenDigital,
        dispositivos:
            state.dispositivoSeleccionados.map((e) => e.dispositivoId).toList(),
      );
      state = state.copyWith(removerResponse: () => removerResponse);

      ref.read(appRouterProvider).push('/sesion-canal-electronico/remover-exitoso');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  void resetConfirmarResponse() {
    state = state.copyWith(confirmarResponse: () => null);
  }

  void resetRemoverResponse() {
    state = state.copyWith(removerResponse: () => null);
  }

  toogleSeleccionarDispositivo(DispositivoSeguro dispositivoSeguro) {
    final dispositivosSeleccionados =
        List<DispositivoSeguro>.from(state.dispositivoSeleccionados);

    final dispositivoExistente = dispositivosSeleccionados
        .cast<DispositivoSeguro?>()
        .firstWhere((d) => d?.dispositivoId == dispositivoSeguro.dispositivoId,
            orElse: () => null);

    if (dispositivoExistente == null) {
      dispositivosSeleccionados.add(dispositivoSeguro);
    } else {
      dispositivosSeleccionados.remove(dispositivoExistente);
    }

    final index = state.dispositivosSeguros.indexWhere((dispositivo) {
      return dispositivo.dispositivoId == dispositivoSeguro.dispositivoId;
    });

    state.dispositivosSeguros[index].estaSelecionado =
        !state.dispositivosSeguros[index].estaSelecionado;
    state = state.copyWith(
        dispositivosSeguros: state.dispositivosSeguros,
        dispositivoSeleccionados: dispositivosSeleccionados);
  }

  toggleSeleccionarTodosLosDispositivos() {
    final todosSeleccionados = state.dispositivoSeleccionados.length ==
        state.dispositivosSeguros.length;

    final dispositivosSeguros = state.dispositivosSeguros.map((dispositivo) {
      dispositivo.estaSelecionado = !todosSeleccionados;
      return dispositivo;
    }).toList();

    final List<DispositivoSeguro> dispositivosSeleccionados =
        todosSeleccionados ? [] : dispositivosSeguros;

    state = state.copyWith(
        dispositivoSeleccionados: dispositivosSeleccionados,
        dispositivosSeguros: dispositivosSeguros);
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.confirmarResponse?.fechaSistema,
          date: state.confirmarResponse?.fechaVencimiento,
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

  changeOtrosDispositivos(bool otrosDispositivosSeguros) async {
    state = state.copyWith(otrosDispositivosSeguros: otrosDispositivosSeguros);
  }
}

class SesionCanalEletronicoState {
  final List<DispositivoSeguro> dispositivoSeleccionados;
  final List<DispositivoSeguro> dispositivosSeguros;
  final List<SesionCanalElectronico> sesionesCanalElectronico;
  final RemoverDispositivoSeguroConfirmacionResponse? confirmarResponse;
  final RemoverDispositivoSeguroResponse? removerResponse;
  final DispositivoSeguro? dispositivoActual;
  final String tokenDigital;
  final bool otrosDispositivosSeguros;

  SesionCanalEletronicoState(
      {this.dispositivoSeleccionados = const [],
      this.dispositivosSeguros = const [],
      this.sesionesCanalElectronico = const [],
      this.confirmarResponse,
      this.removerResponse,
      this.dispositivoActual,
      this.tokenDigital = '',
      this.otrosDispositivosSeguros = false});

  SesionCanalEletronicoState copyWith(
          {List<DispositivoSeguro>? dispositivoSeleccionados,
          List<DispositivoSeguro>? dispositivosSeguros,
          List<SesionCanalElectronico>? sesionesCanalElectronico,
          ValueGetter<RemoverDispositivoSeguroConfirmacionResponse?>?
              confirmarResponse,
          ValueGetter<RemoverDispositivoSeguroResponse?>? removerResponse,
          ValueGetter<DispositivoSeguro?>? dispositivoActual,
          String? tokenDigital,
          bool? otrosDispositivosSeguros}) =>
      SesionCanalEletronicoState(
          dispositivoSeleccionados:
              dispositivoSeleccionados ?? this.dispositivoSeleccionados,
          dispositivosSeguros: dispositivosSeguros ?? this.dispositivosSeguros,
          sesionesCanalElectronico:
              sesionesCanalElectronico ?? this.sesionesCanalElectronico,
          confirmarResponse: confirmarResponse != null
              ? confirmarResponse()
              : this.confirmarResponse,
          removerResponse: removerResponse != null
              ? removerResponse()
              : this.removerResponse,
          dispositivoActual: dispositivoActual != null
              ? dispositivoActual()
              : this.dispositivoActual,
          tokenDigital: tokenDigital ?? this.tokenDigital,
          otrosDispositivosSeguros:
              otrosDispositivosSeguros ?? this.otrosDispositivosSeguros);
}

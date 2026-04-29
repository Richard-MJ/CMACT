import 'dart:async';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/rutas_excluidas_inactividad.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/features/shared/providers/auth_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/widgets/dialog_inactividad.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final inactivityProvider =
    StateNotifierProvider<InactivityNotifier, InactivityState>((ref) {
  return InactivityNotifier(ref);
});

class InactivityNotifier extends StateNotifier<InactivityState> {
  InactivityNotifier(this.ref) : super(InactivityState());

  Timer? _inactivityTimer;
  Timer? _dialogTimer;
  DateTime? _lastInteractionTime;
  final storageService = StorageService();
  final Ref ref;

  @override
  void dispose() {
    cancelarTimers();
    super.dispose();
  }

  Future<void> resetearTimer({int? tiempoRestante}) async {
    if (!puedeDetectarInactividad()) return;

    cancelarTimers();

    final tiempoInactividad =
        await storageService.get<int>(StorageKeys.tiempoInactividad) ?? 0;

    _lastInteractionTime = DateTime.now();

    if (tiempoRestante == null || tiempoRestante > tiempoInactividad + 10) {
      _inactivityTimer =
          Timer(Duration(seconds: tiempoInactividad), mostrarDialogInactividad);
    }
  }

  Future<void> checkInactivity() async {
    if (!puedeDetectarInactividad() || _lastInteractionTime == null) return;

    final tiempoInactividad =
        await storageService.get<int>(StorageKeys.tiempoInactividad) ?? 0;
    final difference = DateTime.now().difference(_lastInteractionTime!);

    if (difference.inSeconds >= tiempoInactividad + 10) {
      cancelarTimers();
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Se cerró la sesión por inactividad', SnackbarType.error);
      ref.read(authProvider.notifier).logout();
    } else if (difference.inSeconds >= tiempoInactividad) {
      mostrarDialogInactividad();
    } else {
      cancelarTimers();
      final remainingSeconds = tiempoInactividad - difference.inSeconds;
      _inactivityTimer =
          Timer(Duration(seconds: remainingSeconds), mostrarDialogInactividad);
    }
  }

  Future<void> mostrarDialogInactividad() async {
    if (!puedeMostrarDialogo()) return;

    establecerEstado(dialogAbierto: true);

    var counter = 10;
    state = state.copyWith(
        tiempoRestante: '${format2digits(0)}:${format2digits(counter)}');

    final bool? result = await showDialog<bool>(
      context: rootNavigatorKey.currentContext!,
      barrierDismissible: true,
      builder: (context) {
        _dialogTimer = Timer.periodic(const Duration(seconds: 1), (timer) {
          counter--;
          state = state.copyWith(
              tiempoRestante: '${format2digits(0)}:${format2digits(counter)}');
          if (counter == 0) {
            if (Navigator.of(context).canPop()) {
              Navigator.of(context).pop(false); // termina diálogo
              timer.cancel();
            }
          }
        });

        return const DialogInactividad();
      },
    );

    establecerEstado(dialogAbierto: false);

    if (result == true || result == null) {
      await resetearTimer(
          tiempoRestante: ref.read(authProvider).tiempoSesionRestante);
    } else {
      cancelarTimers();
      await Future.delayed(const Duration(milliseconds: 300));
      ref.read(authProvider.notifier).logout();
    }
  }

  String format2digits(int value) {
    if (value < 10) {
      return '0$value';
    }

    return '$value';
  }

  void cancelarTimers() {
    if (_inactivityTimer != null) {
      _inactivityTimer!.cancel();
      _inactivityTimer = null;
    }

    if (_dialogTimer != null) {
      _dialogTimer!.cancel();
      _dialogTimer = null;
    }
  }

  bool puedeDetectarInactividad() {
    return !state.dialogAbierto && debeDetectarInactividad();
  }

  bool puedeMostrarDialogo() {
    return !ref.read(loaderProvider).loading && puedeDetectarInactividad();
  }

  void establecerEstado({required bool dialogAbierto}) {
    state = state.copyWith(
      dialogAbierto: dialogAbierto,
    );
  }

  bool debeDetectarInactividad() {
    final currentContext = rootNavigatorKey.currentContext;
    final currentRoute =
        GoRouter.of(currentContext!).routerDelegate.currentConfiguration;

    return !RutasExcluidas.routes.any(currentRoute.fullPath.contains);
  }
}

class InactivityState {
  final bool dialogAbierto;
  final String tiempoRestante;

  InactivityState({this.dialogAbierto = false, this.tiempoRestante = '00:00'});

  InactivityState copyWith({bool? dialogAbierto, String? tiempoRestante}) =>
      InactivityState(
          dialogAbierto: dialogAbierto ?? this.dialogAbierto,
          tiempoRestante: tiempoRestante ?? this.tiempoRestante);
}

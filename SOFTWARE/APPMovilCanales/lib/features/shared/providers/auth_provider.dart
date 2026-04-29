import 'dart:async';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/dialog_app_sesion_terminada.dart';
import 'package:caja_tacna_app/features/shared/events/auth_event.dart';
import 'package:caja_tacna_app/features/shared/events/dismiss_loader_event.dart';
import 'package:caja_tacna_app/features/shared/providers/auth_status_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/signalr_provider.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final authProvider =
    NotifierProvider<AuthNotifier, AuthState>(() => AuthNotifier());

class AuthNotifier extends Notifier<AuthState> {
  @override
  AuthState build() {
    _initListeners();
    return AuthState();
  }

  final storageService = StorageService();
  GlobalKey? authKey;

  StreamSubscription<void>? _logoutSubscription;
  StreamSubscription<void>? _dismissLoaderSubscription;

  Timer? authInterval;
  initAuthInterval() {
    cancelAuthInterval();
    const duration = Duration(seconds: 1);
    int tiempoSesion = 0;
    authInterval = Timer.periodic(duration, (Timer timer) async {
      tiempoSesion++;
      int expiresIn = ref.read(loginProvider).expiresIn ?? 0;

      state = state.copyWith(tiempoSesionRestante: (expiresIn - tiempoSesion));
      final bool tokenValido = tiempoSesion <= expiresIn;
      //si el token ha expirado
      if (!tokenValido) {
        cancelAuthInterval();
        authKey = GlobalKey();
        logout();

        await Future.delayed(const Duration(milliseconds: 500));

        showDialog(
          barrierDismissible: true,
          context: rootNavigatorKey.currentContext!,
          builder: (context) {
            return DialogSesionTerminada(
              key: authKey,
            );
          },
        );
      }
    });
  }

  cancelAuthInterval() {
    if (authInterval != null) {
      authInterval!.cancel();
    }
  }

  logout() async {
    cancelAuthInterval();
    await ref.read(signalRProvider.notifier).disposeConnection();
    await storageService.remove(StorageKeys.token);
    ref
        .read(authStatusProvider.notifier)
        .changeStatus(AuthStatus.notAuthenticated);
    ref.read(appRouterProvider).go('/inicio-sesion/formulario');
  }

  void _initListeners() {
    _logoutSubscription ??= AuthEvents.onLogout.listen((_) {
      logout();
    });

    _dismissLoaderSubscription ??= DissmissLoaderEvents.onDismiss.listen((_) {
      ref.read(loaderProvider.notifier).dismissLoader();
    });

    ref.onDispose(() {
      if (_logoutSubscription != null) {
        _logoutSubscription?.cancel();
      }
      if (_dismissLoaderSubscription != null) {
        _dismissLoaderSubscription?.cancel();
      }
      cancelAuthInterval();
    });
  }
}

class AuthState {
  final int tiempoSesionRestante;

  AuthState({this.tiempoSesionRestante = 0});

  AuthState copyWith({int? tiempoSesionRestante}) => AuthState(
      tiempoSesionRestante: tiempoSesionRestante ?? this.tiempoSesionRestante);
}

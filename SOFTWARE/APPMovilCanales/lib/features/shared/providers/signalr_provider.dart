import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/shared/providers/inactivity_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/dialog_sesion_otro_dispositivo.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:signalr_core/signalr_core.dart';
import 'package:caja_tacna_app/constants/environment.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:caja_tacna_app/features/shared/providers/auth_provider.dart';

final signalRProvider =
    StateNotifierProvider<SignalRNotifier, SignalRState>((ref) {
  return SignalRNotifier(ref);
});

class SignalRNotifier extends StateNotifier<SignalRState> {
  SignalRNotifier(this.ref) : super(SignalRState());

  final Ref ref;
  HubConnection? _hubConnection;
  final String _serverUrl = "${Environment.urlBase}/sessionHub";
  final storageService = StorageService();

  @override
  void dispose() {
    disposeConnection();
    super.dispose();
  }

  Future<void> initConnection() async {
    if (_hubConnection == null ||
        _hubConnection!.state != HubConnectionState.connected) {
      var token = await storageService.get<String>(StorageKeys.token);

      final httpConnectionOptions = HttpConnectionOptions(
        accessTokenFactory: () => Future.value(token),
      );

      _hubConnection = HubConnectionBuilder()
          .withUrl(_serverUrl, httpConnectionOptions)
          .withAutomaticReconnect()
          .build();

      _hubConnection!.onclose((error) async {
        disposeConnection();
        ref.read(inactivityProvider.notifier).cancelarTimers();
        await ref.read(authProvider.notifier).logout();
      });

      _registerOnMethods();
      await _startConnection();
    }
  }

  Future<void> _startConnection() async {
    try {
      if (_hubConnection!.state != HubConnectionState.connected) {
        await _hubConnection!.start();
        state = state.copyWith(connectionIsOpen: true, shouldReconnect: true);
      }
    } catch (_) {
      disposeConnection();
      _handleLogout(message: "Lo sentimos, tenemos unos incovenientes.");
    }
  }

  Future<void> disposeConnection() async {
    state = state.copyWith(shouldReconnect: false);
    try {
      if (_hubConnection != null) {
        await _hubConnection!.stop().timeout(const Duration(seconds: 5));
      }
    } catch (_) {
      // Intentionally left empty; handle the timeout or other errors silently
    } finally {
      _hubConnection = null;
      state = state.copyWith(connectionIsOpen: false);
    }
  }

  void _registerOnMethods() {
    _hubConnection!.on('LogoutNotification', (message) {
      _handleLogout(message: message?[0] ?? "");
    });
  }

  void _handleLogout({required String message}) async {
    ref.read(inactivityProvider.notifier).cancelarTimers();
    await ref.read(authProvider.notifier).logout();
    await Future.delayed(const Duration(milliseconds: 100));
    showDialog(
        barrierDismissible: true,
        context: rootNavigatorKey.currentContext!,
        builder: (context) => DialogSesionOtroDispositivo(
              message: message,
            ));
  }
}

class SignalRState {
  final bool connectionIsOpen;

  SignalRState({this.connectionIsOpen = false});

  SignalRState copyWith({
    bool? connectionIsOpen,
    bool? shouldReconnect,
  }) =>
      SignalRState(
        connectionIsOpen: connectionIsOpen ?? this.connectionIsOpen,
      );
}

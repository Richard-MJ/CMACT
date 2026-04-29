import 'package:flutter_riverpod/flutter_riverpod.dart';

enum AuthStatus { authenticated, notAuthenticated }

final authStatusProvider =
    StateNotifierProvider<AuthStatusNotifier, AuthStatus>(
  (ref) => AuthStatusNotifier(),
);

class AuthStatusNotifier extends StateNotifier<AuthStatus> {
  AuthStatusNotifier() : super(AuthStatus.notAuthenticated);

  String? _pendingRedirect;

  void changeStatus(AuthStatus status) {
    state = status;
  }

  void setPendingRedirect(String path) {
    _pendingRedirect = path;
  }

  bool hasPendingRedirect() => _pendingRedirect != null;

  String? consumePendingRedirect() {
    final path = _pendingRedirect;
    _pendingRedirect = null;
    return path;
  }

  String? peekPendingRedirect() => _pendingRedirect;
}

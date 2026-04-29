import 'dart:async';

class AuthEvents {
  static final StreamController<void> _logoutController =
      StreamController<void>.broadcast();

  static void notifyLogout() {
    _logoutController.add(null);
  }

  static Stream<void> get onLogout => _logoutController.stream;
}

import 'dart:async';

class DissmissLoaderEvents {
  static final StreamController<void> _dismissLoaderController =
      StreamController<void>.broadcast();

  static void notifyDismissLoader() {
    _dismissLoaderController.add(null);
  }

  static Stream<void> get onDismiss => _dismissLoaderController.stream;
}

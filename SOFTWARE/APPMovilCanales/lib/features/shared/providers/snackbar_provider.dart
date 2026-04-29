import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final snackbarProvider =
    StateNotifierProvider<SnackbarNotifier, SnackbarState>((ref) {
  return SnackbarNotifier();
});

class SnackbarNotifier extends StateNotifier<SnackbarState> {
  SnackbarNotifier() : super(SnackbarState());

  showSnackbar(String message, SnackbarType type) {
    state = state.copyWith(showSnackbar: true, message: message, type: type);
    state = state.copyWith(showSnackbar: false);
  }
}

class SnackbarState {
  final bool showSnackbar;
  final String message;
  final SnackbarType type;

  SnackbarState({
    this.showSnackbar = false,
    this.message = '',
    this.type = SnackbarType.success,
  });

  SnackbarState copyWith({
    bool? showSnackbar,
    String? message,
    SnackbarType? type,
  }) =>
      SnackbarState(
        showSnackbar: showSnackbar ?? this.showSnackbar,
        message: message ?? this.message,
        type: type ?? this.type,
      );
}

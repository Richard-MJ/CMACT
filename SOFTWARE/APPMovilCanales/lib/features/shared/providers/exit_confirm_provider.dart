import 'package:flutter_riverpod/flutter_riverpod.dart';

final exitConfirmProvider =
    StateNotifierProvider<ExitConfirmNotifier, ExitConfirmState>((ref) {
  return ExitConfirmNotifier();
});

class ExitConfirmNotifier extends StateNotifier<ExitConfirmState> {
  ExitConfirmNotifier() : super(ExitConfirmState());

  firstTap() {
    state = state.copyWith(
      exit: true,
    );

    Future.delayed(const Duration(milliseconds: 2000), () {
      state = state.copyWith(
        exit: false,
      );
    });
  }
}

class ExitConfirmState {
  final bool exit;

  ExitConfirmState({
    this.exit = false,
  });

  ExitConfirmState copyWith({
    bool? exit,
  }) =>
      ExitConfirmState(
        exit: exit ?? this.exit,
      );
}

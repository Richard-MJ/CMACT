import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final loaderProvider =
    StateNotifierProvider<LoaderNotifier, LoaderState>((ref) {
  return LoaderNotifier();
});

class LoaderNotifier extends StateNotifier<LoaderState> {
  LoaderNotifier() : super(LoaderState());

  showLoader({bool withOpacity = false}) {
    if (state.loading) return;
    FocusManager.instance.primaryFocus?.unfocus();

    state = state.copyWith(
      loading: true,
      withOpacity: withOpacity,
    );
  }

  dismissLoader() {
    if (!state.loading) return;

    state = state.copyWith(
      loading: false,
      withOpacity: false,
    );
  }
}

class LoaderState {
  final bool loading;
  final bool withOpacity;
  LoaderState({
    this.loading = false,
    this.withOpacity = false,
  });

  LoaderState copyWith({
    bool? loading,
    bool? withOpacity,
  }) =>
      LoaderState(
        loading: loading ?? this.loading,
        withOpacity: withOpacity ?? this.withOpacity,
      );
}

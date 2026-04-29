import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final deviceProvider =
    StateNotifierProvider<DeviceNotifier, DeviceState>((ref) {
  return DeviceNotifier();
});

class DeviceNotifier extends StateNotifier<DeviceState> {
  DeviceNotifier() : super(DeviceState());

  setMediaQueryData(MediaQueryData mediaQueryData) {
    state = state.copyWith(
      mediaQueryData: mediaQueryData,
    );
  }
}

class DeviceState {
  final MediaQueryData? mediaQueryData;

  DeviceState({
    this.mediaQueryData,
  });

  DeviceState copyWith({
    MediaQueryData? mediaQueryData,
  }) =>
      DeviceState(
        mediaQueryData: mediaQueryData ?? this.mediaQueryData,
      );
}

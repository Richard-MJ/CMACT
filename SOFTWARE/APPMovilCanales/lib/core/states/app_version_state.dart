import 'package:caja_tacna_app/core/models/app_version_response.dart';
import 'package:flutter/material.dart';

class AppVersionState {
  final String appVersion;
  final AppVersionResponse? versionamiento;

  AppVersionState({
    this.appVersion = '',
    this.versionamiento,
  });

  AppVersionState copyWith({
    String? appVersion,
    ValueGetter<AppVersionResponse?>? versionamiento,
  }) =>
      AppVersionState(
        appVersion: appVersion ?? this.appVersion,
        versionamiento:
            versionamiento != null ? versionamiento() : this.versionamiento,
      );
}

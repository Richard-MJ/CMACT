import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/dialog_app_sesion_terminada.dart';
import 'package:flutter/material.dart';

class DialogManager {
  static final DialogManager _instance = DialogManager._internal();
  factory DialogManager() => _instance;
  DialogManager._internal();

  bool _isDialogShowing = false;

  void showSessionExpiredDialog(BuildContext context) {
    if (!_isDialogShowing) {
      _isDialogShowing = true;
      showDialog(
        barrierDismissible: true,
        context: context,
        builder: (context) {
          return DialogSesionTerminada(
            key: UniqueKey(),
          );
        },
      ).then((_) {
        _isDialogShowing = false;
      });
    }
  }
}

import 'package:caja_tacna_app/features/external/services/parametros_service.dart';
import 'package:caja_tacna_app/features/home/models/configuracion.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/rendering.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final parametrosProvider =
    NotifierProvider<ParametrosNotifier, ParametrosState>(
        () => ParametrosNotifier());

class ParametrosNotifier extends Notifier<ParametrosState> {
  @override
  ParametrosState build() {
    return ParametrosState();
  }

  getParametros() async {
    try {
      final response =
          await ParametrosService.obtenerParametros();

      state = state.copyWith(
        enlaces: () => response,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }
}

class ParametrosState {
  final List<EnlaceDocumento>? enlaces;

  ParametrosState({
    this.enlaces,
  });

  ParametrosState copyWith({
    ValueGetter<List<EnlaceDocumento>?>? enlaces,
  }) {
    return ParametrosState(
      enlaces: enlaces != null ? enlaces() : this.enlaces,
    );
  }
}

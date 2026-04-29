import 'package:caja_tacna_app/core/models/datos_dispositivo.dart';
import 'package:flutter/material.dart';

class DispositivoState {
  final DatosDispositivo? dispositivo;
  final String identificadorDispositivo;

  DispositivoState({
    this.dispositivo,
    this.identificadorDispositivo = '',
  });

  DispositivoState copyWith({
    ValueGetter<DatosDispositivo?>? dispositivo,
    String? identificadorDispositivo,
  }) =>
      DispositivoState(
        dispositivo: dispositivo != null ? dispositivo() : this.dispositivo,
        identificadorDispositivo:
            identificadorDispositivo ?? this.identificadorDispositivo,
      );
}

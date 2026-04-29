import 'package:caja_tacna_app/features/anulacion_tarjetas/models/anular_response.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/models/datos_iniciales_response.dart';
import 'package:flutter/material.dart';

class AnulacionTarjetasState {
  final List<TarjetaAnulacion> tarjetas;
  final TarjetaAnulacion? tarjeta;
  final List<MotivoAnulacionTarjeta> motivos;
  final MotivoAnulacionTarjeta? motivo;
  final AnularResponse? anularResponse;
  final ConfirmarResponse? confirmarResponse;
  final String tokenDigital;

  AnulacionTarjetasState({
    this.tarjetas = const [],
    this.tarjeta,
    this.motivos = const [],
    this.motivo,
    this.anularResponse,
    this.confirmarResponse,
    this.tokenDigital = '',
  });

  AnulacionTarjetasState copyWith({
    List<TarjetaAnulacion>? tarjetas,
    List<MotivoAnulacionTarjeta>? motivos,
    ValueGetter<TarjetaAnulacion?>? tarjeta,
    ValueGetter<MotivoAnulacionTarjeta?>? motivo,
    ValueGetter<AnularResponse?>? anularResponse,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
    String? tokenDigital,
  }) =>
      AnulacionTarjetasState(
        tarjetas: tarjetas ?? this.tarjetas,
        motivos: motivos ?? this.motivos,
        tarjeta: tarjeta != null ? tarjeta() : this.tarjeta,
        motivo: motivo != null ? motivo() : this.motivo,
        anularResponse:
            anularResponse != null ? anularResponse() : this.anularResponse,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
        tokenDigital: tokenDigital ?? this.tokenDigital,
      );
}

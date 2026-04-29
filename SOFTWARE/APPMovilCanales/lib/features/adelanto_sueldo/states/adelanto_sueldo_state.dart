import 'package:caja_tacna_app/features/adelanto_sueldo/inputs/monto_adelanto.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/cuenta_destino.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/pagar_response.dart';
import 'package:flutter/material.dart';

class AdelantoSueldoState {
  final List<CuentaDestinoAdelSuel> cuentasDestino;
  final CuentaDestinoAdelSuel? cuentaDestino;
  final MontoAdelanto monto;
  final PagarResponse? pagarResponse;
  final String tokenDigital;
  final ConfirmarResponse? confirmarResponse;

  AdelantoSueldoState({
    this.cuentasDestino = const [],
    this.cuentaDestino,
    this.pagarResponse,
    this.monto = const MontoAdelanto.pure(''),
    this.tokenDigital = '',
    this.confirmarResponse,
  });

  AdelantoSueldoState copyWith({
    List<CuentaDestinoAdelSuel>? cuentasDestino,
    ValueGetter<CuentaDestinoAdelSuel?>? cuentaDestino,
    MontoAdelanto? monto,
    ValueGetter<PagarResponse?>? pagarResponse,
    String? tokenDigital,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
  }) =>
      AdelantoSueldoState(
        cuentasDestino: cuentasDestino ?? this.cuentasDestino,
        cuentaDestino:
            cuentaDestino != null ? cuentaDestino() : this.cuentaDestino,
        monto: monto ?? this.monto,
        pagarResponse:
            pagarResponse != null ? pagarResponse() : this.pagarResponse,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
      );
}

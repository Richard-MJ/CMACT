import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/monto.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_tercero.dart';
import 'package:caja_tacna_app/features/transferencias/models/confirmar_entre_cuentas_response.dart';
import 'package:caja_tacna_app/features/transferencias/models/cuenta_transferencia.dart';
import 'package:caja_tacna_app/features/transferencias/models/transferir_entre_cuentas_response.dart';
import 'package:flutter/material.dart';

class TransferenciaTercerosState {
  final List<CuentaTransferencia> cuentasOrigen;
  final CuentaTransferencia? cuentaOrigen;
  final NumeroCuentaTercero numeroCuentaDestino;
  final MontoTrans monto;
  final String tokenDigital;
  final String motivo;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final Email correoElectronicoDestinatario;
  final TransferirEntreCuentasResponse? transferirResponse;
  final ConfirmarResponseTransEntreCuentas? confirmarResponse;

  TransferenciaTercerosState({
    this.cuentasOrigen = const [],
    this.cuentaOrigen,
    this.numeroCuentaDestino = const NumeroCuentaTercero.pure(''),
    this.monto = const MontoTrans.pure(''),
    this.tokenDigital = '',
    this.motivo = '',
    this.operacionFrecuente = false,
    this.nombreOperacionFrecuente = '',
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.transferirResponse,
    this.confirmarResponse,
  });

  TransferenciaTercerosState copyWith({
    List<CuentaTransferencia>? cuentasOrigen,
    ValueGetter<CuentaTransferencia?>? cuentaOrigen,
    NumeroCuentaTercero? numeroCuentaDestino,
    MontoTrans? monto,
    String? tokenDigital,
    String? motivo,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    Email? correoElectronicoDestinatario,
    ValueGetter<TransferirEntreCuentasResponse?>? transferirResponse,
    ValueGetter<ConfirmarResponseTransEntreCuentas?>? confirmarResponse,
  }) =>
      TransferenciaTercerosState(
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        numeroCuentaDestino: numeroCuentaDestino ?? this.numeroCuentaDestino,
        monto: monto ?? this.monto,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        motivo: motivo ?? this.motivo,
        operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
        nombreOperacionFrecuente:
            nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,
        correoElectronicoDestinatario:
            correoElectronicoDestinatario ?? this.correoElectronicoDestinatario,
        transferirResponse: transferirResponse != null
            ? transferirResponse()
            : this.transferirResponse,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
      );
}

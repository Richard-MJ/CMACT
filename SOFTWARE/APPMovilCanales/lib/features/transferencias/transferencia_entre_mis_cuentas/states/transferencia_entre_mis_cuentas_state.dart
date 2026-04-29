import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/monto.dart';
import 'package:caja_tacna_app/features/transferencias/models/confirmar_entre_cuentas_response.dart';
import 'package:caja_tacna_app/features/transferencias/models/cuenta_transferencia.dart';
import 'package:caja_tacna_app/features/transferencias/models/transferir_entre_cuentas_response.dart';
import 'package:flutter/material.dart';

class TransferenciaEntreMisCuentasState {
  final List<CuentaTransferencia> cuentasOrigen;
  final List<CuentaTransferencia> cuentasDestino;
  final CuentaTransferencia? cuentaOrigen;
  final CuentaTransferencia? cuentaDestino;
  final MontoTrans monto;
  final String motivo;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final Email correoElectronicoDestinatario;
  final TransferirEntreCuentasResponse? transferirResponse;
  final ConfirmarResponseTransEntreCuentas? confirmarResponse;

  TransferenciaEntreMisCuentasState({
    this.cuentasOrigen = const [],
    this.cuentasDestino = const [],
    this.cuentaOrigen,
    this.cuentaDestino,
    this.monto = const MontoTrans.pure(''),
    this.motivo = '',
    this.operacionFrecuente = false,
    this.nombreOperacionFrecuente = '',
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.transferirResponse,
    this.confirmarResponse,
  });

  TransferenciaEntreMisCuentasState copyWith({
    List<CuentaTransferencia>? cuentasOrigen,
    List<CuentaTransferencia>? cuentasDestino,
    ValueGetter<CuentaTransferencia?>? cuentaOrigen,
    ValueGetter<CuentaTransferencia?>? cuentaDestino,
    MontoTrans? monto,
    String? motivo,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    Email? correoElectronicoDestinatario,
    ValueGetter<TransferirEntreCuentasResponse?>? transferirResponse,
    ValueGetter<ConfirmarResponseTransEntreCuentas?>? confirmarResponse,
  }) =>
      TransferenciaEntreMisCuentasState(
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        cuentasDestino: cuentasDestino ?? this.cuentasDestino,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        cuentaDestino:
            cuentaDestino != null ? cuentaDestino() : this.cuentaDestino,
        monto: monto ?? this.monto,
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

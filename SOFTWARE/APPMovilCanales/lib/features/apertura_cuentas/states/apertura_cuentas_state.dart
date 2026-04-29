import 'package:caja_tacna_app/features/apertura_cuentas/inputs/dias_dpf.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/inputs/monto.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/aperturar_response.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/calculo_dpf_response.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/cuenta_origen.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/emision_giros/models/agencia.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:flutter/material.dart';

class AperturaCuentasState {
  final List<ProductoApertura> tiposCuenta;
  final ProductoApertura? tipoCuenta;
  final List<CuentaOrigenApertura> cuentasOrigen;
  final CuentaOrigenApertura? cuentaOrigen;
  final List<Agencia> agencias;
  final Agencia? agencia;
  final MontoApertura monto;
  final String tokenDigital;
  final Email correoElectronicoDestinatario;
  final AperturarResponse? aperturarResponse;
  final ConfirmarResponse? confirmarResponse;
  final bool aceptarCartilla;
  final bool aceptarClausulas;
  final bool aceptarTdp;

  final DiasDpf diasDpf;
  final CalculoDpfResponse? calculoDpfResponse;
  final GestionTdp? gestionTdp;

  AperturaCuentasState({
    this.tiposCuenta = const [],
    this.tipoCuenta,
    this.agencias = const [],
    this.agencia,
    this.cuentasOrigen = const [],
    this.cuentaOrigen,
    this.monto = const MontoApertura.pure(''),
    this.tokenDigital = '',
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.aperturarResponse,
    this.confirmarResponse,
    this.aceptarCartilla = false,
    this.aceptarClausulas = false,
    this.aceptarTdp = false,
    this.diasDpf = const DiasDpf.pure(''),
    this.calculoDpfResponse,
    this.gestionTdp,
  });

  AperturaCuentasState copyWith({
    List<ProductoApertura>? tiposCuenta,
    ValueGetter<ProductoApertura?>? tipoCuenta,
    List<Agencia>? agencias,
    ValueGetter<Agencia?>? agencia,
    List<CuentaOrigenApertura>? cuentasOrigen,
    ValueGetter<CuentaOrigenApertura?>? cuentaOrigen,
    MontoApertura? monto,
    String? tokenDigital,
    Email? correoElectronicoDestinatario,
    ValueGetter<AperturarResponse?>? aperturarResponse,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
    bool? aceptarCartilla,
    bool? aceptarClausulas,
    bool? aceptarTdp,
    DiasDpf? diasDpf,
    ValueGetter<CalculoDpfResponse?>? calculoDpfResponse,
    ValueGetter<GestionTdp?>? gestionTdp,
  }) =>
      AperturaCuentasState(
        tiposCuenta: tiposCuenta ?? this.tiposCuenta,
        tipoCuenta: tipoCuenta != null ? tipoCuenta() : this.tipoCuenta,
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        agencias: agencias ?? this.agencias,
        agencia: agencia != null ? agencia() : this.agencia,
        monto: monto ?? this.monto,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        correoElectronicoDestinatario:
            correoElectronicoDestinatario ?? this.correoElectronicoDestinatario,
        aperturarResponse: aperturarResponse != null
            ? aperturarResponse()
            : this.aperturarResponse,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
        aceptarCartilla: aceptarCartilla ?? this.aceptarCartilla,
        aceptarClausulas: aceptarClausulas ?? this.aceptarClausulas,
        aceptarTdp: aceptarTdp ?? this.aceptarTdp,
        diasDpf: diasDpf ?? this.diasDpf,
        calculoDpfResponse: calculoDpfResponse != null
            ? calculoDpfResponse()
            : this.calculoDpfResponse,
        gestionTdp: gestionTdp != null ? gestionTdp() : this.gestionTdp,
      );
}

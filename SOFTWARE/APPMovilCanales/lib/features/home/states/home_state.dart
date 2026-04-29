import 'package:caja_tacna_app/features/credito/models/credito.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/home/models/configuracion.dart';
import 'package:caja_tacna_app/features/home/models/datos_cliente.dart';
import 'package:caja_tacna_app/features/novedades/models/obtener_publicidad_response.dart';
import 'package:flutter/material.dart';

class HomeState {
  final List<CuentaAhorro> cuentasAhorro;
  final bool loadingCuentasAhorro;
  final bool loadingCreditos;
  final bool loadingTipoCambio;
  final bool loadingDatosCliente;
  final bool mostrarSaldo;

  final List<Credito> creditos;
  final double tipoCambioCompra;
  final double tipoCambioVenta;
  final DatosCliente? datosCliente;
  final Configuracion? configuracion;
  final bool esAfiliadoBilleteraVirtual;

  List<Publicidad> publicidades;

  HomeState({
    this.cuentasAhorro = const [],
    this.creditos = const [],
    this.tipoCambioCompra = 0.00,
    this.tipoCambioVenta = 0.00,
    this.datosCliente,
    this.loadingCuentasAhorro = false,
    this.loadingCreditos = false,
    this.loadingTipoCambio = false,
    this.loadingDatosCliente = false,
    this.publicidades = const [],
    this.mostrarSaldo = false,
    this.configuracion,
    this.esAfiliadoBilleteraVirtual = false,
  });

  HomeState copyWith({
    List<CuentaAhorro>? cuentasAhorro,
    List<Credito>? creditos,
    double? tipoCambioCompra,
    double? tipoCambioVenta,
    ValueGetter<DatosCliente?>? datosCliente,
    bool? loadingCuentasAhorro,
    bool? loadingCreditos,
    bool? loadingTipoCambio,
    bool? loadingDatosCliente,
    List<Publicidad>? publicidades,
    bool? mostrarSaldo,
    ValueGetter<Configuracion?>? configuracion,
    bool? esAfiliadoBilleteraVirtual
  }) =>
      HomeState(
        cuentasAhorro: cuentasAhorro ?? this.cuentasAhorro,
        creditos: creditos ?? this.creditos,
        tipoCambioCompra: tipoCambioCompra ?? this.tipoCambioCompra,
        tipoCambioVenta: tipoCambioVenta ?? this.tipoCambioVenta,
        datosCliente: datosCliente != null ? datosCliente() : this.datosCliente,
        loadingCuentasAhorro: loadingCuentasAhorro ?? this.loadingCuentasAhorro,
        loadingCreditos: loadingCreditos ?? this.loadingCreditos,
        loadingTipoCambio: loadingTipoCambio ?? this.loadingTipoCambio,
        loadingDatosCliente: loadingDatosCliente ?? this.loadingDatosCliente,
        publicidades: publicidades ?? this.publicidades,
        mostrarSaldo: mostrarSaldo ?? this.mostrarSaldo,
        configuracion:
            configuracion != null ? configuracion() : this.configuracion,
        esAfiliadoBilleteraVirtual: esAfiliadoBilleteraVirtual ?? this.esAfiliadoBilleteraVirtual,
      );
}

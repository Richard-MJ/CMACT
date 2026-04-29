import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/configuracion_notificacion_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/datos_afiliacion_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/billetera_virtual/models/token_response.dart';
import 'package:flutter/material.dart';

class AfiliacionCelularState {
  final NumeroCelular numeroCelular;
  final String tokenDigital;
  final TokenResponse? tokenResponse;
  final ConfirmarResponse? confirmacionResponse;
  final DatosAfiliacionResponse? datosAfiliacion;
  final bool esAfiliada;
  final bool esAfiliadaSimple;
  final bool notificarOperacionesEnviadas;
  final bool notificarOperacionesRecibidas;
  final bool esModificacion;
  final ConfiguracionNotificacionResponse? configuracionNotificacionResponse;

  AfiliacionCelularState(
      {this.tokenDigital = '',
      this.datosAfiliacion,
      this.numeroCelular = const NumeroCelular.pure(''),
      this.confirmacionResponse,
      this.tokenResponse,
      this.esAfiliada = false,
      this.esAfiliadaSimple = false,
      this.notificarOperacionesEnviadas = false,
      this.notificarOperacionesRecibidas = false,
      this.esModificacion = false,
      this.configuracionNotificacionResponse});

  AfiliacionCelularState copyWith({
    NumeroCelular? numeroCelular,
    String? tokenDigital,
    bool? esAfiliada,
    bool? esAfiliadaSimple,
    ConfirmarResponse? confirmacionResponse,
    ValueGetter<DatosAfiliacionResponse?>? datosAfiliacion,
    ValueGetter<TokenResponse?>? tokenResponse,
    bool? notificarOperacionesEnviadas,
    bool? notificarOperacionesRecibidas,
    bool? esModificacion,
    ConfiguracionNotificacionResponse? configuracionNotificacionResponse,
  }) =>
      AfiliacionCelularState(
        numeroCelular: numeroCelular ?? this.numeroCelular,
        confirmacionResponse: confirmacionResponse ?? this.confirmacionResponse,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        esAfiliada: esAfiliada ?? this.esAfiliada,
        esAfiliadaSimple: esAfiliadaSimple ?? this.esAfiliadaSimple,
        tokenResponse:
            tokenResponse != null ? tokenResponse() : this.tokenResponse,
        datosAfiliacion:
            datosAfiliacion != null ? datosAfiliacion() : this.datosAfiliacion,
        notificarOperacionesEnviadas:
            notificarOperacionesEnviadas ?? this.notificarOperacionesEnviadas,
        notificarOperacionesRecibidas:
            notificarOperacionesRecibidas ?? this.notificarOperacionesRecibidas,
        esModificacion: esModificacion ?? this.esModificacion,
        configuracionNotificacionResponse: configuracionNotificacionResponse ?? this.configuracionNotificacionResponse,
      );

  String get tituloOperacion {
    if (esAfiliada && esModificacion && datosAfiliacion!.indicadorAfiliacionCCE) return 'Configuración de notificaciones';
    if (esAfiliada) return 'Afiliación a\nBilletera Virtual';
    return 'Desafiliación a\nBilletera Virtual';
  }
}

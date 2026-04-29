import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_intenet.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_tarjeta.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/models/enviar_sms_response.dart';
import 'package:caja_tacna_app/features/home/models/configuracion.dart';
import 'package:caja_tacna_app/features/token_digital/models/obtener_dispositivo_response.dart';
import 'package:caja_tacna_app/features/token_digital/models/obtener_token_response.dart';
import 'package:flutter/material.dart';

class TokenDigitalState {
  final ClaveTarjeta claveCajero;
  final ClaveInternet claveInternet;
  final ObtenerDispositivoResponse? dispositivoAfiliado;
  final ObtenerDispositivoResponse? afiliarResponse;
  final ObtenerDispositivoResponse? desafiliarResponse;
  final bool aceptarTerminos;
  final ObtenerTokenResponse? obtenerTokenResponse;
  final bool esDispositivoAfiliado;
  final int pasoActualRestablecer;
  final String claveSms;
  final EnviarSmsResponse? enviarSmsResponse;
  final bool cargandoSms;
  final EnlaceDocumento? documentoTermino;

  TokenDigitalState({
    this.dispositivoAfiliado,
    this.afiliarResponse,
    this.desafiliarResponse,
    this.claveCajero = const ClaveTarjeta.pure(''),
    this.claveInternet = const ClaveInternet.pure(''),
    this.aceptarTerminos = false,
    this.obtenerTokenResponse,
    this.esDispositivoAfiliado = false,
    this.pasoActualRestablecer = 1,
    this.claveSms = '',
    this.enviarSmsResponse,
    this.cargandoSms = false,
    this.documentoTermino,  
  });

  TokenDigitalState copyWith({
    ValueGetter<ObtenerDispositivoResponse?>? dispositivoAfiliado,
    ValueGetter<ObtenerDispositivoResponse?>? afiliarResponse,
    ValueGetter<ObtenerDispositivoResponse?>? desafiliarResponse,
    ClaveTarjeta? claveCajero,
    ClaveInternet? claveInternet,
    bool? aceptarTerminos,
    ValueGetter<ObtenerTokenResponse?>? obtenerTokenResponse,
    bool? esDispositivoAfiliado,
    int? pasoActualRestablecer,
    String? claveSms,
    ValueGetter<EnviarSmsResponse?>? enviarSmsResponse,
    bool? cargandoSms,
    ValueGetter<EnlaceDocumento?>? documentoTermino,  
    
  }) =>
      TokenDigitalState(
        dispositivoAfiliado: dispositivoAfiliado != null
            ? dispositivoAfiliado()
            : this.dispositivoAfiliado,
        afiliarResponse:
            afiliarResponse != null ? afiliarResponse() : this.afiliarResponse,
        desafiliarResponse: desafiliarResponse != null
            ? desafiliarResponse()
            : this.desafiliarResponse,
        claveCajero: claveCajero ?? this.claveCajero,
        claveInternet: claveInternet ?? this.claveInternet,
        documentoTermino: documentoTermino != null ? documentoTermino() : this.documentoTermino,
        aceptarTerminos: aceptarTerminos ?? this.aceptarTerminos,
        obtenerTokenResponse: obtenerTokenResponse != null
            ? obtenerTokenResponse()
            : this.obtenerTokenResponse,
        esDispositivoAfiliado:
            esDispositivoAfiliado ?? this.esDispositivoAfiliado,
        pasoActualRestablecer:
            pasoActualRestablecer ?? this.pasoActualRestablecer,
        claveSms: claveSms ?? this.claveSms,
        enviarSmsResponse: enviarSmsResponse != null
            ? enviarSmsResponse()
            : this.enviarSmsResponse,
        cargandoSms: cargandoSms ?? this.cargandoSms,
      );
}

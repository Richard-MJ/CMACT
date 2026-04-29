import 'package:caja_tacna_app/features/perfil/inputs/apodo.dart';
import 'package:caja_tacna_app/features/perfil/inputs/email.dart';
import 'package:caja_tacna_app/features/perfil/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/perfil/models/actualizar_response.dart';
import 'package:caja_tacna_app/features/perfil/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/perfil/models/obtener_datos_response.dart';
import 'package:flutter/material.dart';

class PerfilState {
  final String tokenDigital;
  final Apodo apodo;
  final String apodoLocal;
  final NumeroCelular numeroCelular;
  final Email email;
  final ObtenerDatosResponse? datosIniciales;

  final ActualizarResponse? actualizarResponse;
  final ConfirmarResponse? confirmarResponse;

  final bool tieneEmail;

  PerfilState({
    this.numeroCelular = const NumeroCelular.pure(''),
    this.tokenDigital = '',
    this.apodo = const Apodo.pure(''),
    this.email = const Email.pure(''),
    this.apodoLocal = '',
    this.datosIniciales,
    this.actualizarResponse,
    this.confirmarResponse,
    this.tieneEmail = false,
  });

  get btnDisabled {
    if (!tieneEmail) {
      // Si no tiene email, solo se habilita si ingresa un email válido
      return !(email.value.isNotEmpty && email.isValid);
    } else {
      // Si ya tiene email, se habilita si alguno de los campos es válido/no vacío
      return !(apodo.value.isNotEmpty ||
          (email.value.isNotEmpty && email.isValid) ||
          numeroCelular.value.isNotEmpty);
    }
  }

  PerfilState copyWith({
    NumeroCelular? numeroCelular,
    String? tokenDigital,
    Apodo? apodo,
    ValueGetter<ObtenerDatosResponse?>? datosIniciales,
    Email? email,
    String? apodoLocal,
    ValueGetter<ActualizarResponse?>? actualizarResponse,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
    bool? tieneEmail,
  }) =>
      PerfilState(
        numeroCelular: numeroCelular ?? this.numeroCelular,
        apodo: apodo ?? this.apodo,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        datosIniciales:
            datosIniciales != null ? datosIniciales() : this.datosIniciales,
        email: email ?? this.email,
        actualizarResponse: actualizarResponse != null
            ? actualizarResponse()
            : this.actualizarResponse,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
        apodoLocal: apodoLocal ?? this.apodoLocal,
        tieneEmail: tieneEmail ?? this.tieneEmail,
      );
}

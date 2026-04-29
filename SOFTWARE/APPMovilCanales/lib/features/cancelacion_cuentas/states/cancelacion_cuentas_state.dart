import 'package:caja_tacna_app/features/cancelacion_cuentas/models/cancelar_saldo_cero.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/cancelar_trans_interbancaria_response.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/cancelar_trans_interna_response.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/confirmar_saldo_cero.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/confirmar_trans_interbancaria.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/confirmar_trans_interna_response.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/cuenta_tercero.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/tipo_transferencia.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/cuenta_destino.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/shared/inputs/nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_documento.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_cci.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_tercero.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/tipo_documento.dart';
import 'package:flutter/material.dart';

class CancelacionCuentasState {
  final CuentaAhorro? cuentaAhorro;
  final TipoTransferencia? tipoTransferencia;
  final String tokenDigital;

  //transf propia
  final List<CuentaDestinoCancCuenta> cuentasDestinoPropias;
  final CuentaDestinoCancCuenta? cuentaDestinoPropia;

  //transf tercero
  final NumeroCuentaTercero numeroCuentaTercero;
  final CuentaTercero? cuentaTercero;

  //transf propia y tercero
  final CancelarTransInternaResponse? cancelarInternaResponse;
  final ConfirmarTransInternaResponse? confirmarInternaResponse;

  //saldo cero
  final CancelarSaldoCero? cancelarSaldoCeroResponse;
  final ConfirmarSaldoCero? confirmarSaldoCeroResponse;

  //transf interbancaria
  final List<TipoDocumentoTransInter> tiposDocumento;
  final NumeroCuentaCci cuentaDestinoCci;
  final NombreBeneficiario nombreBeneficiario;
  final TipoDocumentoTransInter? tipoDocumento;
  final NumeroDocumento numeroDocumento;
  final bool esTitular;
  final CancelarTransInterbancariaResponse? cancelarInterbancariaResponse;
  final ConfirmarTransInterbancariaResponse? confirmarInterbancariaResponse;

  CancelacionCuentasState({
    this.cuentaAhorro,
    this.cuentasDestinoPropias = const [],
    this.numeroCuentaTercero = const NumeroCuentaTercero.pure(''),
    this.tipoTransferencia,
    this.cuentaDestinoPropia,
    this.cancelarInternaResponse,
    this.confirmarInternaResponse,
    this.tokenDigital = '',
    this.cuentaTercero,

    //saldo cero
    this.cancelarSaldoCeroResponse,
    this.confirmarSaldoCeroResponse,

    //transf interbancaria
    this.tiposDocumento = const [],
    this.cuentaDestinoCci = const NumeroCuentaCci.pure(''),
    this.nombreBeneficiario = const NombreBeneficiario.pure(''),
    this.tipoDocumento,
    this.numeroDocumento = const NumeroDocumento.pure(''),
    this.esTitular = false,
    this.cancelarInterbancariaResponse,
    this.confirmarInterbancariaResponse,
  });

  CancelacionCuentasState copyWith({
    ValueGetter<CuentaAhorro?>? cuentaAhorro,
    List<CuentaDestinoCancCuenta>? cuentasDestinoPropias,
    ValueGetter<TipoTransferencia?>? tipoTransferencia,
    ValueGetter<CuentaDestinoCancCuenta?>? cuentaDestinoPropia,
    ValueGetter<CancelarTransInternaResponse?>? cancelarInternaResponse,
    ValueGetter<ConfirmarTransInternaResponse?>? confirmarInternaResponse,
    String? tokenDigital,
    NumeroCuentaTercero? numeroCuentaTercero,
    ValueGetter<CuentaTercero?>? cuentaTercero,

    //saldo cero
    ValueGetter<CancelarSaldoCero?>? cancelarSaldoCeroResponse,
    ValueGetter<ConfirmarSaldoCero?>? confirmarSaldoCeroResponse,

    //transf interbancaria
    List<TipoDocumentoTransInter>? tiposDocumento,
    NumeroCuentaCci? cuentaDestinoCci,
    NombreBeneficiario? nombreBeneficiario,
    ValueGetter<TipoDocumentoTransInter?>? tipoDocumento,
    NumeroDocumento? numeroDocumento,
    bool? esTitular,
    ValueGetter<CancelarTransInterbancariaResponse?>?
        cancelarInterbancariaResponse,
    ValueGetter<ConfirmarTransInterbancariaResponse?>?
        confirmarInterbancariaResponse,
  }) =>
      CancelacionCuentasState(
        cuentaAhorro: cuentaAhorro != null ? cuentaAhorro() : this.cuentaAhorro,
        cuentasDestinoPropias:
            cuentasDestinoPropias ?? this.cuentasDestinoPropias,
        tipoTransferencia: tipoTransferencia != null
            ? tipoTransferencia()
            : this.tipoTransferencia,
        cuentaDestinoPropia: cuentaDestinoPropia != null
            ? cuentaDestinoPropia()
            : this.cuentaDestinoPropia,
        cancelarInternaResponse: cancelarInternaResponse != null
            ? cancelarInternaResponse()
            : this.cancelarInternaResponse,
        confirmarInternaResponse: confirmarInternaResponse != null
            ? confirmarInternaResponse()
            : this.confirmarInternaResponse,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        numeroCuentaTercero: numeroCuentaTercero ?? this.numeroCuentaTercero,
        cuentaTercero:
            cuentaTercero != null ? cuentaTercero() : this.cuentaTercero,
        cancelarSaldoCeroResponse: cancelarSaldoCeroResponse != null
            ? cancelarSaldoCeroResponse()
            : this.cancelarSaldoCeroResponse,
        confirmarSaldoCeroResponse: confirmarSaldoCeroResponse != null
            ? confirmarSaldoCeroResponse()
            : this.confirmarSaldoCeroResponse,
        tiposDocumento: tiposDocumento ?? this.tiposDocumento,
        nombreBeneficiario: nombreBeneficiario ?? this.nombreBeneficiario,
        tipoDocumento:
            tipoDocumento != null ? tipoDocumento() : this.tipoDocumento,
        numeroDocumento: numeroDocumento ?? this.numeroDocumento,
        esTitular: esTitular ?? this.esTitular,
        cancelarInterbancariaResponse: cancelarInterbancariaResponse != null
            ? cancelarInterbancariaResponse()
            : this.cancelarInterbancariaResponse,
        confirmarInterbancariaResponse: confirmarInterbancariaResponse != null
            ? confirmarInterbancariaResponse()
            : this.confirmarInterbancariaResponse,
        cuentaDestinoCci: cuentaDestinoCci ?? this.cuentaDestinoCci,
      );
}

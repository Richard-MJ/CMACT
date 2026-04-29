

import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/models/tipo_documento.dart';
import 'package:caja_tacna_app/features/shared/models/select_cuenta_option.dart';
import 'package:caja_tacna_app/features/shared/models/select_entidad_financiera_option.dart';

class DatosInicialesResponse {
    final List<CuentaEfectivo> productosDebito;
    final List<TipoDocumentoTransInter> tiposDocumentos;
    final List<EntidadFinanciera> entidadesFinancieras;
    final List<LimitesTransferencias> limitesTransferencias;

    DatosInicialesResponse({
      required this.productosDebito,
      required this.tiposDocumentos,
      required this.entidadesFinancieras,
      required this.limitesTransferencias,
    });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productosDebito: List<CuentaEfectivo>.from(json["ProductosDebito"]
            .map((x) => CuentaEfectivo.fromJson(x))),
        tiposDocumentos: List<TipoDocumentoTransInter>.from(json["TiposDocumentos"]
            .map((x) => TipoDocumentoTransInter.fromJson(x))),
        entidadesFinancieras: List<EntidadFinanciera>.from(json["EntidadesFinancieras"]
            .map((x) => EntidadFinanciera.fromJson(x))),
        limitesTransferencias: List<LimitesTransferencias>.from(json["LimitesTransferencias"]
            .map((x) => LimitesTransferencias.fromJson(x)))
      );

  Map<String, dynamic> toJson() => {
    "ProductosDebito": List<dynamic>.from(productosDebito.map((x) => x.toJson())),
    "TiposDocumentos": List<dynamic>.from(tiposDocumentos.map((x) => x.toJson())),
    "EntidadesFinancieras": List<dynamic>.from(entidadesFinancieras.map((x) => x.toJson())),
    "LimitesTransferencias": List<dynamic>.from(limitesTransferencias.map((x) => x.toJson())),
  };
}

class CuentaEfectivo extends SelectCuentaOption {
    String titular;
    String tipoProductoInterno;
    String moneda;
    String tipoCuentaTitular;
    bool exoneradoImpuestos;
    String codigoCliente;
    bool indicadorCuentaSueldo;
    int montoRemunerativo;
    int montoNoremunerativo;
    String codigoProducto;
    bool esExoneradoCobroComisiones;
    String codigoMoneda;
    String codigoCuentaInterbancario;
    String indicadorTipoCuenta;
    String nombres;
    String apellidoMaterno;
    String apellidoPaterno;
    bool esMismoFirmante;
    String indicadorTransferenciaCce;
    String numeroDocumento;
    double montoMinimo;
    bool esExoneradaItf;
    bool esCuentaSueldo;

    CuentaEfectivo({
      required String numeroCuenta,
      required this.titular,
      required this.tipoProductoInterno,
      required this.moneda,
      required this.tipoCuentaTitular,
      required this.exoneradoImpuestos,
      required this.codigoCliente,
      required this.indicadorCuentaSueldo,
      required this.montoRemunerativo,
      required this.montoNoremunerativo,
      required this.codigoProducto,
      required this.esExoneradoCobroComisiones,
      required super.alias,
      required this.codigoMoneda,
      required super.simboloMonedaProducto,
      required this.codigoCuentaInterbancario,
      required this.indicadorTipoCuenta,
      required this.nombres,
      required this.apellidoMaterno,
      required this.apellidoPaterno,
      required this.esMismoFirmante,
      required this.indicadorTransferenciaCce,
      required double saldoDisponible,
      required this.numeroDocumento,
      required this.montoMinimo,
      required this.esExoneradaItf,
      required this.esCuentaSueldo,
    }) : super(
          numeroProducto: numeroCuenta,
          montoSaldo: saldoDisponible,
        );

  factory CuentaEfectivo.fromJson(Map<String, dynamic> json) =>
      CuentaEfectivo(
        numeroCuenta: json["NumeroCuenta"],
        titular: json["Titular"],
        tipoProductoInterno: json["TipoProductoInterno"],
        moneda: json["Moneda"],
        tipoCuentaTitular: json["TipoCuentaTitular"],
        exoneradoImpuestos: json["ExoneradoImpuestos"],
        codigoCliente: json["CodigoCliente"],
        indicadorCuentaSueldo: json["IndicadorCuentaSueldo"],
        montoRemunerativo: json["MontoRemunerativo"],
        montoNoremunerativo: json["MontoNoremunerativo"],
        codigoProducto: json["CodigoProducto"],
        esExoneradoCobroComisiones: json["EsExoneradoCobroComisiones"],
        alias: json["Alias"],
        codigoMoneda: json["CodigoMoneda"],
        simboloMonedaProducto: json["SimboloMonedaProducto"],
        codigoCuentaInterbancario: json["CodigoCuentaInterbancario"],
        indicadorTipoCuenta: json["IndicadorTipoCuenta"],
        nombres: json["Nombres"],
        apellidoMaterno: json["ApellidoMaterno"],
        apellidoPaterno: json["ApellidoPaterno"],
        esMismoFirmante: json["EsMismoFirmante"],
        indicadorTransferenciaCce: json["IndicadorTransferenciaCce"],
        saldoDisponible: json["SaldoDisponible"]?.toDouble(),
        numeroDocumento: json["NumeroDocumento"],
        montoMinimo: json["MontoMinimo"],
        esExoneradaItf: json["EsExoneradaITF"],
        esCuentaSueldo: json["EsCuentaSueldo"]
      );

  Map<String, dynamic> toJson() => {
    "NumeroCuenta": numeroProducto,
    "Titular": titular,
    "TipoProductoInterno": tipoProductoInterno,
    "Moneda": moneda,
    "TipoCuentaTitular": tipoCuentaTitular,
    "ExoneradoImpuestos": exoneradoImpuestos,
    "CodigoCliente": codigoCliente,
    "IndicadorCuentaSueldo": indicadorCuentaSueldo,
    "MontoRemunerativo": montoRemunerativo,
    "MontoNoremunerativo": montoNoremunerativo,
    "CodigoProducto": codigoProducto,
    "EsExoneradoCobroComisiones": esExoneradoCobroComisiones,
    "NombreProducto": alias,
    "CodigoMoneda": codigoMoneda,
    "SimboloMonedaProducto": simboloMonedaProducto,
    "CodigoCuentaInterbancario": codigoCuentaInterbancario,
    "IndicadorTipoCuenta": indicadorTipoCuenta,
    "Nombres": nombres,
    "ApellidoMaterno": apellidoMaterno,
    "ApellidoPaterno": apellidoPaterno,
    "EsMismoFirmante": esMismoFirmante,
    "IndicadorTransferenciaCce": indicadorTransferenciaCce,
    "SaldoDisponible": montoSaldo,
    "NumeroDocumento": numeroDocumento,
    "MontoMinimo": montoMinimo,
    "EsExoneradaITF": esExoneradaItf,
    "EsCuentaSueldo": esCuentaSueldo
    };
}

class EntidadFinanciera extends SelectEntidadFinancieraOption  {
  final String codigoEntidad;
  final String oficinaPagoTarjeta;

  EntidadFinanciera({
    required super.idEntidadCce,
    required this.codigoEntidad,
    required super.nombreEntidadCce,
    required this.oficinaPagoTarjeta
  }) : super();

factory EntidadFinanciera.fromJson(Map<String, dynamic> json) {
  return EntidadFinanciera(
    idEntidadCce: json["IdEntidad"],
    codigoEntidad: json["CodigoEntidad"],
    nombreEntidadCce: json["NombreEntidad"],
    oficinaPagoTarjeta: json["OficinaPagoTarjeta"],
  );
}

  Map<String, dynamic> toJson() => {
      "IdEntidad": idEntidadCce,
      "CodigoEntidad": codigoEntidad,
      "NombreEntidad": nombreEntidadCce,
      "OficinaPagoTarjeta": oficinaPagoTarjeta
    };
}

class LimitesTransferencias {
  final String codigoTipoTransferencia;
  final String codigoMoneda;
  final double montoMaximo;
  final double montoMinimo;

  LimitesTransferencias({
    required this.codigoTipoTransferencia,
    required this.codigoMoneda,
    required this.montoMaximo,
    required this.montoMinimo,
  });

  factory LimitesTransferencias.fromJson(Map<String, dynamic> json) =>
      LimitesTransferencias(
        codigoTipoTransferencia: json["CodigoTipoTransferencia"],
        codigoMoneda: json["CodigoMoneda"],
        montoMaximo: json["MontoMaximo"]?.toDouble(),
        montoMinimo: json["MontoMinimo"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "CodigoTipoTransferencia": codigoTipoTransferencia,
        "CodigoMoneda": codigoMoneda,
        "MontoMaximo": montoMaximo,
        "MontoMinimo": montoMinimo,
      };
}
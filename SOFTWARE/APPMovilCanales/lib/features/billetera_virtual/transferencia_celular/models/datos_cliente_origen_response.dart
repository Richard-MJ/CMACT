import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/tipo_documento.dart';

class DatosClienteOrigenResponse {
    CuentaEfectivo cuentaEfectivo;

    DatosClienteOrigenResponse({
      required this.cuentaEfectivo,
    });

  factory DatosClienteOrigenResponse.fromJson(Map<String, dynamic> json) =>
      DatosClienteOrigenResponse(
        cuentaEfectivo: CuentaEfectivo.fromJson(json["CuentaEfectivo"]) 
      );

  Map<String, dynamic> toJson() => {
        "CuentaEfectivo": cuentaEfectivo.toJson(),
      };
}

class CuentaEfectivo {
    String numeroCuenta;
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
    String nombreProducto;
    String codigoMoneda;
    String codigoCuentaInterbancario;
    String indicadorTipoCuenta;
    String nombres;
    String apellidoMaterno;
    String apellidoPaterno;
    bool esMismoFirmante;
    String indicadorTransferenciaCce;
    double saldoDisponible;
    String numeroDocumento;
    TipoDocumento tipoDocumentoOriginante;
    double montoMinimo;
    bool esExoneradaItf;
    bool esCuentaSueldo;

    CuentaEfectivo({
      required this.numeroCuenta,
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
      required this.nombreProducto,
      required this.codigoMoneda,
      required this.codigoCuentaInterbancario,
      required this.indicadorTipoCuenta,
      required this.nombres,
      required this.apellidoMaterno,
      required this.apellidoPaterno,
      required this.esMismoFirmante,
      required this.indicadorTransferenciaCce,
      required this.saldoDisponible,
      required this.numeroDocumento,
      required this.tipoDocumentoOriginante,
      required this.montoMinimo,
      required this.esExoneradaItf,
      required this.esCuentaSueldo,
    });

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
        nombreProducto: json["NombreProducto"],
        codigoMoneda: json["CodigoMoneda"],
        codigoCuentaInterbancario: json["CodigoCuentaInterbancario"],
        indicadorTipoCuenta: json["IndicadorTipoCuenta"],
        nombres: json["Nombres"],
        apellidoMaterno: json["ApellidoMaterno"],
        apellidoPaterno: json["ApellidoPaterno"],
        esMismoFirmante: json["EsMismoFirmante"],
        indicadorTransferenciaCce: json["IndicadorTransferenciaCce"],
        saldoDisponible: json["SaldoDisponible"],
        numeroDocumento: json["NumeroDocumento"],
        tipoDocumentoOriginante: TipoDocumento.fromJson(json["TipoDocumentoOriginante"]),
        montoMinimo: json["MontoMinimo"],
        esExoneradaItf: json["EsExoneradaITF"],
        esCuentaSueldo: json["EsCuentaSueldo"]
      );

  Map<String, dynamic> toJson() => {
    "NumeroCuenta": numeroCuenta,
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
    "NombreProducto": nombreProducto,
    "CodigoMoneda": codigoMoneda,
    "CodigoCuentaInterbancario": codigoCuentaInterbancario,
    "IndicadorTipoCuenta": indicadorTipoCuenta,
    "Nombres": nombres,
    "ApellidoMaterno": apellidoMaterno,
    "ApellidoPaterno": apellidoPaterno,
    "EsMismoFirmante": esMismoFirmante,
    "IndicadorTransferenciaCce": indicadorTransferenciaCce,
    "SaldoDisponible": saldoDisponible,
    "NumeroDocumento": numeroDocumento,
    "TipoDocumentoOriginante": tipoDocumentoOriginante.toJson(),
    "MontoMinimo": montoMinimo,
    "EsExoneradaITF": esExoneradaItf,
    "EsCuentaSueldo": esCuentaSueldo
    };
}
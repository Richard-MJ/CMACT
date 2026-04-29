import 'package:caja_tacna_app/features/shared/models/monto_real_tipo_cambio.dart';

class TransferirEntreCuentasResponse {
  final String descripcionOperacion;
  final Cuenta cuentaOrigen;
  final Cuenta cuentaDestino;
  final double montoTransferencia;
  final double montoItf;
  final bool esPropia;
  final DatosAutorizacion? datosAutorizacion;
  final MontoRealTipoCambio montoReal;

  TransferirEntreCuentasResponse({
    required this.descripcionOperacion,
    required this.cuentaOrigen,
    required this.cuentaDestino,
    required this.montoTransferencia,
    required this.montoItf,
    required this.esPropia,
    required this.datosAutorizacion,
    required this.montoReal,
  });

  factory TransferirEntreCuentasResponse.fromJson(Map<String, dynamic> json) =>
      TransferirEntreCuentasResponse(
        descripcionOperacion: json["DescripcionOperacion"],
        cuentaOrigen: Cuenta.fromJson(json["CuentaOrigen"]),
        cuentaDestino: Cuenta.fromJson(json["CuentaDestino"]),
        montoTransferencia: json["MontoTransferencia"]?.toDouble(),
        montoItf: json["MontoItf"]?.toDouble(),
        esPropia: json["EsPropia"],
        datosAutorizacion: json["DatosAutorizacion"] != null
            ? DatosAutorizacion.fromJson(json["DatosAutorizacion"])
            : null,
        montoReal: MontoRealTipoCambio.fromJson(json["MontoReal"] ?? {}),
      );

  Map<String, dynamic> toJson() => {
        "DescripcionOperacion": descripcionOperacion,
        "CuentaOrigen": cuentaOrigen.toJson(),
        "CuentaDestino": cuentaDestino.toJson(),
        "MontoTransferencia": montoTransferencia,
        "MontoItf": montoItf,
        "EsPropia": esPropia,
        "DatosAutorizacion": datosAutorizacion?.toJson(),
        "MontoReal": montoReal,
      };
}

class Cuenta {
  final String nombreCliente;
  final String codigoSistema;
  final String alias;
  final String numeroProducto;
  final String simboloMonedaProducto;
  final String nombreMonedaProducto;
  final double montoSaldo;
  final double saldoContable;
  final String codigoMonedaProducto;
  final String descripcionMonto;
  final String nombreTipoProducto;
  final String nombreTipoProductoCorto;
  final double montoCuota;
  final DateTime fechaCuotaVigente;
  final double tea;

  Cuenta({
    required this.nombreCliente,
    required this.codigoSistema,
    required this.alias,
    required this.numeroProducto,
    required this.simboloMonedaProducto,
    required this.nombreMonedaProducto,
    required this.montoSaldo,
    required this.saldoContable,
    required this.codigoMonedaProducto,
    required this.descripcionMonto,
    required this.nombreTipoProducto,
    required this.nombreTipoProductoCorto,
    required this.montoCuota,
    required this.fechaCuotaVigente,
    required this.tea,
  });

  factory Cuenta.fromJson(Map<String, dynamic> json) => Cuenta(
        nombreCliente: json["NombreCliente"],
        codigoSistema: json["CodigoSistema"],
        alias: json["Alias"],
        numeroProducto: json["NumeroProducto"],
        simboloMonedaProducto: json["SimboloMonedaProducto"],
        nombreMonedaProducto: json["NombreMonedaProducto"],
        montoSaldo: json["MontoSaldo"]?.toDouble(),
        saldoContable: json["SaldoContable"]?.toDouble(),
        codigoMonedaProducto: json["CodigoMonedaProducto"],
        descripcionMonto: json["DescripcionMonto"],
        nombreTipoProducto: json["NombreTipoProducto"],
        nombreTipoProductoCorto: json["NombreTipoProductoCorto"],
        montoCuota: json["MontoCuota"]?.toDouble(),
        fechaCuotaVigente: DateTime.parse(json["FechaCuotaVigente"]),
        tea: json["TEA"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "NombreCliente": nombreCliente,
        "CodigoSistema": codigoSistema,
        "Alias": alias,
        "NumeroProducto": numeroProducto,
        "SimboloMonedaProducto": simboloMonedaProducto,
        "NombreMonedaProducto": nombreMonedaProducto,
        "MontoSaldo": montoSaldo,
        "SaldoContable": saldoContable,
        "CodigoMonedaProducto": codigoMonedaProducto,
        "DescripcionMonto": descripcionMonto,
        "NombreTipoProducto": nombreTipoProducto,
        "NombreTipoProductoCorto": nombreTipoProductoCorto,
        "MontoCuota": montoCuota,
        "FechaCuotaVigente": fechaCuotaVigente.toIso8601String(),
        "TEA": tea,
      };
}

class DatosAutorizacion {
  final int idVerificacion;
  final String codigoSolicitado;
  final DateTime fechaSistema;
  final DateTime fechaVencimiento;

  DatosAutorizacion({
    required this.idVerificacion,
    required this.codigoSolicitado,
    required this.fechaSistema,
    required this.fechaVencimiento,
  });

  factory DatosAutorizacion.fromJson(Map<String, dynamic> json) =>
      DatosAutorizacion(
        idVerificacion: json["IdVerificacion"],
        codigoSolicitado: json["CodigoSolicitado"],
        fechaSistema: DateTime.parse(json["FechaSistema"]),
        fechaVencimiento: DateTime.parse(json["FechaVencimiento"]),
      );

  Map<String, dynamic> toJson() => {
        "IdVerificacion": idVerificacion,
        "CodigoSolicitado": codigoSolicitado,
        "FechaSistema": fechaSistema.toIso8601String(),
        "FechaVencimiento": fechaVencimiento.toIso8601String(),
      };
}

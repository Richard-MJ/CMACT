class DatosValidacionResponse { 
  final bool indicadorComprasPorInternet;
  DatosAfiliacionResponse? datosAfiliacion;

  DatosValidacionResponse({
    required this.indicadorComprasPorInternet,
    this.datosAfiliacion,
  });

  factory DatosValidacionResponse.fromJson(Map<String, dynamic> json) => 
    DatosValidacionResponse(
        indicadorComprasPorInternet: json["IndicadorComprasPorInternet"],
        datosAfiliacion: json["DatosValidacion"] != null
          ? DatosAfiliacionResponse.fromJson(json["DatosValidacion"]) : null 
      );

  Map<String, dynamic> toJson() => {
        "IndicadorComprasPorInternet": indicadorComprasPorInternet,
        "DatosValidacion": datosAfiliacion?.toJson(),
      };
}

class DatosAfiliacionResponse {
  final String numeroCuentaAfiliada;
  final String numeroCelular;
  final String? tipoOperacion;
  final String? codigoCuentaInterbancario;
  final String? codigoEntidadOriginante;
  final String? nombreProducto;
  final String? numeroTarjeta;
  final String? codigoMonedaCuenta;
  String? codigoAutorizacion;
  final String? codigoCliente;
  final String simboloMoneda;
  String? cadenaHashQR;
  final String nombreAlias;
  final String canal;
  final double saldoDisponible; 
  final bool indicadorAfiliacionCCE;    
  final bool notificarOperacionesEnviadas;
  final bool notificarOperacionesRecibidas;

  DatosAfiliacionResponse({
    required this.numeroCuentaAfiliada,
    required this.numeroCelular,
    required this.tipoOperacion,
    required this.codigoCuentaInterbancario,
    required this.codigoEntidadOriginante,
    required this.nombreProducto,
    required this.numeroTarjeta,
    required this.codigoMonedaCuenta,
    required this.codigoCliente,
    required this.simboloMoneda,
    this.cadenaHashQR,
    required this.nombreAlias,
    required this.canal,
    required this.saldoDisponible,
    required this.indicadorAfiliacionCCE,
    required this.notificarOperacionesEnviadas,
    required this.notificarOperacionesRecibidas,
  });

  factory DatosAfiliacionResponse.fromJson(Map<String, dynamic> json) => DatosAfiliacionResponse(
        numeroCuentaAfiliada: json["NumeroCuentaAfiliada"],
        numeroCelular: json["NumeroCelular"],
        tipoOperacion: json["TipoOperacion"],
        codigoCuentaInterbancario: json["CodigoCuentaInterbancario"],
        codigoEntidadOriginante: json["CodigoEntidadOriginante"],
        nombreProducto: json["NombreProducto"],
        numeroTarjeta: json["NumeroTarjeta"],
        codigoMonedaCuenta: json["CodigoMonedaCuenta"],
        codigoCliente: json["CodigoCliente"],
        simboloMoneda: json["SimboloMoneda"],
        cadenaHashQR: json["CadenaHashQR"],
        nombreAlias: json["NombreAlias"],
        canal: json["Canal"],
        saldoDisponible: json["SaldoDisponible"]?.toDouble(),
        indicadorAfiliacionCCE: json["IndicadorAfiliacionCCE"],
        notificarOperacionesEnviadas: json["NotificarOperacionesEnviadas"],
        notificarOperacionesRecibidas: json["NotificarOperacionesRecibidas"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroCuentaAfiliada": numeroCuentaAfiliada,
        "NumeroCelular": numeroCelular,
        "TipoOperacion": tipoOperacion,
        "CodigoCuentaInterbancario": codigoCuentaInterbancario,
        "CodigoEntidadOriginante": codigoEntidadOriginante,
        "NombreProducto": nombreProducto,
        "NumeroTarjeta": numeroTarjeta,
        "CodigoMonedaCuenta": codigoMonedaCuenta,
        "CodigoCliente": codigoCliente,
        "SimboloMoneda": simboloMoneda,
        "CadenaHashQR": cadenaHashQR,
        "NombreAlias": nombreAlias,
        "Canal": canal,
        "SaldoDisponible": saldoDisponible,
        "IndicadorAfiliacionCCE": indicadorAfiliacionCCE,
        "NotificarOperacionesEnviadas": notificarOperacionesEnviadas,
        "NotificarOperacionesRecibidas": notificarOperacionesRecibidas,
      };
}

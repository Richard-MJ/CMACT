class CuentaAfiliacion {
  final String alias;
  final String numeroCuenta;
  final String titular;
  final String moneda;
  final String codigoCliente;
  final String codigoProducto;
  final String nombreProducto;
  final String codigoMoneda;

  CuentaAfiliacion({
    required this.alias,
    required this.numeroCuenta,
    required this.titular,
    required this.moneda,
    required this.codigoCliente,
    required this.codigoProducto,
    required this.nombreProducto,
    required this.codigoMoneda,
  });

  factory CuentaAfiliacion.fromJson(Map<String, dynamic> json) =>
      CuentaAfiliacion(
        alias: json["Alias"],
        numeroCuenta: json["NumeroCuenta"],
        titular: json["Titular"],
        moneda: json["Moneda"],
        codigoCliente: json["CodigoCliente"],
        codigoProducto: json["CodigoProducto"],
        nombreProducto: json["NombreProducto"],
        codigoMoneda: json["CodigoMoneda"],
      );

  Map<String, dynamic> toJson() => {
        "Alias": alias,
        "NumeroCuenta": numeroCuenta,
        "Titular": titular,
        "Moneda": moneda,
        "CodigoCliente": codigoCliente,
        "CodigoProducto": codigoProducto,
        "NombreProducto": nombreProducto,
        "CodigoMoneda": codigoMoneda,
      };
}

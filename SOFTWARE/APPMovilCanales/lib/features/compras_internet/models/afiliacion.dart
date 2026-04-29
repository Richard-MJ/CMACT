class Afiliacion {
  final DateTime fechaOperacion;
  final String nombreCliente;
  final String numeroCuentaAfiliada;
  final String nombreProductoCuentaAfiliada;
  final String alias;
  final String correoElectronico;
  final dynamic correoElectronicoRemitente;
  final String descripcionOperacion;
  final String codigoMonedaCuenta;

  Afiliacion({
    required this.fechaOperacion,
    required this.nombreCliente,
    required this.numeroCuentaAfiliada,
    required this.nombreProductoCuentaAfiliada,
    required this.alias,
    required this.correoElectronico,
    required this.correoElectronicoRemitente,
    required this.descripcionOperacion,
    required this.codigoMonedaCuenta,
  });

  factory Afiliacion.fromJson(Map<String, dynamic> json) => Afiliacion(
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        nombreCliente: json["NombreCliente"],
        numeroCuentaAfiliada: json["NumeroCuentaAfiliada"],
        nombreProductoCuentaAfiliada: json["NombreProductoCuentaAfiliada"],
        alias: json["Alias"],
        correoElectronico: json["CorreoElectronico"],
        correoElectronicoRemitente: json["CorreoElectronicoRemitente"],
        descripcionOperacion: json["DescripcionOperacion"],
        codigoMonedaCuenta: json["CodigoMonedaCuenta"],
      );

  Map<String, dynamic> toJson() => {
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "NombreCliente": nombreCliente,
        "NumeroCuentaAfiliada": numeroCuentaAfiliada,
        "NombreProductoCuentaAfiliada": nombreProductoCuentaAfiliada,
        "Alias": alias,
        "CorreoElectronico": correoElectronico,
        "CorreoElectronicoRemitente": correoElectronicoRemitente,
        "DescripcionOperacion": descripcionOperacion,
        "CodigoMonedaCuenta": codigoMonedaCuenta,
      };
}

class ConfirmarAfiliacionResponse {
  final int numeroAfiliacionBiometria;
  final String claveAfiliacion;
  final String modeloDispositivo;
  final int codigoTipoBiometria;
  final String descripcionTipoBiometria;
  final DateTime fechaRegistro;

  ConfirmarAfiliacionResponse({
    required this.numeroAfiliacionBiometria,
    required this.claveAfiliacion,
    required this.modeloDispositivo,
    required this.codigoTipoBiometria,
    required this.descripcionTipoBiometria,
    required this.fechaRegistro,
  });

  factory ConfirmarAfiliacionResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarAfiliacionResponse(
        numeroAfiliacionBiometria: json["NumeroAfiliacionBiometria"],
        claveAfiliacion: json["ClaveAfiliacion"],
        modeloDispositivo: json["ModeloDispositivo"],
        codigoTipoBiometria: json["CodigoTipoBiometria"],
        descripcionTipoBiometria: json["DescripcionTipoBiometria"],
        fechaRegistro: DateTime.parse(json["FechaRegistro"]),
      );

  Map<String, dynamic> toJson() => {
        "NumeroAfiliacionBiometria": numeroAfiliacionBiometria,
        "ClaveAfiliacion": claveAfiliacion,
        "ModeloDispositivo": modeloDispositivo,
        "CodigoTipoBiometria": codigoTipoBiometria,
        "DescripcionTipoBiometria": descripcionTipoBiometria,
        "FechaRegistro": fechaRegistro.toIso8601String(),
      };
}

class ConfirmarDesafiliacionResponse {
  final String descripcionTipoBiometria;
  final String modeloDispositivo;
  final DateTime fechaDesafiliacion;

  ConfirmarDesafiliacionResponse({
    required this.descripcionTipoBiometria,
    required this.modeloDispositivo,
    required this.fechaDesafiliacion,
  });

  factory ConfirmarDesafiliacionResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarDesafiliacionResponse(
        descripcionTipoBiometria: json["DescripcionTipoBiometria"],
        modeloDispositivo: json["ModeloDispositivo"],
        fechaDesafiliacion: DateTime.parse(json["FechaDesafiliacion"]),
      );

  Map<String, dynamic> toJson() => {
        "DescripcionTipoBiometria": descripcionTipoBiometria,
        "ModeloDispositivo": modeloDispositivo,
        "FechaDesafiliacion": fechaDesafiliacion.toIso8601String(),
      };
}

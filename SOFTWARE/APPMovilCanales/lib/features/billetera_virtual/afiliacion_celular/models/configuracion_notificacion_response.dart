class ConfiguracionNotificacionResponse {
  final bool notificarOperacionesEnviadas;
  final bool notificarOperacionesRecibidas;
  final DateTime fechaOperacion;

  ConfiguracionNotificacionResponse({
    required this.notificarOperacionesEnviadas,
    required this.notificarOperacionesRecibidas,
    required this.fechaOperacion,
  });

  factory ConfiguracionNotificacionResponse.fromJson(Map<String, dynamic> json) => ConfiguracionNotificacionResponse(
    notificarOperacionesEnviadas: json['NotificarOperacionesEnviadas'],
    notificarOperacionesRecibidas: json['NotificarOperacionesRecibidas'],
    fechaOperacion: DateTime.parse(json['FechaOperacion']),
  );

  Map<String, dynamic> toJson() => {
    'notificarOperacionesEnviadas': notificarOperacionesEnviadas,
    'notificarOperacionesRecibidas': notificarOperacionesRecibidas,
    'fechaOperacion': fechaOperacion.toIso8601String(),
  };
}
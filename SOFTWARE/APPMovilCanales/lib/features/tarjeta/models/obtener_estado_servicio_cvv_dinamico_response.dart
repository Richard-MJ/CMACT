class ObtenerEstadoServicioCvvDinamicoResponse {
  final String estadoServicio;
  final String mensajeAlerta;

  ObtenerEstadoServicioCvvDinamicoResponse({
    required this.estadoServicio,
    required this.mensajeAlerta,
  });

  factory ObtenerEstadoServicioCvvDinamicoResponse.fromJson(Map<String, dynamic> json) =>
      ObtenerEstadoServicioCvvDinamicoResponse(
        estadoServicio: json["EstadoServicio"],
        mensajeAlerta: json["MensajeAlerta"],
      );

  Map<String, dynamic> toJson() => {"EstadoServicio": estadoServicio, "MensajeAlerta": mensajeAlerta};
}

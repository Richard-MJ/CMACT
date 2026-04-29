class ReenvioComprobanteOperacionResponse {
  final String mensaje;

  ReenvioComprobanteOperacionResponse({
    required this.mensaje,
  });

  factory ReenvioComprobanteOperacionResponse.fromJson(
          Map<String, dynamic> json) =>
      ReenvioComprobanteOperacionResponse(
        mensaje: json["Mensaje"],
      );

  Map<String, dynamic> toJson() => {
        "Mensaje": mensaje,
      };
}

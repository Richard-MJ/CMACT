class PagoServicio {
  final String nombreServicio;
  final String codigoServicio;

  PagoServicio({
    required this.nombreServicio,
    required this.codigoServicio,
  });

  factory PagoServicio.fromJson(Map<String, dynamic> json) => PagoServicio(
        nombreServicio: json["NombreServicio"],
        codigoServicio: json["CodigoServicio"],
      );

  Map<String, dynamic> toJson() => {
        "NombreServicio": nombreServicio,
        "CodigoServicio": codigoServicio,
      };
}

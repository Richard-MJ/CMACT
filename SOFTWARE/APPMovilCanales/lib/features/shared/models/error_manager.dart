class ErrorManager {
  final String codigo;
  final String mensaje;

  ErrorManager({
    required this.codigo,
    required this.mensaje,
  });

  factory ErrorManager.fromJson(Map<String, dynamic> json) => ErrorManager(
        codigo: json["Codigo"] ?? json["codigo"] ?? '',
        mensaje: json["Mensaje"] ?? json["mensaje"] ?? 'Ocurrio un error.',
      );

  Map<String, dynamic> toJson() => {
        "Codigo": codigo,
        "Mensaje": mensaje,
      };
}

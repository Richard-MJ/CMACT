class ActualizarAliasResponse {
  final String nombreAlias;
  final String numeroProducto;
  final String codigoSistema;

  ActualizarAliasResponse({
    required this.nombreAlias,
    required this.numeroProducto,
    required this.codigoSistema,
  });

  factory ActualizarAliasResponse.fromJson(Map<String, dynamic> json) =>
      ActualizarAliasResponse(
        nombreAlias: json["NombreAlias"],
        numeroProducto: json["NumeroProducto"],
        codigoSistema: json["CodigoSistema"],
      );

  Map<String, dynamic> toJson() => {
        "NombreAlias": nombreAlias,
        "NumeroProducto": numeroProducto,
        "CodigoSistema": codigoSistema,
      };
}

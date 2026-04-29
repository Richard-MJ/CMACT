class TipoTransferencia {
  final int id;
  final String descripcion;

  TipoTransferencia({
    required this.id,
    required this.descripcion,
  });

  factory TipoTransferencia.fromJson(Map<String, dynamic> json) =>
      TipoTransferencia(
        id: json["id"],
        descripcion: json["descripcion"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "descripcion": descripcion,
      };
}

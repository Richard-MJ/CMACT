class Mes {
  final int numero;
  final String nombre;

  Mes({
    required this.numero,
    required this.nombre,
  });

  factory Mes.fromJson(Map<String, dynamic> json) => Mes(
        numero: json["numero"],
        nombre: json["nombre"],
      );

  Map<String, dynamic> toJson() => {
        "numero": numero,
        "nombre": nombre,
      };
}

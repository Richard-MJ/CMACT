class Agencia {
  final String codigoAgencia;
  final String nombreAgencia;
  final String? direccion;

  Agencia({
    required this.codigoAgencia,
    required this.nombreAgencia,
    this.direccion
  });

  factory Agencia.fromJson(Map<String, dynamic> json) => Agencia(
        codigoAgencia: json["CodigoAgencia"],
        nombreAgencia: json["NombreAgencia"],
        direccion: json["Direccion"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoAgencia": codigoAgencia,
        "NombreAgencia": nombreAgencia,
        "Direccion": direccion
      };
}

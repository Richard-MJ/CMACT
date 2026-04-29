class ContactosBarrido{
  final String numeroCelular;
  final String nombreAlias;

  ContactosBarrido({
    required this.numeroCelular,
    required this.nombreAlias,
  });

  factory ContactosBarrido.fromJson(Map<String, dynamic> json) => ContactosBarrido(
        numeroCelular: json["NumeroCelular"],
        nombreAlias: json["NombreAlias"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroCelular": numeroCelular,
        "NombreAlias": nombreAlias,
      };
}

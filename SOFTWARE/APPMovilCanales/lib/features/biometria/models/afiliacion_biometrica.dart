class AfiliacionBiometrica {
  final int codigoTipoBiometria;
  final String descripcionTipoBiometria;
  final DateTime fechaRegistro;
  final int numeroAfiliacionBiometria;

  AfiliacionBiometrica({
    required this.codigoTipoBiometria,
    required this.descripcionTipoBiometria,
    required this.fechaRegistro,
    required this.numeroAfiliacionBiometria,
  });

  factory AfiliacionBiometrica.fromJson(Map<String, dynamic> json) =>
      AfiliacionBiometrica(
        codigoTipoBiometria: json["CodigoTipoBiometria"],
        descripcionTipoBiometria: json["DescripcionTipoBiometria"],
        fechaRegistro: DateTime.parse(json["FechaRegistro"]),
        numeroAfiliacionBiometria: json["NumeroAfiliacionBiometria"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoTipoBiometria": codigoTipoBiometria,
        "DescripcionTipoBiometria": descripcionTipoBiometria,
        "FechaRegistro": fechaRegistro.toIso8601String(),
        "NumeroAfiliacionBiometria": numeroAfiliacionBiometria,
      };
}

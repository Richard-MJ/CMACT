class PasswordBiometrico {
  final String numeroTarjeta;
  final int codigoTipoBiometria;

  PasswordBiometrico({
    required this.numeroTarjeta,
    required this.codigoTipoBiometria,
  });

  factory PasswordBiometrico.fromJson(Map<String, dynamic> json) =>
      PasswordBiometrico(
        numeroTarjeta: json["numeroTarjeta"],
        codigoTipoBiometria: json["codigoTipoBiometria"],
      );

  Map<String, dynamic> toJson() => {
        "numeroTarjeta": numeroTarjeta,
        "codigoTipoBiometria": codigoTipoBiometria,
      };
}

class ValidarAperturaResponse {
  final ValidacionDatosCliente validacionDatosCliente;

  ValidarAperturaResponse({
    required this.validacionDatosCliente,
  });

  factory ValidarAperturaResponse.fromJson(Map<String, dynamic> json) =>
      ValidarAperturaResponse(
        validacionDatosCliente:
            ValidacionDatosCliente.fromJson(json["validacionDatosCliente"]),
      );

  Map<String, dynamic> toJson() => {
        "validacionDatosCliente": validacionDatosCliente.toJson(),
      };
}

class ValidacionDatosCliente {
  final String validacionDatosCliente;
  final String validacionPolitica;
  final String validacionListas;
  final String validacionResidente;
  final String validacionCorreoElectronico;

  ValidacionDatosCliente({
    required this.validacionDatosCliente,
    required this.validacionPolitica,
    required this.validacionListas,
    required this.validacionResidente,
    required this.validacionCorreoElectronico,
  });

  factory ValidacionDatosCliente.fromJson(Map<String, dynamic> json) =>
      ValidacionDatosCliente(
        validacionDatosCliente: json["ValidacionDatosCliente"],
        validacionPolitica: json["ValidacionPolitica"],
        validacionListas: json["ValidacionListas"],
        validacionResidente: json["ValidacionResidente"],
        validacionCorreoElectronico: json["ValidacionCorreoElectronico"],
      );

  Map<String, dynamic> toJson() => {
        "ValidacionDatosCliente": validacionDatosCliente,
        "ValidacionPolitica": validacionPolitica,
        "ValidacionListas": validacionListas,
        "ValidacionResidente": validacionResidente,
        "ValidacionCorreoElectronico": validacionCorreoElectronico,
      };
}

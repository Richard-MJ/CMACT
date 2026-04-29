class ValidarSmsResponse {
  final String? numeroTarjeta;
  final String? numeroDocumento;
  final String? idVerificacion;
  final String? codigoAutorizacion;
  final int idTipoOperacionCanalElectronico;
  final bool afiliado;
  final String? idRegistroNuevoDispositivo;

  ValidarSmsResponse({
    required this.numeroTarjeta,
    required this.numeroDocumento,
    required this.idVerificacion,
    required this.codigoAutorizacion,
    required this.idTipoOperacionCanalElectronico,
    required this.afiliado,
    required this.idRegistroNuevoDispositivo,
  });

  factory ValidarSmsResponse.fromJson(Map<String, dynamic> json) =>
      ValidarSmsResponse(
        numeroTarjeta: json["NumeroTarjeta"],
        numeroDocumento: json["NumeroDocumento"],
        idVerificacion: json["IdVerificacion"],
        codigoAutorizacion: json["CodigoAutorizacion"],
        idTipoOperacionCanalElectronico:
            json["IdTipoOperacionCanalElectronico"],
        afiliado: json["Afiliado"],
        idRegistroNuevoDispositivo: json["IdRegistroNuevoDispositivo"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroTarjeta": numeroTarjeta,
        "NumeroDocumento": numeroDocumento,
        "IdVerificacion": idVerificacion,
        "CodigoAutorizacion": codigoAutorizacion,
        "IdTipoOperacionCanalElectronico": idTipoOperacionCanalElectronico,
        "Afiliado": afiliado,
        "IdRegistroNuevoDispositivo": idRegistroNuevoDispositivo,
      };
}

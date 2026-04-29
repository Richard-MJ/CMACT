class ConfirmarResponse {
  final String idOperacionTts;
  final String tipoTarjeta;
  final String descripcionMotivo;
  final dynamic documentoIdentidad;
  final String correoElectronicoRemitente;
  final String temaMensaje;
  final String nombreCliente;
  final String numeroTarjeta;
  final String descripcionOperacion;
  final String direccionIp;
  final String modelo;
  final String sistemaOperativo;
  final String navegador;
  final DateTime fechaOperacion;

  ConfirmarResponse({
    required this.idOperacionTts,
    required this.tipoTarjeta,
    required this.descripcionMotivo,
    required this.documentoIdentidad,
    required this.correoElectronicoRemitente,
    required this.temaMensaje,
    required this.nombreCliente,
    required this.numeroTarjeta,
    required this.descripcionOperacion,
    required this.direccionIp,
    required this.modelo,
    required this.sistemaOperativo,
    required this.navegador,
    required this.fechaOperacion,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        idOperacionTts: json["IdOperacionTts"],
        tipoTarjeta: json["TipoTarjeta"],
        descripcionMotivo: json["DescripcionMotivo"],
        documentoIdentidad: json["DocumentoIdentidad"],
        correoElectronicoRemitente: json["CorreoElectronicoRemitente"],
        temaMensaje: json["TemaMensaje"],
        nombreCliente: json["NombreCliente"],
        numeroTarjeta: json["NumeroTarjeta"],
        descripcionOperacion: json["DescripcionOperacion"],
        direccionIp: json["DireccionIP"],
        modelo: json["Modelo"],
        sistemaOperativo: json["SistemaOperativo"],
        navegador: json["Navegador"],
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
      );

  Map<String, dynamic> toJson() => {
        "IdOperacionTts": idOperacionTts,
        "TipoTarjeta": tipoTarjeta,
        "DescripcionMotivo": descripcionMotivo,
        "DocumentoIdentidad": documentoIdentidad,
        "CorreoElectronicoRemitente": correoElectronicoRemitente,
        "TemaMensaje": temaMensaje,
        "NombreCliente": nombreCliente,
        "NumeroTarjeta": numeroTarjeta,
        "DescripcionOperacion": descripcionOperacion,
        "DireccionIP": direccionIp,
        "Modelo": modelo,
        "SistemaOperativo": sistemaOperativo,
        "Navegador": navegador,
        "FechaOperacion": fechaOperacion.toIso8601String(),
      };
}

class SesionCanalElectronico {
  final String dispositivoId;
  final String direccionIp;
  final String sistemaOperativo;
  final String navegador;
  final String modeloDispositivo;
  final String tokenGuid;
  final String indicadorEstado;
  final String indicadorCanal;
  final String canalDescripcion;
  final DateTime fechaRegistro;
  final DateTime fechaModificacion;
  final bool sesionActual;

  SesionCanalElectronico({
    required this.dispositivoId,
    required this.direccionIp,
    required this.sistemaOperativo,
    required this.navegador,
    required this.modeloDispositivo,
    required this.tokenGuid,
    required this.indicadorEstado,
    required this.indicadorCanal,
    required this.canalDescripcion,
    required this.fechaRegistro,
    required this.fechaModificacion,
    required this.sesionActual,
  });

  factory SesionCanalElectronico.fromJson(Map<String, dynamic> json) =>
      SesionCanalElectronico(
        dispositivoId: json["DispositivoId"],
        direccionIp: json["DireccionIp"],
        sistemaOperativo: json["SistemaOperativo"],
        navegador: json["Navegador"],
        modeloDispositivo: json["ModeloDispositivo"],
        tokenGuid: json["TokenGuid"],
        indicadorEstado: json["IndicadorEstado"],
        indicadorCanal: json["IndicadorCanal"],
        canalDescripcion: json["CanalDescripcion"],
        fechaRegistro: DateTime.parse(json["FechaRegistro"]),
        fechaModificacion: DateTime.parse(json["FechaModificacion"]),
        sesionActual: json["SesionActual"],
      );

  Map<String, dynamic> toJson() => {
        "DispositivoId": dispositivoId,
        "DireccionIp": direccionIp,
        "SistemaOperativo": sistemaOperativo,
        "Navegador": navegador,
        "ModeloDispositivo": modeloDispositivo,
        "TokenGuid": tokenGuid,
        "IndicadorEstado": indicadorEstado,
        "IndicadorCanal": indicadorCanal,
        "CanalDescripcion": canalDescripcion,
        "FechaRegistro": fechaRegistro.toIso8601String(),
        "FechaModificacion": fechaModificacion.toIso8601String(),
        "SesionActual": sesionActual,
      };
}

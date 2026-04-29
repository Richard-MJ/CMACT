class DispositivoSeguro {
  final String dispositivoId;
  final String direccionIp;
  final String sistemaOperativo;
  final String navegador;
  final String modeloDispositivo;
  final String indicadorCanal;
  final String canalDescripcion;
  final DateTime fechaRegistro;
  final DateTime fechaModificacion;
  final String indicadorEstado;
  final String? idConexion;
  bool estaSelecionado = false;

  DispositivoSeguro(
      {required this.dispositivoId,
      required this.direccionIp,
      required this.sistemaOperativo,
      required this.navegador,
      required this.modeloDispositivo,
      required this.indicadorCanal,
      required this.canalDescripcion,
      required this.fechaRegistro,
      required this.fechaModificacion,
      required this.indicadorEstado,
      required this.idConexion});

  factory DispositivoSeguro.fromJson(Map<String, dynamic> json) =>
      DispositivoSeguro(
        dispositivoId: json["DispositivoId"],
        direccionIp: json["DireccionIp"],
        sistemaOperativo: json["SistemaOperativo"],
        navegador: json["Navegador"],
        modeloDispositivo: json["ModeloDispositivo"],
        indicadorCanal: json["IndicadorCanal"],
        canalDescripcion: json["CanalDescripcion"],
        fechaRegistro: DateTime.parse(json["FechaRegistro"]),
        fechaModificacion: DateTime.parse(json["FechaModificacion"]),
        indicadorEstado: json["IndicadorEstado"],
        idConexion: json["IdConexion"],
      );

  Map<String, dynamic> toJson() => {
        "DispositivoId": dispositivoId,
        "DireccionIp": direccionIp,
        "SistemaOperativo": sistemaOperativo,
        "Navegador": navegador,
        "ModeloDispositivo": modeloDispositivo,
        "FechaRegistro": fechaRegistro.toIso8601String(),
        "FechaModificacion": fechaModificacion.toIso8601String(),
      };
}

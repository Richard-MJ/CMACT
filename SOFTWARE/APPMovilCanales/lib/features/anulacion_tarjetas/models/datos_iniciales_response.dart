class DatosInicialesResponse {
  final List<MotivoAnulacionTarjeta> motivosAnulacionTarjeta;
  final List<TarjetaAnulacion> tarjetasAnulacion;

  DatosInicialesResponse({
    required this.motivosAnulacionTarjeta,
    required this.tarjetasAnulacion,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        motivosAnulacionTarjeta: List<MotivoAnulacionTarjeta>.from(
            json["MotivosAnulacionTarjeta"]
                .map((x) => MotivoAnulacionTarjeta.fromJson(x))),
        tarjetasAnulacion: List<TarjetaAnulacion>.from(
            json["TarjetasAnulacion"].map((x) => TarjetaAnulacion.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "MotivosAnulacionTarjeta":
            List<dynamic>.from(motivosAnulacionTarjeta.map((x) => x.toJson())),
        "TarjetasAnulacion":
            List<dynamic>.from(tarjetasAnulacion.map((x) => x.toJson())),
      };
}

class MotivoAnulacionTarjeta {
  final String codigoMotivo;
  final String descripcionMotivo;

  MotivoAnulacionTarjeta({
    required this.codigoMotivo,
    required this.descripcionMotivo,
  });

  factory MotivoAnulacionTarjeta.fromJson(Map<String, dynamic> json) =>
      MotivoAnulacionTarjeta(
        codigoMotivo: json["CodigoMotivo"],
        descripcionMotivo: json["DescripcionMotivo"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoMotivo": codigoMotivo,
        "DescripcionMotivo": descripcionMotivo,
      };
}

class TarjetaAnulacion {
  final String numeroTarjeta;
  final String descripcionTarjeta;
  final String titularTarjeta;
  final String codigoTipoTarjeta;

  TarjetaAnulacion({
    required this.numeroTarjeta,
    required this.descripcionTarjeta,
    required this.titularTarjeta,
    required this.codigoTipoTarjeta,
  });

  factory TarjetaAnulacion.fromJson(Map<String, dynamic> json) =>
      TarjetaAnulacion(
        numeroTarjeta: json["NumeroTarjeta"],
        descripcionTarjeta: json["DescripcionTarjeta"],
        titularTarjeta: json["TitularTarjeta"],
        codigoTipoTarjeta: json["CodigoTipoTarjeta"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroTarjeta": numeroTarjeta,
        "DescripcionTarjeta": descripcionTarjeta,
        "TitularTarjeta": titularTarjeta,
        "CodigoTipoTarjeta": codigoTipoTarjeta,
      };
}

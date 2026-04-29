import 'dart:typed_data';

class Configuracion {
  final bool indicadorPagoEfectivoPagoCredito;
  final bool indicadorLecturaNfc;
  final bool indicadorTransferenciaInmediata;
  final List<EnlaceDocumento> enlacesDocumentos;

  Configuracion({
    required this.indicadorPagoEfectivoPagoCredito,
    required this.indicadorLecturaNfc,
    required this.indicadorTransferenciaInmediata,
    required this.enlacesDocumentos,
  });

  factory Configuracion.fromJson(Map<String, dynamic> json) => Configuracion(
        indicadorPagoEfectivoPagoCredito:
            json["IndicadorPagoEfectivoPagoCredito"],
        indicadorLecturaNfc: json["IndicadorLecturaNfc"] ?? false,
        indicadorTransferenciaInmediata: json["IndicadorTransferenciaInmediata"],
        enlacesDocumentos: List<EnlaceDocumento>.from(json["EnlacesDocumentos"].map((x) => EnlaceDocumento.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "IndicadorPagoEfectivoPagoCredito": indicadorPagoEfectivoPagoCredito,
        "IndicadorLecturaNfc": indicadorLecturaNfc,
        "IndicadorTransferenciaInmediata": indicadorTransferenciaInmediata,
        "EnlacesDocumentos": List<dynamic>.from(enlacesDocumentos.map((x) => x.toJson())),
      };
}

class EnlaceDocumento {
  final int idCategoria;
  final String codigoDocumento;
  final String nombreDocumento;
  final String descripcionDocumento;
  final String urlDocumento;
  final String indicadorCanal;
  final String indicadorEstado;
  Uint8List? dataDocumento;

  EnlaceDocumento({
    required this.idCategoria,
    required this.codigoDocumento,
    required this.nombreDocumento,
    required this.descripcionDocumento,
    required this.urlDocumento,
    required this.indicadorCanal,
    required this.indicadorEstado,
    this.dataDocumento,
  });

  factory EnlaceDocumento.fromJson(Map<String, dynamic> json) =>
      EnlaceDocumento(
        idCategoria: json["IdCategoria"] ?? json["idCategoria"],
        codigoDocumento: json["CodigoDocumento"] ?? json["codigoDocumento"],
        nombreDocumento: json["NombreDocumento"] ?? json["nombreDocumento"],
        descripcionDocumento:
            json["DescripcionDocumento"] ?? json["descripcionDocumento"],
        urlDocumento: json["UrlDocumento"] ?? json["urlDocumento"],
        indicadorCanal: json["IndicadorCanal"] ?? json["indicadorCanal"],
        indicadorEstado: json["IndicadorEstado"] ?? json["indicadorEstado"],
      );

  Map<String, dynamic> toJson() => {
        "IdCategoria": idCategoria,
        "CodigoDocumento": codigoDocumento,
        "NombreDocumento": nombreDocumento,
        "DescripcionDocumento": descripcionDocumento,
        "UrlDocumento": urlDocumento,
        "IndicadorCanal": indicadorCanal,
        "IndicadorEstado": indicadorEstado,
      };
}

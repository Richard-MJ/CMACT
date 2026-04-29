class OperadorMovilKasnet {
 
  final String idDetalleEmpresaKasnet;    
  final String codigoEmpresa;
  final String nombreEmpresa;
  final double codigoGrupoEmpresa;
  final double codigoCategoria;
  final double tipoPagoServicio;
  final String nombreCategoria;
  String get codigoOperador => idDetalleEmpresaKasnet;
  String get descripcionOperador => nombreEmpresa;

  OperadorMovilKasnet({
    required this.idDetalleEmpresaKasnet,
    required this.nombreEmpresa,
    required this.codigoEmpresa,
    required this.tipoPagoServicio,
    required this.codigoCategoria,
    required this.codigoGrupoEmpresa,
    required this.nombreCategoria,
  });

    factory OperadorMovilKasnet.fromJson(Map<String, dynamic> json) => OperadorMovilKasnet(
        idDetalleEmpresaKasnet: json["IdDetalleEmpresaKasnet"],
        nombreEmpresa: json["NombreEmpresa"],
        codigoEmpresa: json["CodigoEmpresa"],
        tipoPagoServicio: json["TipoPagoServicio"]?.toDouble(),
        codigoCategoria: json["CodigoCategoria"]?.toDouble(),
        codigoGrupoEmpresa: json["CodigoGrupoEmpresa"]?.toDouble(),
        nombreCategoria: json["NombreCategoria"],
    );

    Map<String, dynamic> toJson() => {
        "IdDetalleEmpresaKasnet": idDetalleEmpresaKasnet,
        "NombreEmpresa": nombreEmpresa,
        "CodigoEmpresa": codigoEmpresa,
        "TipoPagoServicio": tipoPagoServicio,
        "CodigoCategoria": codigoCategoria,
        "CodigoGrupoEmpresa": codigoGrupoEmpresa,
        "NombreCategoria": nombreCategoria,
    };
    
}
import 'package:caja_tacna_app/features/pago_servicios/models/cuenta_origen.dart';

class DatosInicialesResponse {
  final List<CuentaOrigenPagServ> productosDebito;
  final List<CategoriaPagServ> categorias;
  final List<ServicioPagar> ultimosPagos;

  DatosInicialesResponse({
    required this.productosDebito,
    required this.categorias,
    required this.ultimosPagos,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productosDebito: List<CuentaOrigenPagServ>.from(json["ProductosDebito"]
            .map((x) => CuentaOrigenPagServ.fromJson(x))),
        categorias: List<CategoriaPagServ>.from(
            json["Categorias"].map((x) => CategoriaPagServ.fromJson(x))),
        ultimosPagos: List<ServicioPagar>.from(
            json["UltimosPagos"].map((x) => ServicioPagar.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "ProductosDebito":
            List<dynamic>.from(productosDebito.map((x) => x.toJson())),
        "Categorias": List<dynamic>.from(categorias.map((x) => x.toJson())),
        "UltimosPagos": List<dynamic>.from(ultimosPagos.map((x) => x.toJson())),
      };
}

class CategoriaPagServ {
  final int idTipoCategoriaServicio;
  final String descripcionTipoCategoriaServicio;

  CategoriaPagServ({
    required this.idTipoCategoriaServicio,
    required this.descripcionTipoCategoriaServicio,
  });

  factory CategoriaPagServ.fromJson(Map<String, dynamic> json) =>
      CategoriaPagServ(
        idTipoCategoriaServicio: json["IdTipoCategoriaServicio"],
        descripcionTipoCategoriaServicio:
            json["DescripcionTipoCategoriaServicio"],
      );

  Map<String, dynamic> toJson() => {
        "IdTipoCategoriaServicio": idTipoCategoriaServicio,
        "DescripcionTipoCategoriaServicio": descripcionTipoCategoriaServicio,
      };
}

class ServicioPagar {
  final String codigoEmpresa;
  final String nombreEmpresa;
  final String codigoServicio;
  final String nombreServicio;
  final int codigoCategoria;
  final String nombreCategoria;
  final int codigoGrupoEmpresa;
  final int tipoPagoServicio;

  ServicioPagar({
    required this.codigoEmpresa,
    required this.nombreEmpresa,
    required this.codigoServicio,
    required this.nombreServicio,
    required this.codigoCategoria,
    required this.nombreCategoria,
    required this.codigoGrupoEmpresa,
    required this.tipoPagoServicio,
  });

  factory ServicioPagar.fromJson(Map<String, dynamic> json) => ServicioPagar(
        codigoEmpresa: json["CodigoEmpresa"],
        nombreEmpresa: json["NombreEmpresa"],
        codigoServicio: json["CodigoServicio"],
        nombreServicio: json["NombreServicio"],
        codigoCategoria: json["CodigoCategoria"],
        nombreCategoria: json["NombreCategoria"],
        codigoGrupoEmpresa: json["CodigoGrupoEmpresa"],
        tipoPagoServicio: json["TipoPagoServicio"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoEmpresa": codigoEmpresa,
        "NombreEmpresa": nombreEmpresa,
        "CodigoServicio": codigoServicio,
        "NombreServicio": nombreServicio,
        "CodigoCategoria": codigoCategoria,
        "NombreCategoria": nombreCategoria,
        "CodigoGrupoEmpresa": codigoGrupoEmpresa,
        "TipoPagoServicio": tipoPagoServicio,
      };
}

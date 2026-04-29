class ListarOperacionesResponse {
  final List<CategoriaOperacionFrecuente> categorias;
  final List<OperacionFrecuente> operacionesFrecuentes;

  ListarOperacionesResponse({
    required this.categorias,
    required this.operacionesFrecuentes,
  });

  factory ListarOperacionesResponse.fromJson(Map<String, dynamic> json) =>
      ListarOperacionesResponse(
        categorias: List<CategoriaOperacionFrecuente>.from(json["Categorias"]
            .map((x) => CategoriaOperacionFrecuente.fromJson(x))),
        operacionesFrecuentes: List<OperacionFrecuente>.from(
            json["OperacionesFrecuentes"]
                .map((x) => OperacionFrecuente.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "Categorias": List<dynamic>.from(categorias.map((x) => x.toJson())),
        "OperacionesFrecuentes":
            List<dynamic>.from(operacionesFrecuentes.map((x) => x.toJson())),
      };
}

class CategoriaOperacionFrecuente {
  final int numeroCategoriaTipoOperacionFrecuente;
  final String descripcionCategoria;

  CategoriaOperacionFrecuente({
    required this.numeroCategoriaTipoOperacionFrecuente,
    required this.descripcionCategoria,
  });

  factory CategoriaOperacionFrecuente.fromJson(Map<String, dynamic> json) =>
      CategoriaOperacionFrecuente(
        numeroCategoriaTipoOperacionFrecuente:
            json["NumeroCategoriaTipoOperacionFrecuente"],
        descripcionCategoria: json["DescripcionCategoria"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroCategoriaTipoOperacionFrecuente":
            numeroCategoriaTipoOperacionFrecuente,
        "DescripcionCategoria": descripcionCategoria,
      };
}

class OperacionFrecuente {
  final int numeroOperacionFrecuente;
  final int numeroCategoriaTipoOperacionFrecuente;
  final String nombreCategoria;
  final int numeroTipoOperacionFrecuente;
  final String nombreTipo;
  final String numeroCuenta;
  final String nombreOperacionFrecuente;
  final OperacionesFrecuenteDetalle operacionesFrecuenteDetalle;

  OperacionFrecuente({
    required this.numeroOperacionFrecuente,
    required this.numeroCategoriaTipoOperacionFrecuente,
    required this.nombreCategoria,
    required this.numeroTipoOperacionFrecuente,
    required this.nombreTipo,
    required this.numeroCuenta,
    required this.nombreOperacionFrecuente,
    required this.operacionesFrecuenteDetalle,
  });

  factory OperacionFrecuente.fromJson(Map<String, dynamic> json) =>
      OperacionFrecuente(
        numeroOperacionFrecuente: json["NumeroOperacionFrecuente"],
        numeroCategoriaTipoOperacionFrecuente:
            json["NumeroCategoriaTipoOperacionFrecuente"],
        nombreCategoria: json["NombreCategoria"],
        numeroTipoOperacionFrecuente: json["NumeroTipoOperacionFrecuente"],
        nombreTipo: json["NombreTipo"],
        numeroCuenta: json["NumeroCuenta"],
        nombreOperacionFrecuente: json["NombreOperacionFrecuente"],
        operacionesFrecuenteDetalle: OperacionesFrecuenteDetalle.fromJson(
            json["OperacionesFrecuenteDetalle"]),
      );

  Map<String, dynamic> toJson() => {
        "NumeroOperacionFrecuente": numeroOperacionFrecuente,
        "NumeroCategoriaTipoOperacionFrecuente":
            numeroCategoriaTipoOperacionFrecuente,
        "NombreCategoria": nombreCategoria,
        "NumeroTipoOperacionFrecuente": numeroTipoOperacionFrecuente,
        "NombreTipo": nombreTipo,
        "NumeroCuenta": numeroCuenta,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "OperacionesFrecuenteDetalle": operacionesFrecuenteDetalle.toJson(),
      };
}

class OperacionesFrecuenteDetalle {
  final String? numeroCredito;
  final String? tipoDocumento;
  final String? numeroDocumento;
  final String? nombreApellido;
  final String? direccion;
  final String? codigoPais;
  final String? idVinculo;
  final String? otroVinculo;
  final String? idMotivo;
  final String? otroMotivo;
  final String? codigoDepartamento;
  final String? codigoAgencia;
  final String? cuentaDestinoCci;
  final String? nombreDestino;
  final String? mismoTitularEnDestino;
  final String? codigoOperador;
  final String? numeroCelular;
  final String? numeroCuentaCredito;
  final String? codigoSistema;
  final String? suministroPagoServicio;
  final String? codigoEmpresaPagoServicio;
  final String? codigoServicioPagoServicio;
  final String? codigoGrupoEmpresaPagoServicio;
  final String? codigoCategoriaPagoServicio;
  final String? nombreCategoriaPagoServicio;
  final String? nombreEmpresaPagoServicio;
  final String? nombreServicioPagoServicio;
  final String? tipoPagoServicioPagoServicio;

  OperacionesFrecuenteDetalle({
    this.numeroCredito,
    this.tipoDocumento,
    this.numeroDocumento,
    this.nombreApellido,
    this.direccion,
    this.codigoPais,
    this.idVinculo,
    this.otroVinculo,
    this.idMotivo,
    this.otroMotivo,
    this.codigoDepartamento,
    this.codigoAgencia,
    this.cuentaDestinoCci,
    this.nombreDestino,
    this.mismoTitularEnDestino,
    this.codigoOperador,
    this.numeroCelular,
    this.numeroCuentaCredito,
    this.codigoSistema,
    this.suministroPagoServicio,
    this.codigoEmpresaPagoServicio,
    this.codigoServicioPagoServicio,
    this.codigoGrupoEmpresaPagoServicio,
    this.codigoCategoriaPagoServicio,
    this.nombreCategoriaPagoServicio,
    this.nombreEmpresaPagoServicio,
    this.nombreServicioPagoServicio,
    this.tipoPagoServicioPagoServicio,
  });

  factory OperacionesFrecuenteDetalle.fromJson(Map<String, dynamic> json) =>
      OperacionesFrecuenteDetalle(
        numeroCredito: json["NumeroCredito"],
        tipoDocumento: json["TipoDocumento"],
        numeroDocumento: json["NumeroDocumento"],
        nombreApellido: json["NombreApellido"],
        direccion: json["Direccion"],
        codigoPais: json["CodigoPais"],
        idVinculo: json["IdVinculo"],
        otroVinculo: json["OtroVinculo"],
        idMotivo: json["IdMotivo"],
        otroMotivo: json["OtroMotivo"],
        codigoDepartamento: json["CodigoDepartamento"],
        codigoAgencia: json["CodigoAgencia"],
        cuentaDestinoCci: json["CuentaDestinoCci"],
        nombreDestino: json["NombreDestino"],
        mismoTitularEnDestino: json["MismoTitularEnDestino"],
        codigoOperador: json["CodigoOperador"],
        numeroCelular: json["NumeroCelular"],
        numeroCuentaCredito: json["NumeroCuentaCredito"],
        codigoSistema: json["CodigoSistema"],
        suministroPagoServicio: json["SuministroPagoServicio"],
        codigoEmpresaPagoServicio: json["CodigoEmpresaPagoServicio"],
        codigoServicioPagoServicio: json["CodigoServicioPagoServicio"],
        codigoGrupoEmpresaPagoServicio: json["CodigoGrupoEmpresaPagoServicio"],
        codigoCategoriaPagoServicio: json["CodigoCategoriaPagoServicio"],
        nombreCategoriaPagoServicio: json["NombreCategoriaPagoServicio"],
        nombreEmpresaPagoServicio: json["NombreEmpresaPagoServicio"],
        nombreServicioPagoServicio: json["NombreServicioPagoServicio"],
        tipoPagoServicioPagoServicio: json["TipoPagoServicioPagoServicio"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroCredito": numeroCredito,
        "TipoDocumento": tipoDocumento,
        "NumeroDocumento": numeroDocumento,
        "NombreApellido": nombreApellido,
        "Direccion": direccion,
        "CodigoPais": codigoPais,
        "IdVinculo": idVinculo,
        "OtroVinculo": otroVinculo,
        "IdMotivo": idMotivo,
        "OtroMotivo": otroMotivo,
        "CodigoDepartamento": codigoDepartamento,
        "CodigoAgencia": codigoAgencia,
        "CuentaDestinoCci": cuentaDestinoCci,
        "NombreDestino": nombreDestino,
        "MismoTitularEnDestino": mismoTitularEnDestino,
        "CodigoOperador": codigoOperador,
        "NumeroCelular": numeroCelular,
        "NumeroCuentaCredito": numeroCuentaCredito,
        "CodigoSistema": codigoSistema,
        "SuministroPagoServicio": suministroPagoServicio,
        "CodigoEmpresaPagoServicio": codigoEmpresaPagoServicio,
        "CodigoServicioPagoServicio": codigoServicioPagoServicio,
        "CodigoGrupoEmpresaPagoServicio": codigoGrupoEmpresaPagoServicio,
        "CodigoCategoriaPagoServicio": codigoCategoriaPagoServicio,
        "NombreCategoriaPagoServicio": nombreCategoriaPagoServicio,
        "NombreEmpresaPagoServicio": nombreEmpresaPagoServicio,
        "NombreServicioPagoServicio": nombreServicioPagoServicio,
        "TipoPagoServicioPagoServicio": tipoPagoServicioPagoServicio,
      };
}

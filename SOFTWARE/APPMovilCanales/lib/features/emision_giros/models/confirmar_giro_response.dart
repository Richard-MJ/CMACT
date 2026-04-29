class ConfirmarGiroResponse {
  final Giro giro;
  final String correoRemitente;
  final String correoDestinatario;
  final String numeroOperacion;

  ConfirmarGiroResponse({
    required this.giro,
    required this.correoRemitente,
    required this.correoDestinatario,
    required this.numeroOperacion,
  });

  factory ConfirmarGiroResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarGiroResponse(
        giro: Giro.fromJson(json["giro"]),
        correoRemitente: json["correoRemitente"],
        correoDestinatario: json["correoDestinatario"],
        numeroOperacion: json["NumeroOperacion"],
      );

  Map<String, dynamic> toJson() => {
        "giro": giro.toJson(),
        "correoRemitente": correoRemitente,
        "correoDestinatario": correoDestinatario,
        "NumeroOperacion": numeroOperacion,
      };
}

class Giro {
  final String numeroGiro;
  final String codigoAgenciaOrigen;
  final String codigoAgenciaDestino;
  final String direccionAgenciaDestino;
  final String nombreAgenciaOrigen;
  final String nombreAgenciaDestino;
  final String codigoMoneda;
  final String descripcionMoneda;
  final String simboloMoneda;
  final double montoGiro;
  final double montoComision;
  final double montoRecepcionado;
  final double montoComisionCmac;
  final String indicadorModalidad;
  final String indicadorEstado;
  final String descripcionIndicadorEstado;
  final double montoItf;
  final double montoEntregadoPorCajero;
  final double montoRedondeoAFavorDelCliente;
  final double montoTotalEntregadoPorCajero;
  final String numeroCuentaAhorrosOrigenGiro;
  final String fechaGiro;
  final Beneficiario girador;
  final Beneficiario beneficiario;

  Giro({
    required this.numeroGiro,
    required this.codigoAgenciaOrigen,
    required this.codigoAgenciaDestino,
    required this.direccionAgenciaDestino,
    required this.nombreAgenciaOrigen,
    required this.nombreAgenciaDestino,
    required this.codigoMoneda,
    required this.descripcionMoneda,
    required this.simboloMoneda,
    required this.montoGiro,
    required this.montoComision,
    required this.montoRecepcionado,
    required this.montoComisionCmac,
    required this.indicadorModalidad,
    required this.indicadorEstado,
    required this.descripcionIndicadorEstado,
    required this.montoItf,
    required this.montoEntregadoPorCajero,
    required this.montoRedondeoAFavorDelCliente,
    required this.montoTotalEntregadoPorCajero,
    required this.numeroCuentaAhorrosOrigenGiro,
    required this.fechaGiro,
    required this.girador,
    required this.beneficiario,
  });

  factory Giro.fromJson(Map<String, dynamic> json) => Giro(
        numeroGiro: json["NumeroGiro"],
        codigoAgenciaOrigen: json["CodigoAgenciaOrigen"],
        codigoAgenciaDestino: json["CodigoAgenciaDestino"],
        direccionAgenciaDestino: json["DireccionAgenciaDestino"],
        nombreAgenciaOrigen: json["NombreAgenciaOrigen"],
        nombreAgenciaDestino: json["NombreAgenciaDestino"],
        codigoMoneda: json["CodigoMoneda"],
        descripcionMoneda: json["DescripcionMoneda"],
        simboloMoneda: json["SimboloMoneda"],
        montoGiro: json["MontoGiro"]?.toDouble(),
        montoComision: json["MontoComision"]?.toDouble(),
        montoRecepcionado: json["MontoRecepcionado"]?.toDouble(),
        montoComisionCmac: json["MontoComisionCmac"]?.toDouble(),
        indicadorModalidad: json["IndicadorModalidad"],
        indicadorEstado: json["IndicadorEstado"],
        descripcionIndicadorEstado: json["DescripcionIndicadorEstado"],
        montoItf: json["MontoItf"]?.toDouble(),
        montoEntregadoPorCajero: json["MontoEntregadoPorCajero"]?.toDouble(),
        montoRedondeoAFavorDelCliente:
            json["MontoRedondeoAFavorDelCliente"]?.toDouble(),
        montoTotalEntregadoPorCajero:
            json["MontoTotalEntregadoPorCajero"]?.toDouble(),
        numeroCuentaAhorrosOrigenGiro: json["NumeroCuentaAhorrosOrigenGiro"],
        fechaGiro: json["FechaGiro"],
        girador: Beneficiario.fromJson(json["Girador"]),
        beneficiario: Beneficiario.fromJson(json["Beneficiario"]),
      );

  Map<String, dynamic> toJson() => {
        "NumeroGiro": numeroGiro,
        "CodigoAgenciaOrigen": codigoAgenciaOrigen,
        "CodigoAgenciaDestino": codigoAgenciaDestino,
        "DireccionAgenciaDestino": direccionAgenciaDestino,
        "NombreAgenciaOrigen": nombreAgenciaOrigen,
        "NombreAgenciaDestino": nombreAgenciaDestino,
        "CodigoMoneda": codigoMoneda,
        "DescripcionMoneda": descripcionMoneda,
        "SimboloMoneda": simboloMoneda,
        "MontoGiro": montoGiro,
        "MontoComision": montoComision,
        "MontoRecepcionado": montoRecepcionado,
        "MontoComisionCmac": montoComisionCmac,
        "IndicadorModalidad": indicadorModalidad,
        "IndicadorEstado": indicadorEstado,
        "DescripcionIndicadorEstado": descripcionIndicadorEstado,
        "MontoItf": montoItf,
        "MontoEntregadoPorCajero": montoEntregadoPorCajero,
        "MontoRedondeoAFavorDelCliente": montoRedondeoAFavorDelCliente,
        "MontoTotalEntregadoPorCajero": montoTotalEntregadoPorCajero,
        "NumeroCuentaAhorrosOrigenGiro": numeroCuentaAhorrosOrigenGiro,
        "FechaGiro": fechaGiro,
        "Girador": girador.toJson(),
        "Beneficiario": beneficiario.toJson(),
      };
}

class Beneficiario {
  final String codigoCliente;
  final String nombreCliente;
  final dynamic tipoPersona;
  final String? direccionCorreoElectronico;
  final bool indicadorBanzarizado;
  final dynamic numeroTelefonoSms;
  final dynamic numeroTelefonoOficina;
  final dynamic numeroTelefonoCasa;
  final String dni;
  final dynamic sexo;
  final dynamic nombres;
  final dynamic apellidoPaterno;
  final dynamic apellidoMaterno;
  final bool personaNatural;
  final String direccionPorDefecto;
  final dynamic categoriaCliente;
  final dynamic codigoTipoTarjeta;
  final dynamic descripcionTipoTarjeta;
  final dynamic nombreComercial;
  final String codigoTipoDocumento;
  final String? descripcionMotivo;
  final String? descripcionOtroMotivo;
  final String? descripcionVinculo;
  final String? descripcionOtroVinculo;
  final String? descripcionTipoDocumento;
  final String? descripcionNacionalidad;
  final dynamic numeroDocumento;
  final int idVerificacion;
  final DateTime fechaOperacion;
  final dynamic correoRemitente;

  Beneficiario({
    required this.codigoCliente,
    required this.nombreCliente,
    required this.tipoPersona,
    required this.direccionCorreoElectronico,
    required this.indicadorBanzarizado,
    required this.numeroTelefonoSms,
    required this.numeroTelefonoOficina,
    required this.numeroTelefonoCasa,
    required this.dni,
    required this.sexo,
    required this.nombres,
    required this.apellidoPaterno,
    required this.apellidoMaterno,
    required this.personaNatural,
    required this.direccionPorDefecto,
    required this.categoriaCliente,
    required this.codigoTipoTarjeta,
    required this.descripcionTipoTarjeta,
    required this.nombreComercial,
    required this.codigoTipoDocumento,
    required this.descripcionMotivo,
    required this.descripcionOtroMotivo,
    required this.descripcionVinculo,
    required this.descripcionOtroVinculo,
    required this.descripcionTipoDocumento,
    required this.descripcionNacionalidad,
    required this.numeroDocumento,
    required this.idVerificacion,
    required this.fechaOperacion,
    required this.correoRemitente,
  });

  factory Beneficiario.fromJson(Map<String, dynamic> json) => Beneficiario(
        codigoCliente: json["CodigoCliente"],
        nombreCliente: json["NombreCliente"],
        tipoPersona: json["TipoPersona"],
        direccionCorreoElectronico: json["DireccionCorreoElectronico"],
        indicadorBanzarizado: json["IndicadorBanzarizado"],
        numeroTelefonoSms: json["NumeroTelefonoSMS"],
        numeroTelefonoOficina: json["NumeroTelefonoOficina"],
        numeroTelefonoCasa: json["NumeroTelefonoCasa"],
        dni: json["DNI"],
        sexo: json["Sexo"],
        nombres: json["Nombres"],
        apellidoPaterno: json["ApellidoPaterno"],
        apellidoMaterno: json["ApellidoMaterno"],
        personaNatural: json["PersonaNatural"],
        direccionPorDefecto: json["DireccionPorDefecto"],
        categoriaCliente: json["CategoriaCliente"],
        codigoTipoTarjeta: json["CodigoTipoTarjeta"],
        descripcionTipoTarjeta: json["DescripcionTipoTarjeta"],
        nombreComercial: json["NombreComercial"],
        codigoTipoDocumento: json["CodigoTipoDocumento"],
        descripcionMotivo: json["DescripcionMotivo"],
        descripcionOtroMotivo: json["DescripcionOtroMotivo"],
        descripcionVinculo: json["DescripcionVinculo"],
        descripcionOtroVinculo: json["DescripcionOtroVinculo"],
        descripcionTipoDocumento: json["DescripcionTipoDocumento"],
        descripcionNacionalidad: json["DescripcionNacionalidad"],
        numeroDocumento: json["NumeroDocumento"],
        idVerificacion: json["IdVerificacion"],
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        correoRemitente: json["CorreoRemitente"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoCliente": codigoCliente,
        "NombreCliente": nombreCliente,
        "TipoPersona": tipoPersona,
        "DireccionCorreoElectronico": direccionCorreoElectronico,
        "IndicadorBanzarizado": indicadorBanzarizado,
        "NumeroTelefonoSMS": numeroTelefonoSms,
        "NumeroTelefonoOficina": numeroTelefonoOficina,
        "NumeroTelefonoCasa": numeroTelefonoCasa,
        "DNI": dni,
        "Sexo": sexo,
        "Nombres": nombres,
        "ApellidoPaterno": apellidoPaterno,
        "ApellidoMaterno": apellidoMaterno,
        "PersonaNatural": personaNatural,
        "DireccionPorDefecto": direccionPorDefecto,
        "CategoriaCliente": categoriaCliente,
        "CodigoTipoTarjeta": codigoTipoTarjeta,
        "DescripcionTipoTarjeta": descripcionTipoTarjeta,
        "NombreComercial": nombreComercial,
        "CodigoTipoDocumento": codigoTipoDocumento,
        "DescripcionMotivo": descripcionMotivo,
        "DescripcionOtroMotivo": descripcionOtroMotivo,
        "DescripcionVinculo": descripcionVinculo,
        "DescripcionOtroVinculo": descripcionOtroVinculo,
        "DescripcionTipoDocumento": descripcionTipoDocumento,
        "DescripcionNacionalidad": descripcionNacionalidad,
        "NumeroDocumento": numeroDocumento,
        "IdVerificacion": idVerificacion,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "CorreoRemitente": correoRemitente,
      };
}

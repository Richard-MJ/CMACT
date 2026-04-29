class DatosCliente {
  final String codigo;
  final String nombreCompleto;
  final String correoElectronico;
  final String numeroTelefonoSms;
  final String numeroTelefonoOficina;
  final String numeroTelefonoCasa;
  final String dni;
  final String sexo;
  final String tipoPersona;
  final String nombres;
  final String apellidoPaterno;
  final String apellidoMaterno;
  final bool personaNatural;
  final String codigoTipoTarjeta;
  final String descripcionTipoTarjeta;
  final String nombreComercial;
  final bool indicadorMenorEdad;

  DatosCliente({
    required this.codigo,
    required this.nombreCompleto,
    required this.correoElectronico,
    required this.numeroTelefonoSms,
    required this.numeroTelefonoOficina,
    required this.numeroTelefonoCasa,
    required this.dni,
    required this.sexo,
    required this.tipoPersona,
    required this.nombres,
    required this.apellidoPaterno,
    required this.apellidoMaterno,
    required this.personaNatural,
    required this.codigoTipoTarjeta,
    required this.descripcionTipoTarjeta,
    required this.nombreComercial,
    required this.indicadorMenorEdad,
  });

  factory DatosCliente.fromJson(Map<String, dynamic> json) => DatosCliente(
        codigo: json["Codigo"],
        nombreCompleto: json["NombreCompleto"],
        correoElectronico: json["CorreoElectronico"] ?? "",
        numeroTelefonoSms: json["NumeroTelefonoSMS"],
        numeroTelefonoOficina: json["NumeroTelefonoOficina"],
        numeroTelefonoCasa: json["NumeroTelefonoCasa"],
        dni: json["DNI"],
        sexo: json["Sexo"],
        tipoPersona: json["TipoPersona"],
        nombres: json["Nombres"],
        apellidoPaterno: json["ApellidoPaterno"],
        apellidoMaterno: json["ApellidoMaterno"],
        personaNatural: json["PersonaNatural"],
        codigoTipoTarjeta: json["CodigoTipoTarjeta"],
        descripcionTipoTarjeta: json["DescripcionTipoTarjeta"],
        nombreComercial: json["NombreComercial"],
        indicadorMenorEdad: json["IndicadorMenorEdad"],
      );

  Map<String, dynamic> toJson() => {
        "Codigo": codigo,
        "NombreCompleto": nombreCompleto,
        "CorreoElectronico": correoElectronico,
        "NumeroTelefonoSMS": numeroTelefonoSms,
        "NumeroTelefonoOficina": numeroTelefonoOficina,
        "NumeroTelefonoCasa": numeroTelefonoCasa,
        "DNI": dni,
        "Sexo": sexo,
        "TipoPersona": tipoPersona,
        "Nombres": nombres,
        "ApellidoPaterno": apellidoPaterno,
        "ApellidoMaterno": apellidoMaterno,
        "PersonaNatural": personaNatural,
        "CodigoTipoTarjeta": codigoTipoTarjeta,
        "DescripcionTipoTarjeta": descripcionTipoTarjeta,
        "NombreComercial": nombreComercial,
        "IndicadorMenorEdad": indicadorMenorEdad
      };
}

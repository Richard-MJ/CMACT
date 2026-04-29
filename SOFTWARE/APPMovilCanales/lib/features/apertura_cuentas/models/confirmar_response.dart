class ConfirmarResponse {
  final String temaMensaje;
  final String transaccionRealizada;
  final String tipoCuenta;
  final String nroCuenta;
  final String nroCuentaCci;
  final String plazo;
  final String fechaVencimiento;
  final String montoApertura;
  final String moneda;
  final String tea;
  final String? trea;
  final String agencia;
  final String fechaHoraTran;
  final String nombreMoneda;
  final String nombreCliente;
  final String numeroCuentaOrigen;
  final String idOperacionTts;
  final String montoDebito;
  final String codigoSistema;

  ConfirmarResponse({
    required this.temaMensaje,
    required this.transaccionRealizada,
    required this.tipoCuenta,
    required this.nroCuenta,
    required this.nroCuentaCci,
    required this.plazo,
    required this.fechaVencimiento,
    required this.montoApertura,
    required this.moneda,
    required this.tea,
    required this.trea,
    required this.agencia,
    required this.fechaHoraTran,
    required this.nombreMoneda,
    required this.nombreCliente,
    required this.numeroCuentaOrigen,
    required this.idOperacionTts,
    required this.montoDebito,
    required this.codigoSistema,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        temaMensaje: json["TemaMensaje"],
        transaccionRealizada: json["TransaccionRealizada"],
        tipoCuenta: json["TipoCuenta"],
        nroCuenta: json["NroCuenta"],
        nroCuentaCci: json["NroCuentaCci"],
        plazo: json["Plazo"],
        fechaVencimiento: json["FechaVencimiento"],
        montoApertura: json["MontoApertura"],
        moneda: json["Moneda"],
        tea: json["Tea"],
        trea: json["Trea"],
        agencia: json["Agencia"],
        fechaHoraTran: json["FechaHoraTran"],
        nombreMoneda: json["NombreMoneda"],
        nombreCliente: json["NombreCliente"],
        numeroCuentaOrigen: json["NumeroCuentaOrigen"],
        idOperacionTts: json["IdOperacionTts"],
        montoDebito: json["MontoDebito"],
        codigoSistema: json["CodigoSistema"],
      );

  Map<String, dynamic> toJson() => {
        "TemaMensaje": temaMensaje,
        "TransaccionRealizada": transaccionRealizada,
        "TipoCuenta": tipoCuenta,
        "NroCuenta": nroCuenta,
        "NroCuentaCci": nroCuentaCci,
        "Plazo": plazo,
        "FechaVencimiento": fechaVencimiento,
        "MontoApertura": montoApertura,
        "Moneda": moneda,
        "Tea": tea,
        "Trea": trea,
        "Agencia": agencia,
        "FechaHoraTran": fechaHoraTran,
        "NombreMoneda": nombreMoneda,
        "NombreCliente": nombreCliente,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "IdOperacionTts": idOperacionTts,
        "MontoDebito": montoDebito,
        "CodigoSistema": codigoSistema,
      };
}

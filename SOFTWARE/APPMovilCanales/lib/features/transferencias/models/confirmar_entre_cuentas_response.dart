class ConfirmarResponseTransEntreCuentas {
  final int numeroOperacion;
  final String temaMensaje;
  final String? tipoCuentaOrigen;
  final String numeroCuentaOrigen;
  final String nombreMoneda;
  final String nombreClienteBeneficiario;
  final String? tipoCuentaDestino;
  final String numeroCuentaDestino;
  final double montoTransferencia;
  final double montoItf;
  final int idOperacionTts;
  final DateTime fechaOperacion;
  final double montoTipoCambio;
  final String? simboloMoneda;
  
  final String? descripcionOperacion;
  final String? nombreCliente;
  final String? simboloMonedaProductoOrigen;
  final String? simboloMonedaProductoDestino;
  final double? importeCargado;
  final double? importeRecibir;

  ConfirmarResponseTransEntreCuentas({
    required this.numeroOperacion,
    required this.temaMensaje,
    this.tipoCuentaOrigen,
    required this.numeroCuentaOrigen,
    required this.nombreMoneda,
    required this.nombreClienteBeneficiario,
    this.tipoCuentaDestino,
    required this.numeroCuentaDestino,
    required this.montoTransferencia,
    required this.montoItf,
    required this.idOperacionTts,
    required this.fechaOperacion,
    this.simboloMoneda,
    required this.montoTipoCambio,
    this.descripcionOperacion,
    this.nombreCliente,
    this.simboloMonedaProductoOrigen,
    this.simboloMonedaProductoDestino,
    this.importeCargado,
    this.importeRecibir,
  });

  factory ConfirmarResponseTransEntreCuentas.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponseTransEntreCuentas(
        numeroOperacion: json["NumeroOperacion"],
        temaMensaje: json["TemaMensaje"],
        tipoCuentaOrigen: json["TipoCuentaOrigen"],
        numeroCuentaOrigen: json["NumeroCuentaOrigen"],
        nombreMoneda: json["NombreMoneda"],
        nombreClienteBeneficiario: json["NombreClienteBeneficiario"],
        tipoCuentaDestino: json["TipoCuentaDestino"],
        numeroCuentaDestino: json["NumeroCuentaDestino"],
        montoTransferencia: json["MontoTransferencia"]?.toDouble(),
        montoItf: json["MontoItf"]?.toDouble(),
        idOperacionTts: json["IdOperacionTts"],
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        simboloMoneda: json["SimboloMoneda"],
        montoTipoCambio: json["MontoTipoCambio"]?.toDouble(),
        descripcionOperacion: json["DescripcionOperacion"],
        nombreCliente: json["NombreCliente"],
        simboloMonedaProductoOrigen: json["SimboloMonedaProductoOrigen"],
        simboloMonedaProductoDestino: json["SimboloMonedaProductoDestino"],
        importeCargado: json["ImporteCargado"]?.toDouble(),
        importeRecibir: json["ImporteRecibir"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "NumeroOperacion": numeroOperacion,
        "TemaMensaje": temaMensaje,
        "TipoCuentaOrigen": tipoCuentaOrigen,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "NombreMoneda": nombreMoneda,
        "NombreClienteBeneficiario": nombreClienteBeneficiario,
        "TipoCuentaDestino": tipoCuentaDestino,
        "NumeroCuentaDestino": numeroCuentaDestino,
        "MontoTransferencia": montoTransferencia,
        "MontoItf": montoItf,
        "IdOperacionTts": idOperacionTts,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "SimboloMoneda": simboloMoneda,
        "MontoTipoCambio": montoTipoCambio,
        "DescripcionOperacion": descripcionOperacion,
        "NombreCliente": nombreCliente,
        "SimboloMonedaProductoOrigen": simboloMonedaProductoOrigen,
        "SimboloMonedaProductoDestino": simboloMonedaProductoDestino,
        "ImporteCargado": importeCargado,
        "ImporteRecibir": importeRecibir,
      };
}

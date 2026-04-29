abstract class SelectCuentaOption {
  final String alias;
  final double montoSaldo;
  final String simboloMonedaProducto;
  final String numeroProducto;

  SelectCuentaOption({
    required this.alias,
    required this.numeroProducto,
    required this.simboloMonedaProducto,
    required this.montoSaldo,
  });
}

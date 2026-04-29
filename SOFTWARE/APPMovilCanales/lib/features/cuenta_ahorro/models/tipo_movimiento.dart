enum TipoMovimiento {
  movimiento,
  congelado;

  String descripcion() {
    switch (this) {
      case TipoMovimiento.movimiento:
        return 'Movimientos';
      case TipoMovimiento.congelado:
        return 'Movimientos congelados';
    }
  }
}

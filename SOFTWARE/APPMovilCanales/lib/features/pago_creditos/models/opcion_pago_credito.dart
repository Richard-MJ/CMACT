enum TipoPagoCredito {
  abono,
  anticipo,
  cancelacion,
  adelanto;

  String obtenerConcepto() {
    switch (this) {
      case TipoPagoCredito.abono:
        return 'Abono';
      case TipoPagoCredito.anticipo:
        return 'Anticipo';
      case TipoPagoCredito.cancelacion:
        return 'Cancelacion';
      case TipoPagoCredito.adelanto:
        return 'Adelanto';
    }
  }

  String obtenerDescripcion() {
    switch (this) {
      case TipoPagoCredito.abono:
        return 'Abono de créditos';
      case TipoPagoCredito.anticipo:
        return 'Pago anticipado de créditos';
      case TipoPagoCredito.cancelacion:
        return 'Cancelación de créditos';
      case TipoPagoCredito.adelanto:
         return 'Adelanto de cuotas';
    }
  }

  String obtenerNombre() {
    switch (this) {
      case TipoPagoCredito.abono:
        return 'Abono';
      case TipoPagoCredito.anticipo:
        return 'Adelanto y anticipo';
      case TipoPagoCredito.cancelacion:
        return 'Cancelación';
      case TipoPagoCredito.adelanto:
         return 'Adelanto';
    }
  }

  int obtenerIdentificador() {
    switch (this) {
      case TipoPagoCredito.anticipo:
        return 1;
      default:
        return 0;
    }
  }

  bool esAnticipo() {
    return this == TipoPagoCredito.anticipo;// || this == TipoPagoCredito.adelanto;
  }

  bool estaDeshabilitado(
      {required double montoTotalPago, required double montoSaldoCancelacion}) {
    if (this == TipoPagoCredito.anticipo) { //|| this == TipoPagoCredito.adelanto) {
      return montoTotalPago >= montoSaldoCancelacion;
    }
    return false;
  }
}

enum TipoAnticipo {
  defecto,
  monto,
  plazo,
  adelantoCuota;

  String obtenerConceptoAnticipo() {
    switch (this) {
      case TipoAnticipo.monto:
      case TipoAnticipo.plazo:
        return 'Anticipo';
      case TipoAnticipo.adelantoCuota:
        return 'Adelanto';
      case TipoAnticipo.defecto:
        return '';
    }
  }

  String obtenerDescripcion() {
    switch (this) {
      case TipoAnticipo.defecto:
        return '';
      case TipoAnticipo.adelantoCuota:
        return 'Adelanto de cuotas';
      case TipoAnticipo.monto:
        return 'Pago anticipado - reducir monto cuotas';
      case TipoAnticipo.plazo:
        return 'Pago anticipado - reducir número cuotas';
    }
  }

  int obtenerIdentificador() {
    switch (this) {
      case TipoAnticipo.defecto:
        return 0;
      case TipoAnticipo.adelantoCuota:
        return -1;
      case TipoAnticipo.monto:
        return 1;
      case TipoAnticipo.plazo:
        return 2;
    }
  }
}

enum TipoSolicitante {
  titular,
  terceroAcreditado,
  tercero;

  int obtenerIdentificador() {
    switch (this) {
      case TipoSolicitante.titular:
        return 1;
      case TipoSolicitante.terceroAcreditado:
        return 2;
      case TipoSolicitante.tercero:
        return 3;
    }
  }
}

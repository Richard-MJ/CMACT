import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum MontoAmortizarError { empty, minAnticipo, maxCancelacion }

class MontoAnticipo extends FormzInput<String, MontoAmortizarError> {
  const MontoAnticipo.pure(
    String value, {
    this.montoMinimoAnticipo,
    this.montoCancelacion,
    this.simboloMoneda,
    this.ignorarMinimo = false,
  }) : super.pure(value);
  const MontoAnticipo.dirty(
    String value, {
    this.montoMinimoAnticipo,
    this.montoCancelacion,
    this.simboloMoneda,
    this.ignorarMinimo = false,
  }) : super.dirty(value);

  final double? montoMinimoAnticipo;
  final double? montoCancelacion;
  final String? simboloMoneda;
  final bool ignorarMinimo;

  MontoAnticipo copyWith({
    String? value,
    String? simboloMoneda,
    bool? isPure,
    double? montoCancelacion,
    double? montoMinimoAnticipo,
    bool? ignorarMinimo,
  }) {
    if (isPure ?? this.isPure) {
      return MontoAnticipo.pure(
        value ?? this.value,
        montoMinimoAnticipo: montoMinimoAnticipo ?? this.montoMinimoAnticipo,
        montoCancelacion: montoCancelacion ?? this.montoCancelacion,
        simboloMoneda: simboloMoneda ?? this.simboloMoneda,
        ignorarMinimo: ignorarMinimo ?? this.ignorarMinimo,
      );
    } else {
      return MontoAnticipo.dirty(
        value ?? this.value,
        montoMinimoAnticipo: montoMinimoAnticipo ?? this.montoMinimoAnticipo,
        montoCancelacion: montoCancelacion ?? this.montoCancelacion,
        simboloMoneda: simboloMoneda ?? this.simboloMoneda,
        ignorarMinimo: ignorarMinimo ?? this.ignorarMinimo,
      );
    }
  }

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoAmortizarError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == MontoAmortizarError.minAnticipo && !ignorarMinimo){
      return 'El monto mínimo de anticipo debe de ser mayor a\n(${CtUtils.formatCurrency(
        montoMinimoAnticipo,
        simboloMoneda,
      )}).';
    }
    if (displayError == MontoAmortizarError.maxCancelacion) {
      return 'El monto no puede ser igual o mayor al monto de cancelación del crédito\n(${CtUtils.formatCurrency(
        montoCancelacion,
        simboloMoneda,
      )}).';
    }
    return null;
  }

  @override
  MontoAmortizarError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoAmortizarError.empty;
    if (montoMinimoAnticipo != null && double.parse(value) <= montoMinimoAnticipo! && !ignorarMinimo) {
      return MontoAmortizarError.minAnticipo;
    }
    if (montoCancelacion != null && double.parse(value) >= montoCancelacion!) {
      return MontoAmortizarError.maxCancelacion;
    }
    return null;
  }
}

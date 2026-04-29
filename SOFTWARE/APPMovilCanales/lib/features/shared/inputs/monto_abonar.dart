import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum MontoAbonarError { empty, maxCuota }

class MontoAbonar extends FormzInput<String, MontoAbonarError> {
  const MontoAbonar.pure(
    String value, {
    this.montoCuota,
    this.simboloMoneda,
  }) : super.pure(value);
  const MontoAbonar.dirty(
    String value, {
    this.montoCuota,
    this.simboloMoneda,
  }) : super.dirty(value);

  final double? montoCuota;
  final String? simboloMoneda;

  MontoAbonar copyWith({
    String? value,
    String? simboloMoneda,
    bool? isPure,
    double? montoCuota,
  }) {
    if (isPure ?? this.isPure) {
      return MontoAbonar.pure(
        value ?? this.value,
        montoCuota: montoCuota ?? this.montoCuota,
        simboloMoneda: simboloMoneda ?? this.simboloMoneda,
      );
    } else {
      return MontoAbonar.dirty(
        value ?? this.value,
        montoCuota: montoCuota ?? this.montoCuota,
        simboloMoneda: simboloMoneda ?? this.simboloMoneda,
      );
    }
  }

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoAbonarError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == MontoAbonarError.maxCuota) {
      return 'El monto no puede exceder la cuota pendiente\n(${CtUtils.formatCurrency(
        montoCuota,
        simboloMoneda,
      )}).';
    }
    return null;
  }

  @override
  MontoAbonarError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoAbonarError.empty;
    if (montoCuota != null && double.parse(value) > montoCuota!) {
      return MontoAbonarError.maxCuota;
    }
    return null;
  }
}

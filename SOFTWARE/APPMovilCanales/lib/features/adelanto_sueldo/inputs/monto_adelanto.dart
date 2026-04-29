import 'package:formz/formz.dart';

enum MontoAdelantoError { empty, montoMaximo }

class MontoAdelanto extends FormzInput<String, MontoAdelantoError> {
  const MontoAdelanto.pure(String value, {this.montoMaximo})
      : super.pure(value);
  const MontoAdelanto.dirty(String value, {this.montoMaximo})
      : super.dirty(value);

  MontoAdelanto copyWith({String? value, double? montoMaximo, bool? isPure}) {
    if (isPure ?? this.isPure) {
      return MontoAdelanto.pure(value ?? this.value,
          montoMaximo: montoMaximo ?? this.montoMaximo);
    } else {
      return MontoAdelanto.dirty(value ?? this.value,
          montoMaximo: montoMaximo ?? this.montoMaximo);
    }
  }

  final double? montoMaximo;

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoAdelantoError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == MontoAdelantoError.montoMaximo) {
      return 'Este monto supera el máximo aprobado.';
    }

    return null;
  }

  @override
  MontoAdelantoError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoAdelantoError.empty;
    if (montoMaximo != null && double.parse(value) > montoMaximo!) {
      return MontoAdelantoError.montoMaximo;
    }

    return null;
  }
}

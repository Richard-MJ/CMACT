import 'package:formz/formz.dart';

enum IngresoMensualError { empty, saldoMaximo }

class IngresoMensual extends FormzInput<String, IngresoMensualError> {
  const IngresoMensual.pure(String value) : super.pure(value);
  const IngresoMensual.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == IngresoMensualError.empty) {
      return 'Debes completar este campo.';
    }

    return null;
  }

  @override
  IngresoMensualError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return IngresoMensualError.empty;

    return null;
  }
}

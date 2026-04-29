import 'package:formz/formz.dart';

enum MontoError { empty }

class Monto extends FormzInput<String, MontoError> {
  const Monto.pure(String value) : super.pure(value);
  const Monto.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoError.empty) {
      return 'Debes completar este campo.';
    }

    return null;
  }

  @override
  MontoError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoError.empty;

    return null;
  }
}

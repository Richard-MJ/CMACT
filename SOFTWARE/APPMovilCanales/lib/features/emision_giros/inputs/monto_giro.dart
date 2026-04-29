import 'package:formz/formz.dart';

enum MontoGiroError { empty, saldoMaximo }

class MontoGiro extends FormzInput<String, MontoGiroError> {
  const MontoGiro.pure(String value) : super.pure(value);
  const MontoGiro.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoGiroError.empty) {
      return 'Debes completar este campo.';
    }

    return null;
  }

  @override
  MontoGiroError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoGiroError.empty;

    return null;
  }
}

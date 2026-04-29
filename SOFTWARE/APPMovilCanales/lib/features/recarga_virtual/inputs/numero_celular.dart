import 'package:formz/formz.dart';

enum NumeroCelularError { empty, length, format }

class NumeroCelular extends FormzInput<String, NumeroCelularError> {
  const NumeroCelular.pure(String value) : super.pure(value);
  const NumeroCelular.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == NumeroCelularError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == NumeroCelularError.length) {
      return 'Debe tener 9 dígitos';
    }
    if (displayError == NumeroCelularError.format) {
      return 'Numero invalido';
    }
    return null;
  }

  @override
  NumeroCelularError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return NumeroCelularError.empty;
    if (value.length != 9) return NumeroCelularError.length;
    if (value == '111111111' ||
        value == '222222222' ||
        value == '333333333' ||
        value == '444444444' ||
        value == '555555555' ||
        value == '666666666' ||
        value == '777777777' ||
        value == '888888888' ||
        value == '999999999' ||
        value == '000000000') {
      return NumeroCelularError.format;
    }

    return null;
  }
}

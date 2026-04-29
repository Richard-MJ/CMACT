import 'package:formz/formz.dart';

enum NumeroCuentaTerceroError { empty, length }

class NumeroCuentaTercero extends FormzInput<String, NumeroCuentaTerceroError> {
  const NumeroCuentaTercero.pure(String value) : super.pure(value);
  const NumeroCuentaTercero.dirty(String value) : super.dirty(value);

  final numeroDigitos = 15;
  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == NumeroCuentaTerceroError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == NumeroCuentaTerceroError.length) {
      return 'La cuenta ingresada no es correcta';
    }

    return null;
  }

  @override
  NumeroCuentaTerceroError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return NumeroCuentaTerceroError.empty;
    }
    if (value.length < numeroDigitos) {
      return NumeroCuentaTerceroError.length;
    }

    return null;
  }
}

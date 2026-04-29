import 'package:formz/formz.dart';

enum NumeroTarjetaError { empty, length }

class NumeroTarjeta extends FormzInput<String, NumeroTarjetaError> {
  const NumeroTarjeta.pure(String value) : super.pure(value);
  const NumeroTarjeta.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == NumeroTarjetaError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == NumeroTarjetaError.length) {
      return 'Debe tener 16 dígitos';
    }

    return null;
  }

  @override
  NumeroTarjetaError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return NumeroTarjetaError.empty;
    if (value.length < 16) return NumeroTarjetaError.length;

    return null;
  }
}

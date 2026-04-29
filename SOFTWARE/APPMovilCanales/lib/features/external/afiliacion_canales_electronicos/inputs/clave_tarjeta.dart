import 'package:formz/formz.dart';

enum ClaveTarjetaError { empty, length }

class ClaveTarjeta extends FormzInput<String, ClaveTarjetaError> {
  const ClaveTarjeta.pure(String value) : super.pure(value);

  const ClaveTarjeta.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == ClaveTarjetaError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == ClaveTarjetaError.length) {
      return 'La clave debe tener 4 dígitos';
    }

    return null;
  }

  @override
  ClaveTarjetaError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return ClaveTarjetaError.empty;
    if (value.length < 4) return ClaveTarjetaError.length;

    return null;
  }
}

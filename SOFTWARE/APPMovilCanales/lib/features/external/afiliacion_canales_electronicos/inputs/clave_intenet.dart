import 'package:formz/formz.dart';

enum ClaveInternetError { empty, length }

class ClaveInternet extends FormzInput<String, ClaveInternetError> {
  const ClaveInternet.pure(String value) : super.pure(value);

  const ClaveInternet.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == ClaveInternetError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == ClaveInternetError.length) {
      return 'La clave debe tener 6 dígitos';
    }

    return null;
  }

  @override
  ClaveInternetError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return ClaveInternetError.empty;
    if (value.length < 6) return ClaveInternetError.length;

    return null;
  }
}

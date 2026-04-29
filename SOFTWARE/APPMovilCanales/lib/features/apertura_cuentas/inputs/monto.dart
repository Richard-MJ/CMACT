import 'package:formz/formz.dart';

enum MontoAperturaError { empty }

class MontoApertura extends FormzInput<String, MontoAperturaError> {
  const MontoApertura.pure(String value) : super.pure(value);
  const MontoApertura.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoAperturaError.empty) {
      return 'Debes completar este campo.';
    }

    return null;
  }

  @override
  MontoAperturaError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoAperturaError.empty;

    return null;
  }
}

import 'package:formz/formz.dart';

enum MontoTransError { empty }

class MontoTrans extends FormzInput<String, MontoTransError> {
  const MontoTrans.pure(String value) : super.pure(value);
  const MontoTrans.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoTransError.empty) {
      return 'Debes completar este campo.';
    }

    return null;
  }

  @override
  MontoTransError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoTransError.empty;

    return null;
  }
}

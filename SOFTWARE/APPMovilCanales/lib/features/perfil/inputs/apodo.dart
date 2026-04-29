import 'package:formz/formz.dart';

enum ApodoError { length, format }

class Apodo extends FormzInput<String, ApodoError> {
  const Apodo.pure(String value) : super.pure(value);
  const Apodo.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    return null;
  }

  @override
  ApodoError? validator(String value) {
    return null;
  }
}

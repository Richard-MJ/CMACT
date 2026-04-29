import 'package:formz/formz.dart';

enum DiasDpfError { empty }

class DiasDpf extends FormzInput<String, DiasDpfError> {
  const DiasDpf.pure(String value) : super.pure(value);
  const DiasDpf.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == DiasDpfError.empty) {
      return 'Debes completar este campo.';
    }

    return null;
  }

  @override
  DiasDpfError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return DiasDpfError.empty;

    return null;
  }
}

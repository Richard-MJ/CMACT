import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum EmailError { empty, format }

class Email extends FormzInput<String, EmailError> {
  static final RegExp emailRegExp = RegExp(
    r'^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$',
  );

  const Email.pure(String value) : super.pure(value);
  const Email.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == EmailError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == EmailError.format) {
      return 'No tiene formato de correo electrónico';
    }

    return null;
  }

  @override
  EmailError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return EmailError.empty;
    if (!emailRegExp.hasMatch(value)) return EmailError.format;
    if (!CtUtils.isSafeInput(value)) {
      return EmailError.format;
    }
    return null;
  }
}

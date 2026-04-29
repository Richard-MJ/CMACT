import 'package:formz/formz.dart';

enum CuotasError { empty, saldoMaximo }

class Cuotas extends FormzInput<String, CuotasError> {
  const Cuotas.pure(String value) : super.pure(value);
  const Cuotas.dirty(String value) : super.dirty(value);

  final minimoCuota = 3;
  final maximoCuota = 60;

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == CuotasError.empty) {
      return 'Debes completar este campo.';
    } else if (displayError == CuotasError.saldoMaximo) {
      return 'La cuota debe estar entre $minimoCuota y $maximoCuota.';
    }

    return null;
  }

  @override
  CuotasError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return CuotasError.empty;

    final int? cuota = int.tryParse(value);
    if (cuota == null || cuota < minimoCuota || cuota > maximoCuota) {
      return CuotasError.saldoMaximo;
    }

    return null;
  }
}

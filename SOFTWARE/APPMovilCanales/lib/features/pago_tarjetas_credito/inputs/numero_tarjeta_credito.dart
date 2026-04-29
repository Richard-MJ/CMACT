import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum NumeroTarjetaCreditoError {
  empty,
  invalid,
}

class NumeroTarjetaCredito
    extends FormzInput<String, NumeroTarjetaCreditoError> {
  const NumeroTarjetaCredito.pure(super.value) : super.pure();
  const NumeroTarjetaCredito.dirty(super.value) : super.dirty();

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == NumeroTarjetaCreditoError.empty) {
      return 'Debes completar este campo.';
    } 
    
    if (displayError == NumeroTarjetaCreditoError.invalid) {
      return 'El número de tarjeta ingresado no es correcta.';
    }

    return null;
  }

  @override
  NumeroTarjetaCreditoError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return NumeroTarjetaCreditoError.empty;
    }

    if(!CtUtils.validarEstructuraNumeroTarjeta(value.trim())) {
      return NumeroTarjetaCreditoError.invalid;
    }

    return null;
  }
}

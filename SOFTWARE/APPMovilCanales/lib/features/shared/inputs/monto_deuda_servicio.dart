import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum MontoDeudaServicioError { empty, minDeuda, maxDeuda }

class MontoDeudaServicio extends FormzInput<String, MontoDeudaServicioError> {
  const MontoDeudaServicio.pure(
    String value, {
    this.montoMinimoDeduda,
    this.montoMaximoDeuda,
    this.simboloMoneda,
  }) : super.pure(value);
  const MontoDeudaServicio.dirty(
    String value, {
    this.montoMinimoDeduda,
    this.montoMaximoDeuda,
    this.simboloMoneda,
  }) : super.dirty(value);

  final double? montoMinimoDeduda;
  final double? montoMaximoDeuda;
  final String? simboloMoneda;

  MontoDeudaServicio copyWith({
    String? value,
    String? simboloMoneda,
    bool? isPure,
    double? montoMinimoDeduda,
    double? montoMaximoDeuda,
  }) {
    if (isPure ?? this.isPure) {
      return MontoDeudaServicio.pure(
        value ?? this.value,
        montoMinimoDeduda: montoMinimoDeduda ?? this.montoMinimoDeduda,
        montoMaximoDeuda: montoMaximoDeuda ?? this.montoMaximoDeuda,
        simboloMoneda: simboloMoneda ?? this.simboloMoneda,
      );
    } else {
      return MontoDeudaServicio.dirty(
        value ?? this.value,
        montoMinimoDeduda: montoMinimoDeduda ?? this.montoMinimoDeduda,
        montoMaximoDeuda: montoMaximoDeuda ?? this.montoMaximoDeuda,
        simboloMoneda: simboloMoneda ?? this.simboloMoneda,
      );
    }
  }

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoDeudaServicioError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == MontoDeudaServicioError.minDeuda) {
      return 'El monto mínimo a pagar debe de ser \n(${CtUtils.formatCurrency(
        montoMinimoDeduda,
        simboloMoneda,
      )}).';
    }
    if (displayError == MontoDeudaServicioError.maxDeuda) {
      return 'El monto no puede ser mayor a \n(${CtUtils.formatCurrency(
        montoMaximoDeuda,
        simboloMoneda,
      )}).';
    }
    return null;
  }

  @override
  MontoDeudaServicioError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return MontoDeudaServicioError.empty;
    }
    if (montoMinimoDeduda != null && double.parse(value) < montoMinimoDeduda!) {
      return MontoDeudaServicioError.minDeuda;
    }
    if (montoMaximoDeuda != null && double.parse(value) > montoMaximoDeuda!) {
      return MontoDeudaServicioError.maxDeuda;
    }
    return null;
  }
}

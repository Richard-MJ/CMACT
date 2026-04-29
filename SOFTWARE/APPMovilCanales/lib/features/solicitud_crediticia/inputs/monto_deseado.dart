import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/tipo_moneda.dart';
import 'package:formz/formz.dart';

enum MontoDeseadoError { empty, montoMinimo }

class MontoDeseado extends FormzInput<String, MontoDeseadoError> {
  const MontoDeseado.pure(String value, this.tipoMoneda) : super.pure(value);
  const MontoDeseado.dirty(String value, this.tipoMoneda) : super.dirty(value);

  final TipoMoneda? tipoMoneda;
  final double montoMinimo = 300.00;

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoDeseadoError.empty) {
      return 'Debes completar este campo.';
    }

    if (displayError == MontoDeseadoError.montoMinimo) {
      return 'El monto mínimo es ${CtUtils.formatCurrency(montoMinimo, tipoMoneda?.simbolo)}';
    }

    return null;
  }

  @override
  MontoDeseadoError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoDeseadoError.empty;

    final double? montoDeseado = double.tryParse(value);
    if (montoDeseado == null || montoDeseado < montoMinimo) {
      return MontoDeseadoError.montoMinimo;
    }

    return null;
  }
}

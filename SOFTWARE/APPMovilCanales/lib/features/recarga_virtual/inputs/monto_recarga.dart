import 'package:formz/formz.dart';

enum MontoRecargaError { empty, saldoMaximo }

class MontoRecarga extends FormzInput<String, MontoRecargaError> {
  const MontoRecarga.pure(String value, {this.saldoMaximo}) : super.pure(value);
  const MontoRecarga.dirty(String value, {this.saldoMaximo}) : super.dirty(value);

  MontoRecarga copyWith({String? value, double? saldoMaximo, bool? isPure}) {
    if (isPure ?? this.isPure) {
      return MontoRecarga.pure(value ?? this.value,
          saldoMaximo: saldoMaximo ?? this.saldoMaximo);
    } else {
      return MontoRecarga.dirty(value ?? this.value,
          saldoMaximo: saldoMaximo ?? this.saldoMaximo);
    }
  }

  final double? saldoMaximo;

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoRecargaError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == MontoRecargaError.saldoMaximo) {
      return 'El monto supera tu saldo disponible.';
    }

    return null;
  }

  @override
  MontoRecargaError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoRecargaError.empty;
    if (saldoMaximo != null && double.parse(value) > saldoMaximo!) {
      return MontoRecargaError.saldoMaximo;
    }

    return null;
  }
}

import 'package:formz/formz.dart';

enum MontoTransferirError { empty, montoMaximo, montoMinimo }

class MontoTransferir extends FormzInput<String, MontoTransferirError> {
  const MontoTransferir.pure(String value, {this.montoMaximo, this.montoMinimo})
      : super.pure(value);
  const MontoTransferir.dirty(String value, {this.montoMaximo, this.montoMinimo})
      : super.dirty(value);

  MontoTransferir copyWith({String? value, double? montoMaximo, double? montoMinimo, bool? isPure}) {
    if (isPure ?? this.isPure) {
      return MontoTransferir.pure(value ?? this.value,
          montoMaximo: montoMaximo ?? this.montoMaximo,
          montoMinimo: montoMinimo ?? this.montoMinimo);
    } else {
      return MontoTransferir.dirty(value ?? this.value,
          montoMaximo: montoMaximo ?? this.montoMaximo,
          montoMinimo: montoMinimo ?? this.montoMinimo);
    }
  }

  final double? montoMaximo;
  final double? montoMinimo;

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == MontoTransferirError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == MontoTransferirError.montoMinimo) {
      return 'El monto minimo de transferencia es S/ $montoMinimo .';
    }
    if (displayError == MontoTransferirError.montoMaximo) {
      return 'Este monto supera el máximo aprobado.';
    }

    return null;
  }

  @override
  MontoTransferirError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) return MontoTransferirError.empty;
    if (montoMinimo != null && double.parse(value) < montoMinimo!) return MontoTransferirError.montoMinimo;
    if (montoMaximo != null && double.parse(value) > montoMaximo!) return MontoTransferirError.montoMaximo;
    
    return null;
  }
}

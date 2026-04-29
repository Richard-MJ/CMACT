import 'package:formz/formz.dart';

enum MontoTransferirError { empty, montoCero, montoMaximo, montoMinimo }

class MontoTransferir extends FormzInput<String, MontoTransferirError> {
  const MontoTransferir.pure(super.value, {this.montoMaximo, this.montoMinimo})
      : super.pure();
  const MontoTransferir.dirty(super.value, {this.montoMaximo, this.montoMinimo})
      : super.dirty();

  final double? montoMaximo;
  final double? montoMinimo;

  MontoTransferir copyWith({
    String? value,
    double? montoMaximo,
    double? montoMinimo,
    bool? isPure,
  }) {
    if (isPure ?? this.isPure) {
      return MontoTransferir.pure(
        value ?? this.value,
        montoMaximo: montoMaximo ?? this.montoMaximo,
        montoMinimo: montoMinimo ?? this.montoMinimo,
      );
    } else {
      return MontoTransferir.dirty(
        value ?? this.value,
        montoMaximo: montoMaximo ?? this.montoMaximo,
        montoMinimo: montoMinimo ?? this.montoMinimo,
      );
    }
  }

  String? get errorMessage {
    if (isValid || isPure) return null;

  switch (displayError) {
    case MontoTransferirError.empty:
      return 'Debes completar este campo.';
    case MontoTransferirError.montoCero:
      return 'El monto ingresado no puede ser cero.';
    case MontoTransferirError.montoMinimo:
      return 'El monto mínimo de transferencia es S/ $montoMinimo.';
    case MontoTransferirError.montoMaximo:
      return 'Este monto supera el máximo aprobado.';
    default:
      return null;
  }
  }

  @override
  MontoTransferirError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return MontoTransferirError.empty;
    }

    final double? monto = double.tryParse(value);
    if (monto == null) {
      return MontoTransferirError.empty;
    }

    if (monto == 0) {
      return MontoTransferirError.montoCero;
    }

    if (montoMinimo != null && monto < montoMinimo!) {
      return MontoTransferirError.montoMinimo;
    }

    if (montoMaximo != null && monto > montoMaximo!) {
      return MontoTransferirError.montoMaximo;
    }

    return null;
  }
}
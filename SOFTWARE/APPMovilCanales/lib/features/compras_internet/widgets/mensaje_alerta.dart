import 'package:caja_tacna_app/features/shared/widgets/ct_alert_card.dart';
import 'package:flutter/material.dart';

class MensajeAlerta extends StatelessWidget {
  const MensajeAlerta({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtAlertCard(
      message:
          'Recuerda seleccionar una cuenta de ahorros para poder realizar compras por internet.',
    );
  }
}

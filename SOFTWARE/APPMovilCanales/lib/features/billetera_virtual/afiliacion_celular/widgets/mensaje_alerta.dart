import 'package:caja_tacna_app/features/shared/widgets/ct_alert_card.dart';
import 'package:flutter/material.dart';

class MensajeAlerta extends StatelessWidget {
  const MensajeAlerta({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtAlertCard(
      message:
          'Afilia tu cuenta de ahorros para poder enviar dinero en tiempo real a tus contactos.',
    );
  }
}

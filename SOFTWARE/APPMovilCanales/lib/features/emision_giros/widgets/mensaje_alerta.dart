import 'package:caja_tacna_app/features/shared/widgets/ct_alert_card.dart';
import 'package:flutter/material.dart';

class MensajeAlerta extends StatelessWidget {
  const MensajeAlerta({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtAlertCard(
        message:
            'Verifique toda la información del beneficiario, en caso no coincida con la de su documento oficial de identidad, el giro no podrá ser cobrado.');
  }
}

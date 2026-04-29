import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';

class DialogDesafiliarCelular extends StatelessWidget {
  const DialogDesafiliarCelular({super.key});

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
      elevation: 0,
      backgroundColor: AppColors.white,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24),
      contentPadding: const EdgeInsets.only(
        top: 18,
        left: 24,
        right: 24,
        bottom: 24,
      ),
      content: SizedBox(
        width: double.infinity,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Center(
              child: Text(
                'Desafiliar mi Billetera Virtual',
                style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.w800,
                  color: AppColors.black,
                  height: 28 / 20,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ),            
            const SizedBox(
              height: 12,
            ),
            const Text(
              textAlign: TextAlign.center,
              'Si desafilias tu Billetera Virtual no podrás enviar dinero en tiempo real a tus contactos. \n ¿Estás seguro(a) de continuar?',
              style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w400,
                color: AppColors.black,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 18,
            ),
            CtButton(
              text: 'Sí, continuar',
              onPressed: () {
                Navigator.of(context).pop(true);
              },
              borderRadius: 8,
              width: double.infinity,
            ),
            const SizedBox(
              height: 12,
            ),
            CtButton(
              text: 'Cancelar',
              onPressed: () {
                Navigator.of(context).pop(false);
              },
              borderRadius: 8,
              width: double.infinity,
              type: ButtonType.outline,
            )
          ],
        ),
      ),
    );
  }
}

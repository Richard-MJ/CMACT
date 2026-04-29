import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

class DialogAfiliarComprarInternet extends StatelessWidget {
  const DialogAfiliarComprarInternet({super.key});

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
      elevation: 0,
      backgroundColor: AppColors.white,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24),
      content: SizedBox(
        width: double.infinity,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Center(
              child: SvgPicture.asset(
                'assets/icons/alert-circle-2.svg',
                height: 84,
              ),    
            ),
            const SizedBox(
              height: 12,
            ),
            const Text(
              textAlign: TextAlign.center,
              'Recuerda afiliarte a compras por internet.',
              style: TextStyle(
                fontSize: 17,
                fontWeight: FontWeight.w600,
                color: AppColors.black,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 18,
            ),
            CtButton(
              text: 'Afiliarme',
              onPressed: () async {           
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

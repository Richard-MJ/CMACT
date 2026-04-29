import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class DialogDesafiliarToken extends StatelessWidget {
  const DialogDesafiliarToken({super.key});

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
        bottom: 48,
      ),
      titlePadding: const EdgeInsets.only(
        top: 18,
        left: 24,
        right: 18,
        bottom: 0,
      ),
      title: Row(
        mainAxisAlignment: MainAxisAlignment.end,
        children: [
          SizedBox(
            height: 36,
            width: 36,
            child: TextButton(
              style: TextButton.styleFrom(
                shape: const CircleBorder(),
                padding: EdgeInsets.zero,
              ),
              onPressed: () {
                Navigator.pop(context);
              },
              child: SvgPicture.asset(
                'assets/icons/x.svg',
                height: 24,
              ),
            ),
          ),
        ],
      ),
      content: SizedBox(
        width: double.infinity,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Text(
              'Desafiliar mi Token Digital',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w800,
                color: AppColors.black,
                height: 28 / 18,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 24,
            ),
            const Text(
              'Si desafilias tu Token Digital no podrás realizar compras ni operaciones en Caja Tacna App ni en Tu Caja por Internet Personas. ¿Estás seguro(a) de continuar?',
              style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w400,
                color: AppColors.black,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 24,
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

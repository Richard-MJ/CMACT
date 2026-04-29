import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class DialogTokenDigital extends StatelessWidget {
  const DialogTokenDigital({super.key});

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
      elevation: 0,
      backgroundColor: AppColors.primary50,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24),
      contentPadding: const EdgeInsets.only(
        top: 21,
        bottom: 30,
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
            height: 40,
            width: 40,
            child: TextButton(
              style: TextButton.styleFrom(
                shape: const CircleBorder(),
                padding: EdgeInsets.zero,
                backgroundColor: AppColors.primary100,
              ),
              onPressed: () {
                Navigator.of(context).pop(false);
              },
              child: SvgPicture.asset(
                'assets/icons/x.svg',
                height: 20,
                colorFilter: const ColorFilter.mode(
                  AppColors.primary700,
                  BlendMode.srcIn,
                ),
              ),
            ),
          ),
        ],
      ),
      content: Stack(
        children: [
          SizedBox(
            width: double.infinity,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                SvgPicture.asset(
                  'assets/icons/fondo-dialog-token.svg',
                ),
              ],
            ),
          ),
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 39),
            width: double.infinity,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                Center(
                  child: Image.asset(
                    'assets/images/logo_rojo.png',
                    width: 184,
                  ),
                ),
                const SizedBox(
                  height: 107,
                ),
                const Text(
                  'Realiza todas tus operaciones en línea',
                  style: TextStyle(
                    fontSize: 20,
                    fontWeight: FontWeight.w400,
                    color: AppColors.black,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  textAlign: TextAlign.end,
                ),
                const SizedBox(
                  height: 2,
                ),
                const Text(
                  'afiliándote al',
                  style: TextStyle(
                    fontSize: 32,
                    fontWeight: FontWeight.w600,
                    color: AppColors.primary700,
                    height: 1.3,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  textAlign: TextAlign.end,
                ),
                const Text(
                  'Token Digital',
                  style: TextStyle(
                    fontSize: 32,
                    fontWeight: FontWeight.w800,
                    color: AppColors.primary700,
                    height: 1.3,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  textAlign: TextAlign.end,
                ),
                const SizedBox(
                  height: 153,
                ),
                Center(
                  child: CtButton(
                    text: 'Quiero afiliarme',
                    onPressed: () {
                      Navigator.of(context).pop(true);
                    },
                    width: 162,
                  ),
                )
              ],
            ),
          ),
        ],
      ),
    );
  }
}

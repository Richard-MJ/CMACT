import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class OtrosProductosItem extends StatelessWidget {
  const OtrosProductosItem({
    super.key,
    required this.label,
    required this.onPressed,
    this.buttonRed = false,
    required this.icon,
  });

  final String label;
  final void Function() onPressed;
  final bool buttonRed;
  final String icon;

  @override
  Widget build(BuildContext context) {
    return CtCardButton(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 18),
      onPressed: onPressed,
      color: buttonRed ? AppColors.primary100 : AppColors.gray100,
      child: Row(
        children: [
          SvgPicture.asset(
            icon,
            height: 24,
            colorFilter: const ColorFilter.mode(
              AppColors.primary700,
              BlendMode.srcIn,
            ),
          ),
          const SizedBox(
            width: 8,
          ),
          Text(
            label,
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w500,
              color: buttonRed ? AppColors.primary700 : AppColors.gray900,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ],
      ),
    );
  }
}

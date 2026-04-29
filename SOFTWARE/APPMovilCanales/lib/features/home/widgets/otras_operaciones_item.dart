import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class OtrasOperacionesItem extends StatelessWidget {
  const OtrasOperacionesItem({
    super.key,
    required this.label,
    required this.onPressed,
    required this.icon,
  });

  final String label;
  final void Function() onPressed;
  final String icon;

  @override
  Widget build(BuildContext context) {
    return CtCardButton(
      padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 16),
      onPressed: onPressed,
      width: 158,
      color: null,
      gradient: AppColors.localLinearGrey,
      child: Column(
        children: [
          SvgPicture.asset(
            icon,
            width: 46.422,
            height: 50.704,
            colorFilter: const ColorFilter.mode(
              AppColors.primary700,
              BlendMode.srcIn,
            ),
          ),
          const SizedBox(
            height: 8,
          ),
          Text(
            label,
            textAlign: TextAlign.center,
            style: const TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ],
      ),
    );
  }
}

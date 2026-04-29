import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class ContactanosItem extends StatelessWidget {
  const ContactanosItem({
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
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 18),
      onPressed: onPressed,
      child: Row(
        children: [
          SvgPicture.asset(
            icon,
            height: 24,
          ),
          const SizedBox(
            width: 8,
          ),
          Text(
            label,
            style: const TextStyle(
              fontSize: 12,
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

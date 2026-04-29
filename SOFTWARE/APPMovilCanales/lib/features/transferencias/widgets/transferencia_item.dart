import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class TransferenciaItem extends StatelessWidget {
  const TransferenciaItem({
    super.key,
    required this.label,
    required this.onPressed,
    this.disabled = false,
  });

  final String label;
  final void Function() onPressed;
  final bool disabled;

  @override
  Widget build(BuildContext context) {
    return CtCardButton(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 18),
      onPressed: onPressed,
      disabled: disabled,
      child: Row(
        children: [
          SvgPicture.asset(
            'assets/icons/repeat.svg',
            height: 24,
          ),
          const SizedBox(
            width: 8,
          ),
          Text(
            label,
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

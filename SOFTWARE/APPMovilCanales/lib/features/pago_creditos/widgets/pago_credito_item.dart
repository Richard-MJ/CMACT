import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card_button.dart';
import 'package:flutter/material.dart';

class PagoCreditoItem extends StatelessWidget {
  const PagoCreditoItem({
    super.key,
    required this.text,
    required this.onPressed,
  });

  final String text;
  final void Function() onPressed;

  @override
  Widget build(BuildContext context) {
    return CtCardButton(
      height: 48,
      padding: const EdgeInsets.symmetric(horizontal: 16),
      onPressed: onPressed,
      child: Row(
        children: [
          Text(
            text,
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

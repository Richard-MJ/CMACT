import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class ChipItem extends StatelessWidget {
  const ChipItem({
    super.key,
    required this.label,
    required this.onPressed,
  });

  final String label;
  final void Function() onPressed;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 26,
      child: TextButton(
        style: TextButton.styleFrom(
          padding: const EdgeInsets.only(left: 12, right: 10),
          backgroundColor: AppColors.primary100,
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.all(
              Radius.circular(16),
            ),
          ),
        ),
        onPressed: onPressed,
        child: Text(
          label,
          style: const TextStyle(
            color: AppColors.primary700,
            fontSize: 12,
            fontWeight: FontWeight.w500,
          ),
        ),
      ),
    );
  }
}

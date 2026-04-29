import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class CtButtonIcon extends StatelessWidget {
  const CtButtonIcon({
    super.key,
    required this.text,
    required this.icon,
    this.type = ButtonType.solid,
    required this.onPressed,
    this.disabled = false,
  });

  final String text;
  final IconData icon;
  final ButtonType type;
  final void Function() onPressed;
  final bool disabled;

  @override
  Widget build(BuildContext context) {
    final colorBackground = disabled
        ? AppColors.gray400
        : type == ButtonType.solid
            ? AppColors.primary700
            : AppColors.white;

    final colorIcon = disabled
        ? AppColors.gray400
        : type == ButtonType.solid
            ? AppColors.white
            : AppColors.primary700;

    return Material(
      color: Colors.transparent,
      child: InkWell(
        onTap: disabled ? null : onPressed,
        borderRadius: BorderRadius.circular(8),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Container(
              padding: const EdgeInsets.all(16),
              decoration: BoxDecoration(
                border: Border.all(color: colorIcon, width: 1),
                borderRadius: BorderRadius.circular(8),
                color: colorBackground,
                boxShadow: AppColors.shadowSm,
              ),
              child: Icon(icon, color: colorIcon),
            ),
            const SizedBox(height: 4),
            Text(
              text,
              style: const TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w600,
                color: AppColors.black,
                height: 26 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
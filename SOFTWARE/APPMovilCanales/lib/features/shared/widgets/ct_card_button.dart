import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class CtCardButton extends StatelessWidget {
  const CtCardButton({
    super.key,
    this.onPressed,
    this.child,
    this.height,
    this.padding = EdgeInsets.zero,
    this.disabled = false,
    this.color = AppColors.gray100,
    this.width = double.infinity,
    this.gradient,
  });

  final void Function()? onPressed;
  final Widget? child;
  final double? height;
  final EdgeInsets? padding;
  final bool disabled;
  final Color? color;
  final double? width;
  final Gradient? gradient;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: height,
      width: width,
      decoration: BoxDecoration(
        color: disabled ? AppColors.gray25 : color,
        borderRadius: const BorderRadius.all(Radius.circular(8)),
        border:
            disabled ? Border.all(color: AppColors.gray300, width: 1) : null,
        boxShadow: disabled ? null : AppColors.shadowSm,
        gradient: gradient,
      ),
      child: TextButton(
        style: TextButton.styleFrom(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(8),
          ),
          padding: padding,
        ),
        onPressed: disabled
            ? null
            : () {
                if (onPressed != null) {
                  onPressed!();
                }
              },
        child: child!,
      ),
    );
  }
}

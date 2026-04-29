import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';

class CtButton2 extends StatelessWidget {
  const CtButton2({
    super.key,
    required this.text,
    this.type = ButtonType.solid,
    required this.onPressed,
    this.disabled = false,
  });

  final String text;
  final ButtonType type;
  final void Function() onPressed;
  final bool disabled;

  @override
  Widget build(BuildContext context) {
    final background = disabled
        ? AppColors.gray400
        : type == ButtonType.solid
            ? AppColors.primary700
            : const Color.fromRGBO(255, 0, 0, 0.0);

    return Center(
      child: SizedBox(
        height: 32,
        child: TextButton(
          style: TextButton.styleFrom(
            side: BorderSide(
              color: disabled
                  ? AppColors.gray400
                  : type == ButtonType.solid
                      ? const Color.fromRGBO(255, 0, 0, 0.0)
                      : AppColors.primary700,
              width: 1,
            ),
            padding: const EdgeInsetsDirectional.symmetric(horizontal: 14),
            backgroundColor: background,
            shape: const RoundedRectangleBorder(
              borderRadius: BorderRadius.all(
                Radius.circular(24),
              ),
            ),
          ),
          onPressed: disabled ? null : onPressed,
          child: Text(
            text,
            style: TextStyle(
              color: disabled
                  ? AppColors.white
                  : type == ButtonType.solid
                      ? AppColors.gray25
                      : AppColors.primary700,
              fontSize: 12,
              fontWeight: FontWeight.w500,
            ),
          ),
        ),
      ),
    );
  }
}

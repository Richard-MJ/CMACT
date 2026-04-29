import 'package:caja_tacna_app/constants/app_colors.dart';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtCheckbox extends StatelessWidget {
  const CtCheckbox({
    super.key,
    required this.value,
    required this.onPressed,
    this.disabled = false,
  });
  final bool value;
  final void Function() onPressed;
  final bool disabled;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: disabled
          ? null
          : () {
              FocusManager.instance.primaryFocus?.unfocus();
              onPressed();
            },
      child: Opacity(
        opacity: disabled ? 0.5 : 1.0,
        child: Container(
          width: 17,
          height: 17,
          decoration: BoxDecoration(
            color: AppColors.white,
            borderRadius: BorderRadius.circular(4),
            border: Border.all(
              color: value ? AppColors.primary700 : AppColors.gray500,
              width: 1,
            ),
          ),
          child: value
              ? SvgPicture.asset(
                  'assets/icons/check-checkbox.svg',
                  height: 12,
                  width: 12,
                  colorFilter: const ColorFilter.mode(
                    AppColors.primary700,
                    BlendMode.srcIn,
                  ),
                )
              : null,
        ),
      ),
    );
  }
}

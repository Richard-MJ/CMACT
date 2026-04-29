import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class CtButton extends StatelessWidget {
  const CtButton({
    super.key,
    required this.text,
    this.type = ButtonType.solid,
    required this.onPressed,
    this.disabled = false,
    this.width = 215,
    this.height = 48,
    this.borderRadius = 24,
  });

  final String text;
  final ButtonType type;
  final void Function() onPressed;
  final bool disabled;
  final double width;
  final double height;
  final double borderRadius;

  @override
  Widget build(BuildContext context) {
    final buttonStyle = buttonStyles.firstWhere((b) => b.buttonType == type);

    return Center(
      child: SizedBox(
        width: width,
        height: height,
        child: TextButton(
          style: TextButton.styleFrom(
            side: BorderSide(
              color: buttonStyle.borderColor,
              width: 1,
            ),
            backgroundColor:
                disabled ? buttonStyle.disabledColor : buttonStyle.color,
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(borderRadius),
            ),
            foregroundColor: buttonStyle.foregroundColor,
          ),
          onPressed: disabled ? null : onPressed,
          child: Text(
            text,
            style: TextStyle(
              color: buttonStyle.textColor,
              fontSize: 15,
              fontWeight: FontWeight.w500,
            ),
          ),
        ),
      ),
    );
  }
}

enum ButtonType { solid, outline, plain, text }

class ButtonStyle {
  final ButtonType buttonType;
  final Color color;
  final Color disabledColor;
  final Color textColor;
  final Color borderColor;
  final Color foregroundColor;

  ButtonStyle({
    required this.buttonType,
    required this.color,
    required this.disabledColor,
    required this.textColor,
    required this.borderColor,
    required this.foregroundColor,
  });
}

List<ButtonStyle> buttonStyles = [
  ButtonStyle(
    buttonType: ButtonType.solid,
    color: AppColors.primary700,
    disabledColor: AppColors.primary200,
    textColor: AppColors.gray25,
    borderColor: Colors.transparent,
    foregroundColor: AppColors.primary500,
  ),
  ButtonStyle(
    buttonType: ButtonType.outline,
    color: Colors.transparent,
    disabledColor: Colors.transparent,
    textColor: AppColors.primary700,
    borderColor: AppColors.primary700,
    foregroundColor: AppColors.primary500,
  ),
  ButtonStyle(
    buttonType: ButtonType.plain,
    color: AppColors.primary100,
    disabledColor: AppColors.primary100,
    textColor: AppColors.primary700,
    borderColor: Colors.transparent,
    foregroundColor: AppColors.primary500,
  ),
  ButtonStyle(
    buttonType: ButtonType.text,
    color: Colors.transparent,
    disabledColor: Colors.transparent,
    textColor: AppColors.primary700,
    borderColor: Colors.transparent,
    foregroundColor: AppColors.primary500,
  ),
];

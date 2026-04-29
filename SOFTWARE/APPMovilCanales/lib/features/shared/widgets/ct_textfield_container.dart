import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class CtTextFieldContainer extends StatelessWidget {
  const CtTextFieldContainer({
    super.key,
    required this.child,
    this.padding = const EdgeInsets.symmetric(horizontal: 14),
    this.height = 44.0,
    this.width = double.infinity,
    this.errorMessage,
    this.helperMessage,
    this.color = AppColors.white
  });

  final Widget child;
  final EdgeInsetsGeometry padding;
  final double height;
  final Color color;
  final double? width;

  final String? errorMessage;
  final String? helperMessage;

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Container(
          height: height,
          width: width,
          padding: padding,
          decoration: BoxDecoration(
            color: color,
            border: Border.all(
              color:
                  errorMessage == null ? AppColors.gray300 : AppColors.error500,
            ),
            borderRadius: BorderRadius.circular(8),
            boxShadow: const [
              BoxShadow(
                color: Color.fromRGBO(16, 24, 40, 0.05), // Color de la sombra
                spreadRadius: 0, // Radio de expansión de la sombra
                blurRadius: 2, // Radio de desenfoque de la sombra
                offset: Offset(
                  0,
                  1,
                ), // Desplazamiento en eje x e y de la sombra
              )
            ],
          ),
          child: child,
        ),
        if (errorMessage != null)
          Container(
            padding: const EdgeInsets.only(top: 6),
            child: Text(
              '$errorMessage',
              style: const TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w400,
                height: 22 / 14,
                color: AppColors.error500,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
        if (helperMessage != null && errorMessage == null)
          Container(
            padding: const EdgeInsets.only(top: 6),
            child: Text(
              '$helperMessage',
              style: const TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w400,
                height: 22 / 14,
                color: AppColors.gray500,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          )
      ],
    );
  }
}

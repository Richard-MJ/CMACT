import 'package:caja_tacna_app/constants/app_colors.dart';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtOperacion extends StatelessWidget {
  const CtOperacion({
    super.key,
    required this.icon,
    required this.onPressed,
    required this.label,
  });

  final String icon;
  final String label;

  final void Function() onPressed;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 77,
      child: Column(
        children: [
          Container(
            width: 56,
            height: 56,
            decoration: const BoxDecoration(
              shape: BoxShape.circle,
              boxShadow: [
                BoxShadow(
                  color:
                      Color.fromRGBO(178, 183, 199, 0.3), // Color de la sombra
                  offset: Offset(0.0, 4.0), // Desplazamiento en el eje X e Y
                  blurRadius: 12.0, // Radio de desenfoque
                ),
              ],
            ),
            child: TextButton(
              style: TextButton.styleFrom(
                backgroundColor: AppColors.white,
                shape: const CircleBorder(),
              ),
              onPressed: onPressed,
              child: SvgPicture.asset(
                icon,
                height: 21,
              ),
            ),
          ),
          const SizedBox(
            height: 8,
          ),
          Text(
            label,
            style: const TextStyle(
              fontSize: 12,
              fontWeight: FontWeight.w600,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
            textAlign: TextAlign.center,
          ),
        ],
      ),
    );
  }
}

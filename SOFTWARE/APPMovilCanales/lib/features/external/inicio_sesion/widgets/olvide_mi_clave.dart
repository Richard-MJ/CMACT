import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class OlvideMiClave extends StatelessWidget {
  const OlvideMiClave({super.key});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        context.push('/olvide-mi-clave');
      },
      child: const Text(
        'Olvidé mi clave',
        style: TextStyle(
          fontSize: 14,
          fontWeight: FontWeight.w400,
          height: 22 / 14,
          leadingDistribution: TextLeadingDistribution.even,
          color: AppColors.primary700,
        ),
      ),
    );
  }
}

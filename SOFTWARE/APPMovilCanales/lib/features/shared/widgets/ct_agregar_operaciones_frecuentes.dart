import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtAgregarOperacionesFrecuentes extends StatelessWidget {
  const CtAgregarOperacionesFrecuentes({
    super.key,
    required this.value,
    required this.onChanged,
  });

  final bool value;
  final void Function() onChanged;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        onChanged();
      },
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          SvgPicture.asset(
            value ? 'assets/icons/heart-solid.svg' : 'assets/icons/heart.svg',
            height: 20,
          ),
          const SizedBox(
            width: 8,
          ),
          const Text(
            'Agregar a operaciones frecuentes',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w500,
              color: AppColors.primary700,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ],
      ),
    );
  }
}

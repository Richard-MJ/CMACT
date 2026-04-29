import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class Contactanos extends StatelessWidget {
  const Contactanos({super.key});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        context.push('/contactanos');
      },
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          SvgPicture.asset(
            'assets/icons/phone.svg',
            height: 20,
          ),
          const SizedBox(
            width: 8,
          ),
          const Text(
            'Contáctanos',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w500,
              height: 14 / 14,
              leadingDistribution: TextLeadingDistribution.even,
              color: AppColors.primary700,
            ),
          )
        ],
      ),
    );
  }
}

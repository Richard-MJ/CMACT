import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtStepper extends StatelessWidget {
  const CtStepper({
    super.key,
    required this.numPasos,
    required this.pasoActual,
  });

  final int numPasos;
  final int pasoActual;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 24,
      width: 24 * numPasos.toDouble() + 90 * (numPasos - 1),
      child: ListView.separated(
        scrollDirection: Axis.horizontal,
        itemBuilder: (context, index) {
          final bool isActive = pasoActual == index + 1;
          if (index + 1 >= pasoActual) {
            return Container(
              width: 24,
              height: 24,
              decoration: BoxDecoration(
                color: isActive ? AppColors.primary500 : AppColors.primary100,
                border: isActive
                    ? Border.all(width: 2, color: AppColors.primary700)
                    : null,
                borderRadius: BorderRadius.circular(12),
              ),
              child: Center(
                child: Text(
                  '${index + 1}',
                  style: TextStyle(
                    fontSize: 12,
                    fontWeight: FontWeight.w400,
                    color: isActive ? AppColors.white : AppColors.primary700,
                    height: 18 / 12,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ),
            );
          }

          return Container(
            width: 24,
            height: 24,
            decoration: BoxDecoration(
              color: AppColors.primary700,
              borderRadius: BorderRadius.circular(12),
            ),
            child: Center(
              child: SvgPicture.asset(
                'assets/icons/check.svg',
                height: 20,
              ),
            ),
          );
        },
        separatorBuilder: (context, index) {
          return Row(
            children: [
              Container(
                padding: EdgeInsets.zero,
                height: 1,
                width: 90,
                color: const Color(0xFFF2F1F3),
              ),
            ],
          );
        },
        itemCount: numPasos,
      ),
    );
  }
}

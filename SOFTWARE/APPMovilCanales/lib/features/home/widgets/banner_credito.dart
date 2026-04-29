import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class CreditoBanner extends StatelessWidget {
  const CreditoBanner({super.key});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        context.push('/solicitud-crediticia/ingreso-mensual');
      },
      child: Container(
        height: 96,
        margin: EdgeInsets.zero,
        padding: const EdgeInsets.symmetric(horizontal: 20),
        decoration: BoxDecoration(
          color: AppColors.primary50,
          borderRadius: BorderRadius.circular(8),
          boxShadow: AppColors.shadowSm,
        ),
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Image.asset(
              'assets/images/solicitud_crediticia.png',
              width: 64,
            ),
            const SizedBox(
              width: 31,
            ),
            const Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    'Solicita tu crédito',
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w700,
                      color: AppColors.gray900,
                      height: 19 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  SizedBox(
                    height: 7,
                  ),
                  Text(
                    '¡Haz que tus metas se hagan realidad con nuestro crédito!',
                    style: TextStyle(
                      fontSize: 10,
                      fontWeight: FontWeight.w400,
                      color: AppColors.black,
                      height: 14 / 10,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    softWrap: true,
                  ),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}

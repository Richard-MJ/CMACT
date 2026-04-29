import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';

class HomeBanner extends StatelessWidget {
  const HomeBanner({super.key});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        context.push('/apertura-cuentas/ingreso-datos');
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
              'assets/images/pig-ahorro-3.png',
              width: 74,
            ),
            const SizedBox(
              width: 20,
            ),
            const Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    'Abre una cuenta nueva',
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
                    'Con las mejores tasas para que empieces a ahorrar',
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

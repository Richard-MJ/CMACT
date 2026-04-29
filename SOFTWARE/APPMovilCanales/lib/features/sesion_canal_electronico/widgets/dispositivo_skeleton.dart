import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class DispositivoViewSkeleton extends StatelessWidget {
  const DispositivoViewSkeleton({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: AppColors.white, // Color de fondo para el esqueleto
        borderRadius: BorderRadius.circular(20),
      ),
      width: double.infinity,
      padding: const EdgeInsets.all(15),
      child: Row(
        children: [
          // Placeholder para el ícono
          Container(
            width: 55,
            height: 55,
            decoration: const BoxDecoration(
              color: AppColors.primary50, // Color de fondo del círculo
              shape: BoxShape.circle,
            ),
            alignment: Alignment.center,
            child: const SizedBox(
              width: 25,
              height: 25,
              child: CircularProgressIndicator(
                strokeWidth: 2,
                valueColor: AlwaysStoppedAnimation<Color>(AppColors.primary600),
              ),
            ),
          ),
          const SizedBox(
            width: 15,
          ),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                // Placeholder para el texto del modelo del dispositivo
                Container(
                  color: AppColors.gray200,
                  height: 20,
                  width: 150,
                ),
                const SizedBox(height: 10),
                // Placeholder para la dirección IP
                Container(
                  color: AppColors.gray200,
                  height: 15,
                  width: 100,
                ),
                const SizedBox(height: 10),
                // Placeholder para la fecha de modificación
                Container(
                  color: AppColors.gray200,
                  height: 15,
                  width: 120,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class SesionFinalizadaView extends StatelessWidget {
  const SesionFinalizadaView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppColors.primary700,
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Container(
              padding: const EdgeInsets.symmetric(horizontal: 20),
              child: const Text(
                'Su sesión fue cerrada porque no hemos detectado actividad.',
                textAlign: TextAlign.center,
                style: TextStyle(
                  color: AppColors.white,
                  fontSize: 15,
                  fontWeight: FontWeight.w400,
                ),
              ),
            ),
            const SizedBox(height: 30),
            const CircularProgressIndicator(
              color: AppColors.white,
              strokeWidth: 3,
            ),
          ],
        ),
      ),
    );
  }
}

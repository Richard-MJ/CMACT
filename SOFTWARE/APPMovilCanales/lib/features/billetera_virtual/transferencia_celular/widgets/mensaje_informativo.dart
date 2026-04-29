import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class MensajeInformativo extends StatelessWidget {
  final String titulo;
  final String mensaje;

  const MensajeInformativo({
    super.key,
    required this.titulo,
    required this.mensaje,
  });

  @override
  Widget build(BuildContext context) {
    return Align(
      alignment: Alignment.topCenter,
      child: Padding(
        padding: const EdgeInsets.only(top: 100, left: 32, right: 32),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Text(
              titulo,
              textAlign: TextAlign.center,
              style: const TextStyle(
                color: AppColors.primary600,
                fontWeight: FontWeight.w700,
                fontSize: 16,
              ),
            ),
            const SizedBox(height: 10),
            Text(
              mensaje,
              textAlign: TextAlign.center,
              style: const TextStyle(
                fontSize: 13,
                color: AppColors.gray700,
                height: 1.3,
              ),
            ),
          ],
        ),
      ),
    );
  }
}

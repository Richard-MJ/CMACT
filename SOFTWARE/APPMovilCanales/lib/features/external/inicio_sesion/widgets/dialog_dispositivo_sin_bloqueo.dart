import 'package:caja_tacna_app/config/plugins/screen_lock_check_ct.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';

class DialogDispositivoSinBloqueo extends StatelessWidget {
  const DialogDispositivoSinBloqueo({super.key});

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
      elevation: 0,
      backgroundColor: AppColors.white,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24),
      contentPadding: const EdgeInsets.only(
        top: 48,
        left: 24,
        right: 24,
        bottom: 48,
      ),
      titlePadding: const EdgeInsets.only(
        top: 18,
        left: 24,
        right: 18,
        bottom: 0,
      ),
      content: SizedBox(
        width: double.infinity,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Text(
              'No tienes activado ningún patrón o código de seguridad en tu celular',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w800,
                color: AppColors.black,
                height: 28 / 18,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 24,
            ),
            const Text(
              'Por tu seguridad, sigue estos pasos para ingresar a Caja Tacna App',
              style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w400,
                color: AppColors.black,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 24,
            ),
            const Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Expanded(
                  child: Text(
                    '1. Ingresa a Ajustes.',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.black,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ),
              ],
            ),
            const SizedBox(
              height: 12,
            ),
            const Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Expanded(
                  child: Text(
                    '2. Busca la opción de seguridad, configurando un patrón, código u otro.',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.black,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ),
              ],
            ),
            const SizedBox(
              height: 12,
            ),
            const Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Expanded(
                  child: Text(
                    '3. Activa un modo de desbloqueo.',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.black,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ),
              ],
            ),
            const SizedBox(
              height: 12,
            ),
            const Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Expanded(
                  child: Text(
                    '4. ¡Listo! Ahora podrás seguir usando Caja Tacna App.',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.black,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ),
              ],
            ),
            const SizedBox(
              height: 36,
            ),
            CtButton(
              text: 'Reintentar',
              onPressed: () async {
                final isScreenLockEnabled =
                    await ScreenLockCheckCt.isScreenLockEnabled();
                if (isScreenLockEnabled) {
                  Navigator.of(context).pop(true);
                }
              },
              borderRadius: 8,
              width: double.infinity,
            )
          ],
        ),
      ),
    );
  }
}

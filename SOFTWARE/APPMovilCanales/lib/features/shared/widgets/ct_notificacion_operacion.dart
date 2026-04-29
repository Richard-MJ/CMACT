import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_message.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class CtNotificacionOperacion extends ConsumerWidget {
  const CtNotificacionOperacion({
    super.key,
    required this.correoElectronico,
  });

  final String? correoElectronico;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    var numeroTelefonico =
        ref.watch(homeProvider).datosCliente?.numeroTelefonoSms;

    return CtMessage(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          if (correoElectronico != null && correoElectronico!.isNotEmpty) ...[
            const Text(
              'Notificaremos la operación al correo',
              style: TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w400,
                color: AppColors.gray900,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            Text(
              CtUtils.hashearCorreo(correoElectronico),
              style: const TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ] else ...[
            const Text(
              'Notificaremos la operación al número',
              style: TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w400,
                color: AppColors.gray900,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            Text(
              '+51${CtUtils.hashNumeroCelular(numeroTelefonico)}',
              style: const TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ],
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/providers/cancelacion_cuentas_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_cuenta.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class FormTransPropia extends ConsumerWidget {
  const FormTransPropia({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final cancelacionState = ref.watch(cancelacionCuentasProvider);
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        const Text(
          'Cuenta de destino',
          style: TextStyle(
            fontSize: 16,
            fontWeight: FontWeight.w500,
            color: AppColors.gray900,
            height: 1.5,
            leadingDistribution: TextLeadingDistribution.even,
          ),
        ),
        const SizedBox(
          height: 16,
        ),
        CtSelectCuenta(
          cuentas: cancelacionState.cuentasDestinoPropias,
          onChange: (producto) {
            ref
                .read(cancelacionCuentasProvider.notifier)
                .changeCuentaDestinoPropia(producto);
          },
          value: cancelacionState.cuentaDestinoPropia,
        ),
      ],
    );
  }
}

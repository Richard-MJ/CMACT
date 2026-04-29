import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/providers/cancelacion_cuentas_provider.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_cuenta_destino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class FormTransTercero extends ConsumerWidget {
  const FormTransTercero({super.key});

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
        InputCuentaDestino(
          numeroCuenta: cancelacionState.numeroCuentaTercero,
          onChanged: (value) {
            ref
                .read(cancelacionCuentasProvider.notifier)
                .changeCuentaTercero(value);
          },
        ),
      ],
    );
  }
}

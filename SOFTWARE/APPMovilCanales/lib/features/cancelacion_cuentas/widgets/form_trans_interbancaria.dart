import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/providers/cancelacion_cuentas_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_documento.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_cuenta_destino_cci.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_nombre_beneficiario.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class FormTransInterbancaria extends ConsumerWidget {
  const FormTransInterbancaria({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final cancelacionState = ref.watch(cancelacionCuentasProvider);
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        const Text(
          'Código de Cuenta Interbancario (CCI)',
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
        InputCuentaDestinoCci(
          numeroCuenta: cancelacionState.cuentaDestinoCci,
          onChanged: (value) {
            ref
                .read(cancelacionCuentasProvider.notifier)
                .changeCuentaDestinoCci(value);
          },
        ),
        const SizedBox(
          height: 8,
        ),
        Row(
          children: [
            CtCheckbox(
              value: cancelacionState.esTitular,
              onPressed: () {
                ref.read(cancelacionCuentasProvider.notifier).toggleEsTitular();
              },
            ),
            const SizedBox(
              width: 8,
            ),
            const Text(
              'Soy titular de la cuenta',
              style: TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w500,
                color: AppColors.gray700,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ],
        ),
        const SizedBox(
          height: 36,
        ),
        const Text(
          'Nombre de beneficiario',
          style: TextStyle(
            fontSize: 16,
            fontWeight: FontWeight.w500,
            color: AppColors.gray900,
            height: 1.5,
            leadingDistribution: TextLeadingDistribution.even,
          ),
        ),
        const SizedBox(
          height: 8,
        ),
        InputNombreBeneficiario(
          nombreBeneficiario: cancelacionState.nombreBeneficiario,
          onChanged: (value) {
            ref
                .read(cancelacionCuentasProvider.notifier)
                .changeNombreBeneficiario(value);
          },
          readOnly: cancelacionState.esTitular,
        ),
        const SizedBox(
          height: 36,
        ),
        const Text(
          'N° de documento',
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
        CtInputNumeroDocumento(
          tiposDocumento: cancelacionState.tiposDocumento,
          tipoDocumento: cancelacionState.tipoDocumento,
          numeroDocumento: cancelacionState.numeroDocumento,
          onChangeTipoDocumento: (documento) {
            ref
                .read(cancelacionCuentasProvider.notifier)
                .changeDocumento(documento);
          },
          onChangeNumeroDocumento: (numeroDocumento) {
            ref
                .read(cancelacionCuentasProvider.notifier)
                .changeNumeroDocumento(numeroDocumento);
          },
          tipoValidacion: TipoValidacionDocumento.validacion2,
          readOnly: cancelacionState.esTitular,
        ),
      ],
    );
  }
}

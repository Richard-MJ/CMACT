import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/providers/pago_tarjetas_credito_diferida_provider.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/widgets/input_monto.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/widgets/input_motivo.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_stepper.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';

class IngresoMontoPagarDiferidaScreen extends StatelessWidget {
  const IngresoMontoPagarDiferidaScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Pago de tarjetas de crédito',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _IngresoMontoPagarView(),
          )
        ],
      ),
    );
  }
}

class _IngresoMontoPagarView extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoState = ref.watch(pagoTarjetasCreditoDiferidaProvider);

    return Container(
      padding: const EdgeInsets.only(top: 24, bottom: 24, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Center(
            child: CtStepper(
              numPasos: 2,
              pasoActual: 2,
            ),
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Ingresa el monto a pagar',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 28 / 18,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const Divider(
            color: AppColors.border1,
          ),
          const SizedBox(
            height: 8,
          ),        
          Row(
            children: [
              const SizedBox(
                width: 125,
                height: 24,
                child: Text(
                  'N° de tarjeta',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray900,
                    height: 24 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ),
              const SizedBox(
                width: 10,
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      pagoState.entidadFinanciera?.nombreEntidadCce ?? '',
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    Text(
                      CtUtils.formatNumeroTarjeta(
                        numeroCuenta: pagoState.numeroTarjetaCredito.value,
                        hash: true
                      ),
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray500,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    Text(
                      pagoState.nombreBeneficiario.value,
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                  ],
                ),
              )
            ],
          ),
          const SizedBox(
            height: 24,
          ),
          const Divider(
            color: AppColors.border1,
          ),
          const SizedBox(
            height: 8,
          ),          
          const Text(
            'Monto a pagar',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          InputMonto(
            monto: pagoState.monto,
            onChanged: (value) {
              ref.read(pagoTarjetasCreditoDiferidaProvider.notifier).changeMonto(value);
            },
            simboloMoneda: pagoState.cuentaOrigen?.simboloMonedaProducto,
          ),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Motivo (opcional)',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.2,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 8,
          ),
          InputMotivo(
            motivo: pagoState.motivo,
            onChange: (motivo) {
              ref
                  .read(pagoTarjetasCreditoDiferidaProvider.notifier)
                  .changeMotivo(motivo);
            },
          ),
          const SizedBox(
            height: 24,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref
                  .read(pagoTarjetasCreditoDiferidaProvider.notifier)
                  .pagar(withPush: true);
            },
          )
        ],
      ),
    );
  }
}

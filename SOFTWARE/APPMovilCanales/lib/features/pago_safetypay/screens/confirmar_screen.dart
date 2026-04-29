import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_safetypay/providers/pago_safetypay_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfirmarScreen extends StatelessWidget {
  const ConfirmarScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Confirma la operación',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _ConfirmarView(),
          )
        ],
      ),
    );
  }
}

class _ConfirmarView extends ConsumerWidget {
  const _ConfirmarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoSafetypayState = ref.watch(pagoSafetypayProvider);
    final timerState = ref.watch(timerProvider);

    return Container(
      padding: const EdgeInsets.only(top: 61, bottom: 56, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Operación',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Column(
                crossAxisAlignment: CrossAxisAlignment.end,
                children: [
                  const Text(
                    'Pago con SafetyPay',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 19 / 16,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  Text(
                    'Código ${pagoSafetypayState.deuda?.codigoPago}',
                    style: const TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray500,
                      height: 19 / 16,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ],
              )
            ],
          ),
          const SizedBox(
            height: 37,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Monto a pagar',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                CtUtils.formatCurrency(
                  pagoSafetypayState.deuda?.monto,
                  pagoSafetypayState.deuda?.simboloMoneda,
                ),
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 37,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Cuenta de cargo',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Column(
                crossAxisAlignment: CrossAxisAlignment.end,
                children: [
                  const Text(
                    'Cuenta de Ahorro',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 19 / 16,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  Text(
                    CtUtils.formatNumeroCuenta(
                      numeroCuenta:
                          pagoSafetypayState.cuentaOrigen?.numeroProducto,
                    ),
                    style: const TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray500,
                      height: 19 / 16,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ],
              )
            ],
          ),
          const SizedBox(
            height: 30,
          ),
          const Spacer(),
          const Text(
            'Ingresa tu Token Digital',
            style: TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.w800,
              color: AppColors.gray900,
              height: 28 / 18,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 20,
          ),
          CtOtp(
            value: pagoSafetypayState.tokenDigital,
            onChanged: (value) {
              ref.read(pagoSafetypayProvider.notifier).changeToken(value);
            },
          ),
          const SizedBox(
            height: 13,
          ),
          timerState.timerOn
              ? const CtTimer()
              : Align(
                  alignment: Alignment.centerRight,
                  child: GestureDetector(
                    onTap: () {
                      ref
                          .read(pagoSafetypayProvider.notifier)
                          .pagar(withPush: false);
                    },
                    child: const Text(
                      'Solicitar nuevo token',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.primary700,
                        height: 28 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ),
                ),
          const SizedBox(
            height: 30,
          ),
          const Spacer(),
          CtButton(
            text: 'Confirmar',
            onPressed: () {
              ref.read(pagoSafetypayProvider.notifier).confirmar();
            },
          )
        ],
      ),
    );
  }
}

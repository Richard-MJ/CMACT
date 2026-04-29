import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/providers/cancelacion_cuentas_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfirmarInternaScreen extends StatelessWidget {
  const ConfirmarInternaScreen({super.key});

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
    final cancelarState = ref.watch(cancelacionCuentasProvider);
    final timerState = ref.watch(timerProvider);
    final bool disabledButton = cancelarState.tokenDigital.isEmpty ||
        cancelarState.tokenDigital.length != 6;

    return Container(
      padding: const EdgeInsets.only(top: 61, bottom: 56, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Operación',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                'Cancelar cuenta',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
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
                'Cuenta a cancelar',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      cancelarState.cuentaAhorro?.descripcion ?? '',
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    const SizedBox(
                      width: 30,
                    ),
                    Text(
                      CtUtils.formatNumeroCuenta(
                        numeroCuenta: cancelarState.cuentaAhorro?.identificador,
                      ),
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray500,
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
          if (cancelarState.tipoTransferencia != null)
            const SizedBox(
              height: 37,
            ),
          if (cancelarState.tipoTransferencia != null)
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Cuenta de destino',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 19 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      Text(
                        cancelarState.tipoTransferencia?.id == 1
                            ? cancelarState
                                    .cuentaDestinoPropia?.nombreTipoProducto ??
                                ''
                            : cancelarState.cuentaTercero?.descripcion ?? '',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                        textAlign: TextAlign.end,
                      ),
                      const SizedBox(
                        width: 30,
                      ),
                      Text(
                        CtUtils.formatNumeroCuenta(
                          numeroCuenta: cancelarState.tipoTransferencia?.id == 1
                              ? cancelarState
                                  .cuentaDestinoPropia?.numeroProducto
                              : cancelarState.numeroCuentaTercero.value,
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
                  ),
                )
              ],
            ),
          if (cancelarState.tipoTransferencia != null)
            const SizedBox(
              height: 37,
            ),
          if (cancelarState.tipoTransferencia != null)
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Monto a transferir',
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
                    cancelarState.cancelarInternaResponse?.montoDestino,
                    cancelarState.cuentaAhorro?.simboloMoneda,
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
            value: cancelarState.tokenDigital,
            onChanged: (value) {
              ref.read(cancelacionCuentasProvider.notifier).changeToken(value);
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
                          .read(cancelacionCuentasProvider.notifier)
                          .cancelarCuenta(withPush: false);
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
              ref.read(cancelacionCuentasProvider.notifier).confirmar();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

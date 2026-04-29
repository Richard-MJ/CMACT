import 'package:animate_do/animate_do.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/recarga_virtual/providers/recarga_virtual_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_agregar_operaciones_frecuentes.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_alias_operacion.dart';
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
    final recargaVirtualState = ref.watch(recargaVirtualProvider);
    final timerState = ref.watch(timerProvider);
    final bool disabledButton = recargaVirtualState.tokenDigital.isEmpty ||
        recargaVirtualState.tokenDigital.length != 6;

    return Container(
      padding: const EdgeInsets.only(top: 36, bottom: 56, left: 24, right: 24),
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
                    'Recarga virtual',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 19 / 16,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  Text(
                    recargaVirtualState.operadorKasnet?.descripcionOperador ?? '',
                    style: const TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray500,
                      height: 19 / 16,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  Text(
                    recargaVirtualState.numeroCelular.value,
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
                'Monto a recargar',
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
                  double.parse(
                    recargaVirtualState.montoRecarga.value,
                  ),
                  'S/',
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
          if (recargaVirtualState.pagarResponseKasnet?.montoReal.tipoCambio != 1) ...[
            const SizedBox(
              height: 37,
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Tipo de cambio',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 19 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Text(
                  recargaVirtualState.pagarResponse?.montoReal.tipoCambio
                          .toString() ??
                      '',
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 19 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  textAlign: TextAlign.end,
                ),
              ],
            ),
          ],
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
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      recargaVirtualState.cuentaOrigen?.nombreTipoProducto ??
                          '',
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    Text(
                      CtUtils.formatNumeroCuenta(
                        numeroCuenta:
                            recargaVirtualState.cuentaOrigen?.numeroProducto,
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
          const SizedBox(
            height: 37,
          ),
          Center(
            child: CtAgregarOperacionesFrecuentes(
              value: recargaVirtualState.operacionFrecuente,
              onChanged: () {
                ref
                    .read(recargaVirtualProvider.notifier)
                    .toggleOperacionFrecuente();
              },
            ),
          ),
          FadeIn(
            animate: recargaVirtualState.operacionFrecuente,
            duration: const Duration(milliseconds: 150),
            child: Container(
              padding: const EdgeInsets.only(top: 16),
              child: InputAliasOperacion(
                alias: recargaVirtualState.nombreOperacionFrecuente,
                onChanged: (operacionFrecuente) {
                  ref
                      .read(recargaVirtualProvider.notifier)
                      .changeNombreOperacionFrecuente(operacionFrecuente);
                },
              ),
            ),
          ),
          const SizedBox(
            height: 90,
          ),
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
            value: recargaVirtualState.tokenDigital,
            onChanged: (value) {
              ref.read(recargaVirtualProvider.notifier).changeToken(value);
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
                          .read(recargaVirtualProvider.notifier)
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
          const Spacer(),
          const SizedBox(
            height: 40,
          ),
          CtButton(
            text: 'Confirmar',
            onPressed: () {
              ref.read(recargaVirtualProvider.notifier).confirmar();
            },
            disabled: disabledButton,
          ),
        ],
      ),
    );
  }
}

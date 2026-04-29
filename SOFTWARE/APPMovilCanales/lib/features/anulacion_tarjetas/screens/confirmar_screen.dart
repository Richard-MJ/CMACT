import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/providers/anulacion_tarjetas_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfirmarScreen extends ConsumerStatefulWidget {
  const ConfirmarScreen({super.key});

  @override
  ConfirmarScreenState createState() => ConfirmarScreenState();
}

class ConfirmarScreenState extends ConsumerState<ConfirmarScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Confirma la operación',
      child: _ConfirmarView(),
    );
  }
}

class _ConfirmarView extends ConsumerWidget {
  const _ConfirmarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final timerState = ref.watch(timerProvider);
    final anularState = ref.watch(anulacionTarjetasProvider);

    return CustomScrollView(
      slivers: [
        SliverFillRemaining(
          hasScrollBody: false,
          child: Container(
            padding: const EdgeInsets.only(
              top: 36,
              bottom: 56,
              left: 24,
              right: 24,
            ),
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
                    SizedBox(
                      width: 30,
                    ),
                    Expanded(
                      child: Text(
                        'Anular tarjetas',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                        textAlign: TextAlign.end,
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
                      'Tarjeta a cancelar',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    const SizedBox(
                      width: 30,
                    ),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.end,
                        children: [
                          Text(
                            anularState.tarjeta?.descripcionTarjeta ?? '',
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
                            CtUtils.formatNumeroTarjeta(
                              numeroCuenta: anularState.tarjeta?.numeroTarjeta,
                              hash: true,
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
                      'Motivo',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    const SizedBox(
                      width: 30,
                    ),
                    Expanded(
                      child: Text(
                        anularState.motivo?.descripcionMotivo ?? '',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                        textAlign: TextAlign.end,
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 24,
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
                  value: anularState.tokenDigital,
                  onChanged: (value) {
                    ref
                        .read(anulacionTarjetasProvider.notifier)
                        .changeToken(value);
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
                                .read(anulacionTarjetasProvider.notifier)
                                .anular(withPush: false);
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
                  height: 24,
                ),
                CtButton(
                  text: 'Confirmar',
                  onPressed: () {
                    ref.read(anulacionTarjetasProvider.notifier).confirmar();
                  },
                )
              ],
            ),
          ),
        )
      ],
    );
  }
}

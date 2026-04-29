import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/compras_internet/providers/compras_internet_provider.dart';
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
    final comprasInternetState = ref.watch(comprasInternetProvider);

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
                    const SizedBox(
                      width: 30,
                    ),
                    Expanded(
                      child: Text(
                        comprasInternetState.metodoAfiliacion ==
                                MetodoAfiliacion.afiliacion
                            ? 'Habilitar compras por \ninternet'
                            : 'Deshabilitar compras por \ninternet',
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
                  height: 37,
                ),
                if (comprasInternetState.metodoAfiliacion ==
                    MetodoAfiliacion.afiliacion)
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Cuenta seleccionada',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      const SizedBox(
                        width: 15,
                      ),
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.end,
                          children: [
                            Text(
                              comprasInternetState
                                      .cuentaAfiliacion?.nombreProducto ??
                                  '',
                              style: const TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.w400,
                                color: AppColors.gray900,
                                height: 19 / 16,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                              textAlign: TextAlign.end,
                            ),
                            Text(
                              CtUtils.formatNumeroCuenta(
                                numeroCuenta: comprasInternetState
                                    .cuentaAfiliacion?.numeroCuenta,
                              ),
                              style: const TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.w400,
                                color: AppColors.gray500,
                                height: 19 / 16,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                              textAlign: TextAlign.end,
                            ),
                          ],
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
                  value: comprasInternetState.tokenDigital,
                  onChanged: (value) {
                    ref
                        .read(comprasInternetProvider.notifier)
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
                                .read(comprasInternetProvider.notifier)
                                .submit(withPush: false);
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
                    ref.read(comprasInternetProvider.notifier).confirmar();
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

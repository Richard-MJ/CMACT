import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/biometria/providers/biometria_provider.dart';
import 'package:caja_tacna_app/features/compras_internet/screens/configurar_afiliacion_screen.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class HuellaScreen extends ConsumerStatefulWidget {
  const HuellaScreen({super.key});

  @override
  HuellaScreenState createState() => HuellaScreenState();
}

class HuellaScreenState extends ConsumerState<HuellaScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref
          .read(biometriaProvider.notifier)
          .obtenerAfiliaciones(codigoTipoBiometria: 1);
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Volver',
      child: _HuellaView(),
    );
  }
}

class _HuellaView extends ConsumerWidget {
  const _HuellaView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final biometriaState = ref.watch(biometriaProvider);
    return CustomScrollView(
      physics: const ClampingScrollPhysics(),
      slivers: [
        SliverFillRemaining(
          hasScrollBody: false,
          child: Container(
            padding: const EdgeInsets.only(
              top: 24,
              left: 24,
              right: 23,
              bottom: 56,
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                const Center(
                  child: Text(
                    'Habilitar huella dactilar',
                    style: TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.w600,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ),
                const SizedBox(
                  height: 36,
                ),
                SvgPicture.asset(
                  'assets/icons/touch_id.svg',
                  width: 100,
                  height: 100,
                ),
                const SizedBox(
                  height: 36,
                ),
                const Text(
                  'La próxima vez que ingreses a Caja Tacna App, podrás hacerlo con tu huella dactilar.',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 32,
                ),
                Container(
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(8),
                    boxShadow: AppColors.shadowSm,
                    color: AppColors.gray100,
                  ),
                  child: TextButton(
                    onPressed: () {
                      ref.read(biometriaProvider.notifier).toggleSwitchCard();
                    },
                    style: TextButton.styleFrom(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 16,
                        vertical: 18,
                      ),
                      shape: const RoundedRectangleBorder(
                        borderRadius: BorderRadius.all(
                          Radius.circular(8),
                        ),
                      ),
                    ),
                    child: Row(
                      children: [
                        CtSwitch(
                          value: biometriaState.switchCard,
                          onTap: () {},
                        ),
                        const SizedBox(
                          width: 16,
                        ),
                        Expanded(
                          child: Text(
                            'Habilitar huella dactilar en este dispositivo',
                            style: TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w500,
                              color: biometriaState.switchCard
                                  ? AppColors.gray900
                                  : AppColors.gray700,
                              height: 22 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
                const SizedBox(
                  height: 30,
                ),
                const Spacer(),
                CtButton(
                  text: 'Continuar',
                  onPressed: () {
                    ref
                        .read(biometriaProvider.notifier)
                        .afiliar(withPush: true);
                  },
                  disabled: (biometriaState.afiliacion == null &&
                          biometriaState.switchCard == false) ||
                      (biometriaState.afiliacion != null &&
                          biometriaState.switchCard == true),
                )
              ],
            ),
          ),
        ),
      ],
    );
  }
}

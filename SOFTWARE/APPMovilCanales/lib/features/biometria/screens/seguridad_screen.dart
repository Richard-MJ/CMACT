import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/biometria/providers/biometria_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class SeguridadScreen extends ConsumerStatefulWidget {
  const SeguridadScreen({super.key});

  @override
  SeguridadScreenState createState() => SeguridadScreenState();
}

class SeguridadScreenState extends ConsumerState<SeguridadScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(biometriaProvider.notifier).initDatos();
      ref.read(biometriaProvider.notifier).verificarBiometricos();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Seguridad',
      child: _ConfigurarView(),
    );
  }
}

class _ConfigurarView extends ConsumerWidget {
  const _ConfigurarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final biometriaState = ref.watch(biometriaProvider);

    return CustomScrollView(
      slivers: [
        SliverToBoxAdapter(
          child: Container(
            padding: const EdgeInsets.symmetric(
              horizontal: 24,
              vertical: 48,
            ),
            child: Column(
              children: [
                if (biometriaState.touchId || biometriaState.faceId)
                  const Text(
                    'Selecciona cómo deseas proteger tu inicio de sesión',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                if (!biometriaState.dispositivoConBiometricos)
                  const Text(
                    "Tu dispositivo no cuenta con sensores biométricos compatibles. La autenticación biométrica no está disponible en este dispositivo.",
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                if (!biometriaState.touchId &&
                    !biometriaState.faceId &&
                    biometriaState.dispositivoConBiometricos)
                  const Text(
                    'Activa tus datos biométricos (huella dactilar o reconocimiento facial) en tu celular y accede fácilmente a Caja Tacna App.\n¡Es rápido y seguro!',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                if (biometriaState.touchId)
                  const SizedBox(
                    height: 16,
                  ),
                if (biometriaState.touchId)
                  CtCardButton(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 16,
                      vertical: 18,
                    ),
                    onPressed: () {
                      context.push('/biometria/huella');
                    },
                    child: Row(
                      children: [
                        SvgPicture.asset(
                          'assets/icons/touch_id.svg',
                          height: 24,
                        ),
                        const SizedBox(
                          width: 16,
                        ),
                        const Text(
                          'Huella dactilar',
                          style: TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w500,
                            color: AppColors.gray900,
                            height: 22 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                      ],
                    ),
                  ),
                if (biometriaState.faceId)
                  const SizedBox(
                    height: 16,
                  ),
                if (biometriaState.faceId)
                  CtCardButton(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 16,
                      vertical: 18,
                    ),
                    onPressed: () {
                      context.push('/biometria/face-id');
                    },
                    child: Row(
                      children: [
                        SvgPicture.asset(
                          'assets/icons/face_id.svg',
                          height: 24,
                        ),
                        const SizedBox(
                          width: 16,
                        ),
                        const Text(
                          'Face ID',
                          style: TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w500,
                            color: AppColors.gray900,
                            height: 22 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                      ],
                    ),
                  )
              ],
            ),
          ),
        )
      ],
    );
  }
}

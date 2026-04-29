import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/widgets/bg_afiliacion_exitosa.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class AfiliacionExitosaScreen extends ConsumerWidget {
  const AfiliacionExitosaScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    EdgeInsets safeAreaPadding = MediaQuery.of(context).padding;

    return PopScope(
      canPop: false,
      onPopInvoked: (didPop) {
        Future.microtask(() {
          context.go('/inicio-sesion/formulario');
        });
      },
      child: Scaffold(
        body: Stack(
          children: [
            const BgAfiliacionExitosa(),
            LayoutBuilder(
              builder: (context, constraints) => SingleChildScrollView(
                physics: const ClampingScrollPhysics(),
                padding: const EdgeInsets.only(bottom: 0),
                child: Container(
                  constraints: constraints.copyWith(
                    minHeight: constraints.maxHeight,
                    maxHeight: double.infinity,
                  ),
                  width: double.infinity,
                  child: Container(
                    padding: EdgeInsets.only(
                      top: 46 + safeAreaPadding.top,
                      left: 24,
                      right: 24,
                      bottom: 56 + MediaQuery.of(context).padding.bottom,
                    ),
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        Column(
                          children: [
                            Image.asset(
                              'assets/icons/afiliacion-canales-electronicos/logo_rojo.png',
                              width: 164,
                            ),
                            const SizedBox(
                              height: 87,
                            ),
                            SvgPicture.asset(
                              'assets/icons/check-input.svg',
                              height: 110,
                            ),
                            const SizedBox(
                              height: 23,
                            ),
                            const Text(
                              '¡Felicidades!',
                              style: TextStyle(
                                fontSize: 28,
                                fontWeight: FontWeight.w700,
                                color: AppColors.gray900,
                                height: 40 / 28,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                            const SizedBox(
                              height: 8,
                            ),
                            const Text(
                              'Tu afiliación fue exitosa',
                              style: TextStyle(
                                fontSize: 18,
                                fontWeight: FontWeight.w500,
                                color: AppColors.gray900,
                                height: 32 / 18,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                            const SizedBox(
                              height: 8,
                            ),
                            Container(
                              constraints: const BoxConstraints(maxWidth: 190),
                              child: const Text(
                                'Ya puedes empezar a usar Caja Tacna App',
                                style: TextStyle(
                                  fontSize: 16,
                                  fontWeight: FontWeight.w400,
                                  color: AppColors.gray900,
                                  height: 1.5,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                                textAlign: TextAlign.center,
                              ),
                            ),
                          ],
                        ),
                        CtButton(
                          text: 'Iniciar sesión',
                          type: ButtonType.outline,
                          onPressed: () {
                            context.go('/inicio-sesion/formulario');
                          },
                        ),
                      ],
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

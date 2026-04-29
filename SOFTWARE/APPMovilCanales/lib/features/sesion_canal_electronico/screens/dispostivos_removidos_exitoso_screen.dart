import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/providers/sesion_canal_electronico_provider.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/widgets/dispositivo_widget.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_3.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_message.dart';
import 'package:caja_tacna_app/features/shared/widgets/pop_home_scope.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';
import 'package:screenshot/screenshot.dart';

class DispositivosRemovidosExitosoScreen extends StatelessWidget {
  const DispositivosRemovidosExitosoScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const PopHomeScope(
      child: CtLayout3(
        child: _DispositivosRemovidosExitosoView(),
      ),
    );
  }
}

class _DispositivosRemovidosExitosoView extends ConsumerStatefulWidget {
  const _DispositivosRemovidosExitosoView();

  @override
  GiroExitosoViewState createState() => GiroExitosoViewState();
}

class GiroExitosoViewState
    extends ConsumerState<_DispositivosRemovidosExitosoView> {
  ScreenshotController screenshotController = ScreenshotController();

  @override
  Widget build(BuildContext context) {
    final sesionCanalElectronicoState =
        ref.watch(sesionCanalElectronicoProvider);
    final homeState = ref.watch(homeProvider);

    return SingleChildScrollView(
      child: Column(
        children: [
          Screenshot(
            controller: screenshotController,
            child: Container(
              padding: const EdgeInsets.only(
                top: 35,
                bottom: 36,
                left: 24,
                right: 24,
              ),
              color: AppColors.white,
              child: Column(
                children: [
                  SvgPicture.asset(
                    'assets/icons/operacion/logo-caja-ANCHO.svg',
                    height: 90,
                    colorFilter: const ColorFilter.mode(
                        AppColors.primary700, BlendMode.srcIn),
                  ),
                  const SizedBox(
                    height: 16,
                  ),
                  const Text(
                    'Dispositivos seguros retirados',
                    style: TextStyle(
                      fontSize: 18,
                      fontWeight: FontWeight.w800,
                      color: AppColors.gray900,
                      height: 28 / 18,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  const SizedBox(
                    height: 48,
                  ),
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
                        'Retirar dispositivos seguros',
                        style: TextStyle(
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
                    height: 24,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Fecha de operación',
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
                          Text(
                            CtUtils.formatDate(sesionCanalElectronicoState
                                .removerResponse?.fechaOperacion),
                            style: const TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w400,
                              color: AppColors.gray900,
                              height: 19 / 16,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                          Text(
                            CtUtils.formatTime(sesionCanalElectronicoState
                                .removerResponse?.fechaOperacion),
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
                    ],
                  ),
                  const SizedBox(
                    height: 24,
                  ),
                  const Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Dispositivos retirados',
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
                    ],
                  ),
                  const SizedBox(
                    height: 24,
                  ),
                  // Aquí, en lugar de un ListView, utilizamos una Column para mostrar los dispositivos
                  Column(
                    children: List.generate(
                      sesionCanalElectronicoState
                          .removerResponse!.dispositivos.length,
                      (index) => Padding(
                        padding: const EdgeInsets.only(bottom: 5),
                        child: DispositivoView(
                          confirmar: true,
                          dispositivoActual: false,
                          dispositivoSeguro: sesionCanalElectronicoState
                              .removerResponse!.dispositivos[index],
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
          Container(
            padding: const EdgeInsets.only(
              bottom: 35,
              left: 24,
              right: 24,
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                CtMessage(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Notificaremos la operación al correo',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 22 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      Text(
                        CtUtils.hashearCorreo(
                            homeState.datosCliente?.correoElectronico),
                        style: const TextStyle(
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
                const SizedBox(
                  height: 24,
                ),
                CtButton(
                  text: 'Volver al inicio',
                  onPressed: () {
                    context.go('/home');
                  },
                  type: ButtonType.outline,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/providers/afiliacion_celular_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_notificacion_operacion.dart';
import 'package:caja_tacna_app/features/shared/widgets/pop_home_scope.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_3.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/svg.dart';
import 'package:screenshot/screenshot.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';

class ConfiguracionExitosaScreen extends StatelessWidget {
  const ConfiguracionExitosaScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return PopHomeScope(
      child: CtLayout3(
        child: _OperacionExitosaView(),
      ),
    );
  }
}

class _OperacionExitosaView extends ConsumerStatefulWidget {
  @override
  OperacionExitosaViewState createState() => OperacionExitosaViewState();
}

class OperacionExitosaViewState extends ConsumerState<_OperacionExitosaView> {
  ScreenshotController screenshotController = ScreenshotController();

  @override
  Widget build(BuildContext context) {
    final afiliacionCelularState = ref.watch(afiliacionCelularProvider);
    final homeState = ref.watch(homeProvider);

    return Column(
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
                Text(
                  'Configuración exitosa',
                  style: const TextStyle(
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
                    Expanded(
                      child: Text(
                        'Configuración de notificaciones',
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
                  height: 36,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Operaciones recibidas',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                      child: Text(
                        afiliacionCelularState.configuracionNotificacionResponse
                                    ?.notificarOperacionesRecibidas ??
                                false
                            ? 'Habilitado'
                            : 'Deshabilitado',
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
                  height: 36,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Operaciones enviadas',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                      child: Text(
                        afiliacionCelularState.configuracionNotificacionResponse
                                    ?.notificarOperacionesEnviadas ??
                                false
                            ? 'Habilitado'
                            : 'Deshabilitado',
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
                  height: 36,
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
                          CtUtils.formatDate(afiliacionCelularState
                              .configuracionNotificacionResponse
                              ?.fechaOperacion),
                          style: const TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray900,
                            height: 19 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                        Text(
                          CtUtils.formatTime(afiliacionCelularState
                              .configuracionNotificacionResponse
                              ?.fechaOperacion),
                          style: const TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray900,
                            height: 19 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                      ],
                    )
                  ],
                ),
              ],
            ),
          ),
        ),
        const Spacer(),
        Container(
          padding: const EdgeInsets.only(
            bottom: 56,
            left: 24,
            right: 24,
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              CtNotificacionOperacion(
                correoElectronico: homeState.datosCliente?.correoElectronico,
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
              )
            ],
          ),
        ),
      ],
    );
  }
}

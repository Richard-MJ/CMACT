import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/compras_internet/providers/compras_internet_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
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

class AfiliacionExitosaScreen extends StatelessWidget {
  const AfiliacionExitosaScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const PopHomeScope(
      child: CtLayout3(
        child: _AfiliacionExitosaView(),
      ),
    );
  }
}

class _AfiliacionExitosaView extends ConsumerStatefulWidget {
  const _AfiliacionExitosaView();

  @override
  AfiliacionExitosaViewState createState() => AfiliacionExitosaViewState();
}

class AfiliacionExitosaViewState extends ConsumerState<_AfiliacionExitosaView> {
  ScreenshotController screenshotController = ScreenshotController();

  @override
  Widget build(BuildContext context) {
    final comprasInternetState = ref.watch(comprasInternetProvider);
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
                const Text(
                  'Se han guardado los cambios',
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
                          CtUtils.formatDate(
                            comprasInternetState.metodoAfiliacion ==
                                    MetodoAfiliacion.afiliacion
                                ? comprasInternetState
                                    .confirmarAfiliacionResponse?.fechaOperacion
                                : comprasInternetState
                                    .confirmarDesafiliacionResponse
                                    ?.fechaOperacion,
                          ),
                          style: const TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray900,
                            height: 19 / 16,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                        Text(
                          CtUtils.formatTime(
                            comprasInternetState.metodoAfiliacion ==
                                    MetodoAfiliacion.afiliacion
                                ? comprasInternetState
                                    .confirmarAfiliacionResponse?.fechaOperacion
                                : comprasInternetState
                                    .confirmarDesafiliacionResponse
                                    ?.fechaOperacion,
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
                    Text(
                      comprasInternetState.metodoAfiliacion ==
                              MetodoAfiliacion.afiliacion
                          ? 'Cuenta habilitada'
                          : 'Cuenta deshabilita',
                      style: const TextStyle(
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
                            comprasInternetState.metodoAfiliacion ==
                                    MetodoAfiliacion.afiliacion
                                ? comprasInternetState
                                        .confirmarAfiliacionResponse
                                        ?.nombreProductoCuentaAfiliada ??
                                    ''
                                : comprasInternetState
                                        .confirmarDesafiliacionResponse
                                        ?.nombreProductoCuentaAfiliada ??
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
                                  comprasInternetState.metodoAfiliacion ==
                                          MetodoAfiliacion.afiliacion
                                      ? comprasInternetState
                                          .confirmarAfiliacionResponse
                                          ?.numeroCuentaAfiliada
                                      : comprasInternetState
                                          .confirmarDesafiliacionResponse
                                          ?.numeroCuentaAfiliada,
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
              ],
            ),
          ),
        ),
        Expanded(
          child: Container(
            padding: const EdgeInsets.only(
              bottom: 56,
              left: 24,
              right: 24,
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                const Spacer(),
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
        ),
      ],
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/providers/cancelacion_cuentas_provider.dart';
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

class CancelacionExitosaInterbancariaScreen extends StatelessWidget {
  const CancelacionExitosaInterbancariaScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const PopHomeScope(
      child: CtLayout3(
        child: _CancelacionExitosaInterbancariaView(),
      ),
    );
  }
}

class _CancelacionExitosaInterbancariaView extends ConsumerStatefulWidget {
  const _CancelacionExitosaInterbancariaView();

  @override
  CancelacionExitosaInternaViewState createState() =>
      CancelacionExitosaInternaViewState();
}

class CancelacionExitosaInternaViewState
    extends ConsumerState<_CancelacionExitosaInterbancariaView> {
  ScreenshotController screenshotController = ScreenshotController();

  @override
  Widget build(BuildContext context) {
    final cancelarState = ref.watch(cancelacionCuentasProvider);
    final homeState = ref.watch(homeProvider);

    return Column(
      children: [
        Screenshot(
          controller: screenshotController,
          child: Container(
            padding: const EdgeInsets.only(
              top: 35,
              bottom: 48,
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
                  'Cancelaste tu cuenta de ahorros',
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
                      'N° de operación',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Text(
                      cancelarState
                              .confirmarInterbancariaResponse?.numeroOperacion
                              .toString() ??
                          '',
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
                  height: 37,
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
                          CtUtils.formatDate(cancelarState
                              .confirmarInterbancariaResponse?.fechaOperacion),
                          style: const TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray900,
                            height: 19 / 16,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                        Text(
                          CtUtils.formatTime(cancelarState
                              .confirmarInterbancariaResponse?.fechaOperacion),
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
                    const Text(
                      'Cuenta cancelada',
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
                          Text(
                            CtUtils.formatNumeroCuenta(
                              numeroCuenta:
                                  cancelarState.cuentaAhorro?.identificador,
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
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Banco de destino',
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
                      child: Text(
                        cancelarState.cancelarInterbancariaResponse
                                ?.nombreEntidadCce ??
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
                      'Cuenta de destino',
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
                      child: Text(
                        cancelarState.cuentaDestinoCci.value,
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
                      'Beneficiario',
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
                            cancelarState.nombreBeneficiario.value,
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
                            '${cancelarState.tipoDocumento?.descripcion} ${cancelarState.numeroDocumento.value}',
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
                      'Monto transferido',
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
                        cancelarState
                            .confirmarInterbancariaResponse?.montoTransferencia,
                        cancelarState.confirmarInterbancariaResponse
                            ?.simboloMonedaTransferencia,
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
              )
            ],
          ),
        ),
      ],
    );
  }
}

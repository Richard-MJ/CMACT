import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/providers/apertura_cuentas_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_3.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_compartir.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_enviar_correo.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_message.dart';
import 'package:caja_tacna_app/features/shared/widgets/pop_home_scope.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';
import 'package:screenshot/screenshot.dart';

class AperturaExitosaScreen extends StatelessWidget {
  const AperturaExitosaScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const PopHomeScope(
      child: CtLayout3(
        child: _AperturaExitosaView(),
      ),
    );
  }
}

class _AperturaExitosaView extends ConsumerStatefulWidget {
  const _AperturaExitosaView();

  @override
  AperturaExitosaViewState createState() => AperturaExitosaViewState();
}

class AperturaExitosaViewState extends ConsumerState<_AperturaExitosaView> {
  ScreenshotController screenshotController = ScreenshotController();

  @override
  Widget build(BuildContext context) {
    final aperturaState = ref.watch(aperturaCuentasProvider);
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
                  aperturaState.tipoCuenta?.codigoSistema == 'DP'
                      ? 'Apertura de DPF exitoso'
                      : 'Apertura de cuenta exitosa',
                  style: const TextStyle(
                    fontSize: 18,
                    fontWeight: FontWeight.w800,
                    color: AppColors.gray900,
                    height: 28 / 18,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 36,
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
                    SizedBox(
                      width: 140,
                      child: Text(
                        aperturaState.tipoCuenta?.codigoSistema == 'DP'
                            ? 'Apertura de DPF'
                            : 'Apertura de cuenta de ahorros',
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
                          aperturaState.confirmarResponse?.fechaHoraTran ?? '',
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
                  height: 24,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    const Text(
                      'N° de cuenta',
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
                        aperturaState.confirmarResponse?.nroCuenta ?? '',
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
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    const Text(
                      'CCI',
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
                        aperturaState.confirmarResponse?.nroCuentaCci ?? '',
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
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Monto de apertura',
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
                        double.tryParse(
                          aperturaState.confirmarResponse?.montoApertura ?? '',
                        ),
                        aperturaState.confirmarResponse?.moneda,
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
                if (aperturaState.tipoCuenta?.codigoSistema == 'DP') ...[
                  const SizedBox(
                    height: 24,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Tasa de interés',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      Text(
                        '${aperturaState.calculoDpfResponse?.datos.tasaEfectivaAnual} %',
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
                    height: 24,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Plazo (en días)',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      Text(
                        aperturaState.diasDpf.value,
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
                    height: 24,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Fecha de apertura',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      Text(
                        aperturaState.calculoDpfResponse?.datos.fechaApertura ??
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
                    height: 24,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Fecha de vencimiento',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      Text(
                        aperturaState
                                .calculoDpfResponse?.datos.fechaVencimiento ??
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
                ],
                const SizedBox(
                  height: 24,
                ),
                const Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Estado de cuenta',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Text(
                      'Activa',
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
                CtInputEnviarCorreo(
                  onSend: () {
                    ref
                        .read(aperturaCuentasProvider.notifier)
                        .reenviarComprobante();
                  },
                  email: aperturaState.correoElectronicoDestinatario,
                  onChangeEmail: (email) {
                    ref
                        .read(aperturaCuentasProvider.notifier)
                        .changeCorreoDestinatario(email);
                  },
                ),
                const SizedBox(
                  height: 16,
                ),
                CtCompartir(screenshotController: screenshotController),
                const SizedBox(
                  height: 56,
                ),
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

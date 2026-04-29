import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/providers/afiliacion_celular_provider.dart';
import 'package:caja_tacna_app/features/billetera_virtual/shared/widgets/billetera_action_buttons.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_6.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';

class VerificarOperacionScreen extends ConsumerStatefulWidget {
  const VerificarOperacionScreen({super.key});

  @override
  VerificarOperacionScreenState createState() =>
      VerificarOperacionScreenState();
}

class VerificarOperacionScreenState
    extends ConsumerState<VerificarOperacionScreen> {
  @override
  Widget build(BuildContext context) {
    return const CtLayout6(
      title: 'Confirma la operación',
      actions: BilleteraActionButtons(
        showSettings: false,
      ),
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _VerificarOperacionView(),
          )
        ],
      ),
    );
  }
}

class _VerificarOperacionView extends ConsumerWidget {
  const _VerificarOperacionView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final afiliacionCelularState = ref.watch(afiliacionCelularProvider);
    final timerState = ref.watch(timerProvider);

    return Container(
      padding: const EdgeInsets.only(top: 24, left: 24, right: 24, bottom: 36),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Operación',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w600,
                  color: AppColors.gray900,
                  height: 19 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Expanded(
                child: Text(
                  afiliacionCelularState.tituloOperacion,
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 19 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  textAlign: TextAlign.end,
                ),
              ),
            ],
          ),
          if (!afiliacionCelularState.datosAfiliacion!.indicadorAfiliacionCCE) ...[
            const SizedBox(height: 48),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Nro de Celular',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray900,
                    height: 19 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Expanded(
                  child: Text(
                    afiliacionCelularState.numeroCelular.value,
                    style: const TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 19 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    textAlign: TextAlign.end,
                  ),
                ),
              ],
            ),
          ],
          const SizedBox(height: 48),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Cuenta',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w600,
                  color: AppColors.gray900,
                  height: 19 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      afiliacionCelularState.datosAfiliacion?.nombreProducto ??
                          '',
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    Text(
                      CtUtils.formatNumeroCuenta(
                        numeroCuenta: afiliacionCelularState
                            .datosAfiliacion?.numeroCuentaAfiliada,
                      ),
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray500,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                  ],
                ),
              )
            ],
          ),
          if (afiliacionCelularState.datosAfiliacion!.indicadorAfiliacionCCE) ...[
            const SizedBox(height: 48),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Operaciones recibidas',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray900,
                    height: 19 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Expanded(
                  child: Text(
                    afiliacionCelularState.notificarOperacionesRecibidas
                        ? 'Habilitado'
                        : 'Deshabilitado',
                    style: const TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 19 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    textAlign: TextAlign.end,
                  ),
                ),
              ],
            ),
            const SizedBox(
              height: 48,
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Operaciones enviadas',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray900,
                    height: 19 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Expanded(
                  child: Text(
                    afiliacionCelularState.notificarOperacionesEnviadas
                        ? 'Habilitado'
                        : 'Deshabilitado',
                    style: const TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 19 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    textAlign: TextAlign.end,
                  ),
                ),
              ],
            ),
          ],
          const Spacer(),
          if (!afiliacionCelularState.datosAfiliacion!.indicadorAfiliacionCCE) ...[
            Row(
              children: [
                CtCheckbox(
                  value: afiliacionCelularState.notificarOperacionesRecibidas,
                  onPressed: () {
                    ref
                        .read(afiliacionCelularProvider.notifier)
                        .changeNotificarOperacionesRecibidas(
                          !afiliacionCelularState.notificarOperacionesRecibidas,
                        );
                  },
                ),
                const SizedBox(width: 8),
                RichText(
                  text: const TextSpan(
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w400,
                      height: 22 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    children: <TextSpan>[
                      TextSpan(
                        text: 'Notificaciones por ',
                        style: TextStyle(color: AppColors.black),
                      ),
                      TextSpan(
                        text: 'operaciones recibidas',
                        style: TextStyle(color: AppColors.primary700),
                      ),
                    ],
                  ),
                ),
              ],
            ),
            const SizedBox(height: 10),
            Row(
              children: [
                CtCheckbox(
                  value: afiliacionCelularState.notificarOperacionesEnviadas,
                  onPressed: () {
                    ref
                        .read(afiliacionCelularProvider.notifier)
                        .changeNotificarOperacionesEnviadas(
                          !afiliacionCelularState.notificarOperacionesEnviadas,
                        );
                  },
                ),
                const SizedBox(width: 8),
                RichText(
                  text: const TextSpan(
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w400,
                      height: 22 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    children: <TextSpan>[
                      TextSpan(
                        text: 'Notificaciones por ',
                        style: TextStyle(color: AppColors.black),
                      ),
                      TextSpan(
                        text: 'operaciones enviadas',
                        style: TextStyle(color: AppColors.primary700),
                      ),
                    ],
                  ),
                ),
              ],
            ),
            const SizedBox(height: 26),
          ],
          Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Ingresa tu token digital',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w800,
                  color: AppColors.gray900,
                  height: 28 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              const SizedBox(height: 14),
              CtOtp(
                value: afiliacionCelularState.tokenDigital,
                onChanged: (value) {
                  ref
                      .read(afiliacionCelularProvider.notifier)
                      .changeToken(value);
                },
              ),
              const SizedBox(height: 13),
              if (timerState.timerOn)
                const CtTimer()
              else
                Align(
                  alignment: Alignment.centerRight,
                  child: GestureDetector(
                    onTap: () {
                      ref
                          .read(afiliacionCelularProvider.notifier)
                          .afiliar(withPush: false);
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
            ],
          ),
          const SizedBox(height: 24),
          Column(
            children: [
              const SizedBox(height: 17),
              CtButton(
                text: 'Confirmar',
                disabled: afiliacionCelularState.tokenDigital.length < 6,
                onPressed: () {
                  ref.read(afiliacionCelularProvider.notifier).confirmar();
                },
              ),
            ],
          )
        ],
      ),
    );
  }
}

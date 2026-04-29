import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/providers/apertura_cuentas_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfirmarScreen extends StatelessWidget {
  const ConfirmarScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Confirma la operación',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _ConfirmarView(),
          )
        ],
      ),
    );
  }
}

class _ConfirmarView extends ConsumerWidget {
  const _ConfirmarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final aperturaState = ref.watch(aperturaCuentasProvider);
    final timerState = ref.watch(timerProvider);
    final bool disabledButton = aperturaState.tokenDigital.isEmpty ||
        aperturaState.tokenDigital.length != 6 ||
        !aperturaState.aceptarCartilla ||
        !aperturaState.aceptarClausulas ||
        !aperturaState.aceptarTdp;

    return Container(
      padding: const EdgeInsets.only(top: 61, bottom: 56, left: 24, right: 24),
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
              SizedBox(
                width: 200,
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
            height: 37,
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
                  double.parse(aperturaState.monto.value),
                  aperturaState.cuentaOrigen?.simboloMonedaProducto,
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
          const SizedBox(
            height: 37,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Cuenta de cargo',
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
                      aperturaState.cuentaOrigen?.nombreTipoProducto ?? '',
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
                            aperturaState.cuentaOrigen?.numeroProducto,
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
          if (aperturaState.tipoCuenta?.codigoSistema == 'DP') ...[
            const SizedBox(
              height: 37,
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
              height: 37,
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
              height: 37,
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
                  aperturaState.calculoDpfResponse?.datos.fechaApertura ?? '',
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
                  aperturaState.calculoDpfResponse?.datos.fechaVencimiento ??
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
            height: 30,
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
            value: aperturaState.tokenDigital,
            onChanged: (value) {
              ref.read(aperturaCuentasProvider.notifier).changeToken(value);
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
                          .read(aperturaCuentasProvider.notifier)
                          .abrirCuenta(withPush: false);
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
          const SizedBox(
            height: 25,
          ),
          const Spacer(),
          const Text(
            'Declaro y acepto haber leído los siguientes documentos:',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 9,
          ),
          Row(
            children: [
              CtCheckbox(
                value: aperturaState.aceptarClausulas,
                onPressed: () {
                  ref
                      .read(aperturaCuentasProvider.notifier)
                      .toggleAceptarClausulas();
                },
              ),
              const SizedBox(
                width: 8,
              ),
              RichText(
                text: TextSpan(
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  children: <TextSpan>[
                    TextSpan(
                      text: 'Cláusulas contractuales',
                      style: const TextStyle(color: AppColors.primary700),
                      recognizer: TapGestureRecognizer()
                        ..onTap = () async {
                          CtUtils.abrirWeb(
                              url:
                                  'https://cmactacna.com.pe/wp-content/uploads/2025/02/CCPSC01-2025-Contrato-Aprobado-Resolucion-SBS-N-04209-2024.pdf');
                        },
                    ),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 10,
          ),
          Row(
            children: [
              CtCheckbox(
                value: aperturaState.aceptarCartilla,
                onPressed: () {
                  ref
                      .read(aperturaCuentasProvider.notifier)
                      .toggleAceptarCartilla();
                },
              ),
              const SizedBox(
                width: 8,
              ),
              RichText(
                text: TextSpan(
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  children: <TextSpan>[
                    TextSpan(
                      text: 'Cartilla de información',
                      style: const TextStyle(color: AppColors.primary700),
                      recognizer: TapGestureRecognizer()
                        ..onTap = () async {
                          CtUtils.abrirWeb(
                              url:
                                  'https://cmactacna.com.pe/wp-content/uploads/2025/09/CAR-AHO-AH-03-2025-1.pdf');
                        },
                    ),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 10,
          ),
          Row(
            children: [
              CtCheckbox(
                value: aperturaState.aceptarTdp,
                onPressed: () {
                  ref.read(aperturaCuentasProvider.notifier).toggleAceptarTdp();
                },
              ),
              const SizedBox(
                width: 8,
              ),
              RichText(
                text: TextSpan(
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  children: <TextSpan>[
                    TextSpan(
                      text: 'Tratamiento de Datos Personales',
                      style: const TextStyle(color: AppColors.primary700),
                      recognizer: TapGestureRecognizer()
                        ..onTap = () async {
                          CtUtils.abrirWeb(
                              url:
                                  'https://cmactacna.com.pe/wp-content/uploads/2025/03/TDP-01-2025-nuevo-1.pdf');
                        },
                    ),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 24,
          ),
          CtButton(
            text: 'Confirmar',
            onPressed: () {
              ref.read(aperturaCuentasProvider.notifier).confirmar();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

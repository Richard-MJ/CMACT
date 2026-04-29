import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/billetera_virtual/shared/widgets/billetera_action_buttons.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_6.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/providers/transferencia_celular_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfirmarTransferenciaScreen extends ConsumerStatefulWidget {
  const ConfirmarTransferenciaScreen({super.key});

  @override
  ConfirmarTransferenciaScreenState createState() =>
      ConfirmarTransferenciaScreenState();
}

class ConfirmarTransferenciaScreenState
    extends ConsumerState<ConfirmarTransferenciaScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout6(
      title: 'Confirma la operación',
      actions: BilleteraActionButtons(
        showSettings: false,
      ),
      child: _ConfirmarTransferenciaView(),
    );
  }
}

class _ConfirmarTransferenciaView extends ConsumerWidget {
  const _ConfirmarTransferenciaView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final transferirState = ref.watch(transferenciaCelularProvider);
    final timerState = ref.watch(timerProvider);

    return Container(
      padding: const EdgeInsets.only(top: 24, left: 24, right: 24, bottom: 36),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          const Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
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
                  'Transferir dinero a \ncontacto',
                  style: TextStyle(
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
            height: 24,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Contacto',
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
                      transferirState.detalleTransferencia!.nombreReceptor,
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
                      transferirState.contactoSeleccionada!.numeroCelular,
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
          const SizedBox(
            height: 24,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Cuenta Origen',
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
                      transferirState.datosClienteOrigenResponse!.cuentaEfectivo
                          .nombreProducto,
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
                        numeroCuenta: transferirState
                            .datosClienteOrigenResponse!
                            .cuentaEfectivo
                            .numeroCuenta,
                        hash: true,
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
          const SizedBox(
            height: 24,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Billetera destino',
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
                  transferirState
                          .entidadFinancieraSeleccionada?.nombreEntidad ??
                      "",
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
            height: 24,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Monto a enviar',
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
                  CtUtils.formatCurrency(
                    transferirState.montosTotales!.controlMonto.monto,
                    transferirState.datosAfiliacion?.simboloMoneda,
                  ),
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
          if (transferirState
                  .montosTotales!.controlMonto.montoComisionEntidad! >
              0)
            const SizedBox(
              height: 24,
            ),
          if (transferirState
                  .montosTotales!.controlMonto.montoComisionEntidad! >
              0)
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Comisión',
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
                    CtUtils.formatCurrency(
                      transferirState
                          .montosTotales!.controlMonto.montoComisionEntidad,
                      transferirState.datosAfiliacion?.simboloMoneda,
                    ),
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
            height: 24,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'ITF',
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
                  CtUtils.formatCurrency(
                    transferirState.montosTotales!.controlMonto.itf,
                    transferirState.datosAfiliacion?.simboloMoneda,
                  ),
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
          const Spacer(),
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
              const SizedBox(
                height: 14,
              ),
              CtOtp(
                value: transferirState.tokenDigital,
                onChanged: (value) {
                  ref
                      .read(transferenciaCelularProvider.notifier)
                      .changeToken(value);
                },
              ),
              const SizedBox(height: 13),
              timerState.timerOn
                  ? const CtTimer()
                  : Align(
                      alignment: Alignment.centerRight,
                      child: GestureDetector(
                        onTap: () {
                          ref
                              .read(transferenciaCelularProvider.notifier)
                              .obtenerMontosYToken(withPush: false);
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
          const SizedBox(
            height: 24,
          ),
          Column(
            children: [
              CtButton(
                text: 'Confirmar',
                disabled: transferirState.tokenDigital.length < 6,
                onPressed: () {
                  ref.read(transferenciaCelularProvider.notifier).confirmar();
                },
              ),
            ],
          )
        ],
      ),
    );
  }
}

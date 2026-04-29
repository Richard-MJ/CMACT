import 'package:animate_do/animate_do.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_agregar_operaciones_frecuentes.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_terceros/providers/transferencia_terceros_provider.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_alias_operacion.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfirmarTransferenciaATercerosScreen extends ConsumerStatefulWidget {
  const ConfirmarTransferenciaATercerosScreen({super.key});

  @override
  ConfirmarTransferenciaATercerosScreenState createState() =>
      ConfirmarTransferenciaATercerosScreenState();
}

class ConfirmarTransferenciaATercerosScreenState
    extends ConsumerState<ConfirmarTransferenciaATercerosScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Confirma la operación',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _ConfirmarTransferenciaATercerosView(),
          )
        ],
      ),
    );
  }
}

class _ConfirmarTransferenciaATercerosView extends ConsumerWidget {
  const _ConfirmarTransferenciaATercerosView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final timerState = ref.watch(timerProvider);
    final transferenciaState = ref.watch(transferenciaTercerosProvider);
    final bool disabledButton = transferenciaState.tokenDigital.isEmpty ||
        transferenciaState.tokenDigital.length != 6;

    return Container(
      padding: const EdgeInsets.only(top: 36, bottom: 56, left: 24, right: 24),
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
                width: 165,
                child: Text(
                  transferenciaState.transferirResponse?.descripcionOperacion ??
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
            height: 25,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Cuenta de origen',
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
                      '${transferenciaState.cuentaOrigen?.nombreTipoProducto}',
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
                            transferenciaState.cuentaOrigen?.numeroProducto,
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
              ),
            ],
          ),
          const SizedBox(
            height: 25,
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
                width: 30,
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      transferenciaState.transferirResponse?.cuentaDestino
                              .nombreTipoProducto ??
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
                            transferenciaState.numeroCuentaDestino.value,
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
              ),
            ],
          ),
          const SizedBox(
            height: 25,
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
                child: Text(
                  transferenciaState
                          .transferirResponse?.cuentaDestino.nombreCliente ??
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
            height: 25,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Monto',
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
                  double.parse(transferenciaState.monto.value),
                  transferenciaState
                      .transferirResponse?.cuentaDestino.simboloMonedaProducto,
                ),
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                textAlign: TextAlign.end,
              ),
            ],
          ),
          const SizedBox(
            height: 25,
          ),
          if (transferenciaState.transferirResponse?.montoReal.tipoCambio !=
              1) ...[
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Tipo de cambio',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 19 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Text(
                  transferenciaState.transferirResponse?.montoReal.tipoCambio
                          .toString() ??
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
              ],
            ),
            const SizedBox(
              height: 25,
            ),
          ],
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'ITF',
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
                  transferenciaState.transferirResponse?.montoItf,
                  transferenciaState
                      .transferirResponse?.cuentaOrigen.simboloMonedaProducto,
                ),
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                textAlign: TextAlign.end,
              ),
            ],
          ),
          const SizedBox(
            height: 36,
          ),
          Center(
            child: Center(
              child: CtAgregarOperacionesFrecuentes(
                value: transferenciaState.operacionFrecuente,
                onChanged: () {
                  ref
                      .read(transferenciaTercerosProvider.notifier)
                      .toggleOperacionFrecuente();
                },
              ),
            ),
          ),
          FadeIn(
            animate: transferenciaState.operacionFrecuente,
            duration: const Duration(milliseconds: 150),
            child: Container(
              padding: const EdgeInsets.only(top: 16),
              child: InputAliasOperacion(
                alias: transferenciaState.nombreOperacionFrecuente,
                onChanged: (operacionFrecuente) {
                  ref
                      .read(transferenciaTercerosProvider.notifier)
                      .changeNombreOperacionFrecuente(operacionFrecuente);
                },
              ),
            ),
          ),
          const Spacer(),
          const SizedBox(
            height: 20,
          ),
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
            value: transferenciaState.tokenDigital,
            onChanged: (value) {
              ref
                  .read(transferenciaTercerosProvider.notifier)
                  .changeToken(value);
            },
          ),
          const SizedBox(
            height: 13,
          ),
          const Spacer(),
          timerState.timerOn
              ? const CtTimer()
              : Align(
                  alignment: Alignment.centerRight,
                  child: GestureDetector(
                    onTap: () {
                      ref
                          .read(transferenciaTercerosProvider.notifier)
                          .transferir(withPush: false);
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
            height: 40,
          ),
          CtButton(
            text: 'Confirmar',
            onPressed: () {
              ref.read(transferenciaTercerosProvider.notifier).confirmar();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

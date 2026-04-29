import 'package:animate_do/animate_do.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_servicios/providers/pago_servicios_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_agregar_operaciones_frecuentes.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_alias_operacion.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfirmarScreen extends ConsumerStatefulWidget {
  const ConfirmarScreen({super.key});

  @override
  ConfirmarScreenState createState() => ConfirmarScreenState();
}

class ConfirmarScreenState extends ConsumerState<ConfirmarScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Confirma la operación',
      child: _ConfirmarView(),
    );
  }
}

class _ConfirmarView extends ConsumerWidget {
  const _ConfirmarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final timerState = ref.watch(timerProvider);
    final pagoServiciosState = ref.watch(pagoServiciosProvider);
    final bool disabledButton = pagoServiciosState.tokenDigital.isEmpty ||
        pagoServiciosState.tokenDigital.length != 6;

    return CustomScrollView(
      slivers: [
        SliverFillRemaining(
          hasScrollBody: false,
          child: Container(
            padding: const EdgeInsets.only(
              top: 36,
              bottom: 56,
              left: 24,
              right: 24,
            ),
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
                    const SizedBox(
                      width: 30,
                    ),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.end,
                        children: [
                          const Text(
                            'Pago de servicios',
                            style: TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w400,
                              color: AppColors.gray900,
                              height: 19 / 16,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                            textAlign: TextAlign.end,
                          ),
                          Text(
                            pagoServiciosState.servicioPagar?.nombreServicio ??
                                '',
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
                      'N° de suministro',
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
                        pagoServiciosState.numeroSuministro,
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
                        color: AppColors.black,
                        height: 1,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    const SizedBox(
                      width: 10,
                    ),
                    Expanded(
                      child: Text(
                        pagoServiciosState
                                    .cobroServicio?.nombreCliente.isEmpty ??
                                pagoServiciosState
                                        .cobroServicio?.nombreCliente ==
                                    null
                            ? 'Dato no registrado'
                            : (pagoServiciosState
                                        .cobroServicio?.nombreCliente ??
                                    '')
                                .toUpperCase(),
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.black,
                          height: 1.5,
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
                      'Monto a pagar',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 1,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    const SizedBox(
                      width: 30,
                    ),
                    Text(
                      CtUtils.formatCurrency(
                        pagoServiciosState.cobroServicio?.tipoServicio !=
                                'Kasnet'
                            ? pagoServiciosState.cobroServicio?.montoDeuda
                            : double.parse(
                                pagoServiciosState.montoDeudaServicio!.value),
                        pagoServiciosState.cobroServicio?.simboloMoneda,
                      ),
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 1,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 37,
                ),
                if (pagoServiciosState.pagarResponse?.montoReal.tipoCambio !=
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
                        pagoServiciosState.pagarResponse?.montoReal.tipoCambio
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
                    height: 36,
                  ),
                ],
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
                            pagoServiciosState
                                    .cuentaOrigen?.nombreTipoProducto ??
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
                              numeroCuenta: pagoServiciosState
                                  .cuentaOrigen?.numeroProducto,
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
                  height: 46,
                ),
                Center(
                  child: CtAgregarOperacionesFrecuentes(
                    value: pagoServiciosState.operacionFrecuente,
                    onChanged: () {
                      ref
                          .read(pagoServiciosProvider.notifier)
                          .toggleOperacionFrecuente();
                    },
                  ),
                ),
                FadeIn(
                  animate: pagoServiciosState.operacionFrecuente,
                  duration: const Duration(milliseconds: 150),
                  child: Container(
                    padding: const EdgeInsets.only(top: 16),
                    child: InputAliasOperacion(
                      alias: pagoServiciosState.nombreOperacionFrecuente,
                      onChanged: (operacionFrecuente) {
                        ref
                            .read(pagoServiciosProvider.notifier)
                            .changeNombreOperacionFrecuente(operacionFrecuente);
                      },
                    ),
                  ),
                ),
                const SizedBox(
                  height: 24,
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
                  value: pagoServiciosState.tokenDigital,
                  onChanged: (value) {
                    ref.read(pagoServiciosProvider.notifier).changeToken(value);
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
                                .read(pagoServiciosProvider.notifier)
                                .pagarServicio(withPush: false);
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
                const Spacer(),
                const SizedBox(
                  height: 24,
                ),
                CtButton(
                  text: 'Confirmar',
                  onPressed: () {
                    ref.read(pagoServiciosProvider.notifier).confirmar();
                  },
                  disabled: disabledButton,
                )
              ],
            ),
          ),
        )
      ],
    );
  }
}

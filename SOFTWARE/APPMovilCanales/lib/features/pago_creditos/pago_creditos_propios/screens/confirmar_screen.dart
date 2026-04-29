import 'package:animate_do/animate_do.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/opcion_pago_credito.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/providers/pago_creditos_propios_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/providers/pago_creditos_terceros_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/providers/pago_creditos_anticipo_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/index.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_alias_operacion.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/gestures.dart';
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
    final pagoCreditoState = ref.watch(pagoCreditoPropioProvider);
    final pagoCreditoAnticipoState =
        ref.watch(pagoCreditoAnticipoProvider);
    final timerState = ref.watch(timerProvider);

  final bool isTokenInvalid =
      pagoCreditoState.tokenDigital.isEmpty || 
      pagoCreditoState.tokenDigital.length != 6;

  final bool isAnticipoInvalid =
      pagoCreditoState.tipoPagoCredito == TipoPagoCredito.anticipo &&
      ((!pagoCreditoAnticipoState.aceptarNuevoCronograma && pagoCreditoState.tipoAnticipo != TipoAnticipo.adelantoCuota) 
      || !pagoCreditoAnticipoState.aceptarCondiciones);

  // final isAdelantoInvalid =
  //     pagoCreditoState.tipoPagoCredito == TipoPagoCredito.adelanto && 
  //     !pagoCreditoAnticipoState.aceptarCondiciones;

  final bool disabledButton = isTokenInvalid || isAnticipoInvalid; //|| isAdelantoInvalid;

    return CustomScrollView(
      slivers: [
        SliverFillRemaining(
          hasScrollBody: false,
          child: Container(
            padding: const EdgeInsets.only(
              top: 28,
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
                          Text(
                            pagoCreditoState.tipoAnticipo == TipoAnticipo.adelantoCuota
                              ? TipoPagoCredito.adelanto.obtenerDescripcion()
                              :  pagoCreditoState.tipoPagoCredito.obtenerDescripcion(),
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
                            pagoCreditoState
                                    .creditoAbonar?.descripcionSubProducto ??
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
                if (pagoCreditoState.tipoPagoCredito ==
                    TipoPagoCredito.anticipo && pagoCreditoState.tipoAnticipo != TipoAnticipo.adelantoCuota) ...[
                  const SizedBox(
                    height: 37,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Tipo de pago',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      const SizedBox(width: 15),
                      Expanded(
                        child: Text(
                          pagoCreditoState.tipoAnticipo.obtenerDescripcion(),
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
                ],
                const SizedBox(
                  height: 37,
                ),
                if (pagoCreditoState.tipoPago == TipoPago.aplicativo) ...[
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
                              pagoCreditoState.cuentaOrigen!.nombreTipoProducto,
                              style: const TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.w400,
                                color: AppColors.gray900,
                                height: 19 / 16,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                              textAlign: TextAlign.end,
                            ),
                            Text(
                              CtUtils.formatNumeroCuenta(
                                numeroCuenta: pagoCreditoState
                                    .cuentaOrigen!.numeroProducto,
                              ),
                              style: const TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.w400,
                                color: AppColors.gray500,
                                height: 19 / 16,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                              textAlign: TextAlign.end,
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                ],
                const SizedBox(
                  height: 37,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Número de crédito',
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
                        pagoCreditoState.creditoAbonar?.numeroCredito
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
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Text(
                      CtUtils.formatCurrency(
                        pagoCreditoState.tipoPagoCredito.esAnticipo()
                            ? double.parse(
                                pagoCreditoAnticipoState.montoAnticipo.value)
                            : double.parse(pagoCreditoState.monto.value),
                        pagoCreditoState.creditoAbonar?.simboloMoneda,
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
                if (pagoCreditoState.tipoPago == TipoPago.aplicativo) ...[
                  if (pagoCreditoState
                          .pagarAppResponse!.datosMontoReal.tipoCambio !=
                      1) ...[
                    const SizedBox(
                      height: 37,
                    ),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const Text(
                          'Monto a debitar',
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
                            pagoCreditoState
                                .pagarAppResponse!.datosMontoReal.valorReal,
                            pagoCreditoState
                                .cuentaOrigen!.simboloMonedaProducto,
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
                      height: 37,
                    ),
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
                          pagoCreditoState
                              .pagarAppResponse!.datosMontoReal.tipoCambio
                              .toString(),
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
                  ],
                  const SizedBox(
                    height: 37,
                  ),
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
                          pagoCreditoState
                                  .pagarAppResponse?.datosMontoReal.itf ??
                              0,
                          pagoCreditoState.cuentaOrigen!.simboloMonedaProducto,
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
                ],
                if (!pagoCreditoState.tipoPagoCredito.esAnticipo()) ...[
                  const SizedBox(
                    height: 36,
                  ),
                  Center(
                    child: CtAgregarOperacionesFrecuentes(
                      value: pagoCreditoState.operacionFrecuente,
                      onChanged: () {
                        ref
                            .read(pagoCreditoPropioProvider.notifier)
                            .toggleOperacionFrecuente();
                      },
                    ),
                  ),
                  FadeIn(
                    animate: pagoCreditoState.operacionFrecuente,
                    duration: const Duration(milliseconds: 150),
                    child: Container(
                      padding: const EdgeInsets.only(top: 16),
                      child: InputAliasOperacion(
                        alias: pagoCreditoState.nombreOperacionFrecuente,
                        onChanged: (operacionFrecuente) {
                          ref
                              .read(pagoCreditoPropioProvider.notifier)
                              .changeNombreOperacionFrecuente(
                                  operacionFrecuente);
                        },
                      ),
                    ),
                  ),
                ],
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
                  value: pagoCreditoState.tokenDigital,
                  onChanged: (value) {
                    ref
                        .read(pagoCreditoPropioProvider.notifier)
                        .changeToken(value);
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
                                .read(pagoCreditoPropioProvider.notifier)
                                .pagar(context: context, withPush: false);
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
                if (pagoCreditoState.tipoPagoCredito.esAnticipo()) ...[
                  const SizedBox(
                    height: 36,
                  ),
                  Row(
                    children: [
                      CtCheckbox(
                        value:
                            pagoCreditoAnticipoState.aceptarCondiciones,
                        onPressed: () {
                          ref
                              .read(pagoCreditoAnticipoProvider.notifier)
                              .toggleAceptarCondiciones();
                        },
                      ),
                      const SizedBox(
                        width: 8,
                      ),
                      Container(
                        width: double.infinity,
                        constraints: const BoxConstraints(maxWidth: 280),
                        child: RichText(
                          text: TextSpan(
                            style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w500,
                              height: 22 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                            children: <TextSpan>[
                              const TextSpan(
                                text: 'Acepto las implicancias económicas del ',
                                style: TextStyle(color: AppColors.gray700),
                              ),
                              TextSpan(
                                  text: pagoCreditoState.tipoAnticipo.obtenerIdentificador() == -1
                                      ? 'Adelanto de Cuotas'
                                      : 'Pago Anticipado',
                                  style: const TextStyle(color: AppColors.primary700),
                                  recognizer: TapGestureRecognizer()
                                    ..onTap = () async {
                                      await ref
                                          .read(pagoCreditoAnticipoProvider.notifier)
                                          .showCondicionesModal(
                                            context: context,
                                            tipoPagoCredito:
                                            pagoCreditoState.tipoAnticipo.obtenerIdentificador() == -1
                                              ? TipoPagoCredito.adelanto
                                              : pagoCreditoState.tipoPagoCredito,
                                            creditoPropio: true,
                                          );
                                    },
                                ),
                              const TextSpan(
                                text: ' realizado',
                                style: TextStyle(color: AppColors.gray700),
                              ),
                            ],
                          ),
                        ),
                      ),
                    ],
                  ),
                  if (pagoCreditoState.tipoPagoCredito ==
                      TipoPagoCredito.anticipo && pagoCreditoState.tipoAnticipo.obtenerIdentificador() != -1) ...[
                    const SizedBox(
                      height: 10,
                    ),
                    Row(
                      children: [
                        CtCheckbox(
                          value:
                              pagoCreditoAnticipoState.aceptarNuevoCronograma,
                          onPressed: () {
                            ref
                                .read(pagoCreditoAnticipoProvider.notifier)
                                .toggleAceptarNuevoCronograma();
                          },
                        ),
                        const SizedBox(
                          width: 8,
                        ),
                        Container(
                          width: double.infinity,
                          constraints: const BoxConstraints(maxWidth: 260),
                          child: RichText(
                            text: TextSpan(
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w500,
                                height: 22 / 14,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                              children: <TextSpan>[
                                const TextSpan(
                                  text: 'Acepto ',
                                  style: TextStyle(color: AppColors.gray700),
                                ),
                                TextSpan(
                                  text: 'nuevo cronograma de pagos',
                                  style: const TextStyle(
                                      color: AppColors.primary700),
                                  recognizer: TapGestureRecognizer()
                                    ..onTap = () async {
                                      await ref
                                          .read(pagoCreditoAnticipoProvider
                                              .notifier)
                                          .mostrarArchivoDocumentoPlanPago();
                                    },
                                )
                              ],
                            ),
                          ),
                        ),
                      ],
                    ),
                  ]
                ],
                const SizedBox(
                  height: 20,
                ),
                CtButton(
                  text: 'Confirmar',
                  onPressed: () {
                    ref.read(pagoCreditoPropioProvider.notifier).confirmar();
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

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/compras_internet/screens/configurar_afiliacion_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/opcion_pago_credito.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/widgets/input_monto_anticipo.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/widgets/tipo_pago_opcion.dart';
import 'package:caja_tacna_app/features/pago_creditos/providers/pago_creditos_anticipo_provider.dart';
import 'package:caja_tacna_app/features/providers.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/widgets/input_monto.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/index.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class PagarScreen extends StatelessWidget {
  const PagarScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Pago de créditos propios',
      child: _PagarView(),
    );
  }
}

class _PagarView extends ConsumerWidget {
  const _PagarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoCreditoState = ref.watch(pagoCreditoPropioProvider);
    final pagoCreditoAnticipoState = ref.watch(pagoCreditoAnticipoProvider);
    final bool isCuentaOrigenInvalida =
        pagoCreditoState.cuentaOrigen == null &&
        pagoCreditoState.tipoPago == TipoPago.aplicativo;

    final bool isMontoInvalido = pagoCreditoState.monto.isNotValid;

    final bool isAnticipoInvalido =
        pagoCreditoState.tipoPagoCredito == TipoPagoCredito.anticipo &&
        (
          pagoCreditoAnticipoState.montoAnticipo
              .copyWith(
                ignorarMinimo:
                    pagoCreditoState.tipoAnticipo == TipoAnticipo.adelantoCuota,
              )
              .isNotValid ||
          pagoCreditoState.tipoAnticipo == TipoAnticipo.defecto
        );

    final bool disabledButton = 
        isCuentaOrigenInvalida || isMontoInvalido || isAnticipoInvalido;

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
                if (pagoCreditoState.creditoAbonar != null)
                  CtCard(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 16,
                      vertical: 18,
                    ),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Expanded(
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              Text(
                                pagoCreditoState.creditoAbonar
                                        ?.descripcionSubProducto ??
                                    '',
                                style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w500,
                                  color: AppColors.gray900,
                                  height: 22 / 14,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                              ),
                              Text(
                                '${pagoCreditoState.creditoAbonar?.numeroCredito}',
                                style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w400,
                                  color: AppColors.gray900,
                                  height: 22 / 14,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                              ),
                              Text(
                                'Total a pagar: ${CtUtils.formatCurrency(pagoCreditoState.creditoAbonar?.montoTotalPago, pagoCreditoState.creditoAbonar?.simboloMoneda)}',
                                style: const TextStyle(
                                  fontSize: 12,
                                  fontWeight: FontWeight.w400,
                                  color: AppColors.gray900,
                                  height: 1.5,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                              ),
                            ],
                          ),
                        ),
                        Text(
                          CtUtils.formatCurrency(
                            pagoCreditoState.creditoAbonar?.montoSaldoCapital,
                            pagoCreditoState.creditoAbonar?.simboloMoneda,
                          ),
                          style: const TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.w500,
                            color: AppColors.gray900,
                            height: 1.5,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                      ],
                    ),
                  ),
                const SizedBox(
                  height: 36,
                ),
                TipoPagoOpcion(
                  pagoCreditoState: pagoCreditoState,
                  ref: ref,
                ),
                const SizedBox(
                  height: 36,
                ),
                if (pagoCreditoState.tipoPago == TipoPago.aplicativo) ...[
                  const Text(
                    'Cuenta de origen',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w500,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  const SizedBox(
                    height: 16,
                  ),
                  CtSelectCuenta(
                    cuentas: ref.watch(pagoCreditoPropioProvider).cuentasOrigen,
                    onChange: (producto) {
                      ref
                          .read(pagoCreditoPropioProvider.notifier)
                          .changeCuentaOrigen(producto);
                    },
                    value: pagoCreditoState.cuentaOrigen,
                  ),
                  const SizedBox(
                    height: 16,
                  ),
                  const Divider(
                    color: AppColors.gray300,
                    thickness: 1.0,
                    height: 0,
                  ),
                  const SizedBox(
                    height: 16,
                  )
                ],
                if (pagoCreditoState.tipoPagoCredito ==
                    TipoPagoCredito.cancelacion) ...[
                  const Text(
                    'Deuda total',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w500,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  const SizedBox(
                    height: 4,
                  ),
                  Text(
                    CtUtils.formatCurrency(
                      pagoCreditoState.creditoAbonar?.montoSaldoCancelacion,
                      pagoCreditoState.creditoAbonar?.simboloMoneda,
                    ),
                    style: const TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.w600,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ],
                if (pagoCreditoState.tipoPagoCredito ==
                    TipoPagoCredito.abono) ...[
                  const Text(
                    'Cuota',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w500,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  const SizedBox(
                    height: 4,
                  ),
                  InputMonto(
                    montoAbono: pagoCreditoState.creditoAbonar?.montoTotalPago,
                    simboloMoneda:
                        pagoCreditoState.creditoAbonar?.simboloMoneda,
                    monto: pagoCreditoState.monto,
                    onChangeMonto: (monto) {
                      ref
                          .read(pagoCreditoPropioProvider.notifier)
                          .changeMonto(monto);
                    },
                  ),
                ],
                if (pagoCreditoState.tipoPagoCredito.esAnticipo()) ...[
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      if (pagoCreditoState.tipoPagoCredito ==
                          TipoPagoCredito.anticipo) ...[
                        Container(
                          decoration: BoxDecoration(
                            border: Border.all(
                              color: pagoCreditoState.tipoAnticipo ==
                               TipoAnticipo.adelantoCuota
                                  ? Colors.transparent
                                  : AppColors.gray300,
                              width: 1,
                            ),
                            borderRadius: BorderRadius.circular(8),
                            boxShadow: AppColors.shadowSm,
                            color: pagoCreditoState.tipoAnticipo ==
                                    TipoAnticipo.adelantoCuota
                                ? AppColors.gray100
                                : AppColors.gray25,
                          ),
                          child: TextButton(
                            onPressed: () {
                              ref
                                  .read(pagoCreditoPropioProvider.notifier)
                                  .changeReduccionAnticipo(
                                      TipoAnticipo.adelantoCuota);
                            },
                            style: TextButton.styleFrom(
                              padding: const EdgeInsets.symmetric(
                                horizontal: 16,
                                vertical: 18,
                              ),
                              shape: const RoundedRectangleBorder(
                                borderRadius: BorderRadius.all(
                                  Radius.circular(8),
                                ),
                              ),
                            ),
                            child: Row(
                              children: [
                                CtSwitch(
                                  value: pagoCreditoState.tipoAnticipo ==
                                      TipoAnticipo.adelantoCuota,
                                  onTap: () {
                                    ref
                                        .read(
                                            pagoCreditoPropioProvider.notifier)
                                        .changeReduccionAnticipo(
                                            TipoAnticipo.adelantoCuota);
                                  },
                                ),
                                const SizedBox(
                                  width: 16,
                                ),
                                Expanded(
                                  child: AutoMarqueeText(
                                    text: TipoAnticipo.adelantoCuota
                                        .obtenerDescripcion(),
                                    style: const TextStyle(
                                      fontSize: 14,
                                      fontWeight: FontWeight.w500,
                                      color: AppColors.gray900,
                                      height: 22 / 14,
                                      leadingDistribution:
                                          TextLeadingDistribution.even,
                                      overflow: TextOverflow.ellipsis,
                                    ),
                                    activo: pagoCreditoState.tipoAnticipo == TipoAnticipo.adelantoCuota,
                                  ),
                                )
                              ],
                            ),
                          ),
                        ),
                        const SizedBox(
                          height: 15,
                        ),
                        Container(
                          decoration: BoxDecoration(
                            border: Border.all(
                              color: pagoCreditoState.tipoAnticipo ==
                                      TipoAnticipo.monto
                                  ? Colors.transparent
                                  : AppColors.gray300,
                              width: 1,
                            ),
                            borderRadius: BorderRadius.circular(8),
                            boxShadow: AppColors.shadowSm,
                            color: pagoCreditoState.tipoAnticipo ==
                                    TipoAnticipo.monto
                                ? AppColors.gray100
                                : AppColors.gray25,
                          ),
                          child: TextButton(
                            onPressed: () {
                              ref
                                  .read(pagoCreditoPropioProvider.notifier)
                                  .changeReduccionAnticipo(TipoAnticipo.monto);
                            },
                            style: TextButton.styleFrom(
                              padding: const EdgeInsets.symmetric(
                                horizontal: 16,
                                vertical: 18,
                              ),
                              shape: const RoundedRectangleBorder(
                                borderRadius: BorderRadius.all(
                                  Radius.circular(8),
                                ),
                              ),
                            ),
                            child: Row(
                              children: [
                                CtSwitch(
                                  value: pagoCreditoState.tipoAnticipo ==
                                      TipoAnticipo.monto,
                                  onTap: () {
                                    ref
                                      .read(pagoCreditoPropioProvider.notifier)
                                      .changeReduccionAnticipo(TipoAnticipo.monto);
                                  },
                                ),
                                const SizedBox(
                                  width: 16,
                                ),
                                Expanded(
                                  child: AutoMarqueeText(
                                    text: TipoAnticipo.monto.obtenerDescripcion(),
                                    style: const TextStyle(
                                      fontSize: 14,
                                      fontWeight: FontWeight.w500,
                                      color: AppColors.gray900,
                                      height: 22 / 14,
                                      leadingDistribution:
                                          TextLeadingDistribution.even,
                                      overflow: TextOverflow.ellipsis,
                                    ),
                                    velocity: 250,
                                    activo: pagoCreditoState.tipoAnticipo == TipoAnticipo.monto,
                                  ),
                                )
                              ],
                            ),
                          ),
                        ),
                        const SizedBox(
                          height: 15,
                        ),
                        Container(
                          decoration: BoxDecoration(
                            border: Border.all(
                              color: pagoCreditoState.tipoAnticipo ==
                                      TipoAnticipo.plazo
                                  ? Colors.transparent
                                  : AppColors.gray300,
                              width: 1,
                            ),
                            borderRadius: BorderRadius.circular(8),
                            boxShadow: AppColors.shadowSm,
                            color: pagoCreditoState.tipoAnticipo ==
                                    TipoAnticipo.plazo
                                ? AppColors.gray100
                                : AppColors.gray25,
                          ),
                          child: TextButton(
                            onPressed: () {
                              ref
                                  .read(pagoCreditoPropioProvider.notifier)
                                  .changeReduccionAnticipo(TipoAnticipo.plazo);
                            },
                            style: TextButton.styleFrom(
                              padding: const EdgeInsets.symmetric(
                                horizontal: 16,
                                vertical: 18,
                              ),
                              shape: const RoundedRectangleBorder(
                                borderRadius: BorderRadius.all(
                                  Radius.circular(8),
                                ),
                              ),
                            ),
                            child: Row(
                              children: [
                                CtSwitch(
                                  value: pagoCreditoState.tipoAnticipo ==
                                      TipoAnticipo.plazo,
                                  onTap: () {
                                    ref
                                      .read(pagoCreditoPropioProvider.notifier)
                                      .changeReduccionAnticipo(TipoAnticipo.plazo);
                                  },
                                ),
                                const SizedBox(
                                  width: 16,
                                ),
                                Expanded(
                                  child: AutoMarqueeText(
                                    text: TipoAnticipo.plazo.obtenerDescripcion(),
                                    style: const TextStyle(
                                      fontSize: 14,
                                      fontWeight: FontWeight.w500,
                                      color: AppColors.gray900,
                                      height: 22 / 14,
                                      leadingDistribution:
                                          TextLeadingDistribution.even,
                                      overflow: TextOverflow.ellipsis,
                                    ),
                                    velocity: 250,
                                    activo: pagoCreditoState.tipoAnticipo == TipoAnticipo.plazo,
                                  ),
                                )
                              ],
                            ),
                          ),
                        ),
                        const SizedBox(
                          height: 16,
                        ),
                      ],
                      const Text(
                        'Monto a pagar',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray900,
                          height: 1.5,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      const SizedBox(
                        height: 4,
                      ),
                      InputMontoAnticipo(
                        montoAnticipo: pagoCreditoState.tipoAnticipo == TipoAnticipo.adelantoCuota
                            ? 1
                            : pagoCreditoState.creditoAbonar!.montoMinimoAnticipo + 1,
                        simboloMoneda: pagoCreditoState.creditoAbonar?.simboloMoneda,
                        monto: pagoCreditoAnticipoState.montoAnticipo.copyWith(
                          ignorarMinimo: pagoCreditoState.tipoAnticipo == TipoAnticipo.adelantoCuota,
                        ),
                        onChangeMonto: (montoAnticipo) {
                          ref.read(pagoCreditoAnticipoProvider.notifier)
                            .changeMontoAnticipo(montoAnticipo);
                        },
                      ),
                      const SizedBox(
                        height: 25,
                      ),
                    if (pagoCreditoState.tipoAnticipo == TipoAnticipo.adelantoCuota) 
                      RichText(
                        textAlign: TextAlign.justify,
                        text: const TextSpan(
                          style: TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray600,
                            height: 22 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                          children: <TextSpan>[
                            TextSpan(
                              text:
                                  'Puedes pagar desde (1) o más cuotas, conforme tu cronograma de pagos. ',
                            ),
                            TextSpan(
                              text: '\nImportante: ',
                              style: TextStyle(fontWeight: FontWeight.w900),
                            ),
                            TextSpan(
                              text:
                                  'Si optas por adelantar tus cuotas a través de esta opción, no se generará reducción de intereses, comisiones y gastos a favor del cliente.',
                            ),
                          ],
                        ),
                      ),
                    ],
                  )
                ],
                if (pagoCreditoState.tipoPagoCredito ==
                    TipoPagoCredito.abono) ...[
                  if (pagoCreditoState.monto.value.isEmpty ||
                      double.parse(pagoCreditoState.monto.value.toString()) <=
                          pagoCreditoState.creditoAbonar!.montoTotalPago) ...[
                    const SizedBox(
                      height: 4,
                    ),
                    Text(
                      'Vence ${CtUtils.formatDate(pagoCreditoState.creditoAbonar?.fechaCuotaVigente)}',
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 22 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ],               
                ],
                const Spacer(),
                const SizedBox(
                  height: 30,
                ),
                CtButton(
                  text: 'Continuar',
                  onPressed: () {
                    FocusManager.instance.primaryFocus?.unfocus();
                    ref
                        .read(pagoCreditoPropioProvider.notifier)
                        .pagar(context: context, withPush: true);
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

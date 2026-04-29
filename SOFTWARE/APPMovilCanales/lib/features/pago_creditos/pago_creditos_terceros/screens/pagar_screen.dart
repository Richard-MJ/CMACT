import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/compras_internet/screens/configurar_afiliacion_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/opcion_pago_credito.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/widgets/input_monto.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/widgets/input_monto_anticipo.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/providers/pago_creditos_terceros_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/widgets/input_numero_credito.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/widgets/tipo_pago_opcion.dart';
import 'package:caja_tacna_app/features/pago_creditos/providers/pago_creditos_anticipo_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/index.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class PagarScreen extends ConsumerStatefulWidget {
  const PagarScreen({super.key});

  @override
  PagarScreenState createState() => PagarScreenState();
}

class PagarScreenState extends ConsumerState<PagarScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(pagoCreditoTercerosProvider.notifier).initDatos();
      ref.read(pagoCreditoTercerosProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Pago de créditos de terceros',
      child: _PagarView(),
    );
  }
}

class _PagarView extends ConsumerWidget {
  const _PagarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoCreditoState = ref.watch(pagoCreditoTercerosProvider);
    final pagoCreditoAnticipoState = ref.watch(pagoCreditoAnticipoProvider);
    final bool disabledButton = (pagoCreditoState.cuentaOrigen == null &&
        pagoCreditoState.tipoPago == TipoPago.aplicativo) ||
        pagoCreditoState.monto.isNotValid ||
        pagoCreditoState.creditoAbonar == null ||
        (pagoCreditoState.tipoPagoCredito == TipoPagoCredito.anticipo &&
            (pagoCreditoAnticipoState.montoAnticipo.copyWith(
                  ignorarMinimo: pagoCreditoState.tipoAnticipo == TipoAnticipo.adelantoCuota,
                ).isNotValid ||
            pagoCreditoState.tipoAnticipo == TipoAnticipo.defecto));

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
                if (pagoCreditoState.tipoPago == TipoPago.aplicativo)
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
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
                        cuentas: ref
                            .watch(pagoCreditoTercerosProvider)
                            .cuentasOrigen,
                        onChange: (producto) {
                          ref
                              .read(pagoCreditoTercerosProvider.notifier)
                              .changeCuentaOrigen(producto);
                        },
                        value: pagoCreditoState.cuentaOrigen,
                      ),
                      const SizedBox(
                        height: 36,
                      ),
                    ],
                  ),
                const Text(
                  'Número de crédito',
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
                InputNumeroCredito(
                  value: pagoCreditoState.numeroCredito,
                  onChange: (value) {
                    ref
                        .read(pagoCreditoTercerosProvider.notifier)
                        .changeNumeroCredito(value);
                  },
                  onSubmitted: () async {
                    await ref
                        .read(pagoCreditoTercerosProvider.notifier)
                        .getDatosCredito();
                  },
                ),
                const SizedBox(
                  height: 24,
                ),
                if (pagoCreditoState.creditoAbonar != null)
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                    children: [
                      TipoPagoOpcion(
                          pagoCreditoState: pagoCreditoState, ref: ref),
                      const SizedBox(
                        height: 24,
                      ),
                      const Text(
                        'Pendiente de pago',
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
                      CtCard(
                          padding: const EdgeInsets.symmetric(
                            horizontal: 16,
                            vertical: 18,
                          ),
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.stretch,
                            children: [
                              Row(
                                mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                children: [
                                  const Text(
                                    'Titular',
                                    style: TextStyle(
                                      fontSize: 14,
                                      fontWeight: FontWeight.w500,
                                      color: AppColors.gray700,
                                      height: 22 / 14,
                                      leadingDistribution:
                                          TextLeadingDistribution.even,
                                    ),
                                  ),
                                  const SizedBox(
                                    width: 20,
                                  ),
                                  Expanded(
                                    child: Text(
                                      '${pagoCreditoState.creditoAbonar?.nombreCliente}',
                                      style: const TextStyle(
                                        fontSize: 14,
                                        fontWeight: FontWeight.w500,
                                        color: AppColors.gray900,
                                        height: 22 / 14,
                                        leadingDistribution:
                                            TextLeadingDistribution.even,
                                      ),
                                      textAlign: TextAlign.right,
                                    ),
                                  ),
                                ],
                              ),
                              const SizedBox(
                                height: 8,
                              ),
                              Row(
                                mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                children: [
                                  const Text(
                                    'Cuota',
                                    style: TextStyle(
                                      fontSize: 14,
                                      fontWeight: FontWeight.w500,
                                      color: AppColors.gray700,
                                      height: 22 / 14,
                                      leadingDistribution:
                                          TextLeadingDistribution.even,
                                    ),
                                  ),
                                  const SizedBox(
                                    width: 20,
                                  ),
                                  Expanded(
                                    child: Text(
                                      CtUtils.formatCurrency(
                                        pagoCreditoState
                                            .creditoAbonar?.montoTotalPago,
                                        pagoCreditoState
                                            .creditoAbonar?.simboloMoneda,
                                      ),
                                      style: const TextStyle(
                                        fontSize: 14,
                                        fontWeight: FontWeight.w500,
                                        color: AppColors.gray900,
                                        height: 22 / 14,
                                        leadingDistribution:
                                            TextLeadingDistribution.even,
                                      ),
                                      textAlign: TextAlign.right,
                                    ),
                                  ),
                                ],
                              )
                            ],
                          )),
                      const SizedBox(
                        height: 24,
                      ),
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
                            pagoCreditoState
                                .creditoAbonar?.montoSaldoCancelacion,
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
                          height: 16,
                        ),
                        InputMonto(
                          monto: pagoCreditoState.monto,
                          onChangeMonto: (value) {
                            ref
                                .read(pagoCreditoTercerosProvider.notifier)
                                .changeMonto(value);
                          },
                          montoAbono:
                              pagoCreditoState.creditoAbonar?.montoTotalPago,
                          simboloMoneda:
                              pagoCreditoState.creditoAbonar?.simboloMoneda,
                        ),
                        if (pagoCreditoState.monto.value.isEmpty ||
                            double.parse(
                                    pagoCreditoState.monto.value.toString()) <=
                                pagoCreditoState
                                    .creditoAbonar!.montoTotalPago) ...[
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
                      if (pagoCreditoState.tipoPagoCredito.esAnticipo()) ...[
                        Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            if (pagoCreditoState.tipoPagoCredito ==
                                TipoPagoCredito.anticipo) ...[
                              Container(
                                decoration: BoxDecoration(
                                  border: Border.all(
                                    color: pagoCreditoState.tipoAnticipo == TipoAnticipo.adelantoCuota
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
                                        .read(pagoCreditoTercerosProvider
                                            .notifier)
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
                                              .read(pagoCreditoTercerosProvider
                                                  .notifier)
                                              .changeReduccionAnticipo(
                                                  TipoAnticipo.adelantoCuota);
                                        },
                                      ),
                                      const SizedBox(
                                        width: 16,
                                      ),
                                      Expanded(
                                        child: AutoMarqueeText(
                                          text: TipoAnticipo.adelantoCuota.obtenerDescripcion(),
                                          style: const TextStyle(
                                            fontSize: 14,
                                            fontWeight: FontWeight.w500,
                                            color: AppColors.gray900,
                                            height: 22 / 14,
                                            leadingDistribution: TextLeadingDistribution.even,
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
                                height: 16,
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
                                        .read(pagoCreditoTercerosProvider
                                            .notifier)
                                        .changeReduccionAnticipo(
                                            TipoAnticipo.plazo);
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
                                          ref.read(pagoCreditoTercerosProvider.notifier)
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
                                      ),
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
                                leadingDistribution:
                                    TextLeadingDistribution.even,
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
                                ref
                                    .read(pagoCreditoAnticipoProvider.notifier)
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
                    ],
                  ),
                const Spacer(),
                const SizedBox(
                  height: 10,
                ),
                CtButton(
                  text: 'Continuar',
                  onPressed: () {
                    FocusManager.instance.primaryFocus?.unfocus();
                    ref
                        .read(pagoCreditoTercerosProvider.notifier)
                        .pagar(withPush: true);
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

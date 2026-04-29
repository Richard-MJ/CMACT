import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_servicios/providers/pago_servicios_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_cuenta.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
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
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Pago de servicios',
      child: _PagarView(),
    );
  }
}

class _PagarView extends ConsumerWidget {
  const _PagarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoServiciosState = ref.watch(pagoServiciosProvider);
    final bool disabledButton = pagoServiciosState.cuentaOrigen == null;

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
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text(
                            'Servicio',
                            style: TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w500,
                              color: AppColors.gray900,
                              height: 1.5,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                          Text(
                            pagoServiciosState.servicioPagar?.nombreServicio ??
                                '',
                            style: const TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w400,
                              color: AppColors.gray900,
                              height: 1.5,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                        ],
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
                            'N° de suministro',
                            style: TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w500,
                              color: AppColors.gray900,
                              height: 1.5,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                          Text(
                            pagoServiciosState.cobroServicio?.suministro ?? '',
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
                  ],
                ),                
                const SizedBox(
                  height: 24,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text(
                            'N° de recibo',
                            style: TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w400,
                              color: AppColors.black,
                              height: 1,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                          Text(
                            pagoServiciosState.cobroServicio?.numeroRecibo ??
                                '',
                            style: const TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w400,
                              color: AppColors.black,
                              height: 1,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                        ],
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
                        color: AppColors.black,
                        height: 1.5,
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
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text(
                            'Comisión',
                            style: TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w400,
                              color: AppColors.black,
                              height: 1,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                        ],
                      ),
                    ),
                    const SizedBox(
                      width: 30,
                    ),
                    Text(
                      CtUtils.formatCurrency(
                        pagoServiciosState.cobroServicio?.comisionDeuda,
                        pagoServiciosState.cobroServicio?.simboloMoneda,
                      ),
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.black,
                        height: 1.5,
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
                      'Monto total',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w600,
                        color: AppColors.black,
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
                            : (double.parse(pagoServiciosState
                                    .montoDeudaServicio!.value) +
                                pagoServiciosState
                                    .cobroServicio!.comisionDeuda),
                        pagoServiciosState.cobroServicio?.simboloMoneda,
                      ),
                      style: const TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.w600,
                        color: AppColors.black,
                        height: 1,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 24,
                ),
                const Text(
                  'Cuenta de cargo',
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
                  cuentas: ref.watch(pagoServiciosProvider).cuentasOrigen,
                  onChange: (producto) {
                    ref
                        .read(pagoServiciosProvider.notifier)
                        .changeProducto(producto);
                  },
                  value: pagoServiciosState.cuentaOrigen,
                ),
                const SizedBox(
                  height: 24,
                ),
                const Spacer(),
                CtButton(
                  text: 'Continuar',
                  onPressed: () {
                    ref
                        .read(pagoServiciosProvider.notifier)
                        .pagarServicio(withPush: true);
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

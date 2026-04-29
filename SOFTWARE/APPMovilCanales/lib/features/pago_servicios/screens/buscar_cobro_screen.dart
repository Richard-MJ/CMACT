import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_servicios/providers/pago_servicios_provider.dart';
import 'package:caja_tacna_app/features/pago_servicios/widgets/input_numero_suministro.dart';
import 'package:caja_tacna_app/features/pago_servicios/widgets/pago_item.dart';
import 'package:caja_tacna_app/features/pago_servicios/widgets/pago_item_editable.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

class BuscarCobroScreen extends ConsumerStatefulWidget {
  const BuscarCobroScreen({super.key});

  @override
  BuscarCobroScreenState createState() => BuscarCobroScreenState();
}

class BuscarCobroScreenState extends ConsumerState<BuscarCobroScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(pagoServiciosProvider.notifier).ocultarCobro();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Pago de servicios',
      child: _BuscarCobroView(),
    );
  }
}

class _BuscarCobroView extends ConsumerWidget {
  const _BuscarCobroView();
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoServiciosState = ref.watch(pagoServiciosProvider);

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
                Center(
                  child: Text(
                    pagoServiciosState.servicioPagar?.nombreEmpresa ?? '',
                    style: const TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.w600,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ),
                const SizedBox(
                  height: 24,
                ),
                Row(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Tipo de servicio',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray900,
                        height: 1.5,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    const SizedBox(
                      width: 16,
                    ),
                    Flexible(
                      child: Text(
                        pagoServiciosState.servicioPagar?.nombreServicio ?? '',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 1.5,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 36,
                ),
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
                const SizedBox(
                  height: 16,
                ),
                InputNumeroSuministro(
                  value: pagoServiciosState.numeroSuministro,
                  onChanged: (value) {
                    ref
                        .read(pagoServiciosProvider.notifier)
                        .changeNumeroSuministro(value);
                  },
                  onSubmitted: () async {
                    await ref
                        .read(pagoServiciosProvider.notifier)
                        .obtenerCobroServicio();
                  },
                ),
                const SizedBox(
                  height: 36,
                ),
                if (pagoServiciosState.cobroServicio != null &&
                    pagoServiciosState.mostrarCobro)
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
                if (pagoServiciosState.cobroServicio != null &&
                    pagoServiciosState.mostrarCobro) ...[
                  if (pagoServiciosState.cobroServicio!.tipoServicio ==
                      'Kasnet') ...[
                    PagoItemEditable(
                      cobroServicio: pagoServiciosState.cobroServicio,
                      montoDeudaServicio:
                          pagoServiciosState.montoDeudaServicio!,
                      onChangeMonto: (montoDeudaServicio) {
                        ref
                            .read(pagoServiciosProvider.notifier)
                            .changeMontoDedudaServicio(montoDeudaServicio);
                      },
                    ),
                  ] else ...[
                    PagoItem(
                      cobroServicio: pagoServiciosState.cobroServicio,
                    ),
                  ]
                ],
                const SizedBox(
                  height: 30,
                ),
                const Spacer(),
                CtButton(
                  text: 'Continuar',
                  onPressed: () {
                    context.push('/pago-servicios/pagar');
                  },
                  disabled: pagoServiciosState.cobroServicio == null ||
                      (pagoServiciosState.cobroServicio!.tipoServicio == 'Kasnet' && pagoServiciosState.montoDeudaServicio!.isNotValid),
                )
              ],
            ),
          ),
        ),
      ],
    );
  }
}

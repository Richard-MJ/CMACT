import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/credito/models/movimiento_credito.dart';
import 'package:caja_tacna_app/features/credito/provider/credito_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_movimiento_skeleton.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:intl/intl.dart';

class MovimientosCredito extends ConsumerStatefulWidget {
  const MovimientosCredito({
    super.key,
    required this.numeroCredito,
  });

  final String numeroCredito;

  @override
  MovimientosCreditoState createState() => MovimientosCreditoState();
}

class MovimientosCreditoState extends ConsumerState<MovimientosCredito> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(creditoProvider.notifier).getMovimientos(widget.numeroCredito);
    });
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Detalle de movimientos',
      child: _MovimientosCreditoView(
        numeroCredito: widget.numeroCredito,
      ),
    );
  }
}

class _MovimientosCreditoView extends ConsumerWidget {
  const _MovimientosCreditoView({
    required this.numeroCredito,
  });
  final String numeroCredito;
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final creditoState = ref.watch(creditoProvider);
    return Column(
      children: [
        Container(
          padding: const EdgeInsets.only(
            top: 46,
            left: 24,
            right: 24,
          ),
          child: const Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              Text(
                'Movimientos',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w700,
                  color: AppColors.gray900,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              SizedBox(
                height: 15,
              ),
            ],
          ),
        ),
        Expanded(
          child: CustomScrollView(
            slivers: [
              SliverPadding(
                padding: const EdgeInsets.only(
                  left: 24,
                  right: 24,
                  bottom: 38,
                ),
                sliver: !creditoState.loadingMovimientos
                    ? SliverList.separated(
                        itemCount: creditoState.movimientos.length,
                        itemBuilder: (context, index) {
                          final movimiento = creditoState.movimientos[index];
                          return _Movimiento(
                            movimiento: movimiento,
                          );
                        },
                        separatorBuilder: (context, index) {
                          return const SizedBox(
                            height: 12,
                          );
                        },
                      )
                    : SliverList.separated(
                        itemCount: 4,
                        itemBuilder: (context, index) {
                          return const CtMovimientoSkeleton();
                        },
                        separatorBuilder: (context, index) {
                          return const SizedBox(
                            height: 12,
                          );
                        },
                      ),
              ),
            ],
          ),
        ),
      ],
    );
  }
}

class _Movimiento extends ConsumerWidget {
  const _Movimiento({required this.movimiento});

  final MovimientoCredito movimiento;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final creditoState = ref.watch(creditoProvider);

    return Container(
      padding: const EdgeInsets.only(bottom: 10),
      decoration: const BoxDecoration(
        border: Border(
          bottom: BorderSide(
            color: AppColors.border1,
            width: 1,
          ),
        ),
      ),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  movimiento.descripcionTransaccion,
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray900,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Text(
                  formatearFecha(movimiento.fechaRecibo),
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ],
            ),
          ),
          Text(
            CtUtils.formatCurrency(
              movimiento.montoTotalPagar,
              creditoState.credito!.simboloMoneda,
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
    );
  }
}

String formatearFecha(DateTime fecha) {
  final formato = DateFormat('d/MM/y  h:mm:s a', 'es');
  return formato.format(fecha);
}

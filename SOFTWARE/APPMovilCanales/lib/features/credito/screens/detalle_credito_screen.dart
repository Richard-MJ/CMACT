import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/credito/provider/credito_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/screens/pago_creditos_propios_screen.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_operacion.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';

class DetalleCredito extends ConsumerStatefulWidget {
  const DetalleCredito({
    super.key,
    required this.numeroCredito,
  });

  final String numeroCredito;

  @override
  DetalleCreditoState createState() => DetalleCreditoState();
}

class DetalleCreditoState extends ConsumerState<DetalleCredito> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout4(
      title: 'Mis Créditos',
      child: _DetalleCreditoView(
        numeroCredito: widget.numeroCredito,
      ),
    );
  }
}

class _DetalleCreditoView extends ConsumerWidget {
  const _DetalleCreditoView({
    required this.numeroCredito,
  });
  final String numeroCredito;
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final creditoState = ref.watch(creditoProvider);
    final homeState = ref.watch(homeProvider);

    return Container(
      padding: const EdgeInsets.only(
        top: 36,
        left: 24,
        right: 24,
        bottom: 38,
      ),
      width: double.infinity,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Alias',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          Text(
            '${creditoState.credito?.alias}',
            style: const TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w600,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          Text(
            creditoState.credito?.descripcionSubProducto ?? '',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 36,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              const Text(
                'Crédito total',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                CtUtils.formatCurrency(
                  creditoState.credito?.montoDesembolsado,
                  creditoState.credito?.simboloMoneda,
                ),
                style: const TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.w700,
                  color: AppColors.gray900,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ],
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              Text(
                'Tasa anual ${creditoState.credito?.porcentajeTasaInteres}%',
                style: const TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ],
          ),
          Container(
            height: 19,
            decoration: const BoxDecoration(
              border: Border(
                bottom: BorderSide(
                  width: 1,
                  color: AppColors.border1,
                ),
              ),
            ),
          ),
          const SizedBox(
            height: 31,
          ),
          const Text(
            'Información del crédito',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 13,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              const Text(
                'Saldo capital',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                CtUtils.formatCurrency(
                  creditoState.credito?.saldoPendiente,
                  creditoState.credito?.simboloMoneda,
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
          const SizedBox(
            height: 15,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              const Text(
                'N° de cuota',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                '${creditoState.credito?.cuotaActual} de ${creditoState.credito?.cuotasTotales}',
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
          const SizedBox(
            height: 15,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text(
                'Próxima cuota: ${_formatearFecha(creditoState.credito?.fechaCuotaVigente)}',
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                CtUtils.formatCurrency(
                  creditoState.credito?.montoCuotaPactada,
                  creditoState.credito?.simboloMoneda,
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
          const SizedBox(
            height: 22,
          ),
          Center(
            child: GestureDetector(
              onTap: () {
                context.push('/credito/movimientos/$numeroCredito');
              },
              child: const Text(
                'Ver movimientos',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w500,
                  color: AppColors.primary700,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ),
          ),
          const SizedBox(
            height: 40,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              CtOperacion(
                label: 'Pagar \ncrédito',
                icon: 'assets/icons/payment.svg',
                onPressed: () {
                  showPagoCreditoBottomSheet(
                    context: context,
                    pagoApp: () {
                      ref.read(creditoProvider.notifier).pagarApp();
                    },
                    pagoCip: () {
                      ref.read(creditoProvider.notifier).pagarCip();
                    },
                    indicadorCip: homeState.configuracion?.indicadorPagoEfectivoPagoCredito ?? false
                  );
                },
              ),
              const SizedBox(
                width: 8,
              ),
              CtOperacion(
                label: 'Enviar \ncronograma',
                icon: 'assets/icons/send-2.svg',
                onPressed: () {
                  ref.read(creditoProvider.notifier).enviarPlanPagosCredito();
                },
              ),
            ],
          )
        ],
      ),
    );
  }
}

String _formatearFecha(DateTime? fecha) {
  if (fecha != null) {
    return DateFormat('dd/MM').format(fecha);
  }
  return '';
}

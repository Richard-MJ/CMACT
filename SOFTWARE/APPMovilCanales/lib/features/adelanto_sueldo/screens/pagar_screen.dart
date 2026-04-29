import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/providers/adelanto_sueldo_provider.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/widgets/input_monto.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card.dart';
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
    Future.microtask(() {
      ref.read(adelantoSueldoProvider.notifier).initDatos();
      ref.read(adelantoSueldoProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Adelanto de sueldo',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _PagarView(),
          )
        ],
      ),
    );
  }
}

class _PagarView extends ConsumerWidget {
  const _PagarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final adelantoSueldoState = ref.watch(adelantoSueldoProvider);
    final bool disabledButton = adelantoSueldoState.cuentaDestino == null ||
        adelantoSueldoState.monto.isNotValid;

    return Container(
      padding: const EdgeInsets.only(top: 36, left: 24, right: 23, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          CtCard(
            padding: const EdgeInsets.symmetric(
              horizontal: 16,
              vertical: 18,
            ),
            child: Column(
              children: [
                const Text(
                  'Monto máximo aprobado',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 8,
                ),
                Text(
                  adelantoSueldoState.cuentaDestino != null
                      ? CtUtils.formatCurrency(
                          adelantoSueldoState.cuentaDestino?.montoMaximo,
                          adelantoSueldoState
                              .cuentaDestino?.simboloMonedaProducto,
                        )
                      : '--:--',
                  style: const TextStyle(
                    fontSize: 24,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray900,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ],
            ),
          ),
          const SizedBox(
            height: 64,
          ),
          const Text(
            'Cuenta de destino',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          CtSelectCuenta(
            cuentas: adelantoSueldoState.cuentasDestino,
            onChange: (producto) {
              ref
                  .read(adelantoSueldoProvider.notifier)
                  .changeProducto(producto);
            },
            value: adelantoSueldoState.cuentaDestino,
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            '¿Qué monto deseas solicitar?',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          InputMontoAdelSuel(
            onChangeMonto: (value) {
              ref.read(adelantoSueldoProvider.notifier).changeMonto(value);
            },
            monto: adelantoSueldoState.monto,
            simboloMoneda:
                adelantoSueldoState.cuentaDestino?.simboloMonedaProducto,
          ),
          const SizedBox(
            height: 30,
          ),
          const Spacer(),
          Center(
            child: Text(
              'Se cobrará una comisión de ${adelantoSueldoState.cuentaDestino != null ? CtUtils.formatCurrency(
                  adelantoSueldoState.cuentaDestino?.montoComision,
                  adelantoSueldoState.cuentaDestino?.simboloMonedaProducto,
                ) : '--:--'}',
              style: const TextStyle(
                fontSize: 12,
                fontWeight: FontWeight.w400,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
          const SizedBox(
            height: 15,
          ),
          CtButton(
            text: 'Lo quiero',
            onPressed: () {
              ref.read(adelantoSueldoProvider.notifier).pagar(withPush: true);
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

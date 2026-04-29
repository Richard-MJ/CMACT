import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_safetypay/providers/pago_safetypay_provider.dart';
import 'package:caja_tacna_app/features/pago_safetypay/widgets/input_codigo_pago.dart';
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
      ref.read(pagoSafetypayProvider.notifier).initDatos();
      ref.read(pagoSafetypayProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Volver',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _PagarScreenView(),
          )
        ],
      ),
    );
  }
}

class _PagarScreenView extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoSafetypayState = ref.watch(pagoSafetypayProvider);

    return Container(
      padding: const EdgeInsets.only(top: 34, left: 24, right: 23, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Center(
            child: Text(
              'Safety Pay',
              style: TextStyle(
                fontSize: 24,
                fontWeight: FontWeight.w600,
                color: AppColors.gray900,
                height: 36 / 24,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Código de pago',
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
          InputCodigoPago(
            value: pagoSafetypayState.codigoPago,
            onChange: (value) {
              ref.read(pagoSafetypayProvider.notifier).changeCodigoPago(value);
            },
            onSubmitted: () async {
              await ref.read(pagoSafetypayProvider.notifier).obtenerDeuda();
            },
            maxLength: 10,
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
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          CtSelectCuenta(
            cuentas: pagoSafetypayState.cuentasOrigen,
            onChange: (producto) {
              ref.read(pagoSafetypayProvider.notifier).changeProducto(producto);
            },
            value: pagoSafetypayState.cuentaOrigen,
          ),
          const SizedBox(
            height: 24,
          ),
          if (pagoSafetypayState.deuda != null)
            const Text(
              'Pendiente de pago',
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
          if (pagoSafetypayState.deuda != null)
            CtCard(
              padding: const EdgeInsets.symmetric(
                horizontal: 16,
                vertical: 18,
              ),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    'Código: ${pagoSafetypayState.deuda?.codigoPago}',
                    style: const TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w500,
                      color: AppColors.gray700,
                      height: 22 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  const SizedBox(
                    width: 20,
                  ),
                  Expanded(
                    child: Text(
                      CtUtils.formatCurrency(
                        pagoSafetypayState.deuda?.monto,
                        pagoSafetypayState.deuda?.simboloMoneda,
                      ),
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray900,
                        height: 22 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.right,
                    ),
                  ),
                ],
              ),
            ),
          const SizedBox(
            height: 30,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref.read(pagoSafetypayProvider.notifier).pagar(withPush: true);
            },
            disabled: pagoSafetypayState.deuda == null,
          )
        ],
      ),
    );
  }
}

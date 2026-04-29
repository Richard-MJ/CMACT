import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/emision_giros/providers/emision_giros_provider.dart';
import 'package:caja_tacna_app/features/emision_giros/widgets/input_monto.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_cuenta.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_stepper.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class IngresoMontoScreen extends ConsumerStatefulWidget {
  const IngresoMontoScreen({super.key});

  @override
  IngresoMontoScreenState createState() => IngresoMontoScreenState();
}

class IngresoMontoScreenState extends ConsumerState<IngresoMontoScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(emisionGirosProvider.notifier).initDatos();
      ref.read(emisionGirosProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Emisión de giros',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _IngresoMontoView(),
          )
        ],
      ),
    );
  }
}

class _IngresoMontoView extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final giroState = ref.watch(emisionGirosProvider);
    final bool disabledButton =
        giroState.cuentaOrigen == null || giroState.montoGiro.isNotValid;

    return Container(
      padding: const EdgeInsets.only(top: 16, left: 24, right: 23, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Center(
            child: CtStepper(
              numPasos: 3,
              pasoActual: 1,
            ),
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Ingresa el monto a girar',
            style: TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 28 / 18,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Cuenta de origen',
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
            cuentas: giroState.cuentasOrigen,
            onChange: (producto) {
              ref
                  .read(emisionGirosProvider.notifier)
                  .changeCuentaOrigen(producto);
            },
            value: giroState.cuentaOrigen,
          ),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Monto a girar',
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
          InputMontoGiro(
            montoGiro: giroState.montoGiro,
            onChangeMontoGiro: (value) {
              ref.read(emisionGirosProvider.notifier).changeMontoGiro(value);
            },
            simboloMoneda: giroState.cuentaOrigen?.simboloMonedaProducto,
          ),
          const SizedBox(
            height: 24,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref.read(emisionGirosProvider.notifier).ingresarMontoSubmit();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

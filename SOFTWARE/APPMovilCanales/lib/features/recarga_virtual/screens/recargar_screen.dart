import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/recarga_virtual/providers/recarga_virtual_provider.dart';
import 'package:caja_tacna_app/features/recarga_virtual/widgets/input_celular.dart';
import 'package:caja_tacna_app/features/recarga_virtual/widgets/input_monto.dart';
import 'package:caja_tacna_app/features/recarga_virtual/widgets/select_operador.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_cuenta.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class RecargarScreen extends ConsumerStatefulWidget {
  const RecargarScreen({super.key});

  @override
  RecargarScreenState createState() => RecargarScreenState();
}

class RecargarScreenState extends ConsumerState<RecargarScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(recargaVirtualProvider.notifier).initDatos();
      ref.read(recargaVirtualProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Recarga virtual',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _RecargarView(),
          )
        ],
      ),
    );
  }
}

class _RecargarView extends ConsumerWidget {
  const _RecargarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final recargaVirtualState = ref.watch(recargaVirtualProvider);
    final bool disabledButton = recargaVirtualState.cuentaOrigen == null ||
        recargaVirtualState.operadorKasnet == null ||
        recargaVirtualState.montoRecarga.isNotValid ||
        recargaVirtualState.numeroCelular.isNotValid;

    return Container(
      padding: const EdgeInsets.only(top: 36, left: 24, right: 23, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Busca tu operador móvil',
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
          SelectOperador(
            value: recargaVirtualState.operadorKasnet,
            onChanged: (nuevoOperadorKasnet) {
              ref
                  .read(recargaVirtualProvider.notifier)
                  .changeOperador(nuevoOperadorKasnet);
            },
            operadoresMovilesKasnet: recargaVirtualState.operadoresKasnet,
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Ingresa tu número de celular',
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
          // cambiar al widget para especificar numeros celulares
          InputCelular(
            numeroCelular: recargaVirtualState.numeroCelular,
            onChangeNumeroCelular: (numerocelular) {
              ref
                  .read(recargaVirtualProvider.notifier)
                  .changeNumeroCelular(numerocelular);
            },
          ),
          const SizedBox(
            height: 36,
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
            cuentas: recargaVirtualState.cuentasOrigen,
            onChange: (producto) {
              ref
                  .read(recargaVirtualProvider.notifier)
                  .changeProducto(producto);
            },
            value: recargaVirtualState.cuentaOrigen,
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Monto a recargar',
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
          InputMonto(
            onChangeMontoRecarga: (value) {
              ref
                  .read(recargaVirtualProvider.notifier)
                  .changeMontoRecarga(value);
            },
            montoRecarga: recargaVirtualState.montoRecarga,
            simboloMoneda: 'S/',
            maxLength: 4,
          ),
          const SizedBox(
            height: 30,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref.read(recargaVirtualProvider.notifier).pagar(withPush: true);
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

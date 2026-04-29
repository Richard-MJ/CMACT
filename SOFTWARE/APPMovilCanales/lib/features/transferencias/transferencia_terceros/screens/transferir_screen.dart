import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_terceros/providers/transferencia_terceros_provider.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_cuenta_destino.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_monto.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_motivo.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_cuenta.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TransferirATercerosScreen extends ConsumerStatefulWidget {
  const TransferirATercerosScreen({super.key});

  @override
  TransferirATercerosScreenState createState() =>
      TransferirATercerosScreenState();
}

class TransferirATercerosScreenState
    extends ConsumerState<TransferirATercerosScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(transferenciaTercerosProvider.notifier).initDatos();
      ref.read(transferenciaTercerosProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Transferencia a terceros',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _TransferirATercerosView(),
          )
        ],
      ),
    );
  }
}

class _TransferirATercerosView extends ConsumerWidget {
  const _TransferirATercerosView();
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final transferenciaState = ref.watch(transferenciaTercerosProvider);
    final bool disabledButton = transferenciaState.cuentaOrigen == null ||
        transferenciaState.numeroCuentaDestino.isNotValid ||
        transferenciaState.monto.isNotValid;

    return Container(
      padding: const EdgeInsets.only(top: 36, bottom: 56, left: 24, right: 24),
      child: Column(
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
            onChange: (producto) {
              ref
                  .read(transferenciaTercerosProvider.notifier)
                  .changeCuentaOrigen(producto);
            },
            value: transferenciaState.cuentaOrigen,
            cuentas: transferenciaState.cuentasOrigen,
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Cuenta de destino',
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
          InputCuentaDestino(
            numeroCuenta: transferenciaState.numeroCuentaDestino,
            onChanged: (value) {
              ref
                  .read(transferenciaTercerosProvider.notifier)
                  .changeCuentaDestino(value);
            },
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Monto a transferir',
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
          InputMontoTrans(
            simboloMoneda: null,
            monto: transferenciaState.monto,
            onChangeMonto: (monto) {
              ref
                  .read(transferenciaTercerosProvider.notifier)
                  .changeMonto(monto);
            },
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Motivo (opcional)',
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
          InputMotivo(
            motivo: transferenciaState.motivo,
            onChange: (motivo) {
              ref
                  .read(transferenciaTercerosProvider.notifier)
                  .changeMotivo(motivo);
            },
          ),
          const SizedBox(
            height: 36,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref
                  .read(transferenciaTercerosProvider.notifier)
                  .transferir(withPush: true);
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

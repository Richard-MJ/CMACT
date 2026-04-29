import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/providers/transferencia_entre_mis_cuentas_provider.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/widgets/dialog_transferir_dpf.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_monto.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_motivo.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_cuenta.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TransferirEntreMisCuentasScreen extends ConsumerStatefulWidget {
  const TransferirEntreMisCuentasScreen({super.key});

  @override
  TransferirEntreMisCuentasScreenState createState() =>
      TransferirEntreMisCuentasScreenState();
}

class TransferirEntreMisCuentasScreenState
    extends ConsumerState<TransferirEntreMisCuentasScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(transferenciaEntreMisCuentasProvider.notifier).initDatos();
      ref
          .read(transferenciaEntreMisCuentasProvider.notifier)
          .getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Transferencia entre mis cuentas',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _TransferirEntreMisCuentasView(),
          )
        ],
      ),
    );
  }
}

class _TransferirEntreMisCuentasView extends ConsumerWidget {
  const _TransferirEntreMisCuentasView();
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final transferenciaState = ref.watch(transferenciaEntreMisCuentasProvider);

    final bool disabledButton = transferenciaState.cuentaOrigen == null ||
        transferenciaState.cuentaDestino == null ||
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
                  .read(transferenciaEntreMisCuentasProvider.notifier)
                  .changeCuentaOrigen(producto);
            },
            value: transferenciaState.cuentaOrigen,
            cuentas: transferenciaState.cuentasOrigen,
            cuentaDisabled: transferenciaState.cuentaDestino,
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
          CtSelectCuenta(
            onChange: (producto) {
              ref
                  .read(transferenciaEntreMisCuentasProvider.notifier)
                  .changeCuentaDestino(producto);
            },
            value: transferenciaState.cuentaDestino,
            cuentas: transferenciaState.cuentasDestino,
            cuentaDisabled: transferenciaState.cuentaOrigen,
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
            simboloMoneda:
                transferenciaState.cuentaDestino?.simboloMonedaProducto,
            monto: transferenciaState.monto,
            onChangeMonto: (monto) {
              ref
                  .read(transferenciaEntreMisCuentasProvider.notifier)
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
                  .read(transferenciaEntreMisCuentasProvider.notifier)
                  .changeMotivo(motivo);
            },
          ),
          const SizedBox(
            height: 54,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () async {
              if (transferenciaState.cuentaDestino?.codigoSistema == 'DP') {
                final bool? continuar = await showDialog(
                  context: context,
                  builder: (BuildContext context) {
                    return const DialogTransferirDpf();
                  },
                );
                if (continuar != true) return;
              }

              ref
                  .read(transferenciaEntreMisCuentasProvider.notifier)
                  .transferir(withPush: true);
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/providers/transferencia_interbancaria_inmediata_provider.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_cuenta_destino_cci.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_monto_transferir.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_motivo.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_documento.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_cuenta.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';

class TransferirInterbancarioInmediataScreen extends ConsumerStatefulWidget {
  const TransferirInterbancarioInmediataScreen({super.key});

  @override
  TransferirInterbancarioScreenState createState() =>
      TransferirInterbancarioScreenState();
}

class TransferirInterbancarioScreenState
    extends ConsumerState<TransferirInterbancarioInmediataScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(transferenciaInterbancariaInmediataProvider.notifier).initDatos();
      ref.read(transferenciaInterbancariaInmediataProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _TransferirInterbancarioView(),
          )
        ],
      );
  }
}

class _TransferirInterbancarioView extends ConsumerWidget {
  const _TransferirInterbancarioView();
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final transferenciaState = ref.watch(transferenciaInterbancariaInmediataProvider);
    final bool disabledButton = transferenciaState.cuentaOrigen == null ||
        transferenciaState.cuentaDestino.isNotValid ||
        transferenciaState.monto.isNotValid ||
        transferenciaState.nombreBeneficiario.value.isEmpty;

    final bool disabledInput = transferenciaState.cuentaOrigen == null;

    return Container(
      padding: const EdgeInsets.only(top: 0, bottom: 36, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Cuenta de origen',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.2,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 12,
          ),
          CtSelectCuenta(
            onChange: (producto) {
              ref
                  .read(transferenciaInterbancariaInmediataProvider.notifier)
                  .changeCuentaOrigen(producto);
            },
            value: transferenciaState.cuentaOrigen,
            cuentas: transferenciaState.cuentasOrigen,
          ),
          const SizedBox(
            height: 20,
          ),
          const Text(
            'Código de Cuenta Interbancario (CCI)',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.2,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 12,
          ),
          InputCuentaDestinoCci(
            numeroCuenta: transferenciaState.cuentaDestino,
            onChanged: (value) {
              ref
                  .read(transferenciaInterbancariaInmediataProvider.notifier)
                  .changeCuentaDestino(value);
            },
            readOnly: disabledInput
          ),
          const SizedBox(
            height: 20,
          ),
          const Text(
            'Nombres y apellidos del beneficiario',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.2,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 12,
          ),
          InputNombreBeneficiario(
            nombreBeneficiario: transferenciaState.nombreBeneficiario,
            onChanged: (value) {
            },
            readOnly: true,
          ),
          const SizedBox(
            height: 20,
          ),
          const Text(
            'Nº de documento de identidad',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.2,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 12,
          ),
          CtInputNumeroDocumento(
            tiposDocumento: transferenciaState.tiposDocumento,
            tipoDocumento: transferenciaState.tipoDocumento,
            numeroDocumento: transferenciaState.numeroDocumento,
            onChangeTipoDocumento: (documento) {
            },
            onChangeNumeroDocumento: (numeroDocumento) {
            },
            readOnly: true,
            tipoValidacion: TipoValidacionDocumento.validacion2,
          ),
          const SizedBox(
            height: 20,
          ),
          const Text(
            'Monto a transferir',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.2,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          InputMontoTransferir(
            monto: transferenciaState.monto,
            onChangeMonto: (monto) {
              ref
                  .read(transferenciaInterbancariaInmediataProvider.notifier)
                  .changeMonto(monto);
            },
            simboloMoneda: transferenciaState.cuentaOrigen?.simboloMonedaProducto,
          ),
          const SizedBox(
            height: 20,
          ),
          const Text(
            'Motivo (opcional)',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.2,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 12,
          ),
          InputMotivo(
            motivo: transferenciaState.motivo,
            onChange: (motivo) {
              ref
                  .read(transferenciaInterbancariaInmediataProvider.notifier)
                  .changeMotivo(motivo);
            },
          ),
          const SizedBox(
            height: 20,
          ),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref
                  .read(transferenciaInterbancariaInmediataProvider.notifier)
                  .transferir(withPush: true);
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

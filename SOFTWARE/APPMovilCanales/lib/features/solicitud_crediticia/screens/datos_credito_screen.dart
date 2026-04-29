import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_stepper.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/providers/solicitud_crediticia_provider.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/widgets/input_cuotas.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/widgets/input_monto_deseado.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/widgets/select_destino_credito.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/widgets/select_tipo_moneda.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class DatosCreditoScreen extends ConsumerStatefulWidget {
  const DatosCreditoScreen({super.key});

  @override
  DatosCreditoScreenState createState() => DatosCreditoScreenState();
}

class DatosCreditoScreenState extends ConsumerState<DatosCreditoScreen> {
  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Solicitud de crédito',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _DatosCreditoView(),
          )
        ],
      ),
    );
  }
}

class _DatosCreditoView extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final solicitudCrediticiaState = ref.watch(solicitudCrediticiaProvider);
    final bool disabledButton = solicitudCrediticiaState.tipoMoneda == null ||
        solicitudCrediticiaState.montoDeseado.isNotValid ||
        solicitudCrediticiaState.cuotas.isNotValid ||
        solicitudCrediticiaState.destinoCredito == null;

    return Container(
      padding: const EdgeInsets.only(top: 16, left: 24, right: 24, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Center(
            child: CtStepper(numPasos: 3, pasoActual: 2),
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Ingrese los datos del crédito a solicitar',
            style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 28 / 18,
                leadingDistribution: TextLeadingDistribution.even),
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Tipo de moneda',
            style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 24 / 16,
                leadingDistribution: TextLeadingDistribution.even),
          ),
          const SizedBox(
            height: 16,
          ),
          SelectTipoMoneda(
              tiposMoneda: solicitudCrediticiaState.tiposMoneda,
              onChanged: (tipoMoneda) {
                ref
                    .read(solicitudCrediticiaProvider.notifier)
                    .changeTipoMoneda(tipoMoneda);
              },
              value: solicitudCrediticiaState.tipoMoneda),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Monto deseado',
            style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 24 / 16,
                leadingDistribution: TextLeadingDistribution.even),
          ),
          const SizedBox(
            height: 16,
          ),
          InputMontoDeseado(
              tipoMoneda: solicitudCrediticiaState.tipoMoneda,
              onChangeMontoDeseado: (value) {
                ref
                    .read(solicitudCrediticiaProvider.notifier)
                    .changeMontoDeseado(value);
              },
              montoDeseado: solicitudCrediticiaState.montoDeseado),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Cuotas',
            style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 24 / 16,
                leadingDistribution: TextLeadingDistribution.even),
          ),
          const SizedBox(
            height: 16,
          ),
          InputCuotas(
              cuotas: solicitudCrediticiaState.cuotas,
              onChanged: (value) {
                ref
                    .read(solicitudCrediticiaProvider.notifier)
                    .changeCuotas(value);
              }),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Destino del crédito',
            style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 24 / 16,
                leadingDistribution: TextLeadingDistribution.even),
          ),
          const SizedBox(
            height: 16,
          ),
          SelectDestinoCredito(
            destinosCredito: solicitudCrediticiaState.destinosCredito,
            onChanged: (value) {
              ref
                  .read(solicitudCrediticiaProvider.notifier)
                  .changeDestinoCredito(value);
            },
            value: solicitudCrediticiaState.destinoCredito,
          ),
          const SizedBox(
            height: 24,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref
                  .read(solicitudCrediticiaProvider.notifier)
                  .ingresoDatosCreditoSubmit();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

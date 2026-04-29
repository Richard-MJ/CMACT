import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_stepper.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/providers/solicitud_crediticia_provider.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/widgets/input_ingreso_mensual.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/widgets/select_tipo_ingreso.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class SolicitudCrediticiaScreen extends ConsumerStatefulWidget {
  const SolicitudCrediticiaScreen({super.key});

  @override
  SolicitudCrediticiaScreenState createState() =>
      SolicitudCrediticiaScreenState();
}

class SolicitudCrediticiaScreenState
    extends ConsumerState<SolicitudCrediticiaScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(solicitudCrediticiaProvider.notifier).initDatos();
      ref.read(solicitudCrediticiaProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Solicitud de crédito',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _SolicitudCrediticiaView(),
          )
        ],
      ),
    );
  }
}

class _SolicitudCrediticiaView extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final solicitudCrediticiaState = ref.watch(solicitudCrediticiaProvider);
    final bool disabledButton = solicitudCrediticiaState.tipoIngreso == null ||
        solicitudCrediticiaState.ingresoMensual.isNotValid;

    return Container(
      padding: const EdgeInsets.only(top: 16, left: 24, right: 24, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Center(
            child: CtStepper(numPasos: 3, pasoActual: 1),
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Ingresa los datos de tus ingresos',
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
            'Tipo de ingreso',
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
          SelectTipoIngreso(
              tiposIngresos: solicitudCrediticiaState.tiposIngreso,
              onChanged: (tipoIngreso) {
                ref
                    .read(solicitudCrediticiaProvider.notifier)
                    .changeTipoIngreso(tipoIngreso);
              },
              value: solicitudCrediticiaState.tipoIngreso),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Ingreso mensual',
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
          InputIngresoMensual(
              simboloMoneda: 'S/',
              onChangeMontoIngresoMensual: (value) {
                ref
                    .read(solicitudCrediticiaProvider.notifier)
                    .changeIngresoMensual(value);
              },
              ingresoMensual: solicitudCrediticiaState.ingresoMensual),
          const SizedBox(
            height: 24,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref
                  .read(solicitudCrediticiaProvider.notifier)
                  .ingresoMensualSubmit();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

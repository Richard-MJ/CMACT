import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/emision_giros/providers/emision_giros_provider.dart';
import 'package:caja_tacna_app/features/emision_giros/widgets/input_direccion.dart';
import 'package:caja_tacna_app/features/emision_giros/widgets/mensaje_alerta.dart';
import 'package:caja_tacna_app/features/emision_giros/widgets/select_agencia.dart';
import 'package:caja_tacna_app/features/emision_giros/widgets/select_departamento.dart';
import 'package:caja_tacna_app/features/emision_giros/widgets/select_motivo.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_stepper.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class DireccionBeneficiarioScreen extends StatelessWidget {
  const DireccionBeneficiarioScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Emisión de giros',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _DireccionBeneficiarioView(),
          )
        ],
      ),
    );
  }
}

class _DireccionBeneficiarioView extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final giroState = ref.watch(emisionGirosProvider);
    final bool disabledButton = giroState.departamento == null ||
        giroState.direccion.isNotValid ||
        giroState.agencia == null ||
        giroState.motivo == null;

    return Container(
      padding: const EdgeInsets.only(top: 16, left: 24, right: 23, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          Center(
            child: CtStepper(
              numPasos: 3,
              pasoActual: (giroState.departamento == null ||
                      giroState.direccion.isNotValid ||
                      giroState.agencia == null ||
                      giroState.motivo == null)
                  ? 3
                  : 4,
            ),
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Dirección del beneficiario',
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
          InputDireccion(
            direccion: giroState.direccion,
            onChanged: (value) {
              ref.read(emisionGirosProvider.notifier).changeDireccion(value);
            },
          ),
          const SizedBox(
            height: 24,
          ),
          SelectDepartamento(
            value: giroState.departamento,
            onChanged: (departamento) {
              ref
                  .read(emisionGirosProvider.notifier)
                  .changeDepartamento(departamento);
            },
            departamentos: giroState.departamentos,
          ),
          const SizedBox(
            height: 24,
          ),
          SelectAgencia(
            value: giroState.agencia,
            onChanged: (agencia) {
              ref.read(emisionGirosProvider.notifier).changeAgencia(agencia);
            },
            agencias: giroState.agencias,
          ),
          const SizedBox(
            height: 24,
          ),
          SelectMotivo(
            value: giroState.motivo,
            onChanged: (motivo) {
              ref.read(emisionGirosProvider.notifier).changeMotivo(motivo);
            },
            motivos: giroState.motivos,
          ),
          const Spacer(),
          const MensajeAlerta(),
          const SizedBox(
            height: 24,
          ),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref.read(emisionGirosProvider.notifier).pagar(withPush: true);
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

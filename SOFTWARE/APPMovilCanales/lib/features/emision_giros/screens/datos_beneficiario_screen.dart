import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/emision_giros/models/tipo_documento.dart';
import 'package:caja_tacna_app/features/emision_giros/providers/emision_giros_provider.dart';
import 'package:caja_tacna_app/features/emision_giros/widgets/select_nacionalidad.dart';
import 'package:caja_tacna_app/features/emision_giros/widgets/select_vinculo.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_documento.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_stepper.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_nombre_beneficiario.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class DatosBeneficiarioScreen extends StatelessWidget {
  const DatosBeneficiarioScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Emisión de giros',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _DatosBeneficiarioView(),
          )
        ],
      ),
    );
  }
}

class _DatosBeneficiarioView extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final giroState = ref.watch(emisionGirosProvider);
    final bool disabledButton = giroState.vinculo == null ||
        giroState.nombreBeneficiario.isNotValid ||
        giroState.nacionalidad == null ||
        giroState.vinculo == null;

    return Container(
      padding: const EdgeInsets.only(top: 16, left: 24, right: 23, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Center(
            child: CtStepper(
              numPasos: 3,
              pasoActual: 2,
            ),
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Datos del beneficiario',
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
          CtInputNumeroDocumento<TipoDocumentoGiro>(
            tipoDocumento: giroState.tipoDocumento,
            numeroDocumento: giroState.numeroDocumento,
            onChangeTipoDocumento: (tipoDocumento) {
              ref
                  .read(emisionGirosProvider.notifier)
                  .changeTipoDocumento(tipoDocumento);
            },
            onChangeNumeroDocumento: (numeroDocumento) {
              ref
                  .read(emisionGirosProvider.notifier)
                  .changeNumeroDocumento(numeroDocumento);
            },
            tiposDocumento: giroState.tiposDocumentos,
            tipoValidacion: TipoValidacionDocumento.validacion1,
          ),
          const SizedBox(
            height: 24,
          ),
          InputNombreBeneficiario(
            nombreBeneficiario: giroState.nombreBeneficiario,
            onChanged: (value) {
              ref
                  .read(emisionGirosProvider.notifier)
                  .changeNombreBeneficiario(value);
            },
          ),
          const SizedBox(
            height: 24,
          ),
          SelectNacionalidad(
            value: giroState.nacionalidad,
            onChanged: (nacionalidad) {
              ref
                  .read(emisionGirosProvider.notifier)
                  .changeNacionalidad(nacionalidad);
            },
            nacionalidades: giroState.nacionalidades,
          ),
          const SizedBox(
            height: 24,
          ),
          SelectVinculo(
            value: giroState.vinculo,
            onChanged: (vinculo) {
              ref.read(emisionGirosProvider.notifier).changeVinculo(vinculo);
            },
            vinculos: giroState.vinculos,
          ),
          const Spacer(),
          const SizedBox(
            height: 24,
          ),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref.read(emisionGirosProvider.notifier).datosBeneficiarioSubmit();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

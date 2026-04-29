import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_stepper.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/providers/solicitud_crediticia_provider.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/widgets/input_direccion_agencia.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/widgets/select_agencia.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/widgets/select_departamento.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class DatosAtencionScreen extends ConsumerStatefulWidget {
  const DatosAtencionScreen({super.key});

  @override
  DatosAtencionScreenState createState() => DatosAtencionScreenState();
}

class DatosAtencionScreenState extends ConsumerState<DatosAtencionScreen> {
  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Solicitud de crédito',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _DatosAtencionView(),
          )
        ],
      ),
    );
  }
}

class _DatosAtencionView extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final solicitudCrediticiaState = ref.watch(solicitudCrediticiaProvider);
    final bool disabledButton = solicitudCrediticiaState.agencia == null;

    return Container(
      padding: const EdgeInsets.only(top: 16, left: 24, right: 24, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Center(
            child: CtStepper(numPasos: 3, pasoActual: 3),
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Elige la agencia de tu preferencia',
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
            'Departamento',
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
          SelectDepartamento(
              departamentos: solicitudCrediticiaState.departamentos,
              onChanged: (departamento) {
                ref
                    .read(solicitudCrediticiaProvider.notifier)
                    .changeDepartamento(departamento);
              },
              value: solicitudCrediticiaState.departamento),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Agencia de atención',
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
          SelectAgencia(
              agencias: solicitudCrediticiaState.agencias,
              onChanged: (agencia) {
                ref
                    .read(solicitudCrediticiaProvider.notifier)
                    .changeAgencia(agencia);
              },
              value: solicitudCrediticiaState.agencia),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Dirección',
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
          InputDireccionAgencia(
              direccionAgencia: solicitudCrediticiaState.agencia?.direccion ?? '',
            ),
          const SizedBox(
            height: 24,
          ),
          const Spacer(),
          Row(
            children: [
              CtCheckbox(
                value: solicitudCrediticiaState.aceptarPoliticaTratamientoDatos,
                onPressed: () {
                  ref
                      .read(solicitudCrediticiaProvider.notifier)
                      .toggleAceptarPoliticaTratamientoDatos();
                },
              ),
              const SizedBox(
                width: 8,
              ),
              Container(
                width: double.infinity,
                constraints: const BoxConstraints(maxWidth: 310),
                child: RichText(
                  text: TextSpan(
                    style: const TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w500,
                      height: 22 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    children: <TextSpan>[
                      const TextSpan(
                        text: 'He leído y acepto la ',
                        style: TextStyle(color: AppColors.gray700),
                      ),
                      TextSpan(
                        text: 'Política de tratamiento de datos personales',
                        style: const TextStyle(color: AppColors.primary700),
                        recognizer: TapGestureRecognizer()
                          ..onTap = () async {
                            CtUtils.abrirWeb(
                                url:
                                    'https://cmactacna.com.pe/wp-content/uploads/2021/11/PPPDP-02-2021.pdf');
                          },
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 10,
          ),
          Row(
            children: [
              CtCheckbox(
                value: solicitudCrediticiaState.aceptarPublicidad,
                onPressed: () {
                  ref
                      .read(solicitudCrediticiaProvider.notifier)
                      .toggleAceptarPublicidad();
                },
              ),
              const SizedBox(
                width: 8,
              ),
              Container(
                width: double.infinity,
                constraints: const BoxConstraints(maxWidth: 310),
                child: RichText(
                  textAlign: TextAlign.justify,
                  text: const TextSpan(
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w500,
                      height: 22 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    children: <TextSpan>[
                      TextSpan(
                        text:
                            'Acepto recibir información, publicidad, entre otros de los productos y/o servicios de Caja Tacna',
                        style: TextStyle(color: AppColors.gray700),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 36,
          ),
          CtButton(
            text: 'Confirmar',
            onPressed: () {
              ref
                  .read(solicitudCrediticiaProvider.notifier)
                  .ingresoDatosAtencionSubmit();
            },
            disabled: disabledButton ||
                !solicitudCrediticiaState.aceptarPoliticaTratamientoDatos,
          )
        ],
      ),
    );
  }
}

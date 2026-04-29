import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_intenet.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/input_clave_aleatoria.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/token_digital/providers/token_digital_provider.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class AfiliarScreen extends ConsumerStatefulWidget {
  const AfiliarScreen({super.key});

  @override
  AfiliarScreenState createState() => AfiliarScreenState();
}

class AfiliarScreenState extends ConsumerState<AfiliarScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Volver',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _AfiliarView(),
          )
        ],
      ),
    );
  }
}

class _AfiliarView extends ConsumerWidget {
  const _AfiliarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final tokenState = ref.watch(tokenDigitalProvider);
    bool disabledButton = !tokenState.aceptarTerminos ||
        tokenState.claveCajero.isNotValid ||
        tokenState.claveInternet.isNotValid;

    return Container(
      padding: const EdgeInsets.only(top: 36, left: 24, right: 23, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Center(
            child: Text(
              'Afiliarme al Token Digital',
              style: TextStyle(
                fontSize: 24,
                fontWeight: FontWeight.w800,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
          const SizedBox(
            height: 24,
          ),
          const Center(
            child: Text(
              'Podrás usar el Token Digital para todas tus operaciones en Caja Tacna App y Tu Caja por Internet Personas.',
              style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w400,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
          const SizedBox(
            height: 48,
          ),
          const Text(
            'Clave de tarjeta',
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
          InputClaveAleatoria(
            titulo: 'Ingrese su clave de tarjeta',
            value: tokenState.claveCajero.value,
            onChanged: (valor) {
              ref
                  .read(tokenDigitalProvider.notifier)
                  .changeClaveCajero(ClaveTarjeta.dirty(valor));
            },
            length: 4,
            errorMessage: tokenState.claveCajero.errorMessage,
            hint: 'Clave de tarjeta',
          ),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Clave de internet',
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
          InputClaveAleatoria(
            titulo: 'Ingrese su clave de internet',
            value: tokenState.claveInternet.value,
            onChanged: (valor) {
              ref
                  .read(tokenDigitalProvider.notifier)
                  .changeClaveInternet(ClaveInternet.dirty(valor));
            },
            length: 6,
            errorMessage: tokenState.claveInternet.errorMessage,
            hint: 'Clave de Internet',
          ),
          const SizedBox(
            height: 30,
          ),
          const Spacer(),
          Row(
            children: [
              CtCheckbox(
                value: tokenState.aceptarTerminos,
                onPressed: () {
                  ref
                      .read(tokenDigitalProvider.notifier)
                      .toggleAceptarTerminos();
                },
              ),
              const SizedBox(
                width: 8,
              ),
              Container(
                width: double.infinity,
                constraints: const BoxConstraints(maxWidth: 260),
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
                        text: 'Acepto los ',
                        style: TextStyle(color: AppColors.gray700),
                      ),
                      TextSpan(
                        text: 'Términos y Condiciones',
                        style: const TextStyle(color: AppColors.primary700),
                       recognizer: TapGestureRecognizer()
                          ..onTap = () async {
                            CtUtils.abrirWeb(url: tokenState.documentoTermino!.urlDocumento);
                          },
                      ),
                      const TextSpan(
                        text: ' del Token Digital',
                        style: TextStyle(color: AppColors.gray700),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 16,
          ),
          CtButton(
            text: 'Afiliarme',
            onPressed: () async {
              ref.read(tokenDigitalProvider.notifier).afiliar();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

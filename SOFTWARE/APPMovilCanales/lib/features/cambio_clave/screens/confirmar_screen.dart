import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cambio_clave/providers/cambio_clave_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfirmarScreen extends ConsumerStatefulWidget {
  const ConfirmarScreen({super.key});

  @override
  ConfirmarScreenState createState() => ConfirmarScreenState();
}

class ConfirmarScreenState extends ConsumerState<ConfirmarScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Confirma la operación',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _ConfirmarView(),
          )
        ],
      ),
    );
  }
}

class _ConfirmarView extends ConsumerWidget {
  const _ConfirmarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final cambioClaveState = ref.watch(cambioClaveProvider);
    final timerState = ref.watch(timerProvider);

    return Container(
      padding: const EdgeInsets.only(top: 36, bottom: 56, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Operación',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                'Cambio de clave de internet',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                textAlign: TextAlign.end,
              ),
            ],
          ),
          const Spacer(),
          const Text(
            'Ingresa tu Token Digital',
            style: TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.w800,
              color: AppColors.gray900,
              height: 28 / 18,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 20,
          ),
          CtOtp(
            value: cambioClaveState.tokenDigital,
            onChanged: (value) {
              ref.read(cambioClaveProvider.notifier).changeToken(value);
            },
          ),
          const SizedBox(
            height: 13,
          ),
          timerState.timerOn
              ? const CtTimer()
              : Align(
                  alignment: Alignment.centerRight,
                  child: GestureDetector(
                    onTap: () {
                      ref
                          .read(cambioClaveProvider.notifier)
                          .cambiarClave(withPush: false);
                    },
                    child: const Text(
                      'Solicitar nuevo token',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.primary700,
                        height: 28 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ),
                ),
          const SizedBox(
            height: 87,
          ),
          Row(
            children: [
              CtCheckbox(
                value: cambioClaveState.aceptoTerminos,
                onPressed: () {
                  ref
                      .read(cambioClaveProvider.notifier)
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
                        text: 'Recuerda los ',
                        style: TextStyle(color: AppColors.gray700),
                      ),
                      TextSpan(
                        text: 'beneficios, riesgos y condiciones',
                        style: const TextStyle(color: AppColors.primary700),
                        recognizer: TapGestureRecognizer()
                          ..onTap = () async {
                            CtUtils.abrirWeb(
                                url:
                                    'https://cmactacna.com.pe/wp-content/uploads/2024/07/BRC-S-CE-01-2024.pdf');
                          },
                      ),
                      const TextSpan(
                        text: ' de los canales electrónicos',
                        style: TextStyle(color: AppColors.gray700),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 24,
          ),
          CtButton(
            text: 'Confirmar',
            onPressed: () {
              ref.read(cambioClaveProvider.notifier).confirmar();
            },
          )
        ],
      ),
    );
  }
}

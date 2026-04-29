import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/home/states/home_state.dart';
import 'package:caja_tacna_app/features/home/widgets/icon_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_verificar_afiliar.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_message.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_stepper.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/token_digital/providers/nfc_scanner_provider.dart';
import 'package:caja_tacna_app/features/token_digital/providers/token_digital_provider.dart';
import 'package:caja_tacna_app/features/token_digital/states/nfc_scanner_state.dart';
import 'package:caja_tacna_app/features/token_digital/states/token_digital_state.dart';
import 'package:caja_tacna_app/features/shared/widgets/info_red_card.dart';
import 'package:caja_tacna_app/features/token_digital/widgets/wifi_bars.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class ConfirmarRestablecerScreen extends ConsumerStatefulWidget {
  const ConfirmarRestablecerScreen({super.key});

  @override
  ConsumerState<ConsumerStatefulWidget> createState() =>
      ConfirmarRestablecerScreenState();
}

class ConfirmarRestablecerScreenState
    extends ConsumerState<ConfirmarRestablecerScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      _startNfcScan();
    });
  }

  void _startNfcScan() {
    ref.read(nfcScannerProvider.notifier).stopNfcReading();
    ref.read(nfcScannerProvider.notifier).startNfcReading(context);
  }

  // @override
  // void dispose() {
  //   // **Detener el escáner NFC al salir de la pantalla**
  //   ref.read(nfcScannerProvider.notifier).stopNfcReading();
  //   super.dispose();
  // }

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Volver',
      onBack: () async {
        if (ref.read(tokenDigitalProvider).pasoActualRestablecer == 1) {
          context.pop();
        } else {
          _startNfcScan();
          ref.read(tokenDigitalProvider.notifier).goToRestablecerPaso(1);
        }
      },
      child: const CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _ConfirmarRestablecerView(),
          )
        ],
      ),
    );
  }
}

class _ConfirmarRestablecerView extends ConsumerWidget {
  const _ConfirmarRestablecerView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final tokenState = ref.watch(tokenDigitalProvider);
    final nfcScannerState = ref.watch(nfcScannerProvider);

    return Container(
      padding: const EdgeInsets.only(top: 16, left: 24, right: 24, bottom: 36),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          Center(
            child: CtStepper(
                numPasos: 2, pasoActual: tokenState.pasoActualRestablecer),
          ),
          const SizedBox(height: 36),
          const Text(
            'Verifica tu identidad',
            style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 28 / 18,
                leadingDistribution: TextLeadingDistribution.even),
          ),
          const SizedBox(height: 15),
          tokenState.pasoActualRestablecer == 1
              ? _PrimerPasoConfirmarRestablecerView(
                  nfcScannerState: nfcScannerState,
                  tokenState: tokenState,
                )
              : _SegundoPasoRestablecerView(
                  tokenState: tokenState,
                  homeState: ref.watch(homeProvider),
                  timerState: ref.watch(timerProvider),
                  nfcScannerState: nfcScannerState,
                ),
        ],
      ),
    );
  }
}

class _PrimerPasoConfirmarRestablecerView extends ConsumerWidget {
  const _PrimerPasoConfirmarRestablecerView(
      {required this.nfcScannerState, required this.tokenState});

  final NfcScannerState nfcScannerState;
  final TokenDigitalState tokenState;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final bool disabledButton = nfcScannerState.status != 'SUCCESS' ||
        nfcScannerState.nfcCardData == null;

    return Expanded(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Acerca tu tarjeta de débito VISA a tu teléfono para verificar tu identidad.',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(height: 36),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              IconTarjeta(width: 110, girar: true),
              const WifiBars(),
              SvgPicture.asset(
                'assets/icons/cellphone.svg',
                width: 90,
              ),
            ],
          ),
          const SizedBox(height: 36),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Container(
                padding:
                    const EdgeInsets.symmetric(horizontal: 25, vertical: 12),
                decoration: BoxDecoration(
                  color: nfcScannerState.status == 'SUCCESS'
                      ? AppColors.success400
                      : AppColors.primary700,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Column(
                  children: [
                    const Text(
                      'Lectura de NFC',
                      style: TextStyle(
                        fontSize: 10,
                        color: AppColors.white,
                        fontWeight: FontWeight.w400,
                      ),
                    ),
                    Text(
                      nfcScannerState.status == 'SUCCESS'
                          ? 'EXITOSA'
                          : 'PENDIENTE',
                      style: const TextStyle(
                        fontSize: 14,
                        color: AppColors.white,
                        fontWeight: FontWeight.w700,
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(height: 36),
          const CtMessage(
            child: Expanded(
              child: Text(
                'Acerca tu tarjeta al teléfono y prueba colocarla en distintas posiciones para asegurar una correcta lectura NFC.',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ),
          ),
          const SizedBox(height: 15),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () async {
              await ref
                  .read(tokenDigitalProvider.notifier)
                  .goToSmsRestablecerTokenDigital(withStep: true);
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

class _SegundoPasoRestablecerView extends ConsumerWidget {
  const _SegundoPasoRestablecerView({
    required this.tokenState,
    required this.homeState,
    required this.timerState,
    required this.nfcScannerState,
  });

  final NfcScannerState nfcScannerState;
  final TokenDigitalState tokenState;
  final HomeState homeState;
  final TimerState timerState;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final bool disabledButton =
        !tokenState.aceptarTerminos || tokenState.claveSms.length < 6;

    return Expanded(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            width: double.infinity,
            constraints: const BoxConstraints(maxWidth: 330),
            child: Text(
              'Te hemos enviado una clave dinámica al +51${CtUtils.hashNumeroCelular(homeState.datosCliente?.numeroTelefonoSms)}',
              style: const TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w400,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
          const SizedBox(
            height: 30,
          ),
          Wrap(
            spacing: 8,
            children: [
              const Text(
                '¿No recibiste la clave dinámica?',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              GestureDetector(
                onTap: () {
                  ref
                      .read(tokenDigitalProvider.notifier)
                      .goToSmsRestablecerTokenDigital(withStep: false);
                },
                child: const Text(
                  'Reenviar clave',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    color: AppColors.primary700,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 16,
          ),
          if (!tokenState.cargandoSms)
            InputVerificarIdentidad(
              onChanged: (value) {
                ref.read(tokenDigitalProvider.notifier).changeClaveSms(value);
              },
            ),
          const SizedBox(
            height: 10,
          ),
          if (timerState.timerOn) const CtTimer(),
          const SizedBox(
            height: 17,
          ),
          const Spacer(),
          InfoRedCard(
            content:
                'Al restablecer tu Token Digital, se anulará la afiliación anterior y se activará automáticamente en este nuevo dispositivo.',
          ),
          const SizedBox(
            height: 36,
          ),
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
            height: 26,
          ),
          CtButton(
            text: 'Confirmar',
            onPressed: () async {
              await ref
                  .read(tokenDigitalProvider.notifier)
                  .restablecerTokenDigital(
                      cardData: nfcScannerState.nfcCardData);
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

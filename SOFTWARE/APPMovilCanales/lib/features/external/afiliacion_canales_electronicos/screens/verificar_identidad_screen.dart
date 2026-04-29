import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/providers/afiliacion_canales_electronicos_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_verificar_afiliar.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class VerificarIdentidadScreen extends ConsumerWidget {
  const VerificarIdentidadScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        scrolledUnderElevation: 0,
        automaticallyImplyLeading: false,
        toolbarHeight: 64,
        flexibleSpace: SafeArea(
          child: Container(
            height: 64,
            padding: const EdgeInsets.symmetric(horizontal: 24),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Container(
                  width: 32,
                  height: 32,
                  decoration: const BoxDecoration(
                    shape: BoxShape.circle,
                    color: AppColors.primary100,
                  ),
                  child: TextButton(
                    style: TextButton.styleFrom(
                      shape: const CircleBorder(),
                      padding: EdgeInsets.zero,
                    ),
                    onPressed: () {
                      context.pop();
                    },
                    child: SvgPicture.asset(
                      'assets/icons/shared/chevron-left.svg',
                      colorFilter: const ColorFilter.mode(
                        AppColors.primary700,
                        BlendMode.srcIn,
                      ),
                      width: 24,
                      height: 24,
                    ),
                  ),
                ),
                Image.asset(
                  'assets/images/logo_rojo.png',
                  width: 88,
                ),
                const SizedBox(
                  width: 32,
                )
              ],
            ),
          ),
        ),
      ),
      body: SafeArea(
        child: LayoutBuilder(
          builder: (context, constraints) {
            return SingleChildScrollView(
              child: ConstrainedBox(
                constraints: BoxConstraints(minHeight: constraints.maxHeight),
                child: IntrinsicHeight(
                  child: _CrearClaveView(),
                ),
              ),
            );
          },
        ),
      ),
    );
  }
}

class _CrearClaveView extends ConsumerStatefulWidget {
  const _CrearClaveView();

  @override
  _CrearClaveViewState createState() => _CrearClaveViewState();
}

class _CrearClaveViewState extends ConsumerState<_CrearClaveView> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    final afiliacionState = ref.watch(afiliacionCanElectProvider);
    final timerState = ref.watch(timerProvider);

    return Padding(
      padding: const EdgeInsets.only(
        top: 21,
        left: 24,
        right: 24,
        bottom: 41,
      ),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Container(
                width: double.infinity,
                constraints: const BoxConstraints(maxWidth: 300),
                child: const Text(
                  'Queremos verificar tu identidad',
                  style: TextStyle(
                    fontSize: 24,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray900,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ),
              const SizedBox(
                height: 8,
              ),
              Container(
                width: double.infinity,
                constraints: const BoxConstraints(maxWidth: 330),
                child: Text(
                  'Te hemos enviado una clave dinámica al +51${afiliacionState.enviarSmsResponse?.numeroTelefonoSms}',
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
                          .read(afiliacionCanElectProvider.notifier)
                          .crearClaveSubmit(withPush: false);
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
              if (!afiliacionState.cargandoSms)
                InputVerificarIdentidad(
                  onChanged: (value) {
                    ref
                        .read(afiliacionCanElectProvider.notifier)
                        .changeClaveSms(value);
                  },
                ),
              const SizedBox(
                height: 10,
              ),
              if (timerState.timerOn) const CtTimer()
            ],
          ),
          Column(
            children: [
              Row(
                children: [
                  CtCheckbox(
                    value: afiliacionState.aceptarTerminos,
                    onPressed: () {
                      ref
                          .read(afiliacionCanElectProvider.notifier)
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
                                CtUtils.abrirWeb(
                                    url:
                                        'https://cmactacna.com.pe/wp-content/uploads/2024/06/TC-CE-CTAPP-01-2024.pdf');
                              },
                          ),
                          const TextSpan(
                            text: ' de Caja Tacna App',
                            style: TextStyle(color: AppColors.gray700),
                          ),
                        ],
                      ),
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 17),
              CtButton(
                text: 'Continuar',
                disabled: afiliacionState.claveSms.length < 6 ||
                    !afiliacionState.aceptarTerminos,
                onPressed: () {
                  ref
                      .read(afiliacionCanElectProvider.notifier)
                      .verificarIdentidad();
                },
              ),
            ],
          )
        ],
      ),
    );
  }
}

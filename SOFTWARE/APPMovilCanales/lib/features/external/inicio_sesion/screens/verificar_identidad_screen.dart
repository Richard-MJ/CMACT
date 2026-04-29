import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
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

class VerificarIdentidadScreen extends ConsumerStatefulWidget {
  const VerificarIdentidadScreen({super.key});

  @override
  VerificarIdentidadScreenState createState() =>
      VerificarIdentidadScreenState();
}

class VerificarIdentidadScreenState
    extends ConsumerState<VerificarIdentidadScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(loginProvider.notifier).initVerificarIdentidadScreen();
    });
  }

  @override
  void dispose() {
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
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
                  child: _VerificarClaveView(),
                ),
              ),
            );
          },
        ),
      ),
    );
  }
}

class _VerificarClaveView extends ConsumerWidget {
  const _VerificarClaveView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final loginState = ref.watch(loginProvider);
    final timerState = ref.watch(timerProvider);

    return Padding(
      padding: EdgeInsets.only(
        top: 35,
        left: 24,
        right: 24,
        bottom: 35 + MediaQuery.of(context).padding.bottom,
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
                  'Te hemos enviado una clave dinámica al +51${loginState.enviarSmsResponse?.numeroTelefonoSms}',
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
                height: 48,
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
                    onTap: () => ref
                        .read(loginProvider.notifier)
                        .iniciarSesion(withPush: false),
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
              if (!loginState.cargandoSms)
                InputVerificarIdentidad(
                  onChanged: (value) {
                    ref
                        .read(loginProvider.notifier)
                        .changeCodigoAutorizacion(value);
                  },
                ),
              const SizedBox(height: 13),
              if (timerState.timerOn) const CtTimer()
            ],
          ),
          Column(
            children: [
              Row(
                children: [
                  CtCheckbox(
                    value: loginState.aceptarTerminos,
                    onPressed: () {
                      ref.read(loginProvider.notifier).toggleAceptarTerminos();
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
              const SizedBox(height: 17),
              CtButton(
                text: 'Continuar',
                disabled: loginState.claveSms.length < 6 ||
                    !loginState.aceptarTerminos,
                onPressed: () {
                  ref.read(loginProvider.notifier).verificarIdentidad();
                },
              ),
            ],
          )
        ],
      ),
    );
  }
}

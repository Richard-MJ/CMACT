import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/token_digital/providers/token_digital_provider.dart';
import 'package:caja_tacna_app/features/token_digital/widgets/dialog_desafiliar_token.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/svg.dart';
import 'package:go_router/go_router.dart';

class TokenScreen extends ConsumerStatefulWidget {
  const TokenScreen({super.key});

  @override
  AfiliarScreenState createState() => AfiliarScreenState();
}

class AfiliarScreenState extends ConsumerState<TokenScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(tokenDigitalProvider.notifier).obtenerToken();
    });
  }

  @override
  void dispose() {
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Volver',
      child: const CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _TokenView(),
          )
        ],
      ),
      onBack: () async {
        //cuando se presiona el boton de retroceso del appbar
        await ref.read(timerProvider.notifier).cancelTimer();
        if (!context.mounted) return;
        context.pop();
      },
    );
  }
}

class _TokenView extends ConsumerWidget {
  const _TokenView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final tokenState = ref.watch(tokenDigitalProvider);
    final timerState = ref.watch(timerProvider);

    return PopScope(
      canPop: false,
      onPopInvokedWithResult: (didPop, result) {
        if (didPop) return;
        //cuando se presiona el boton de retroceso nativo en android
        ref.read(timerProvider.notifier).cancelTimer();
        context.pop();
      },
      child: Container(
        padding:
            const EdgeInsets.only(top: 36, left: 24, right: 23, bottom: 56),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Center(
              child: Text(
                'Token Digital',
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
                'Usa esta clave para realizar transacciones en Tu Caja por Internet Personas.',
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
              height: 72,
            ),
            Center(
              child: SizedBox(
                width: 268,
                height: 268,
                child: Stack(
                  children: [
                    SizedBox.expand(
                      child: CircularProgressIndicator(
                        value: (timerState.timeDifference -
                                timerState.curentTimeDifference) /
                            timerState.timeDifference,
                        strokeWidth: 10,
                        color: AppColors.primary700,
                        backgroundColor: AppColors.primary100,
                      ),
                    ),
                    SizedBox.expand(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          const SizedBox(
                            height: 55,
                          ),
                          SvgPicture.asset(
                            'assets/icons/lock2.svg',
                            height: 36,
                          ),
                          const SizedBox(
                            height: 12,
                          ),
                          if (tokenState.esDispositivoAfiliado) ...[
                            Text(
                              tokenState
                                      .obtenerTokenResponse?.codigoSolicitado ??
                                  '',
                              style: const TextStyle(
                                fontSize: 40,
                                fontWeight: FontWeight.w600,
                                color: AppColors.gray900,
                                height: 1.6,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                            const SizedBox(
                              height: 6,
                            ),
                            Text(
                              'Expira en ${timerState.timerText}',
                              style: const TextStyle(
                                fontSize: 16,
                                fontWeight: FontWeight.w400,
                                color: AppColors.gray900,
                                height: 1.5,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                          ] else ...[
                            Container(
                                padding: EdgeInsets.symmetric(
                                    horizontal: 20, vertical: 10),
                                child: Text(
                                  'Tu Token Digital\nestá afiliado a otro dispositivo',
                                  style: const TextStyle(
                                      leadingDistribution:
                                          TextLeadingDistribution.even,
                                      fontWeight: FontWeight.w600,
                                      fontSize: 20,
                                      height: 1.6,
                                      color: AppColors.gray900),
                                  textAlign: TextAlign.center,
                                ))
                          ]
                        ],
                      ),
                    )
                  ],
                ),
              ),
            ),
            const Spacer(),
            tokenState.esDispositivoAfiliado
                ? _DesafiliarButton()
                : _RestablecerButton(ref: ref)
          ],
        ),
      ),
    );
  }
}

class _DesafiliarButton extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return CtButton(
      text: 'Desafiliar mi Token Digital',
      width: 250,
      type: ButtonType.outline,
      onPressed: () async {
        bool continuar = await showDialog(
          context: context,
          builder: (BuildContext context) {
            return const DialogDesafiliarToken();
          },
        );

        if (!continuar) return;
        if (!context.mounted) return;
        context.push('/token-digital/desafiliar');
      },
    );
  }
}

class _RestablecerButton extends StatelessWidget {
  final WidgetRef ref;
  const _RestablecerButton({required this.ref});

  @override
  Widget build(BuildContext context) {
    return CtButton(
      text: 'Restablecer Token Digital',
      width: 250,
      type: ButtonType.outline,
      onPressed: () {
        ref.read(tokenDigitalProvider.notifier).goToRestablecer();
      },
    );
  }
}

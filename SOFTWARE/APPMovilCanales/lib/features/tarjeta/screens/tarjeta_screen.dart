import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/tipo_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/tarjeta/providers/tarjeta_provider.dart';
import 'package:caja_tacna_app/features/tarjeta/widgets/card_tarjeta.dart';
import 'package:caja_tacna_app/features/tarjeta/widgets/card_tarjeta_skeleton.dart';
import 'package:caja_tacna_app/features/tarjeta/widgets/card_tarjeta_view.dart';
import 'package:caja_tacna_app/features/tarjeta/widgets/text_encabezado_tarjeta_screen.dart';
import 'package:caja_tacna_app/features/tarjeta/widgets/text_informacion_tarjeta.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TarjetaScreen extends ConsumerStatefulWidget {
  const TarjetaScreen({super.key});

  @override
  TarjetaScreenState createState() => TarjetaScreenState();
}

class TarjetaScreenState extends ConsumerState<TarjetaScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(tarjetaProvider.notifier).inicializarDatos();
      ref.read(tarjetaProvider.notifier).obtenerDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Configurar mi tarjeta',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _TarjetaView(),
          )
        ],
      ),
    );
  }
}

class _TarjetaView extends ConsumerWidget {
  const _TarjetaView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final timerState = ref.watch(timerProvider);
    final tarjetaState = ref.watch(tarjetaProvider);

    return Container(
      padding: const EdgeInsets.symmetric(vertical: 20, horizontal: 24)
          .copyWith(bottom: 46),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          const TextEncabezadoTarjetaScreen(),
          const SizedBox(height: 13),
          _buildTarjetaCard(tarjetaState),
          const SizedBox(height: 24),
          _buildInformacionTarjeta(context, ref, tarjetaState),
          const Spacer(),
          if (tarjetaState.afiliacionComprasInternet != null)
            _buildTokenDigitalSection(context, ref, tarjetaState, timerState),
          _buildAnularTarjeta(context, ref, tarjetaState),
        ],
      ),
    );
  }

  Widget _buildTarjetaCard(TarjetaState tarjetaState) {
    return TarjetaCard(
      width: 330,
      tipoTarjeta: tarjetaState.tipoTarjeta,
      child: !tarjetaState.datosCargados
          ? TarjetaSkeleton(tipoTarjeta: tarjetaState.tipoTarjeta)
          : const TarjetaViewCard(),
    );
  }

  Widget _buildInformacionTarjeta(
      BuildContext context, WidgetRef ref, TarjetaState tarjetaState) {
    if (!tarjetaState.datosCargados) return const SizedBox();
    return tarjetaState.afiliacionComprasInternet == null &&
            tarjetaState.tipoTarjeta == TipoTarjeta.debitoVisa
        ? Container(
            decoration: BoxDecoration(
              color: AppColors.gray50,
              borderRadius: BorderRadius.circular(8),
            ),
            width: double.infinity,
            padding: const EdgeInsets.symmetric(
              horizontal: 16,
              vertical: 18,
            ),
            child: RichText(
              textAlign: TextAlign.justify,
              text: TextSpan(
                style: const TextStyle(
                  fontSize: 15,
                  fontWeight: FontWeight.w400,
                  height: 23 / 15,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                children: <TextSpan>[
                  const TextSpan(
                    text:
                        'Recuerda afiliar una cuenta de ahorros para realizar compras por internet. Hazlo ',
                    style: TextStyle(color: AppColors.black),
                  ),
                  TextSpan(
                    text: 'aquí.',
                    style: const TextStyle(color: AppColors.primary700),
                    recognizer: TapGestureRecognizer()
                      ..onTap = () {
                        ref
                            .read(tarjetaProvider.notifier)
                            .irComprasPorInternet();
                      },
                  ),
                ],
              ),
            ),
          )
        : SizedBox(
            width: double.infinity,
            child:
                TextInformacionTarjeta(tipoTarjeta: tarjetaState.tipoTarjeta),
          );
  }

  Widget _buildTokenDigitalSection(BuildContext context, WidgetRef ref,
      TarjetaState tarjetaState, TimerState timerState) {
    if (!tarjetaState.mostrarTokenDigital) return const Column();

    final bool disabledButton = tarjetaState.tokenDigital.isEmpty ||
        tarjetaState.tokenDigital.length != 6;

    return Column(
      children: [
        const Row(
          children: [
            Text(
              'Ingresa tu token digital',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w800,
                color: AppColors.gray900,
                height: 1.55,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            )
          ],
        ),
        const SizedBox(height: 20),
        CtOtp(
          value: tarjetaState.tokenDigital,
          onChanged: (value) {
            ref.read(tarjetaProvider.notifier).changeToken(value);
          },
        ),
        const SizedBox(height: 13),
        timerState.timerOn
            ? const CtTimer()
            : Align(
                alignment: Alignment.centerRight,
                child: GestureDetector(
                  onTap: () {
                    ref
                        .read(tarjetaProvider.notifier)
                        .confirmarMostrarDatos(context);
                  },
                  child: const Text(
                    'Solicitar nuevo token',
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w500,
                      color: AppColors.primary700,
                      height: 2,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ),
              ),
        const SizedBox(height: 35),
        CtButton(
          text: 'Confirmar',
          onPressed: () {
            ref.read(tarjetaProvider.notifier).mostrarDatos(context);
          },
          disabled: disabledButton,
        ),
      ],
    );
  }

  Widget _buildAnularTarjeta(
      BuildContext context, WidgetRef ref, TarjetaState tarjetaState) {
    if (tarjetaState.mostrarTokenDigital ||
        tarjetaState.tipoTarjeta != "3" ||
        !tarjetaState.datosCargados) {
      return const SizedBox();
    }
    return SizedBox(
      width: double.infinity,
      child: RichText(
        textAlign: TextAlign.center,
        text: TextSpan(
          style: const TextStyle(
            fontSize: 16,
            fontWeight: FontWeight.w400,
            height: 1.5,
            leadingDistribution: TextLeadingDistribution.even,
          ),
          children: <TextSpan>[
            const TextSpan(
              text: 'Si quieres anular tu tarjeta ingresa ',
              style: TextStyle(color: AppColors.black),
            ),
            TextSpan(
              text: 'AQUÍ',
              style: const TextStyle(color: AppColors.primary700),
              recognizer: TapGestureRecognizer()
                ..onTap = () {
                  ref.read(tarjetaProvider.notifier).irAnularTarjeta();
                },
            ),
          ],
        ),
      ),
    );
  }
}

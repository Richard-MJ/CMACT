import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/providers/sesion_canal_electronico_provider.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/widgets/dispositivo_widget.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfirmarRemoverDispositivosScreen extends ConsumerStatefulWidget {
  const ConfirmarRemoverDispositivosScreen({super.key});

  @override
  ConfirmarRemoverDispositivosScreenState createState() =>
      ConfirmarRemoverDispositivosScreenState();
}

class ConfirmarRemoverDispositivosScreenState
    extends ConsumerState<ConfirmarRemoverDispositivosScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
        title: 'Confirma la operación', child: _ConfirmarView());
  }
}

class _ConfirmarView extends ConsumerWidget {
  const _ConfirmarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final sesionCanalElectronicoState =
        ref.watch(sesionCanalElectronicoProvider);
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
                'Retirar dispositivos seguros',
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
          const SizedBox(
            height: 30,
          ),
          const Text(
            'Dispositivos a retirar',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 19 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(height: 15),
          Expanded(
              child: ListView.separated(
                  itemCount: sesionCanalElectronicoState
                      .dispositivoSeleccionados.length,
                  itemBuilder: (context, index) {
                    return DispositivoView(
                      confirmar: true,
                      dispositivoActual: false,
                      dispositivoSeguro: sesionCanalElectronicoState
                          .dispositivoSeleccionados[index],
                    );
                  },
                  separatorBuilder: (context, index) => const SizedBox(
                        height: 5,
                      ))),
          const SizedBox(height: 15),
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
            value: sesionCanalElectronicoState.tokenDigital,
            onChanged: (value) {
              ref
                  .read(sesionCanalElectronicoProvider.notifier)
                  .changeToken(value);
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
                          .read(sesionCanalElectronicoProvider.notifier)
                          .removerDispositivosSeguros(withPush: false);
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
            height: 24,
          ),
          CtButton(
            text: 'Confirmar',
            onPressed: () {
              ref.read(sesionCanalElectronicoProvider.notifier).confirmar();
            },
          )
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/providers/sesion_canal_electronico_provider.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/widgets/dispositivo_skeleton.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/widgets/dispositivo_widget.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_message.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class DispositivosSegurosScreen extends ConsumerStatefulWidget {
  const DispositivosSegurosScreen({super.key});

  @override
  DispositivosSegurosScreenState createState() =>
      DispositivosSegurosScreenState();
}

class DispositivosSegurosScreenState
    extends ConsumerState<DispositivosSegurosScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(sesionCanalElectronicoProvider.notifier).initDatos();
      ref
          .read(sesionCanalElectronicoProvider.notifier)
          .obtenerDispositivosSeguros(context);
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: '¿Donde inicie sesión?',
      child: _DispositivosSegurosView(),
    );
  }
}

class _DispositivosSegurosView extends ConsumerWidget {
  const _DispositivosSegurosView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final sesionCanalEletronicoState =
        ref.watch(sesionCanalElectronicoProvider);

    return Container(
      padding: const EdgeInsets.only(
        top: 28,
        bottom: 56,
        left: 24,
        right: 24,
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Dispositivos Seguros',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          Row(
            children: [
              CtButton2(
                text: 'Dispositivo actual',
                onPressed: () {
                  ref
                      .read(sesionCanalElectronicoProvider.notifier)
                      .changeOtrosDispositivos(false);
                },
                type: sesionCanalEletronicoState.otrosDispositivosSeguros
                    ? ButtonType.outline
                    : ButtonType.solid,
              ),
              const SizedBox(
                width: 8,
              ),
              CtButton2(
                text: 'Otros dispositivos seguros',
                onPressed: () {
                  ref
                      .read(sesionCanalElectronicoProvider.notifier)
                      .changeOtrosDispositivos(true);
                },
                type: !sesionCanalEletronicoState.otrosDispositivosSeguros
                    ? ButtonType.outline
                    : ButtonType.solid,
              )
            ],
          ),
          const SizedBox(
            height: 15,
          ),
          if (!sesionCanalEletronicoState.otrosDispositivosSeguros) ...[
            sesionCanalEletronicoState.dispositivoActual == null
                ? const DispositivoViewSkeleton()
                : DispositivoView(
                    confirmar: false,
                    dispositivoActual: true,
                    dispositivoSeguro:
                        sesionCanalEletronicoState.dispositivoActual),
          ] else ...[
            if (sesionCanalEletronicoState.dispositivosSeguros.isNotEmpty) ...[
              Expanded(
                  child: ListView.separated(
                      itemCount:
                          sesionCanalEletronicoState.dispositivosSeguros.length,
                      itemBuilder: (context, index) {
                        return DispositivoView(
                          confirmar: false,
                          dispositivoActual: false,
                          dispositivoSeguro: sesionCanalEletronicoState
                              .dispositivosSeguros[index],
                        );
                      },
                      separatorBuilder: (context, index) => const SizedBox(
                            height: 5,
                          ))),
              const SizedBox(
                height: 16,
              ),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 10),
                child: Row(
                  children: [
                    CtCheckbox(
                        value: sesionCanalEletronicoState.dispositivoActual !=
                                null &&
                            sesionCanalEletronicoState
                                    .dispositivoSeleccionados.length ==
                                sesionCanalEletronicoState
                                    .dispositivosSeguros.length,
                        onPressed: () {
                          ref
                              .read(sesionCanalElectronicoProvider.notifier)
                              .toggleSeleccionarTodosLosDispositivos();
                        }),
                    const SizedBox(
                      width: 10,
                    ),
                    GestureDetector(
                      onTap: () => ref
                          .read(sesionCanalElectronicoProvider.notifier)
                          .toggleSeleccionarTodosLosDispositivos(),
                      child: const Text(
                        'Seleccionar todos los dispositivos',
                        style:
                            TextStyle(color: AppColors.gray700, fontSize: 14),
                      ),
                    )
                  ],
                ),
              ),
              const SizedBox(
                height: 15,
              ),
              const CtMessage(
                child: Expanded(
                  child: Text(
                    'Si desea retirar un dispositivo que está registrado como seguro, selecciónelo y presione "Continuar".',
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 22 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    textAlign: TextAlign.justify,
                  ),
                ),
              ),
              const SizedBox(
                height: 20,
              ),
              CtButton(
                text: 'Continuar',
                onPressed: () {
                  ref
                      .read(sesionCanalElectronicoProvider.notifier)
                      .removerDispositivosSeguros(withPush: true);
                },
                disabled: !sesionCanalEletronicoState
                    .dispositivoSeleccionados.isNotEmpty,
              )
            ] else ...[
              const SizedBox(
                height: 15,
              ),
              const CtMessage(
                child: Expanded(
                  child: Text(
                    'Aún no ha registrado otros dispositivos como seguros.',
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 22 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    textAlign: TextAlign.justify,
                  ),
                ),
              ),
            ],
          ]
        ],
      ),
    );
  }
}

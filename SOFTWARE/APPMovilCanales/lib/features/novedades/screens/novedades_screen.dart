import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/novedades/providers/novedades_provider.dart';
import 'package:caja_tacna_app/features/novedades/widgets/sesion_widget.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/providers/sesion_canal_electronico_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button_2.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class NovedadesScreen extends ConsumerStatefulWidget {
  const NovedadesScreen({super.key});

  @override
  NovedadesScreenState createState() => NovedadesScreenState();
}

class NovedadesScreenState extends ConsumerState<NovedadesScreen> {
  @override
  void initState() {
    Future.microtask(() {
      ref.read(novedadesProvider.notifier).initData();
      ref.read(novedadesProvider.notifier).obtenerDatos();
    });
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Centro de novedades',
      child: _NovedadesView(),
    );
  }
}

class _NovedadesView extends ConsumerWidget {
  const _NovedadesView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final novedadesState = ref.watch(novedadesProvider);
    final sesionCanalEletronicoState =
        ref.watch(sesionCanalElectronicoProvider);
    return Container(
      padding: const EdgeInsets.only(top: 28, bottom: 56, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          SizedBox(
            height: 36,
            child: ListView.separated(
              scrollDirection: Axis.horizontal,
              itemBuilder: (context, index) {
                final tab = novedadesState.tabs[index];
                return CtButton2(
                  text: tab.nombre,
                  onPressed: () {
                    ref
                        .read(novedadesProvider.notifier)
                        .obtenerNovedades(tab.id);
                  },
                  type: novedadesState.tabSeleccionado == tab.id
                      ? ButtonType.solid
                      : ButtonType.outline,
                );
              },
              separatorBuilder: (context, index) {
                return const SizedBox(
                  width: 8,
                );
              },
              itemCount: novedadesState.tabs.length,
            ),
          ),
          if (novedadesState.categoriaSeleccionada?.nombre == "ALERTAS") ...[
            const SizedBox(
              height: 18,
            ),
            const Text(
              'Últimos dispositivos en los que se inició sesion',
              textAlign: TextAlign.start,
            ),
            const SizedBox(
              height: 18,
            ),
            Expanded(
                child: ListView.separated(
                    itemCount: sesionCanalEletronicoState
                        .sesionesCanalElectronico.length,
                    itemBuilder: (context, index) {
                      return SesionView(
                        sesion: sesionCanalEletronicoState
                            .sesionesCanalElectronico[index],
                      );
                    },
                    separatorBuilder: (context, index) => const SizedBox(
                          height: 15,
                        )))
          ] else ...[
            const SizedBox(
              height: 30,
            ),
            Expanded(
                child: CustomScrollView(
              slivers: [
                SliverPadding(
                  padding: const EdgeInsets.all(0),
                  sliver: SliverList.separated(
                    itemBuilder: (context, index) {
                      final novedad = novedadesState.novedades[index];

                      return ListTile(
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(8),
                        ),
                        contentPadding: EdgeInsets.zero,
                        minVerticalPadding: 0,
                        onTap: () {
                          ref
                              .read(novedadesProvider.notifier)
                              .selectNovedad(novedad);
                        },
                        title: Container(
                          padding: const EdgeInsets.symmetric(
                            horizontal: 16,
                            vertical: 18,
                          ),
                          decoration: BoxDecoration(
                            border: Border.all(
                              width: 1,
                              color: AppColors.border1,
                            ),
                            borderRadius: BorderRadius.circular(8),
                          ),
                          child: Row(
                            children: [
                              SizedBox(
                                width: 100,
                                height: 100,
                                child: ClipRRect(
                                  borderRadius: BorderRadius.circular(8),
                                  child: Image.network(
                                    novedad.urlImagen,
                                    fit: BoxFit.cover,
                                    errorBuilder: (context, error, stackTrace) {
                                      return Container();
                                    },
                                  ),
                                ),
                              ),
                              const SizedBox(
                                width: 16,
                              ),
                              Expanded(
                                child: Column(
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  children: [
                                    Text(
                                      novedad.titulo,
                                      style: const TextStyle(
                                        fontSize: 14,
                                        fontWeight: FontWeight.w500,
                                        color: AppColors.gray900,
                                        height: 22 / 14,
                                        leadingDistribution:
                                            TextLeadingDistribution.even,
                                      ),
                                    ),
                                    const SizedBox(
                                      height: 4,
                                    ),
                                    Text(
                                      'Del ${novedad.fechaInicioFormat} al ${novedad.fechaFinalFormat}',
                                      style: const TextStyle(
                                        fontSize: 12,
                                        fontWeight: FontWeight.w400,
                                        color: AppColors.gray900,
                                        height: 18 / 12,
                                        leadingDistribution:
                                            TextLeadingDistribution.even,
                                      ),
                                    ),
                                    const SizedBox(
                                      height: 4,
                                    ),
                                    Text(
                                      novedad.resumen,
                                      style: const TextStyle(
                                        fontSize: 12,
                                        fontWeight: FontWeight.w500,
                                        color: AppColors.gray900,
                                        height: 18 / 12,
                                        leadingDistribution:
                                            TextLeadingDistribution.even,
                                      ),
                                    ),
                                  ],
                                ),
                              )
                            ],
                          ),
                        ),
                      );
                    },
                    separatorBuilder: (context, index) {
                      return const SizedBox(
                        height: 16,
                      );
                    },
                    itemCount: novedadesState.novedades.length,
                  ),
                ),
              ],
            ))
          ]
        ],
      ),
    );
  }
}

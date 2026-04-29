import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/providers/anulacion_tarjetas_provider.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/widgets/select_motivo.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class AnularScreen extends ConsumerStatefulWidget {
  const AnularScreen({super.key});

  @override
  AnularScreenState createState() => AnularScreenState();
}

class AnularScreenState extends ConsumerState<AnularScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(anulacionTarjetasProvider.notifier).initDatos();
      ref.read(anulacionTarjetasProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Anular tarjetas',
      child: _AnularView(),
    );
  }
}

class _AnularView extends ConsumerWidget {
  const _AnularView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final anularState = ref.watch(anulacionTarjetasProvider);
    return CustomScrollView(
      slivers: [
        SliverToBoxAdapter(
          child: Container(
            padding: const EdgeInsets.only(
              top: 34,
              left: 24,
              right: 24,
              bottom: 13,
            ),
            child: const Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  'Selecciona la tarjeta que deseas anular',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 24 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ],
            ),
          ),
        ),
        SliverPadding(
          padding: const EdgeInsets.only(
            left: 24,
            right: 23,
          ),
          sliver: SliverList.separated(
            itemBuilder: (context, index) {
              final tarjeta = anularState.tarjetas[index];
              final isSelected = tarjeta.codigoTipoTarjeta ==
                  anularState.tarjeta?.codigoTipoTarjeta;

              return Container(
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(8),
                  color: isSelected ? AppColors.gray100 : AppColors.gray25,
                  border:
                      isSelected ? null : Border.all(color: AppColors.gray300),
                  boxShadow: AppColors.shadowSm,
                ),
                child: TextButton(
                  onPressed: () {
                    ref
                        .read(anulacionTarjetasProvider.notifier)
                        .changeTarjeta(tarjeta);
                  },
                  style: TextButton.styleFrom(
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(8),
                    ),
                    padding: EdgeInsets.zero,
                  ),
                  child: Container(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 16,
                      vertical: 18,
                    ),
                    child: Row(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Container(
                          width: 16,
                          height: 16,
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.circular(8),
                            border: Border.all(
                              color: isSelected
                                  ? AppColors.primary600
                                  : AppColors.gray300,
                              width: 1,
                            ),
                          ),
                          child: isSelected
                              ? Center(
                                  child: Container(
                                    width: 6,
                                    height: 6,
                                    decoration: BoxDecoration(
                                      borderRadius: BorderRadius.circular(3),
                                      color: AppColors.primary600,
                                    ),
                                  ),
                                )
                              : null,
                        ),
                        const SizedBox(
                          width: 16,
                        ),
                        Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          mainAxisAlignment: MainAxisAlignment.start,
                          children: [
                            Text(
                              tarjeta.descripcionTarjeta,
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
                              CtUtils.formatNumeroTarjeta(
                                numeroCuenta: tarjeta.numeroTarjeta,
                                hash: true,
                              ),
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w400,
                                color: AppColors.gray900,
                                height: 22 / 14,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                          ],
                        )
                      ],
                    ),
                  ),
                ),
              );
            },
            itemCount: anularState.tarjetas.length,
            separatorBuilder: (context, index) {
              return const SizedBox(
                height: 16,
              );
            },
          ),
        ),
        SliverFillRemaining(
          hasScrollBody: false,
          child: Container(
            padding: const EdgeInsets.only(
              top: 24,
              left: 24,
              right: 23,
              bottom: 56,
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Ingresa el motivo de anulación',
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
                SelectMotivo(
                  value: anularState.motivo,
                  onChanged: (motivo) {
                    ref
                        .read(anulacionTarjetasProvider.notifier)
                        .changeMotivo(motivo);
                  },
                  motivos: anularState.motivos,
                ),
                const SizedBox(
                  height: 30,
                ),
                const Spacer(),
                CtButton(
                  text: 'Continuar',
                  onPressed: () {
                    ref
                        .read(anulacionTarjetasProvider.notifier)
                        .anular(withPush: true);
                  },
                  disabled:
                      anularState.tarjeta == null || anularState.motivo == null,
                )
              ],
            ),
          ),
        ),
      ],
    );
  }
}

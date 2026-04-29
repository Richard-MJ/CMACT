import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class DetalleOperacionScreen extends ConsumerStatefulWidget {
  const DetalleOperacionScreen({super.key});

  @override
  DetalleOperacionScreenState createState() => DetalleOperacionScreenState();
}

class DetalleOperacionScreenState
    extends ConsumerState<DetalleOperacionScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Detalle de operación',
      child: _DetalleOperacionView(),
    );
  }
}

class _DetalleOperacionView extends ConsumerWidget {
  const _DetalleOperacionView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final operacionesState = ref.watch(operacionesFrecuentesProvider);
    final detalleOperacion =
        ref.watch(operacionesFrecuentesProvider).detalleOperacion;

    return CustomScrollView(
      slivers: [
        SliverFillRemaining(
          child: Container(
            padding: const EdgeInsets.only(
              left: 24,
              right: 24,
              top: 34,
              bottom: 56,
            ),
            child: Column(
              children: [
                Container(
                  width: double.infinity,
                  padding: const EdgeInsets.symmetric(
                    horizontal: 16,
                    vertical: 18,
                  ),
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(8),
                    border: Border.all(
                      width: 1,
                      color: AppColors.gray300,
                    ),
                    color: AppColors.gray50,
                  ),
                  child: Column(
                    children: [
                      Text(
                        detalleOperacion?.nombreOperacionFrecuente ?? '',
                        style: const TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray900,
                          height: 28 / 18,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      const SizedBox(
                        height: 8,
                      ),
                      GestureDetector(
                        onTap: () {
                          ref
                              .read(operacionesFrecuentesProvider.notifier)
                              .goEditarAlias();
                        },
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            SvgPicture.asset(
                              'assets/icons/edit-2.svg',
                              height: 20,
                              width: 20,
                              colorFilter: const ColorFilter.mode(
                                AppColors.primary700,
                                BlendMode.srcIn,
                              ),
                            ),
                            const SizedBox(
                              width: 8,
                            ),
                            const Text(
                              'Editar alias',
                              style: TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w500,
                                color: AppColors.primary700,
                                height: 22 / 14,
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
                const Spacer(),
                CtButton(
                  text: 'Ejecutar operación',
                  onPressed: () {
                    ref
                        .read(operacionesFrecuentesProvider.notifier)
                        .selectOperacion(operacionesState.detalleOperacion!);
                  },
                ),
                const SizedBox(
                  height: 24,
                ),
                CtButton(
                  text: 'Eliminar operación',
                  onPressed: () {
                    ref
                        .read(operacionesFrecuentesProvider.notifier)
                        .eliminarOperacion();
                  },
                  type: ButtonType.plain,
                )
              ],
            ),
          ),
        )
      ],
    );
  }
}

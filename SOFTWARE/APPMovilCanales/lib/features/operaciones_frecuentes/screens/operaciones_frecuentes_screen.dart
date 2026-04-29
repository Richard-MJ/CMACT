import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/models/listar_operaciones_response.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/widgets/input_buscar.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button_2.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class OperacionesFrecuentesScreen extends ConsumerStatefulWidget {
  const OperacionesFrecuentesScreen({super.key});

  @override
  OperacionesFrecuentesScreenState createState() =>
      OperacionesFrecuentesScreenState();
}

class OperacionesFrecuentesScreenState
    extends ConsumerState<OperacionesFrecuentesScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(operacionesFrecuentesProvider.notifier).initData();
      ref.read(operacionesFrecuentesProvider.notifier).listarOperaciones();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Operaciones frecuentes',
      child: _OperacionesFrecuentesView(),
    );
  }
}

class _OperacionesFrecuentesView extends ConsumerWidget {
  const _OperacionesFrecuentesView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final operacionesState = ref.watch(operacionesFrecuentesProvider);
    List<OperacionFrecuente> operacionesFiltradas = operacionesState
        .operacionesFrecuentes
        .where((operacion) => operacion.nombreOperacionFrecuente
            .toLowerCase()
            .contains(operacionesState.search.toLowerCase()))
        .toList();
    if (operacionesState.categoria != null) {
      operacionesFiltradas = operacionesFiltradas
          .where((operacion) =>
              operacion.numeroCategoriaTipoOperacionFrecuente ==
              operacionesState.categoria!.numeroCategoriaTipoOperacionFrecuente)
          .toList();
    }

    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        Container(
          padding: const EdgeInsets.only(
            left: 24,
            right: 24,
            top: 36,
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  SizedBox(
                    child: GestureDetector(
                      onTap: () {
                        context.push('/operaciones-frecuentes/administrar');
                      },
                      child: Row(
                        children: [
                          SvgPicture.asset(
                            'assets/icons/edit.svg',
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
                            'Administrar operaciones',
                            style: TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w500,
                              color: AppColors.primary700,
                              height: 22 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ],
              ),
              const SizedBox(
                height: 24,
              ),
              const Text(
                'Busca tus operaciones guardadas',
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
              InputBuscar(
                value: operacionesState.search,
                onChanged: (value) {
                  ref
                      .read(operacionesFrecuentesProvider.notifier)
                      .changeSearch(value);
                },
                onSubmitted: () {},
              ),
              const SizedBox(
                height: 16,
              ),
            ],
          ),
        ),
        SizedBox(
          height: 32,
          child: SingleChildScrollView(
            padding: const EdgeInsets.symmetric(
              horizontal: 24,
              vertical: 0,
            ),
            scrollDirection: Axis.horizontal,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.start,
              children: [
                CtButton2(
                  text: 'Todos',
                  onPressed: () {
                    ref
                        .read(operacionesFrecuentesProvider.notifier)
                        .selectCategoria(null);
                  },
                  type: operacionesState.categoria == null
                      ? ButtonType.solid
                      : ButtonType.outline,
                ),
                const SizedBox(
                  width: 8,
                ),
                ListView.separated(
                  scrollDirection: Axis.horizontal,
                  shrinkWrap: true,
                  itemBuilder: (context, index) {
                    final categoria = operacionesState.categorias[index];
                    return CtButton2(
                      text: categoria.descripcionCategoria,
                      onPressed: () {
                        ref
                            .read(operacionesFrecuentesProvider.notifier)
                            .selectCategoria(categoria);
                      },
                      type: operacionesState.categoria
                                  ?.numeroCategoriaTipoOperacionFrecuente ==
                              categoria.numeroCategoriaTipoOperacionFrecuente
                          ? ButtonType.solid
                          : ButtonType.outline,
                    );
                  },
                  separatorBuilder: (context, index) {
                    return const SizedBox(
                      width: 8,
                    );
                  },
                  itemCount: operacionesState.categorias.length,
                )
              ],
            ),
          ),
        ),
        const SizedBox(
          height: 16,
        ),
        Expanded(
          child: ListView.separated(
            padding: const EdgeInsets.only(
              left: 24,
              right: 24,
              bottom: 58,
            ),
            separatorBuilder: (context, index) {
              return const SizedBox(
                height: 8,
              );
            },
            itemBuilder: (context, index) {
              final operacion = operacionesFiltradas[index];
              return ListTile(
                contentPadding: EdgeInsetsDirectional.zero,
                dense: true,
                minVerticalPadding: 0,
                onTap: () {
                  ref
                      .read(operacionesFrecuentesProvider.notifier)
                      .selectOperacion(operacion);
                },
                title: Container(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 16,
                    vertical: 8,
                  ),
                  decoration: const BoxDecoration(
                    border: Border(
                      bottom: BorderSide(
                        width: 2,
                        color: AppColors.gray200,
                      ),
                    ),
                  ),
                  child: Row(
                    children: [
                      Container(
                        width: 36,
                        height: 36,
                        decoration: BoxDecoration(
                          borderRadius: BorderRadius.circular(18),
                          color: AppColors.primary100,
                        ),
                        child: Center(
                          child: Text(
                            operacion.nombreOperacionFrecuente.isEmpty
                                ? ''
                                : operacion.nombreOperacionFrecuente
                                    .substring(0, 1)
                                    .toUpperCase(),
                            style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w500,
                              color: AppColors.primary700,
                              height: 22 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
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
                              operacion.nombreOperacionFrecuente,
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w500,
                                color: AppColors.gray900,
                                height: 22 / 14,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                            Text(
                              operacion.nombreTipo,
                              style: const TextStyle(
                                fontSize: 12,
                                fontWeight: FontWeight.w500,
                                color: AppColors.gray700,
                                height: 1.5,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                          ],
                        ),
                      ),
                      SvgPicture.asset(
                        'assets/icons/chevron-right.svg',
                        height: 20,
                        width: 20,
                      ),
                    ],
                  ),
                ),
              );
            },
            itemCount: operacionesFiltradas.length,
          ),
        ),
      ],
    );
  }
}

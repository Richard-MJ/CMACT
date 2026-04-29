import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/models/listar_operaciones_response.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/widgets/input_buscar.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class AdministrarOperacionesFrecuentesScreen extends ConsumerStatefulWidget {
  const AdministrarOperacionesFrecuentesScreen({super.key});

  @override
  AdministrarOperacionesFrecuentesScreenState createState() =>
      AdministrarOperacionesFrecuentesScreenState();
}

class AdministrarOperacionesFrecuentesScreenState
    extends ConsumerState<AdministrarOperacionesFrecuentesScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Administrar operaciones frecuentes',
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

    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        Container(
          padding: const EdgeInsets.only(
            left: 24,
            right: 24,
            top: 28,
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
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
                      .detalleOperacion(operacion);
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
                        'assets/icons/edit-2.svg',
                        height: 20,
                        width: 20,
                        colorFilter: const ColorFilter.mode(
                          AppColors.gray500,
                          BlendMode.srcIn,
                        ),
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

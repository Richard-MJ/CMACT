import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/pago_servicio.dart';
import 'package:caja_tacna_app/features/pago_servicios/providers/pago_servicios_provider.dart';
import 'package:caja_tacna_app/features/pago_servicios/widgets/chip_item.dart';
import 'package:caja_tacna_app/features/pago_servicios/widgets/input_buscar.dart';
import 'package:caja_tacna_app/features/pago_servicios/widgets/servicio_item.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class BuscarScreen extends ConsumerStatefulWidget {
  const BuscarScreen({super.key});

  @override
  BuscarScreenState createState() => BuscarScreenState();
}

class BuscarScreenState extends ConsumerState<BuscarScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(pagoServiciosProvider.notifier).initDatos();
      ref.read(pagoServiciosProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Pago de servicios',
      child: _BuscarView(),
    );
  }
}

class _BuscarView extends ConsumerWidget {
  const _BuscarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoServiciosState = ref.watch(pagoServiciosProvider);

    return CustomScrollView(
      slivers: [
        SliverToBoxAdapter(
          child: Container(
            padding: const EdgeInsets.only(
              top: 36,
              bottom: 24,
              left: 24,
              right: 24,
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                const Text(
                  'Busca la empresa que deseas pagar',
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
                  value: pagoServiciosState.searchEmpresa,
                  onChanged: (value) {
                    ref
                        .read(pagoServiciosProvider.notifier)
                        .changeSearchEmpresa(value);
                  },
                  onSubmitted: () {
                    ref
                        .read(pagoServiciosProvider.notifier)
                        .obtenerEmpresasPorTexto(
                          texto: pagoServiciosState.searchEmpresa,
                          mostrarResultados: true,
                        );
                  },
                ),
              ],
            ),
          ),
        ),
        SliverToBoxAdapter(
          child: SizedBox(
            height: 26,
            child: ListView.separated(
              padding: const EdgeInsetsDirectional.symmetric(horizontal: 24),
              scrollDirection: Axis.horizontal,
              itemBuilder: (context, index) {
                final categoria = pagoServiciosState.categorias[index];
                return ChipItem(
                  label: categoria.descripcionTipoCategoriaServicio,
                  onPressed: () {
                    ref
                        .read(pagoServiciosProvider.notifier)
                        .obtenerEmpresasPorCategoria(categoria);
                  },
                );
              },
              separatorBuilder: (context, index) {
                return const SizedBox(
                  width: 12,
                );
              },
              itemCount: pagoServiciosState.categorias.length,
            ),
          ),
        ),
        if (pagoServiciosState.statusBusqueda == StatusBusqueda.editando)
          SliverToBoxAdapter(
            child: Container(
              padding: const EdgeInsets.only(
                top: 36,
                bottom: 16,
                left: 24,
                right: 24,
              ),
              child: const Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  Text(
                    'Servicios más usados',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w500,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ],
              ),
            ),
          ),
        if (pagoServiciosState.statusBusqueda == StatusBusqueda.editando)
          SliverPadding(
            padding: const EdgeInsets.only(
              left: 24,
              right: 24,
              bottom: 56,
            ),
            sliver: SliverList.separated(
              itemCount: pagoServiciosState.ultimosPagos.length,
              itemBuilder: (context, index) {
                final ultimoPago = pagoServiciosState.ultimosPagos[index];
                return ServicioItem(
                  label: ultimoPago.nombreServicio,
                  onPressed: () {
                    ref
                        .read(pagoServiciosProvider.notifier)
                        .seleccionarServicio(
                          servicio: ultimoPago,
                        );
                  },
                );
              },
              separatorBuilder: (context, index) {
                return const SizedBox(
                  height: 8,
                );
              },
            ),
          ),
        if (pagoServiciosState.statusBusqueda ==
            StatusBusqueda.mostrarResultados)
          SliverToBoxAdapter(
            child: Container(
              padding: const EdgeInsets.only(
                top: 36,
                bottom: 16,
                left: 24,
                right: 24,
              ),
              child: const Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  Text(
                    'Resultados',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w500,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ],
              ),
            ),
          ),
        if (pagoServiciosState.statusBusqueda ==
                StatusBusqueda.mostrarResultados &&
            pagoServiciosState.empresas.isNotEmpty)
          SliverPadding(
            padding: const EdgeInsets.only(
              left: 24,
              right: 24,
              bottom: 56,
            ),
            sliver: SliverList.separated(
              itemCount: pagoServiciosState.empresas.length,
              itemBuilder: (context, index) {
                final empresa = pagoServiciosState.empresas[index];
                final List<PagoServicio> servicios =
                    pagoServiciosState.servicios[empresa.codigoEmpresa] ?? [];

                return Container(
                  decoration: const BoxDecoration(
                    boxShadow: [
                      BoxShadow(
                        color: Color.fromRGBO(249, 205, 204, 0.50),
                        offset: Offset(0, 2),
                        blurRadius: 0,
                        spreadRadius: 0,
                      ),
                    ],
                    color: AppColors.white,
                  ),
                  child: ListTileTheme(
                    dense: true,
                    child: ExpansionTile(
                      title: Text(
                        empresa.nombreEmpresa,
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray900,
                          height: 22 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      tilePadding: const EdgeInsetsDirectional.symmetric(
                        horizontal: 16,
                        vertical: 0,
                      ),
                      trailing: SvgPicture.asset(
                        'assets/icons/chevron-down.svg',
                        height: 24,
                        colorFilter: const ColorFilter.mode(
                          AppColors.primary700,
                          BlendMode.srcIn,
                        ),
                      ),
                      shape: const Border(),
                      childrenPadding: EdgeInsets.zero,
                      children: servicios.map((servicio) {
                        return ListTile(
                          dense: true,
                          contentPadding: EdgeInsets.zero,
                          onTap: () {
                            ref
                                .read(pagoServiciosProvider.notifier)
                                .seleccionarServicio(
                                  servicio: ServicioPagar(
                                    codigoEmpresa: empresa.codigoEmpresa,
                                    nombreEmpresa: empresa.nombreEmpresa,
                                    codigoServicio: servicio.codigoServicio,
                                    nombreServicio: servicio.nombreServicio,
                                    codigoCategoria: empresa.codigoCategoria,
                                    nombreCategoria: empresa.nombreCategoria,
                                    codigoGrupoEmpresa:
                                        empresa.codigoGrupoEmpresa,
                                    tipoPagoServicio: empresa.tipoPagoServicio,
                                  ),
                                );
                          },
                          title: Container(
                            width: double.infinity,
                            padding: const EdgeInsets.only(
                              left: 36,
                              right: 16,
                              top: 0,
                              bottom: 0,
                            ),
                            child: Text(
                              servicio.nombreServicio,
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w400,
                                color: AppColors.gray800,
                                height: 22 / 14,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                              textAlign: TextAlign.start,
                            ),
                          ),
                        );
                      }).toList(),
                      onExpansionChanged: (value) {
                        if (!value) return;
                        ref
                            .read(pagoServiciosProvider.notifier)
                            .cargarServicios(index);
                      },
                    ),
                  ),
                );
              },
              separatorBuilder: (context, index) {
                return const SizedBox(
                  height: 8,
                );
              },
            ),
          ),
        if (pagoServiciosState.statusBusqueda ==
                StatusBusqueda.mostrarResultados &&
            pagoServiciosState.empresas.isEmpty)
          SliverToBoxAdapter(
              child: Container(
            padding: const EdgeInsets.only(top: 15.5),
            child: const Center(
              child: Text(
                'No se encontraron coincidencias',
                style: TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.w500,
                  color: AppColors.gray800,
                  height: 32 / 18,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                textAlign: TextAlign.start,
              ),
            ),
          ))
      ],
    );
  }
}

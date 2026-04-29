import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/providers/configurar_cuentas_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ConfigurarCuentasScreen extends ConsumerStatefulWidget {
  const ConfigurarCuentasScreen({super.key});

  @override
  ConfigurarCuentasScreenState createState() => ConfigurarCuentasScreenState();
}

class ConfigurarCuentasScreenState
    extends ConsumerState<ConfigurarCuentasScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(configurarCuentasProvider.notifier).initDatos();
      ref.read(configurarCuentasProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Configurar cuentas',
      child: _ConfigurarView(),
    );
  }
}

class _ConfigurarView extends ConsumerWidget {
  const _ConfigurarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final configurarCuentasState = ref.watch(configurarCuentasProvider);

    return CustomScrollView(
      slivers: [
        SliverPadding(
          padding: const EdgeInsets.only(
            top: 48,
            left: 24,
            right: 24,
            bottom: 48,
          ),
          sliver: SliverList.separated(
            separatorBuilder: (context, index) {
              return const SizedBox(
                height: 16,
              );
            },
            itemCount: configurarCuentasState.cuentasAhorro.length,
            itemBuilder: (context, index) {
              final cuentaAhorro = configurarCuentasState.cuentasAhorro[index];

              return CtCardButton(
                padding: const EdgeInsets.symmetric(
                  horizontal: 16,
                  vertical: 18,
                ),
                onPressed: () {
                  ref
                      .read(configurarCuentasProvider.notifier)
                      .selectCuentaAhorro(cuentaAhorro);
                },
                child: Row(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            cuentaAhorro.alias,
                            style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w500,
                              color: AppColors.gray900,
                              height: 22 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                              overflow: TextOverflow.ellipsis,
                            ),
                          ),
                          const SizedBox(
                            height: 4,
                          ),
                          Text(
                            CtUtils.formatNumeroCuenta(
                              numeroCuenta: cuentaAhorro.identificador,
                              hash: true,
                            ),
                            style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w400,
                              color: AppColors.gray900,
                              height: 22 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                              overflow: TextOverflow.ellipsis,
                            ),
                          ),
                        ],
                      ),
                    ),
                    const SizedBox(
                      width: 20,
                    ),
                    Text(
                      CtUtils.formatCurrency(
                        cuentaAhorro.saldoDisponible,
                        cuentaAhorro.simboloMoneda,
                      ),
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray900,
                        height: 24 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ],
                ),
              );
            },
          ),
        ),
      ],
    );
  }
}

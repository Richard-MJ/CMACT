import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/providers/afiliacion_celular_provider.dart';
import 'package:caja_tacna_app/features/compras_internet/widgets/dialog_desafiliar_interoperabilidad.dart';
import 'package:caja_tacna_app/features/compras_internet/providers/compras_internet_provider.dart';
import 'package:caja_tacna_app/features/compras_internet/widgets/mensaje_alerta.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';

class ConfigurarScreen extends ConsumerStatefulWidget {
  const ConfigurarScreen({super.key});

  @override
  ConfigurarScreenState createState() => ConfigurarScreenState();
}

class ConfigurarScreenState extends ConsumerState<ConfigurarScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() async {
      ref.read(comprasInternetProvider.notifier).initDatos();
      ref.read(comprasInternetProvider.notifier).getCuentasAfiliacion();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Compras por internet',
      child: _ConfigurarView(),
    );
  }
}

class _ConfigurarView extends ConsumerWidget {
  const _ConfigurarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final comprasInternetState = ref.watch(comprasInternetProvider);
    final cuentaAfiliacionSeleccionada = comprasInternetState.cuentaAfiliacion;
    final afiliacion = comprasInternetState.afiliacion;

    return CustomScrollView(
      slivers: [
        SliverToBoxAdapter(
          child: Container(
            padding: const EdgeInsets.only(
              top: 36,
              bottom: 16,
              left: 24,
              right: 24,
            ),
            child: const Text(
              'Solo puedes elegir una cuenta para realizar tus compras por internet:',
              style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w400,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
        ),
        SliverPadding(
          padding: const EdgeInsets.only(
            left: 24,
            right: 24,
          ),
          sliver: SliverList.separated(
            separatorBuilder: (context, index) {
              return const SizedBox(
                height: 16,
              );
            },
            itemCount: comprasInternetState.cuentasAfiliacion.length,
            itemBuilder: (context, index) {
              final cuentaAfiliacion =
                  comprasInternetState.cuentasAfiliacion[index];
              final isSelected = cuentaAfiliacion.numeroCuenta ==
                  cuentaAfiliacionSeleccionada?.numeroCuenta;
              return Container(
                decoration: BoxDecoration(
                  border: Border.all(
                    color: isSelected ? Colors.transparent : AppColors.gray300,
                    width: 1,
                  ),
                  borderRadius: BorderRadius.circular(8),
                  boxShadow: AppColors.shadowSm,
                  color: isSelected ? AppColors.gray100 : AppColors.gray25,
                ),
                child: TextButton(
                  onPressed: () {
                    ref
                        .read(comprasInternetProvider.notifier)
                        .selectCuenta(cuentaAfiliacion);
                  },
                  style: TextButton.styleFrom(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 16,
                      vertical: 18,
                    ),
                    shape: const RoundedRectangleBorder(
                      borderRadius: BorderRadius.all(
                        Radius.circular(8),
                      ),
                    ),
                  ),
                  child: Row(
                    children: [
                      CtSwitch(
                        value: isSelected,
                        onTap: () {},
                      ),
                      const SizedBox(
                        width: 16,
                      ),
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(
                              cuentaAfiliacion.alias,
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w500,
                                color: AppColors.gray900,
                                height: 22 / 14,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                                overflow: TextOverflow.ellipsis,
                              ),
                            ),
                            const SizedBox(
                              height: 4,
                            ),
                            Text(
                              CtUtils.formatNumeroCuenta(
                                numeroCuenta: cuentaAfiliacion.numeroCuenta,
                                hash: true,
                              ),
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w400,
                                color: AppColors.gray900,
                                height: 22 / 14,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                                overflow: TextOverflow.ellipsis,
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
          ),
        ),
        SliverFillRemaining(
          hasScrollBody: false,
          child: Container(
            padding: const EdgeInsets.only(
              bottom: 56,
              left: 24,
              right: 24,
              top: 24,
            ),
            child: Column(
              children: [
                const Spacer(),
                if (comprasInternetState.cuentaAfiliacion == null)
                  const MensajeAlerta(),
                const SizedBox(
                  height: 24,
                ),
                CtButton(
                  text: 'Guardar cambios',
                  disabled: (cuentaAfiliacionSeleccionada?.numeroCuenta ==
                              afiliacion?.numeroCuentaAfiliada &&
                          cuentaAfiliacionSeleccionada != null &&
                          afiliacion != null) ||
                      (afiliacion == null &&
                      cuentaAfiliacionSeleccionada == null),
                  onPressed: () async {
                      await ref.read(afiliacionCelularProvider.notifier).getAfiliacionBilleteraVirtual();
                      final esAfiliadaSimple = ref.read(afiliacionCelularProvider).esAfiliadaSimple;
                      if(esAfiliadaSimple){
                        bool? continuar = await showDialog(
                          context: context,
                          builder: (BuildContext context) {
                            return const DialogDesafiliarInteroperabilidad();
                          },
                        );
                        if (continuar == null || !continuar) return;
                        if (!context.mounted) return;
                      }
                      ref
                          .read(comprasInternetProvider.notifier)
                          .submit(withPush: true);
                  },
                )
              ],
            ),
          ),
        )
      ],
    );
  }
}

class CtSwitch extends StatelessWidget {
  const CtSwitch({
    super.key,
    required this.value,
    required this.onTap,
  });

  final bool value;
  final void Function() onTap;
  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        onTap();
      },
      child: Stack(
        children: [
          SizedBox(
            width: 40,
            height: 16,
            child: Center(
              child: AnimatedContainer(
                duration: const Duration(milliseconds: 300),
                width: 24,
                height: 10,
                decoration: BoxDecoration(
                  color: value ? AppColors.success400 : AppColors.gray100,
                  borderRadius: BorderRadius.circular(16),
                  border: value
                      ? null
                      : Border.all(color: AppColors.gray200, width: 1),
                  boxShadow: AppColors.shadowSm,
                ),
              ),
            ),
          ),
          AnimatedPositioned(
            duration: const Duration(milliseconds: 300),
            curve: Curves.easeInOut,
            left: value ? 24 : 0,
            child: Container(
              width: 16,
              height: 16,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(8),
                color: value ? AppColors.white : AppColors.gray100,
                border: value
                    ? null
                    : Border.all(color: AppColors.gray200, width: 1),
              ),
              child: Center(
                child: value
                    ? Container(
                        width: 2,
                        height: 6,
                        decoration: BoxDecoration(
                          color: AppColors.success400,
                          borderRadius: BorderRadius.circular(1),
                        ),
                      )
                    : Container(
                        width: 5,
                        height: 5,
                        decoration: BoxDecoration(
                          color: Colors.transparent,
                          borderRadius: BorderRadius.circular(2.5),
                          border:
                              Border.all(color: AppColors.gray200, width: 1),
                        ),
                      ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}

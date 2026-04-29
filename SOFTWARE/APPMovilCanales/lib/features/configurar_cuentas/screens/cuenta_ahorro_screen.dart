import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/providers/cancelacion_cuentas_provider.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/providers/configurar_cuentas_provider.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/widgets/dialog_cancelar_cuenta.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/widgets/dialog_editar_alias.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';
import 'package:share_plus/share_plus.dart';

class CuentaAhorroScreen extends ConsumerStatefulWidget {
  const CuentaAhorroScreen({super.key});

  @override
  CuentaAhorroScreenState createState() => CuentaAhorroScreenState();
}

class CuentaAhorroScreenState extends ConsumerState<CuentaAhorroScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Cuenta de ahorros',
      child: _CuentaAhorroView(),
    );
  }
}

class _CuentaAhorroView extends ConsumerWidget {
  const _CuentaAhorroView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final cuentaAhorro = ref.watch(configurarCuentasProvider).cuentaAhorro;

    return CustomScrollView(
      slivers: [
        SliverFillRemaining(
          hasScrollBody: false,
          child: Container(
            padding: const EdgeInsets.only(
              top: 28,
              left: 24,
              right: 24,
              bottom: 69,
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                  children: [
                    Expanded(
                      child: Text(
                        cuentaAhorro?.alias ?? '',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray900,
                          height: 24 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                    ),
                    SizedBox(
                      width: 32,
                      height: 32,
                      child: TextButton(
                        style: TextButton.styleFrom(
                          shape: const CircleBorder(),
                          padding: EdgeInsets.zero,
                        ),
                        onPressed: () {
                          showDialog(
                            context: context,
                            builder: (BuildContext context) {
                              return const DialogEditarAlias();
                            },
                          );
                        },
                        child: SvgPicture.asset(
                          'assets/icons/edit.svg',
                          height: 16,
                          width: 16,
                        ),
                      ),
                    )
                  ],
                ),
                const SizedBox(
                  height: 16,
                ),
                Row(
                  mainAxisSize: MainAxisSize.min,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    Text(
                      CtUtils.formatNumeroCuenta(
                        numeroCuenta: cuentaAhorro?.identificador,
                        hash: true,
                      ),
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 1.5,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    const SizedBox(
                      width: 8,
                    ),
                    Builder(
                      builder: (context) {
                        return GestureDetector(
                          onTap: () async {
                            final box =
                                context.findRenderObject() as RenderBox?;
                            final sharePositionOrigin = box != null
                                ? box.localToGlobal(Offset.zero) & box.size
                                : null;
                            if (cuentaAhorro?.codigoSistema == 'CC') {
                              await Share.share(
                                'Mi número de ${cuentaAhorro?.descripcion} en Caja Tacna es: ${cuentaAhorro?.identificador} y mi número de CCI es: ${cuentaAhorro?.identificadorCci}',
                                sharePositionOrigin: sharePositionOrigin,
                              );
                            } else {
                              await Share.share(
                                'Mi número de ${cuentaAhorro?.descripcion} en Caja Tacna es: ${cuentaAhorro?.identificador}',
                                sharePositionOrigin: sharePositionOrigin,
                              );
                            }
                          },
                          child: SvgPicture.asset(
                            'assets/icons/share.svg',
                            height: 24,
                          ),
                        );
                      },
                    ),
                    const Spacer(),
                    Text(
                      CtUtils.formatCurrency(
                        cuentaAhorro?.saldoDisponible,
                        cuentaAhorro?.simboloMoneda,
                      ),
                      style: const TextStyle(
                        fontSize: 24,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 1.5,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 16,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Saldo contable',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Text(
                      CtUtils.formatCurrency(
                        cuentaAhorro?.saldoContable,
                        cuentaAhorro?.simboloMoneda,
                      ),
                      style: const TextStyle(
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
                  height: 16,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Estado',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Text(
                      CtUtils.capitalize(cuentaAhorro?.descripcionEstado ?? ""),
                      style: const TextStyle(
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
                  height: 16,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'CCI',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Text(
                      cuentaAhorro?.identificadorCci ?? "-",
                      style: const TextStyle(
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
                const Spacer(),
                const Text(
                  'Configura tu cuenta',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray900,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 16,
                ),
                if (cuentaAhorro?.codigoSistema == 'CC')
                  Column(
                    children: [
                      CtCardButton(
                        padding: const EdgeInsets.symmetric(
                          horizontal: 16,
                          vertical: 18,
                        ),
                        onPressed: () {
                          cuentaAhorro.codigoProducto == 'CC082'
                              ? context.push(
                                  '/configurar-cuentas/limite-transacciones-semanal')
                              : context.push(
                                  '/configurar-cuentas/limite-transacciones');
                        },
                        child: Row(
                          children: [
                            Text(
                              cuentaAhorro!.codigoProducto == 'CC082'
                                  ? 'Asigna monto límite semanal'
                                  : 'Asigna límite por transacción',
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w500,
                                color: AppColors.gray900,
                                height: 22 / 14,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                          ],
                        ),
                      ),
                      const SizedBox(
                        height: 16,
                      ),
                      CtCardButton(
                        padding: const EdgeInsets.symmetric(
                          horizontal: 16,
                          vertical: 18,
                        ),
                        onPressed: () {
                          context
                              .push('/configurar-cuentas/limite-operaciones');
                        },
                        child: const Row(
                          children: [
                            Flexible(
                              child: Text(
                                'Asigna número máximo de transacciones diarias',
                                style: TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w500,
                                  color: AppColors.gray900,
                                  height: 22 / 14,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                      const SizedBox(
                        height: 16,
                      ),
                    ],
                  ),
                CtCardButton(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 16,
                    vertical: 18,
                  ),
                  onPressed: () async {
                    if (cuentaAhorro == null) return;
                    final bool? continuar = await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return const DialogCancelarCuenta();
                      },
                    );
                    if (continuar != true) return;
                    ref
                        .read(cancelacionCuentasProvider.notifier)
                        .selectCuentaAhorro(cuentaAhorro);
                  },
                  child: const Row(
                    children: [
                      Text(
                        'Cancelar cuenta',
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
                )
              ],
            ),
          ),
        ),
      ],
    );
  }
}

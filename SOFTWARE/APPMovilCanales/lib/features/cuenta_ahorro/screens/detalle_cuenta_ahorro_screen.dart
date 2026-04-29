import 'dart:math';

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/providers/configurar_cuentas_provider.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/movimiento_cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/tipo_movimiento.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/providers/cuenta_ahorro_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/index.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import 'package:share_plus/share_plus.dart';

class DetalleCuentaAhorroScreen extends ConsumerStatefulWidget {
  const DetalleCuentaAhorroScreen({
    super.key,
    required this.codigoAgencia,
    required this.identificador,
  });

  final String codigoAgencia;
  final String identificador;

  @override
  DetalleCuentaAhorroState createState() => DetalleCuentaAhorroState();
}

class DetalleCuentaAhorroState
    extends ConsumerState<DetalleCuentaAhorroScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(cuentaAhorroProvider.notifier).initVistaDetalle();
      ref
          .read(cuentaAhorroProvider.notifier)
          .getDetalle(widget.codigoAgencia, widget.identificador);
      ref
          .read(cuentaAhorroProvider.notifier)
          .getMovimientos(widget.codigoAgencia, widget.identificador);
      ref
          .read(cuentaAhorroProvider.notifier)
          .getMovimientosCongelados(widget.identificador);
    });
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout4(
      title: 'Mis cuentas',
      child: _DetalleCuentaAhorroView(
        codigoAgencia: widget.codigoAgencia,
        identificador: widget.identificador,
      ),
    );
  }
}

class _DetalleCuentaAhorroView extends ConsumerWidget {
  const _DetalleCuentaAhorroView({
    required this.codigoAgencia,
    required this.identificador,
  });
  final String codigoAgencia;
  final String identificador;
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final cuentaAhorroState = ref.watch(cuentaAhorroProvider);

    return Container(
      padding: const EdgeInsets.only(
        top: 13,
      ),
      child: Column(
        children: [
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 24),
            child: Container(
              padding: const EdgeInsets.symmetric(
                horizontal: 12,
                vertical: 16,
              ),
              decoration: BoxDecoration(
                color: AppColors.primary25,
                borderRadius: BorderRadius.circular(8),
                border: Border.all(color: AppColors.primary400),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      cuentaAhorroState.loadingDetalle
                          ? CtShimmer.rectangular(
                              width: Random().nextInt(60) + 130,
                              height: 16,
                              margin: const EdgeInsets.symmetric(vertical: 4),
                            )
                          : Flexible(
                              child: Text(
                                cuentaAhorroState.cuentaAhorro?.alias ?? '',
                                style: const TextStyle(
                                  fontSize: 16,
                                  fontWeight: FontWeight.w600,
                                  color: AppColors.gray900,
                                  height: 1.5,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                              ),
                            ),
                      if (cuentaAhorroState.cuentaAhorro != null)
                        GestureDetector(
                          onTap: () {
                            ref
                                .read(configurarCuentasProvider.notifier)
                                .selectCuentaAhorro(
                                    cuentaAhorroState.cuentaAhorro!);
                          },
                          child: SvgPicture.asset(
                            'assets/icons/cuenta-ahorro/settings.svg',
                            height: 24,
                          ),
                        ),
                    ],
                  ),
                  const SizedBox(
                    height: 10,
                  ),
                  Row(
                    mainAxisSize: MainAxisSize.min,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      cuentaAhorroState.loadingDetalle
                          ? const CtShimmer.rectangular(
                              width: 140,
                              height: 16,
                              margin: EdgeInsets.symmetric(vertical: 4),
                            )
                          : Text(
                              cuentaAhorroState.cuentaAhorro?.identificador
                                      .substring((cuentaAhorroState.cuentaAhorro
                                                  ?.identificador.length ??
                                              5) -
                                          4) ??
                                  '',
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w600,
                                color: AppColors.gray700,
                                height: 1.5,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
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
                              if (cuentaAhorroState
                                      .cuentaAhorro?.codigoSistema ==
                                  'CC') {
                                await Share.share(
                                  'Mi número de ${cuentaAhorroState.cuentaAhorro?.descripcion} en Caja Tacna es: ${cuentaAhorroState.cuentaAhorro?.identificador} y mi número de CCI es: ${cuentaAhorroState.cuentaAhorro?.identificadorCci}',
                                  sharePositionOrigin: sharePositionOrigin,
                                );
                              } else {
                                await Share.share(
                                  'Mi número de ${cuentaAhorroState.cuentaAhorro?.descripcion} en Caja Tacna es: ${cuentaAhorroState.cuentaAhorro?.identificador}',
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
                      cuentaAhorroState.loadingDetalle
                          ? const CtShimmer.rectangular(
                              width: 105,
                              height: 20,
                              margin: EdgeInsets.symmetric(vertical: 5),
                            )
                          : Text(
                              CtUtils.formatCurrency(
                                cuentaAhorroState.cuentaAhorro?.codigoTipo ==
                                        'DP'
                                    ? (cuentaAhorroState
                                            .cuentaAhorro!.saldoContable +
                                        cuentaAhorroState
                                            .cuentaAhorro!.saldoDisponible)
                                    : cuentaAhorroState
                                        .cuentaAhorro?.saldoDisponible,
                                cuentaAhorroState.cuentaAhorro?.simboloMoneda,
                              ),
                              style: const TextStyle(
                                fontSize: 22,
                                fontWeight: FontWeight.w600,
                                color: AppColors.gray900,
                                height: 1.5,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                    ],
                  ),
                  const SizedBox(
                    height: 8,
                  ),
                  Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: [
                      Expanded(
                        child: cuentaAhorroState.loadingDetalle
                            ? const CtShimmer.rectangular(
                                width: 150,
                                height: 14,
                                margin: EdgeInsets.symmetric(vertical: 4),
                              )
                            : Text(
                                cuentaAhorroState.cuentaAhorro?.descripcion ??
                                    '',
                                style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w400,
                                  color: AppColors.gray600,
                                  height: 22 / 14,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                                textAlign: TextAlign.end,
                              ),
                      ),
                    ],
                  ),
                ],
              ),
            ),
          ),
          const SizedBox(
            height: 15,
          ),
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 24),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                if (cuentaAhorroState.movimientosCongelados.isEmpty) ...[
                  Text(
                    cuentaAhorroState.tipoMovimiento.descripcion(),
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w700,
                      color: AppColors.gray900,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ] else ...[
                  Row(
                    children: [
                      CtButton2(
                        text: TipoMovimiento.movimiento.descripcion(),
                        onPressed: () {
                          ref
                              .read(cuentaAhorroProvider.notifier)
                              .changeMovimientosCongelados(
                                  TipoMovimiento.movimiento);
                        },
                        type: cuentaAhorroState.tipoMovimiento ==
                                TipoMovimiento.movimiento
                            ? ButtonType.solid
                            : ButtonType.outline,
                      ),
                      const SizedBox(
                        width: 8,
                      ),
                      CtButton2(
                        text: TipoMovimiento.congelado.descripcion(),
                        onPressed: () {
                          ref
                              .read(cuentaAhorroProvider.notifier)
                              .changeMovimientosCongelados(
                                  TipoMovimiento.congelado);
                        },
                        type: cuentaAhorroState.tipoMovimiento ==
                                TipoMovimiento.congelado
                            ? ButtonType.solid
                            : ButtonType.outline,
                      ),
                    ],
                  ),
                ],
                SizedBox(
                  width: 36,
                  height: 36,
                  child: TextButton(
                    style: TextButton.styleFrom(
                      shape: const CircleBorder(),
                      padding: EdgeInsets.zero,
                    ),
                    onPressed: () {
                      context.push(
                          '/cuenta-ahorro/movimientos/$codigoAgencia/$identificador');
                    },
                    child: SvgPicture.asset(
                      'assets/icons/search.svg',
                      width: 24,
                      height: 24,
                    ),
                  ),
                ),
              ],
            ),
          ),
          const SizedBox(
            height: 15,
          ),
          Expanded(
            child: CustomScrollView(
              slivers: [
                SliverPadding(
                  padding: const EdgeInsets.only(
                    top: 15,
                    left: 24,
                    right: 24,
                  ),
                  sliver: !cuentaAhorroState.loadingMovimientos
                      ? SliverList.separated(
                          itemCount: cuentaAhorroState.tipoMovimiento ==
                                  TipoMovimiento.movimiento
                              ? cuentaAhorroState.movimientos.length
                              : cuentaAhorroState.movimientosCongelados.length,
                          itemBuilder: (context, index) {
                            final movimiento =
                                cuentaAhorroState.tipoMovimiento ==
                                        TipoMovimiento.movimiento
                                    ? cuentaAhorroState.movimientos[index]
                                    : cuentaAhorroState
                                        .movimientosCongelados[index];
                            return _Movimiento(
                              movimiento: movimiento,
                            );
                          },
                          separatorBuilder: (context, index) {
                            return const SizedBox(
                              height: 12,
                            );
                          },
                        )
                      : SliverList.separated(
                          itemCount: 4,
                          itemBuilder: (context, index) {
                            return const CtMovimientoSkeleton();
                          },
                          separatorBuilder: (context, index) {
                            return const SizedBox(
                              height: 12,
                            );
                          },
                        ),
                ),
                if (!cuentaAhorroState.loadingMovimientos &&
                    cuentaAhorroState.tipoMovimiento ==
                        TipoMovimiento.movimiento)
                  SliverToBoxAdapter(
                    child: Container(
                      padding: const EdgeInsets.only(
                        left: 24,
                        right: 24,
                        bottom: 38,
                      ),
                      child: Column(
                        children: [
                          const SizedBox(
                            height: 20,
                          ),
                          Center(
                            child: CtButton(
                              text: 'Ver más movimientos',
                              type: ButtonType.text,
                              onPressed: () {
                                context.push(
                                    '/cuenta-ahorro/movimientos/$codigoAgencia/$identificador');
                              },
                            ),
                          ),
                        ],
                      ),
                    ),
                  )
              ],
            ),
          ),
        ],
      ),
    );
  }
}

class _Movimiento extends StatelessWidget {
  const _Movimiento({required this.movimiento});

  final MovimientoCuentaAhorro movimiento;

  @override
  Widget build(BuildContext context) {
    final signoMovimiento = movimiento.signoMovimiento == '-' ? '-' : '';
    return Container(
      padding: const EdgeInsets.only(bottom: 10),
      decoration: const BoxDecoration(
        border: Border(
          bottom: BorderSide(
            color: AppColors.border1,
            width: 1,
          ),
        ),
      ),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  movimiento.descripcion,
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray900,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Text(
                  formatearFecha(movimiento.fecha),
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ],
            ),
          ),
          Text(
            '$signoMovimiento ${CtUtils.formatCurrency(
              movimiento.monto,
              movimiento.simboloMoneda,
            )}',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: movimiento.signoMovimiento == '+'
                  ? AppColors.gray900
                  : AppColors.primary700,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ],
      ),
    );
  }
}

String formatearFecha(DateTime fecha) {
  final formato = DateFormat('d/MM/y  h:mm:s a', 'es');
  return formato.format(fecha);
}

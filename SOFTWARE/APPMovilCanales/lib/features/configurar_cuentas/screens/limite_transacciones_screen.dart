import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/tipo_limite.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/providers/configurar_cuentas_provider.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/providers/limites_provider.dart.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/widgets/dialog_editar_alias.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/widgets/input_limite_transacciones.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:share_plus/share_plus.dart';

class LimiteTransaccionesScreen extends ConsumerStatefulWidget {
  const LimiteTransaccionesScreen({super.key});

  @override
  LimiteTransaccionesScreenState createState() =>
      LimiteTransaccionesScreenState();
}

class LimiteTransaccionesScreenState
    extends ConsumerState<LimiteTransaccionesScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(limitesProvider.notifier).initDatos();
      ref.read(limitesProvider.notifier).obtenerLimites();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Asigna límite por transacción',
      child: _LimiteTransaccionesView(),
    );
  }
}

class _LimiteTransaccionesView extends ConsumerWidget {
  const _LimiteTransaccionesView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final cuentaAhorro = ref.watch(configurarCuentasProvider).cuentaAhorro;
    final limiteState = ref.watch(limitesProvider);
    final bool disabledButton =
        CtUtils.formatStringWithTwoDecimals(limiteState.limiteTransacciones) ==
                CtUtils.formatStringWithTwoDecimals(limiteState
                        .limites?.montoMaximoOperacionLimiteCliente
                        .toString() ??
                    '') ||
            limiteState.limiteTransacciones.isEmpty;
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
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                Row(
                  children: [
                    Text(
                      cuentaAhorro?.alias ?? '',
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray900,
                        height: 24 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
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
                  height: 78,
                ),
                const Text(
                  'Asigna el límite por transacción',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray900,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 8,
                ),
                const Text(
                  'Digita o elige el monto que desees asignar al límite por transacción:',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 21,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    InputLimiteTransaciones(
                      onChange: (value) {
                        ref
                            .read(limitesProvider.notifier)
                            .changeLimiteTransaccionesInput(value);
                      },
                      value: limiteState.limiteTransacciones,
                      simboloMoneda: cuentaAhorro?.simboloMoneda,
                    ),
                  ],
                ),
                const SizedBox(
                  height: 40,
                ),
                SliderTheme(
                  data: SliderThemeData(
                    trackHeight: 8,
                    rangeTrackShape: const RectangularRangeSliderTrackShape(),
                    minThumbSeparation: 0,
                    thumbShape: const RoundSliderThumbShape(
                      enabledThumbRadius: 12,
                      pressedElevation: 0.0,
                      elevation: 0.5,
                    ),
                    trackShape: SliderCustomTrackShape(),
                    overlayShape: const RoundSliderOverlayShape(
                      overlayRadius: 0,
                    ),
                  ),
                  child: Slider(
                    value: double.tryParse(limiteState.limiteTransacciones) ??
                        (limiteState.limites?.montoMinimoOperacionDefecto ?? 0),
                    onChanged: (value) {
                      ref
                          .read(limitesProvider.notifier)
                          .changeLimiteTransacciones(value);
                    },
                    activeColor: AppColors.primary700,
                    min: limiteState.limites?.montoMinimoOperacionDefecto ?? 0,
                    max: limiteState.limites?.montoMaximoOperacionDefecto ?? 0,
                    thumbColor: AppColors.primary200,
                    inactiveColor: AppColors.gray200,
                  ),
                ),
                const SizedBox(
                  height: 8,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      CtUtils.formatStringWithTwoDecimals(limiteState
                              .limites?.montoMinimoOperacionDefecto
                              .toString() ??
                          ''),
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray700,
                        height: 22 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Text(
                      CtUtils.formatStringWithTwoDecimals(limiteState
                              .limites?.montoMaximoOperacionDefecto
                              .toString() ??
                          ''),
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray700,
                        height: 22 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ],
                ),
                const Spacer(),
                CtButton(
                  text: 'Confirmar',
                  onPressed: () {
                    ref.read(limitesProvider.notifier).actualizar(
                        withPush: true,
                        codigoTipoLimite: TipoLimite.montoPorTransaccion);
                  },
                  disabled: disabledButton,
                )
              ],
            ),
          ),
        ),
      ],
    );
  }
}

class SliderCustomTrackShape extends RoundedRectSliderTrackShape {
  @override
  Rect getPreferredRect({
    required RenderBox parentBox,
    Offset offset = Offset.zero,
    required SliderThemeData sliderTheme,
    bool isEnabled = false,
    bool isDiscrete = false,
  }) {
    final double? trackHeight = sliderTheme.trackHeight;
    final double trackLeft = offset.dx;
    final double trackTop =
        offset.dy + (parentBox.size.height - trackHeight!) / 2;
    final double trackWidth = parentBox.size.width;
    return Rect.fromLTWH(trackLeft, trackTop, trackWidth, trackHeight);
  }
}

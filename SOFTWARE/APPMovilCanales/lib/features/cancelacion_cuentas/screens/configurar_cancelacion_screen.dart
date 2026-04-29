import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/data/tipos_transferencia.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/tipo_transferencia.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/providers/cancelacion_cuentas_provider.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/widgets/alert_card.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/widgets/form_trans_interbancaria.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/widgets/form_trans_propia.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/widgets/form_trans_tercero.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/widgets/select_tipo_transferencia.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:share_plus/share_plus.dart';

class ConfigurarCancelacionScreen extends ConsumerStatefulWidget {
  const ConfigurarCancelacionScreen({super.key});

  @override
  ConfigurarCancelacionScreenState createState() =>
      ConfigurarCancelacionScreenState();
}

class ConfigurarCancelacionScreenState
    extends ConsumerState<ConfigurarCancelacionScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(cancelacionCuentasProvider.notifier).initDataCancelacion();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Cancelar cuenta',
      child: _CuentaAhorroView(),
    );
  }
}

class _CuentaAhorroView extends ConsumerWidget {
  const _CuentaAhorroView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final cancelacionState = ref.watch(cancelacionCuentasProvider);
    final cuentaAhorro = cancelacionState.cuentaAhorro;

    final disabledButton = (cancelacionState.tipoTransferencia == null) ||
        (cancelacionState.tipoTransferencia?.id == 1 &&
            cancelacionState.cuentaDestinoPropia == null) ||
        (cancelacionState.tipoTransferencia?.id == 2 &&
            cancelacionState.numeroCuentaTercero.isNotValid) ||
        (cancelacionState.tipoTransferencia?.id == 3 &&
            (cancelacionState.cuentaDestinoCci.isNotValid ||
                cancelacionState.nombreBeneficiario.isNotValid ||
                cancelacionState.tipoDocumento == null ||
                cancelacionState.numeroDocumento.isNotValid));

    List<TipoTransferencia> tiposTransferenciasFiltradas = tiposTransferencias;
    if (cuentaAhorro?.codigoTipo == 'DP') {
      tiposTransferenciasFiltradas = [tiposTransferenciasFiltradas[0]];
    }

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
                  height: 36,
                ),
                if (cancelacionState.cuentaAhorro != null &&
                    cancelacionState.cuentaAhorro!.saldoDisponible > 0)
                  const AlertCard(),
                const SizedBox(
                  height: 24,
                ),
                const Text(
                  'Tipo de transferencia',
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
                SelectTipoTransferencia(
                  value: cancelacionState.tipoTransferencia,
                  onChanged: (tipoTransferencia) {
                    ref
                        .read(cancelacionCuentasProvider.notifier)
                        .changeTipoTransferencia(tipoTransferencia);
                  },
                  tiposTransferencia: tiposTransferenciasFiltradas,
                ),
                const SizedBox(
                  height: 20,
                ),
                if (cancelacionState.tipoTransferencia?.id == 1)
                  const FormTransPropia(),
                if (cancelacionState.tipoTransferencia?.id == 2)
                  const FormTransTercero(),
                if (cancelacionState.tipoTransferencia?.id == 3)
                  const FormTransInterbancaria(),
                const Spacer(),
                const SizedBox(
                  height: 89,
                ),
                CtButton(
                  text: 'Continuar',
                  onPressed: () {
                    ref
                        .read(cancelacionCuentasProvider.notifier)
                        .cancelarCuenta(withPush: true);
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

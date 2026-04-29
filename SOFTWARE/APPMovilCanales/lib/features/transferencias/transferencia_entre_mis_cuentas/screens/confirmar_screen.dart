import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_agregar_operaciones_frecuentes.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/providers/transferencia_entre_mis_cuentas_provider.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_alias_operacion.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:animate_do/animate_do.dart';

class ConfirmarTransferenciaEntreMisCuentasScreen
    extends ConsumerStatefulWidget {
  const ConfirmarTransferenciaEntreMisCuentasScreen({super.key});

  @override
  ConfirmarTransferenciaEntreMisCuentasScreenState createState() =>
      ConfirmarTransferenciaEntreMisCuentasScreenState();
}

class ConfirmarTransferenciaEntreMisCuentasScreenState
    extends ConsumerState<ConfirmarTransferenciaEntreMisCuentasScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Confirma la operación',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _ConfirmarTransferenciaEntreMisCuentasView(),
          )
        ],
      ),
    );
  }
}

class _ConfirmarTransferenciaEntreMisCuentasView extends ConsumerWidget {
  const _ConfirmarTransferenciaEntreMisCuentasView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final transferenciaState = ref.watch(transferenciaEntreMisCuentasProvider);

    return Container(
      padding: const EdgeInsets.only(top: 36, bottom: 56, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Operación',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              SizedBox(
                width: 165,
                child: Text(
                  transferenciaState.transferirResponse?.descripcionOperacion ??
                      '',
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 19 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  textAlign: TextAlign.end,
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 25,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Cuenta de origen',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              const SizedBox(
                width: 30,
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      '${transferenciaState.cuentaOrigen?.nombreTipoProducto}',
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    Text(
                      CtUtils.formatNumeroCuenta(
                        numeroCuenta:
                            transferenciaState.cuentaOrigen?.numeroProducto,
                      ),
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray500,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 25,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Cuenta de destino',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              const SizedBox(
                width: 30,
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      '${transferenciaState.cuentaDestino?.nombreTipoProducto}',
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    Text(
                      CtUtils.formatNumeroCuenta(
                        numeroCuenta:
                            transferenciaState.cuentaDestino?.numeroProducto,
                      ),
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray500,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 25,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Monto',
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
                  double.parse(transferenciaState.monto.value),
                  transferenciaState.cuentaDestino?.simboloMonedaProducto,
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
            height: 25,
          ),
          if (transferenciaState.transferirResponse?.montoReal.tipoCambio !=
              1) ...[
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Tipo de cambio',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 19 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Text(
                  transferenciaState.transferirResponse?.montoReal.tipoCambio
                          .toString() ??
                      '',
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
              height: 25,
            ),
          ],
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'ITF',
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
                  transferenciaState.transferirResponse?.montoItf,
                  transferenciaState.cuentaOrigen?.simboloMonedaProducto,
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
            height: 35,
          ),
          Center(
            child: CtAgregarOperacionesFrecuentes(
              value: transferenciaState.operacionFrecuente,
              onChanged: () {
                ref
                    .read(transferenciaEntreMisCuentasProvider.notifier)
                    .toggleOperacionFrecuente();
              },
            ),
          ),
          FadeIn(
            animate: transferenciaState.operacionFrecuente,
            duration: const Duration(milliseconds: 150),
            child: Container(
              padding: const EdgeInsets.only(top: 16),
              child: InputAliasOperacion(
                alias: transferenciaState.nombreOperacionFrecuente,
                onChanged: (operacionFrecuente) {
                  ref
                      .read(transferenciaEntreMisCuentasProvider.notifier)
                      .changeNombreOperacionFrecuente(operacionFrecuente);
                },
              ),
            ),
          ),
          const Spacer(),
          const SizedBox(
            height: 40,
          ),
          CtButton(
            text: 'Confirmar',
            onPressed: () {
              ref
                  .read(transferenciaEntreMisCuentasProvider.notifier)
                  .confirmar();
            },
          )
        ],
      ),
    );
  }
}

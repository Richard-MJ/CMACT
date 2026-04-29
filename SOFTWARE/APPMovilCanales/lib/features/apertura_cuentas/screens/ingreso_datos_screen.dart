import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/providers/apertura_cuentas_provider.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/widgets/input_dias_dpf.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/widgets/input_monto.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/widgets/select_tipo_cuenta.dart';
import 'package:caja_tacna_app/features/emision_giros/widgets/select_agencia.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_cuenta.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class IngresoDatosScreen extends ConsumerStatefulWidget {
  const IngresoDatosScreen({super.key});

  @override
  IngresoDatosScreenState createState() => IngresoDatosScreenState();
}

class IngresoDatosScreenState extends ConsumerState<IngresoDatosScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(aperturaCuentasProvider.notifier).initDatos();
      ref.read(aperturaCuentasProvider.notifier).getDatosIniciales();
      ref.read(aperturaCuentasProvider.notifier).initDatos();
    });
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Abrir nueva cuenta',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _IngresoDatosView(),
          )
        ],
      ),
    );
  }
}

class _IngresoDatosView extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final aperturaState = ref.watch(aperturaCuentasProvider);
    final aperturaCuentaState = ref.watch(aperturaCuentasProvider);

    final disabledButton = aperturaState.cuentaOrigen == null ||
        aperturaCuentaState.tipoCuenta == null ||
        aperturaCuentaState.monto.value == '' ||
        (aperturaCuentaState.tipoCuenta?.codigoSistema == 'DP' &&
            (aperturaCuentaState.diasDpf.value == '' ||
                aperturaCuentaState.calculoDpfResponse == null));

    return Container(
      padding: const EdgeInsets.only(top: 28, left: 24, right: 23, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Text(
            'Tipo de cuenta a crear',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          SelectTipoCuenta(
            value: aperturaCuentaState.tipoCuenta,
            onChanged: (tipoCuenta) {
              ref
                  .read(aperturaCuentasProvider.notifier)
                  .changeTipoCuenta(tipoCuenta);
            },
            tiposCuenta: aperturaCuentaState.tiposCuenta,
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Cuenta de origen',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          CtSelectCuenta(
            cuentas: aperturaState.cuentasOrigen,
            onChange: (producto) {
              ref
                  .read(aperturaCuentasProvider.notifier)
                  .changeCuentaOrigen(producto);
            },
            value: aperturaState.cuentaOrigen,
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Agencia',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          SelectAgencia(
            value: aperturaState.agencia,
            onChanged: (agencia) {
              ref.read(aperturaCuentasProvider.notifier).changeAgencia(agencia);
            },
            agencias: aperturaState.agencias,
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Monto a transferir',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          InputMontoApertura(
            montoGiro: aperturaState.monto,
            onChangeMontoGiro: (value) {
              ref.read(aperturaCuentasProvider.notifier).changeMonto(value);
            },
            simboloMoneda: aperturaState.cuentaOrigen?.simboloMonedaProducto,
          ),
          const SizedBox(
            height: 36,
          ),
          if (aperturaCuentaState.tipoCuenta?.codigoSistema == 'DP') ...[
            Row(
              children: [
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Text(
                        'Plazo (en días)',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray900,
                          height: 24 / 16,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      const SizedBox(
                        height: 16,
                      ),
                      InputDiasDpf(
                        diasDpf: aperturaState.diasDpf,
                        onChanged: (value) {
                          ref
                              .read(aperturaCuentasProvider.notifier)
                              .changeDiasDpf(value);
                        },
                      )
                    ],
                  ),
                ),
                const SizedBox(
                  width: 24,
                ),
                Column(
                  children: [
                    const Text(
                      'Tasa de Interés',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray900,
                        height: 24 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    const SizedBox(
                      height: 16,
                    ),
                    Container(
                      width: 124,
                      height: 44,
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(8),
                        border: Border.all(
                          color: AppColors.gray200,
                          width: 1,
                        ),
                        color: AppColors.gray100,
                        boxShadow: AppColors.shadowXs,
                      ),
                      child: Center(
                        child: aperturaCuentaState.calculoDpfResponse != null
                            ? Text(
                                '${aperturaCuentaState.calculoDpfResponse?.datos.tasaEfectivaAnual} %',
                                style: const TextStyle(
                                  fontSize: 18,
                                  fontWeight: FontWeight.w500,
                                  color: AppColors.gray900,
                                  height: 28 / 18,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                              )
                            : null,
                      ),
                    )
                  ],
                ),
              ],
            ),
            const SizedBox(
              height: 36,
            ),
            if (aperturaCuentaState.calculoDpfResponse != null)
              Row(
                children: [
                  const Text(
                    'Interés a ganar (*)',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w500,
                      color: AppColors.gray900,
                      height: 24 / 16,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  Expanded(
                    child: Text(
                      CtUtils.formatCurrency(
                        double.tryParse(aperturaCuentaState
                                .calculoDpfResponse?.datos.totalIntereses ??
                            '0'),
                        aperturaCuentaState
                            .calculoDpfResponse?.datos.simboloMoneda,
                      ),
                      style: const TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray900,
                        height: 28 / 18,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                  ),
                ],
              ),
            const SizedBox(
              height: 30,
            ),
          ],
          const Spacer(),
          if (aperturaCuentaState.tipoCuenta?.codigoSistema == 'DP') ...[
            const Text(
              '(*) En caso de cumplir con las condiciones pactadas y de no efectuar retiro de capital o intereses antes de la fecha de vencimiento.',
              style: TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w400,
                color: AppColors.gray900,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 24,
            ),
          ],
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref
                  .read(aperturaCuentasProvider.notifier)
                  .abrirCuenta(withPush: true);
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}

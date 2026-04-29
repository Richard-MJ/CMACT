import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/providers/cuenta_ahorro_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_message.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_month_year_picker.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class MovimientosCuentaAhorroScreen extends ConsumerStatefulWidget {
  const MovimientosCuentaAhorroScreen({
    super.key,
    required this.codigoAgencia,
    required this.identificador,
  });

  final String codigoAgencia;
  final String identificador;

  @override
  MovimientosCuentaAhorroState createState() => MovimientosCuentaAhorroState();
}

class MovimientosCuentaAhorroState
    extends ConsumerState<MovimientosCuentaAhorroScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(cuentaAhorroProvider.notifier).initVistaMovimientos();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Volver',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _MovimientosCuentaAhorroView(),
          )
        ],
      ),
    );
  }
}

class _MovimientosCuentaAhorroView extends ConsumerWidget {
  const _MovimientosCuentaAhorroView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final home = ref.watch(homeProvider);
    final cuentaAhorroState = ref.watch(cuentaAhorroProvider);

    return Container(
      padding: const EdgeInsets.only(top: 35, bottom: 79, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Movimientos',
            style: TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.w800,
              color: AppColors.gray900,
              height: 28 / 18,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 13,
          ),
          const Text(
            'Selecciona un rango de fechas para visualizar los movimientos',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 24,
          ),
          Row(
            children: [
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Desde',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray700,
                        height: 22 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    CtMonthYearPicker(
                      value: cuentaAhorroState.fechaInicio,
                      onChange: (month, year) {
                        ref
                            .read(cuentaAhorroProvider.notifier)
                            .setFechaInicio(month, year);
                      },
                    )
                  ],
                ),
              ),
              const SizedBox(
                width: 24,
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Hasta',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray700,
                        height: 22 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    CtMonthYearPicker(
                      value: cuentaAhorroState.fechaFin,
                      onChange: (month, year) {
                        ref
                            .read(cuentaAhorroProvider.notifier)
                            .setFechaFin(month, year);
                      },
                    )
                  ],
                ),
              ),
            ],
          ),
          const Spacer(),
          const SizedBox(
            height: 24,
          ),
          CtMessage(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Enviaremos la operación al correo',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                Text(
                  CtUtils.hashearCorreo(home.datosCliente?.correoElectronico),
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ],
            ),
          ),
          const SizedBox(
            height: 18,
          ),
          CtButton(
            text: 'Enviar estado de cuenta',
            disabled: cuentaAhorroState.fechaInicio == null ||
                cuentaAhorroState.fechaFin == null,
            onPressed: () {
              ref
                  .read(cuentaAhorroProvider.notifier)
                  .enviarEstadoCuentaAhorro();
            },
          ),
        ],
      ),
    );
  }
}

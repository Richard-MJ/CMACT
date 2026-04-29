import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/providers/transferencia_celular_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button_icon.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_notificacion_operacion.dart';
import 'package:caja_tacna_app/features/shared/widgets/pop_not_scope.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_compartir.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_3.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:screenshot/screenshot.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';

class TransferenciaExitosaScreen extends StatelessWidget {
  const TransferenciaExitosaScreen({super.key, required this.esAfiliacion});

  final bool esAfiliacion;

  @override
  Widget build(BuildContext context) {
    return PopNotScope(
      child: CtLayout3(
        child: _TransferenciaExitosaView(
          esAfiliacion: esAfiliacion,
        ),
      ),
    );
  }
}

class _TransferenciaExitosaView extends ConsumerStatefulWidget {
  const _TransferenciaExitosaView({required this.esAfiliacion});
  final bool esAfiliacion;

  @override
  TransferenciaExitosaViewState createState() => TransferenciaExitosaViewState();
}

class TransferenciaExitosaViewState extends ConsumerState<_TransferenciaExitosaView> {
  ScreenshotController screenshotController = ScreenshotController();

  @override
  Widget build(BuildContext context) {
    final transferirState = ref.watch(transferenciaCelularProvider);
    final homeState = ref.watch(homeProvider);

    return Column(
      children: [
        Screenshot(
          controller: screenshotController,
          child: Container(
          padding: const EdgeInsets.only(top: 24, left: 24, right: 24, bottom: 36),
            color: AppColors.white,
            child: Column(
              children: [
                Image.asset(
                  'assets/images/logo_rojo.png',
                  width: 160,
                ),
                const SizedBox(
                  height: 14,
                ),
                const Text(
                    'Transferencia exitosa',
                  style: TextStyle(
                    fontSize: 18,
                    fontWeight: FontWeight.w800,
                    color: AppColors.gray900,
                    height: 28 / 18,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 24,
                ),
                const Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Operación',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                      child: Text(
                        'Transferir dinero a \ncontacto',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                        textAlign: TextAlign.end,
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 24,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Fecha de operación',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.end,
                      children: [
                        Text(
                          CtUtils.formatDate(DateTime.now()),
                          style: const TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray900,
                            height: 19 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                        Text(
                          CtUtils.formatTime(transferirState.datosOperacionResponse?.fechaOperacion),
                          style: const TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray900,
                            height: 19 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                      ],
                    )
                  ],
                ),
                const SizedBox(
                  height: 24,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Contacto',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.end,
                        children: [
                          Text(
                            transferirState.detalleTransferencia!.nombreReceptor,
                            style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w400,
                              color: AppColors.gray900,
                              height: 19 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                            textAlign: TextAlign.end,
                          ),
                          Text(
                            transferirState.contactoSeleccionada!.numeroCelular,
                            style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w400,
                              color: AppColors.gray500,
                              height: 19 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                            textAlign: TextAlign.end,
                          ),
                        ],
                      ),
                    )
                  ],
                ),
                const SizedBox(
                  height: 24,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Cuenta origen',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.end,
                        children: [
                          Text(
                            transferirState.datosClienteOrigenResponse!.cuentaEfectivo.nombreProducto,
                            style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w400,
                              color: AppColors.gray900,
                              height: 19 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                            textAlign: TextAlign.end,
                          ),
                          Text(
                            CtUtils.formatNumeroCuenta(
                              numeroCuenta: transferirState.datosClienteOrigenResponse!.cuentaEfectivo.numeroCuenta,
                              hash: true,
                            ),
                            style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w400,
                              color: AppColors.gray500,
                              height: 19 / 14,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                            textAlign: TextAlign.end,
                          ),
                        ],
                      ),
                    )
                  ],
                ),
                const SizedBox(
                  height: 24,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Billetera destino',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                      child: Text(
                        transferirState.entidadFinancieraSeleccionada?.nombreEntidad ?? "",
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                        textAlign: TextAlign.end,
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 24,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Monto a enviar',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                      child: Text(
                        CtUtils.formatCurrency(
                          transferirState.montosTotales!.controlMonto.monto,
                          transferirState.datosAfiliacion?.simboloMoneda,
                        ),
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                        textAlign: TextAlign.end,
                      ),
                    ),
                  ],
                ),
                if(transferirState.montosTotales!.controlMonto.montoComisionEntidad! > 0)
                const SizedBox(
                  height: 24,
                ),
                if(transferirState.montosTotales!.controlMonto.montoComisionEntidad! > 0)
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Comisión',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                      child: Text(
                        CtUtils.formatCurrency(
                          transferirState.montosTotales!.controlMonto.montoComisionEntidad,
                          transferirState.datosAfiliacion?.simboloMoneda,
                        ),
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                        textAlign: TextAlign.end,
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 24,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'ITF',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                    child: Text(
                        CtUtils.formatCurrency(
                          transferirState.montosTotales!.controlMonto.itf,
                          transferirState.datosAfiliacion?.simboloMoneda,
                        ),
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                        textAlign: TextAlign.end,
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 24,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Nro. de operación',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w600,
                        color: AppColors.gray900,
                        height: 19 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Expanded(
                      child: Text(
                        transferirState.datosOperacionResponse!.numeroOperacion.toString(),
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 19 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
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
          height: 10,
        ),
        const Spacer(),
        Container(
          padding: const EdgeInsets.only(
            bottom: 36,
            left: 24,
            right: 24,
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              CtCompartir(screenshotController: screenshotController),
              const SizedBox(
                height: 12,
              ),
               CtNotificacionOperacion(
                correoElectronico: homeState.datosCliente?.correoElectronico,
              ),
              const SizedBox(
                height: 22,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  CtButtonIcon(
                    text: 'Inicio',
                    icon: Icons.home,
                    onPressed: () {
                      context.go("/home");
                    },
                    type: ButtonType.outline,
                  ),
                  CtButtonIcon(
                    text: 'Enviar',
                    icon: Icons.send_outlined,
                    onPressed: () {
                      context.go('/billetera-virtual/transferencia-celular/contactos');
                    },
                    type: ButtonType.solid,
                  ),
                ],
              ),
            ],
          ),
        ),
      ],
    );
  }
}

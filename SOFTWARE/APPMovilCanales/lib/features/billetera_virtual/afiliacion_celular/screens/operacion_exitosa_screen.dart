import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/providers/afiliacion_celular_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button_icon.dart';
import 'package:caja_tacna_app/features/shared/widgets/pop_home_scope.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_compartir.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_3.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_message.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:qr_flutter/qr_flutter.dart';
import 'package:screenshot/screenshot.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';

class OperacionExitosaScreen extends StatelessWidget {
  const OperacionExitosaScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return PopHomeScope(
      child: CtLayout3(
        child: _OperacionExitosaView(),
      ),
    );
  }
}

class _OperacionExitosaView extends ConsumerStatefulWidget {

  @override
  OperacionExitosaViewState createState() => OperacionExitosaViewState();
}

class OperacionExitosaViewState extends ConsumerState<_OperacionExitosaView> {
  ScreenshotController screenshotController = ScreenshotController();

  @override
  Widget build(BuildContext context) {
    final afiliacionCelularState = ref.watch(afiliacionCelularProvider);
    final homeState = ref.watch(homeProvider);

    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.only(top: 24, left: 24, right: 24, bottom: 24),
        child: 
          Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: <Widget>[
              Expanded(
                child: SingleChildScrollView(
                  child: Column(
                    children: [
                      Screenshot(
                        controller: screenshotController,
                        child: Container(
                          padding: const EdgeInsets.only(
                            bottom: 5,
                            left: 5,
                            right: 5,
                          ),
                          color: AppColors.white,
                          child: Center(
                            child: Column(
                                children: [
                                  Image.asset(
                                    'assets/images/logo_rojo.png',
                                    width: 160,
                                  ),
                                  const SizedBox(
                                    height: 14,
                                  ),
                                  Text(
                                    afiliacionCelularState.esAfiliada
                                        ? 'Afiliación exitosa'
                                        : 'Desafiliación exitosa',
                                    style: const TextStyle(
                                      fontSize: 18,
                                      fontWeight: FontWeight.w800,
                                      color: AppColors.gray900,
                                      height: 28 / 18,
                                      leadingDistribution: TextLeadingDistribution.even,
                                    ),
                                  ),
                                  if(afiliacionCelularState.esAfiliada)
                                  const SizedBox(
                                    height: 12,
                                  ),
                                  if(afiliacionCelularState.esAfiliada)
                                  Center(
                                    child: QrImageView(
                                      data: afiliacionCelularState.confirmacionResponse?.cadenaHash ?? "",
                                      version: QrVersions.auto,
                                      size: 170.0,
                                    ),
                                  ),
                                  const SizedBox(
                                    height: 24,
                                  ),
                                  Row(
                                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                    crossAxisAlignment: CrossAxisAlignment.start,
                                    children: [
                                      const Text(
                                        'Operación',
                                        style: TextStyle(
                                          fontSize: 14,
                                          fontWeight: FontWeight.w800,
                                          color: AppColors.gray900,
                                          height: 19 / 14,
                                          leadingDistribution: TextLeadingDistribution.even,
                                        ),
                                      ),
                                      Expanded(
                                        child: Text(
                                          afiliacionCelularState.esAfiliada 
                                            ? 'Afiliación a\nBilletera Virtual'
                                            : 'Desafiliación a\nBilletera Virtual',
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
                                        'Fecha de operación',
                                        style: TextStyle(
                                          fontSize: 14,
                                          fontWeight: FontWeight.w800,
                                          color: AppColors.gray900,
                                          height: 19 / 14,
                                          leadingDistribution: TextLeadingDistribution.even,
                                        ),
                                      ),
                                      Column(
                                        crossAxisAlignment: CrossAxisAlignment.end,
                                        children: [
                                          Text(
                                            CtUtils.formatDate(afiliacionCelularState.confirmacionResponse?.fechaOperacion),
                                            style: const TextStyle(
                                              fontSize: 14,
                                              fontWeight: FontWeight.w400,
                                              color: AppColors.gray900,
                                              height: 19 / 14,
                                              leadingDistribution: TextLeadingDistribution.even,
                                            ),
                                          ),
                                          Text(
                                            CtUtils.formatTime(afiliacionCelularState.confirmacionResponse?.fechaOperacion),
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
                                        'Nro de Celular',
                                        style: TextStyle(
                                          fontSize: 14,
                                          fontWeight: FontWeight.w800,
                                          color: AppColors.gray900,
                                          height: 19 / 14,
                                          leadingDistribution: TextLeadingDistribution.even,
                                        ),
                                      ),
                                      Expanded(
                                        child: Text(
                                          afiliacionCelularState.numeroCelular.value,
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
                                        'Cuenta',
                                        style: TextStyle(
                                          fontSize: 14,
                                          fontWeight: FontWeight.w800,
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
                                              afiliacionCelularState.datosAfiliacion?.nombreProducto ?? '',
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
                                                numeroCuenta: afiliacionCelularState.datosAfiliacion?.numeroCuentaAfiliada,
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
                                ],
                            ),
                          ),
                        ),
                      ),
                    ],
                  )
                )
              ),
              const SizedBox(
                height: 6,
              ),
              CtCompartir(screenshotController: screenshotController),
              Container(
                padding: const EdgeInsets.only(
                  top: 12,
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.stretch,
                  children: [
                    CtMessage(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            afiliacionCelularState.esAfiliada
                              ? 'Constancia de afiliación enviada al correo'
                              : 'Constancia de desafiliación enviada al correo',
                            style: const TextStyle(
                              fontSize: 12.5,
                              fontWeight: FontWeight.w400,
                              color: AppColors.gray900,
                              height: 22 / 12.5,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                          Text(
                            CtUtils.hashearCorreo(
                                homeState.datosCliente?.correoElectronico),
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
                      height: 16,
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
                        if(afiliacionCelularState.esAfiliada)
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
            ]
          ),
      ),
    );
  }
}

import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/providers/afiliacion_celular_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_compartir_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_descargar.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:qr_flutter/qr_flutter.dart';
import 'package:screenshot/screenshot.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

class CompartirQrScreen extends StatelessWidget {
  const CompartirQrScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return _CompartirQrView();
  }
}

class _CompartirQrView extends ConsumerStatefulWidget {

  @override
  CompartirQrViewState createState() => CompartirQrViewState();
}

class CompartirQrViewState extends ConsumerState<_CompartirQrView> {
  ScreenshotController screenshotController = ScreenshotController();

  @override
  Widget build(BuildContext context) {
    final afiliacionCelularState = ref.watch(afiliacionCelularProvider);

    return Scaffold(
      appBar: AppBar(
        backgroundColor: AppColors.white,
        scrolledUnderElevation: 0,
        automaticallyImplyLeading: false,
        toolbarHeight: 64,
        flexibleSpace: SafeArea(
          child: Container(
            height: 64,
            padding: const EdgeInsets.symmetric(horizontal: 24),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                Container(
                  width: 32,
                  height: 32,
                  decoration: const BoxDecoration(
                    shape: BoxShape.circle,
                    color: AppColors.primary200,
                  ),
                  child: TextButton(
                    style: TextButton.styleFrom(
                      shape: const CircleBorder(),
                      padding: EdgeInsets.zero,
                    ),
                    onPressed: () {
                      context.pop();
                    },
                    child: SvgPicture.asset(
                      'assets/icons/x.svg',
                      colorFilter: const ColorFilter.mode(
                        AppColors.primary700,
                        BlendMode.srcIn,
                      ),
                      width: 24,
                      height: 24,
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
      body: Container(
        padding: const EdgeInsets.only(
          bottom: 84,
        ),
        color: AppColors.primary200,
        width: double.infinity,
        child: 
          Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: <Widget>[
              Screenshot(
                controller: screenshotController,
                child: Container(
                  decoration: const BoxDecoration(
                    borderRadius: BorderRadius.only(
                      bottomLeft: Radius.circular(36),
                      bottomRight: Radius.circular(36)),
                    color: AppColors.white,
                    boxShadow: AppColors.shadowSm,
                  ),
                  width: double.infinity,
                  child: Center(
                    child: Column(
                        children: [
                          const SizedBox(
                            height: 24,
                          ),
                          Image.asset(
                            'assets/images/logo_rojo.png',
                            width: 160,
                          ),
                          const SizedBox(
                            height: 36,
                          ),
                          Text(
                            textAlign: TextAlign.center,
                            'Billetera Virtual\n${afiliacionCelularState.datosAfiliacion!.nombreAlias}',
                            style: const TextStyle(
                              fontSize: 20,
                              fontWeight: FontWeight.w600,
                              color: AppColors.black,
                              height: 26 / 20,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                          const SizedBox(
                            height: 10,
                          ),
                          Container(
                            padding: const EdgeInsets.all(6),
                            decoration: BoxDecoration(
                              borderRadius: BorderRadius.circular(8),
                              color: AppColors.primary700,
                              border: Border.all(color: AppColors.primary700),
                              boxShadow: AppColors.shadowSm,
                            ),
                            child: QrImageView(
                              backgroundColor: AppColors.white,
                              data: afiliacionCelularState.datosAfiliacion?.cadenaHashQR ?? "",
                              version: QrVersions.auto,
                              size: 250.0,
                            ),
                          ),
                          const SizedBox(
                            height: 10,
                          ),
                          const Text(
                            'Escanea el QR',
                            style: TextStyle(
                              fontSize: 20,
                              fontWeight: FontWeight.w600,
                              color: AppColors.black,
                              height: 26 / 20,
                              leadingDistribution: TextLeadingDistribution.even,
                            ),
                          ),
                          const SizedBox(
                            height: 36,
                          ),
                        ],
                    ),
                  ),
                )
              ),
              const Spacer(),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  CtCompartir2(screenshotController: screenshotController),
                  CtDescargar(screenshotController: screenshotController),
                ],
              ),
            ]
          ),
      ),
    );
  }
}

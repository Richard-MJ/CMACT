import 'dart:async';
import 'dart:io';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/providers/transferencia_celular_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/svg.dart';
import 'package:go_router/go_router.dart';
import 'package:google_mlkit_barcode_scanning/google_mlkit_barcode_scanning.dart' as sc;
import 'package:image_picker/image_picker.dart';
import 'package:qr_code_scanner_plus/qr_code_scanner_plus.dart';
import 'package:screenshot/screenshot.dart';

class QrScannerScreen extends StatelessWidget {
  const QrScannerScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const _QRViewExample();
  }
}

class _QRViewExample extends ConsumerStatefulWidget {
  const _QRViewExample();

  @override
  QrScannerViewState createState() => QrScannerViewState();
}

class QrScannerViewState extends ConsumerState<_QRViewExample> with SingleTickerProviderStateMixin {
  QRViewController? controller;
  ScreenshotController screenshotController = ScreenshotController();
  AnimationController? animationController;
  bool qrCodeProcesando = false;
  final ImagePicker imagen = ImagePicker();
  final GlobalKey qrKey = GlobalKey(debugLabel: 'QR');

  @override
  void initState() {
    super.initState();
    animationController = AnimationController(
      vsync: this,
      duration: const Duration(seconds: 2),  
    )..repeat(reverse: true);
  }

  @override
  void reassemble() {
    super.reassemble();
    if(Platform.isAndroid){
      controller?.pauseCamera();
    }else{
      if(Platform.isIOS){
        controller?.resumeCamera();
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: <Widget>[
          Expanded(flex: 4, child: _buildQrView(context))
        ]
      )
    );
  }

  Widget _buildQrView(BuildContext context) {
    var scanArea = (MediaQuery.of(context).size.width < 400 ||
            MediaQuery.of(context).size.height < 400)
        ? 250.0 : 300.0;

    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.transparent,
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
                    color: AppColors.primary100,
                  ),
                  child: TextButton(
                    style: TextButton.styleFrom(
                      shape: const CircleBorder(),
                      padding: EdgeInsets.zero,
                    ),
                    onPressed: () {
                      controller?.pauseCamera();
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
      extendBodyBehindAppBar: true,
      body: Stack(
        alignment: Alignment.center,
        children: <Widget>[
          QRView(
            key: qrKey,
            onQRViewCreated: _onQRViewCreated,
            overlay: QrScannerOverlayShape(
              borderColor: AppColors.primary700,
              borderRadius: 10,
              borderLength: 30,
              borderWidth: 8,
              cutOutSize: scanArea
            ),
            onPermissionSet: (ctrl, p) => _onPermissionSet(context, ctrl, p),
          ),
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              Stack(
                children: [
                  CustomPaint(
                    size: Size(scanArea-10, scanArea-20),
                    painter: LineaPainter(animationController!),
                  )
                ],
              ),            
            ], 
          ),
          Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              Stack(
                children: [
                  SizedBox(
                    height: scanArea,
                    width: scanArea,
                  )
                ],
              ), 
              const SizedBox(height: 48),
              const Text(
                'Enfoca el código QR dentro \ndel recuadro',
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.white,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),          
            ], 
          ),
          Align(
            alignment: Alignment.bottomCenter,
            child: Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: <Widget>[
                  ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.black.withOpacity(0.6),
                      foregroundColor: Colors.white,
                      padding: const EdgeInsets.symmetric(horizontal: 24,vertical: 12)
                      ),
                    onPressed: () async {
                      await controller?.toggleFlash();
                      setState(() {});
                    }, 
                    child: FutureBuilder(
                      future: controller?.getFlashStatus(),
                      builder: (context, snapshot) {
                        return const Text('Enceder linterna');
                    }),
                  ),
                  const SizedBox(height: 10),
                  Container(
                    decoration: BoxDecoration(
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(8),
                      boxShadow: const [
                        BoxShadow(
                          color: Colors.black26,
                          blurRadius: 4,
                          offset: Offset(0, 2)
                        )
                      ],
                    ),
                    child: TextButton.icon(
                      onPressed: (){
                        _imagenQRViewScanner();
                      }, 
                      icon: const Icon(Icons.image, color: AppColors.primary700,), 
                      label: const Text(
                        'Subir una imagen con QR',
                        style: TextStyle(
                          color: Colors.black
                        ),
                      ),
                      style: TextButton.styleFrom(
                        padding: const EdgeInsets.symmetric(horizontal: 64, vertical: 24),                        
                      ),
                    ),
                  )
                ],
              ),
            )
          )
        ]
      )
    );
  }

  void _onQRViewCreated(QRViewController controller) {
    setState(() {
      this.controller = controller;
    });
    controller.scannedDataStream.listen((scanData) async {
      if(!qrCodeProcesando){
         setState(() {
          qrCodeProcesando = true;
        });
        
        final procesado = await ref.watch(transferenciaCelularProvider.notifier).lecturaCodigoQr(scanData.code);
        controller.pauseCamera();
        if(procesado){
          context.replace('/billetera-virtual/transferencia-celular/transferir');
        } else {
          context.pop();
        }
      }
    });
  }

  Future<void> _imagenQRViewScanner() async {
    final XFile? archivoImagen = await imagen.pickImage(source: ImageSource.gallery);
    if(archivoImagen != null){
      final File imagenfile = File(archivoImagen.path);
      await _decodificarQrImagen(imagenfile);
    }
  }

  Future<void> _decodificarQrImagen(File imagenfile) async {
    final sc.InputImage inputImage = sc.InputImage.fromFile(imagenfile);
    final sc.BarcodeScanner barcodeScanner = sc.BarcodeScanner();
    final List<sc.Barcode> barcodes = await barcodeScanner.processImage(inputImage);
    barcodeScanner.close();

    if(barcodes.isNotEmpty) {
      if(!qrCodeProcesando) {
        qrCodeProcesando = true;
        final procesado = await ref.watch(transferenciaCelularProvider.notifier).lecturaCodigoQr(barcodes.first.rawValue);
        controller?.pauseCamera();
        
        if(procesado){
          context.replace('/billetera-virtual/transferencia-celular/transferir');
        } else {
          context.pop();
        }
      }
    }    
  }

  void _onPermissionSet(BuildContext context, QRViewController ctrl, bool permiso) {
    if (!permiso) {
        Navigator.of(context).pop();
        ref.read(snackbarProvider.notifier)
          .showSnackbar(
            "La aplicación no tiene permiso para acceder a tu camara.", 
            SnackbarType.info);
    }
  }

  @override
  void dispose() {
    controller?.dispose();
    animationController?.dispose();
    super.dispose();
  }
}

class LineaPainter extends CustomPainter {
  final Animation<double> animation;

  LineaPainter(this.animation) : super(repaint: animation);

  @override
  void paint(Canvas canvas, Size size) {
    final paint = Paint()
      ..color = AppColors.primary600
      ..strokeWidth = 3.0;

    final y = size.height * animation.value;
    canvas.drawLine(
      Offset(0, y),
      Offset(size.width, y),
      paint
    );
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) => true;

  @override
  bool shouldRebuildSemantics(covariant CustomPainter oldDelegate) => true;
}

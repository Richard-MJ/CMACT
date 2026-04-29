import 'dart:io';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:path_provider/path_provider.dart';
import 'package:screenshot/screenshot.dart';
import 'package:share_plus/share_plus.dart';

class CtCompartir2 extends StatelessWidget {
  const CtCompartir2({
    super.key,
    this.onPress,
    this.screenshotController,
  });

  final void Function()? onPress;
  final ScreenshotController? screenshotController;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () async {
        if (onPress != null) {
          onPress!();
        } else {
          if (screenshotController != null) {
            screenshotController!
                .capture(
                  delay: const Duration(milliseconds: 10),
                  pixelRatio: 3.0,
                )
                .then((image) async {
              if (image != null) {
                final directory = await getApplicationDocumentsDirectory();
            final imagePath = await File(
                    '${directory.path}/caja_tacna_qr_${DateTime.now().millisecondsSinceEpoch}.png')
                .create();
            await imagePath.writeAsBytes(image);

            if (!context.mounted) return;

            final box = context.findRenderObject() as RenderBox?;

                await Share.shareXFiles(
                  [XFile(imagePath.path)],
                  sharePositionOrigin:
                      box!.localToGlobal(Offset.zero) & box.size,
                );
              }
            });
          }
        }
      },
      child: Column(
        children: [
          Container(
            padding: const EdgeInsets.all(16),
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(8),
              color: AppColors.primary700,
              boxShadow: AppColors.shadowSm,
            ),
            child: const Icon(Icons.share, color: AppColors.white),
          ),
          const SizedBox(
            height: 8,
          ),
          const Text(
            'Compartir',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w600,
              color: AppColors.black,
              height: 26 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ],
      ),
    );
  }
}

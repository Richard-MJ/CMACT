import 'dart:io';

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:path_provider/path_provider.dart';
import 'package:screenshot/screenshot.dart';
import 'package:share_plus/share_plus.dart';

class CtCompartir extends StatelessWidget {
  const CtCompartir({
    super.key,
    this.onPress,
    this.screenshotController,
  });

  final void Function()? onPress;
  final ScreenshotController? screenshotController;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        if (onPress != null) {
          onPress!();
        } else {
          if (screenshotController != null) {
            screenshotController!
                .capture(delay: const Duration(milliseconds: 10))
                .then((image) async {
              if (image != null) {
                final directory = await getApplicationDocumentsDirectory();
                final imagePath =
                    await File('${directory.path}/image.png').create();
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
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          SvgPicture.asset(
            'assets/icons/share.svg',
            height: 20,
          ),
          const SizedBox(
            width: 8,
          ),
          const Text(
            'Compartir',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w500,
              color: AppColors.primary700,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ],
      ),
    );
  }
}

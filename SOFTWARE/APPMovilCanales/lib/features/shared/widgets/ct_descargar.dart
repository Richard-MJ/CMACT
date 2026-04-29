import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:gal/gal.dart';
import 'package:screenshot/screenshot.dart';

class CtDescargar extends StatelessWidget {
  const CtDescargar({
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
            final image = await screenshotController!.capture(
              delay: const Duration(milliseconds: 10),
              pixelRatio: 3.0,
            );

            if (image != null) {
            try {
              await Gal.putImageBytes(image);
              if (!context.mounted) return;
              ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(
                  content: Container(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 17,
                      vertical: 22,
                    ),
                    decoration: const BoxDecoration(
                      color: AppColors.gray100,
                      boxShadow: AppColors.shadowSm,
                    ),
                    child: Row(
                      children: [
                        SvgPicture.asset(
                          'assets/icons/check-input.svg',
                          height: 24,
                        ),
                        const SizedBox(
                          width: 16,
                        ),
                        const Text(
                          'Se ha guardado la imagen en la galería.',
                          style: TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray900,
                            height: 22 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                      ],
                    ),
                  ),
                  elevation: 0,
                  behavior: SnackBarBehavior.floating,
                  backgroundColor: AppColors.gray100,
                  padding: EdgeInsets.zero,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(
                      8,
                    ),
                  ),
                ),
              );
            } catch (e) {
              if (!context.mounted) return;
              ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(
                  content: Text('Error al guardar la imagen: $e'),
                ),
              );
              }
            }
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
            child: const Icon(Icons.download, color: AppColors.white)
          ),     
          const SizedBox(
            height: 8,
          ),                 
          const Text(
            'Descargar',
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

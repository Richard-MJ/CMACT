import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/libro_reclamaciones.dart';
import 'package:caja_tacna_app/features/home/models/configuracion.dart';
import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

class BannerLibroReclamaciones extends StatelessWidget {
  const BannerLibroReclamaciones({super.key, required this.enlaceReclamo});

  final EnlaceDocumento? enlaceReclamo;
  
  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () async {
          final urlDoc = enlaceReclamo?.urlDocumento;
          if (urlDoc != null && urlDoc.isNotEmpty) {
            final Uri url = Uri.parse(urlDoc);
            await launchUrl(
              url,
              mode: LaunchMode.externalApplication,
            );
          }
      },
      child: Container(
        height: 96,
        margin: EdgeInsets.zero,
        padding: const EdgeInsets.symmetric(horizontal: 20),
        decoration: BoxDecoration(
          color: AppColors.primary50,
          borderRadius: BorderRadius.circular(8),
          boxShadow: AppColors.shadowSm,
        ),
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            LibroReclamaciones(width: 74),
            const SizedBox(
              width: 20,
            ),
            const Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    'Libro de Reclamaciones',
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w700,
                      color: AppColors.gray900,
                      height: 19 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  SizedBox(
                    height: 7,
                  ),
                  Text(
                    'Accede al Libro de Reclamaciones de manera rápida y fácil',
                    style: TextStyle(
                      fontSize: 10,
                      fontWeight: FontWeight.w400,
                      color: AppColors.black,
                      height: 14 / 10,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    softWrap: true,
                  ),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}

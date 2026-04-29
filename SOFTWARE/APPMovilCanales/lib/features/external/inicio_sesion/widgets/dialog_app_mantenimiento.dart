import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter/gestures.dart';
import 'package:url_launcher/url_launcher.dart';

class DialogAppMantenimiento extends StatelessWidget {
  DialogAppMantenimiento({super.key});

  Future<void> lanzarWebTuCaja() async {
    final Uri webLaunchUri = Uri.parse('https://www.cmactacna.com.pe');
    if (await canLaunchUrl(webLaunchUri)) {
      await launchUrl(webLaunchUri);
    }
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
      elevation: 0,
      backgroundColor: AppColors.white,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24),
      contentPadding: const EdgeInsets.only(
        top: 48,
        left: 24,
        right: 24,
        bottom: 48,
      ),
      content: SizedBox(
        width: double.infinity,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Text(
              'Lo sentimos',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w800,
                color: AppColors.black,
                height: 28 / 18,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(height: 24),
            const Text(
              'Estimado cliente Caja Tacna APP se encuentra en mantenimiento, verifique si cuenta con la última versión a través de su tienda de aplicaciones.',
              style: TextStyle(
                fontSize: 15,
                fontWeight: FontWeight.w400,
                color: AppColors.black,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(height: 20),
            RichText(
              text: TextSpan(
                style: const TextStyle(
                  fontSize: 17,
                  fontWeight: FontWeight.w400,
                  color: AppColors.black,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                children: <TextSpan>[
                  const TextSpan(
                    text:
                        'Recuerde: Puede realizar operaciones o consultas a través de nuestro canal electrónico Tu Caja Por Internet (Personas) ingresando desde ',
                  ),
                  TextSpan(
                    text: 'www.cmactacna.com.pe',
                    style: const TextStyle(
                      color: Colors.blue,
                      decoration: TextDecoration.underline,
                    ),
                    recognizer: TapGestureRecognizer()..onTap = lanzarWebTuCaja,
                  ),
                  const TextSpan(text: '.'),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/core/providers/app_version_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:url_launcher/url_launcher.dart';

class DialogAppVersion extends ConsumerStatefulWidget {
  const DialogAppVersion({super.key});

  @override
  DialogAppVersionState createState() => DialogAppVersionState();
}

class DialogAppVersionState extends ConsumerState<DialogAppVersion> {
  Future<void> abrirStore() async {
    final Uri url =
        Uri.parse(ref.read(appVersionProvider).versionamiento?.urlApp ?? '');
    if (await canLaunchUrl(url)) {
      launchUrl(url, mode: LaunchMode.externalApplication);
    }
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
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
              'Actualiza tu Caja Tacna App',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w800,
                color: AppColors.black,
                height: 28 / 18,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 24,
            ),
            const Text(
              'Hay una nueva versión disponible, actualiza tu app y vuelve a ingresar',
              style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w400,
                color: AppColors.black,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 36,
            ),
            CtButton(
              text: 'Actualizar app',
              onPressed: () {
                abrirStore();
              },
              borderRadius: 8,
              width: double.infinity,
            ),
          ],
        ),
      ),
    );
  }
}

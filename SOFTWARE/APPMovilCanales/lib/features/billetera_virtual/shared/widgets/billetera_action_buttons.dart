import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/codigo_enlace.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';
import 'package:url_launcher/url_launcher.dart';

class BilleteraActionButtons extends ConsumerWidget {
  const BilleteraActionButtons({
    super.key,
    this.showSettings = false,
    this.additionalButtons,
  });

  final bool showSettings;

  final List<Widget>? additionalButtons;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final documentoPreguntasFrecuentes = ref
        .watch(homeProvider)
        .configuracion
        ?.enlacesDocumentos
        .where((x) =>
            x.codigoDocumento == CodigoEnlace.billeteraPreguntasFrecuentas)
        .firstOrNull;

    return Wrap(
      spacing: 8,
      children: [
        GestureDetector(
          onTap: () async {
            final Uri phoneLaunchUri = Uri(
              scheme: 'tel',
              path: '052583658',
            );

            if (await canLaunchUrl(phoneLaunchUri)) {
              await launchUrl(phoneLaunchUri);
            }
          },
          child: SvgPicture.asset(
            'assets/icons/billetera-virtual/telefono.svg',
            height: 28,
            width: 28,
            colorFilter: const ColorFilter.mode(
              AppColors.gray25,
              BlendMode.srcIn,
            ),
          ),
        ),
        GestureDetector(
          onTap: () async {
            final urlDoc = documentoPreguntasFrecuentes?.urlDocumento;
            if (urlDoc != null && urlDoc.isNotEmpty) {
              final Uri url = Uri.parse(urlDoc);
              await launchUrl(
                url,
                mode: LaunchMode.externalApplication,
              );
            }
          },
          child: SvgPicture.asset(
            'assets/icons/billetera-virtual/interrogante.svg',
            height: 28,
            width: 28,
            colorFilter: const ColorFilter.mode(
              AppColors.gray25,
              BlendMode.srcIn,
            ),
          ),
        ),
        if (additionalButtons != null) ...additionalButtons!,
        if (showSettings)
          GestureDetector(
            onTap: () {
              context.push(
                '/billetera-virtual/afiliacion/datos-operacion',
              );
            },
            child: SvgPicture.asset(
              'assets/icons/cuenta-ahorro/settings.svg',
              height: 28,
              width: 28,
              colorFilter: const ColorFilter.mode(
                AppColors.gray25,
                BlendMode.srcIn,
              ),
            ),
          ),
      ],
    );
  }
}

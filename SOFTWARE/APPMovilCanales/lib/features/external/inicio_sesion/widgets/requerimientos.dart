import 'package:caja_tacna_app/constants/codigo_enlace.dart';
import 'package:caja_tacna_app/features/external/providers/parametros_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:url_launcher/url_launcher.dart';

class Requerimientos extends ConsumerWidget {
  const Requerimientos({super.key, required this.width});
  final double width;
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final enlaceRequerimiento = ref
        .watch(parametrosProvider)
        .enlaces
        ?.where((x) => x.codigoDocumento == CodigoEnlace.requerimiento)
        .firstOrNull;

    return GestureDetector(
        onTap: () async {
          final urlDoc = enlaceRequerimiento?.urlDocumento;
          if (urlDoc != null && urlDoc.isNotEmpty) {
            final Uri url = Uri.parse(urlDoc);
            await launchUrl(
              url,
              mode: LaunchMode.externalApplication,
            );
          }
        },
        child: Image.asset(
          'assets/images/requerimientos.png',
          width: width,
          fit: BoxFit.contain,
        ));
  }
}

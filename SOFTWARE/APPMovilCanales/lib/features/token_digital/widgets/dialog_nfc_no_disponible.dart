import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';

class DialogNfc extends StatelessWidget {
  const DialogNfc({super.key, this.titulo, this.contenido});

  final String? titulo;
  final String? contenido;

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
            if (titulo != null) ...[
              Text(
                titulo!,
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
            ],
            if (contenido != null) ...[
              Text(
                contenido!,
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: AppColors.black,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              const SizedBox(
                height: 25,
              ),
            ],
            CtButton(
                text: 'Aceptar',
                borderRadius: 8,
                width: double.infinity,
                onPressed: () {
                  if (Navigator.of(context).canPop()) {
                    Navigator.of(context).pop(true);
                  }
                }),
          ],
        ),
      ),
    );
  }
}

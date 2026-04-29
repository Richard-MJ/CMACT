import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:share_plus/share_plus.dart';

class DialogInvitarCliente extends StatelessWidget {
  const DialogInvitarCliente({super.key});

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
      elevation: 0,
      backgroundColor: AppColors.white,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24),
      content: SizedBox(
        width: double.infinity,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Center(
              child: SvgPicture.asset(
                'assets/icons/check-input.svg',
                height: 84,
              ),
            ),
            const SizedBox(
              height: 12,
            ),
            const Text(
              textAlign: TextAlign.center,
              'Este número aún no esta afiliado',
              style: TextStyle(
                fontSize: 17,
                fontWeight: FontWeight.w600,
                color: AppColors.black,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 12,
            ),
            const Center(
              child: Text(
                textAlign: TextAlign.justify,
                'Invitalo a afiliarse a la Billetera Virtual de Caja Tacna APP y poder enviar dinero en tiempo real a sus contactos.',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ),
            const SizedBox(
              height: 24,
            ),
            Builder(
              builder: (context) {
                return CtButton(
                  text: 'Invitalo',
                  onPressed: () async {
                    final box = context.findRenderObject() as RenderBox?;
                    final sharePositionOrigin = box != null
                        ? box.localToGlobal(Offset.zero) & box.size
                        : null;
                    await Share.share(
                      'Hola, te invitó a afiliarte al servicio de Billetera Virtual de Caja Tacna, descargar nuestra APP y afiliate al servicio. https://play.google.com/store/apps/details?id=com.cajatacna.droid&hl=es_PE',
                      sharePositionOrigin: sharePositionOrigin,
                    );
                    if (!context.mounted) return;
                    Navigator.of(context).pop(false);
                  },
                  borderRadius: 8,
                  width: double.infinity,
                );
              },
            ),
            const SizedBox(
              height: 12,
            ),
            CtButton(
              text: 'Cancelar',
              onPressed: () {
                Navigator.of(context).pop(false);
              },
              borderRadius: 8,
              width: double.infinity,
              type: ButtonType.outline,
            )
          ],
        ),
      ),
    );
  }
}

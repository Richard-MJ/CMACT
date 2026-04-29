import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/contactanos/widgets/contactanos_item.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_5.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:url_launcher/url_launcher.dart';

class ContactanosScreen extends StatelessWidget {
  const ContactanosScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout5(
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _ContactanosView(),
          )
        ],
      ),
    );
  }
}

class _ContactanosView extends StatelessWidget {
  const _ContactanosView();

  Future<void> enviarEmail() async {
    final Uri emailLaunchUri = Uri(
      scheme: 'mailto',
      path: 'canaleselectronicos@cmactacna.com.pe',
      query: CtUtils.encodeQueryParameters(
        <String, String>{
          'subject': 'Consulta cliente',
        },
      ),
    );

    if (await canLaunchUrl(emailLaunchUri)) {
      await launchUrl(emailLaunchUri);
    }
  }

  Future<void> llamar() async {
    final Uri phoneLaunchUri = Uri(
      scheme: 'tel',
      path: '052583658',
    );

    if (await canLaunchUrl(phoneLaunchUri)) {
      await launchUrl(phoneLaunchUri);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.only(
        left: 24,
        right: 24,
        bottom: 52,
        top: 62,
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Text(
            'Mantente comunicado',
            style: TextStyle(
              fontSize: 24,
              fontWeight: FontWeight.w600,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
              color: AppColors.gray900,
            ),
            textAlign: TextAlign.left,
          ),
          const SizedBox(
            height: 36,
          ),
          const Text(
            'Puedes comunicarte con nosotros a través de los siguientes medios:',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w400,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
              color: AppColors.gray900,
            ),
            textAlign: TextAlign.left,
          ),
          const SizedBox(
            height: 36,
          ),
          ContactanosItem(
            onPressed: () {
              enviarEmail();
            },
            label: 'canaleselectronicos@cmactacna.com.pe',
            icon: 'assets/icons/at-sign.svg',
          ),
          const SizedBox(
            height: 24,
          ),
          ContactanosItem(
            onPressed: () {
              llamar();
            },
            label: '052-583658',
            icon: 'assets/icons/phone-black.svg',
          ),
          const SizedBox(
            height: 24,
          ),
          ContactanosItem(
            onPressed: () {
              CtUtils.abrirWeb(url: 'https://cmactacna.com.pe');
            },
            label: 'Web Caja Tacna',
            icon: 'assets/icons/globe.svg',
          ),
          const Spacer(),
          CtButton(
            text: 'Volver al inicio',
            onPressed: () {
              context.pop();
            },
            type: ButtonType.outline,
            width: 215,
          )
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_5.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class OlvideMiClaveScreen extends StatelessWidget {
  const OlvideMiClaveScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout5(
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _OlvideMiClaveView(),
          )
        ],
      ),
    );
  }
}

class _OlvideMiClaveView extends StatelessWidget {
  const _OlvideMiClaveView();

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
            'Nosotros te ayudamos',
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
            'Acércate a una de nuestras agencias a solicitar el reseteo de tu clave de internet.',
            style: TextStyle(
              fontSize: 20,
              fontWeight: FontWeight.w500,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
              color: AppColors.gray900,
            ),
            textAlign: TextAlign.left,
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

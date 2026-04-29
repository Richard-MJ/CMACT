import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class InputClaveInternet extends ConsumerWidget {
  const InputClaveInternet({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return GestureDetector(
      onTap: () {
        ref.read(loginProvider.notifier).ingresarClave();
      },
      child: CtTextFieldContainer(
        child: Row(
          children: [
            SvgPicture.asset(
              'assets/icons/lock.svg',
              height: 20,
            ),
            const SizedBox(
              width: 8,
            ),
            const Expanded(
              child: Text(
                'Clave de internet',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                  color: AppColors.gray500,
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtQrCode extends StatelessWidget {
  const CtQrCode({super.key, required this.onPressed});

  final void Function() onPressed;
  @override
  Widget build(BuildContext context) {
    return TextButton(
      onPressed: () {
        onPressed();
      },
      child: SvgPicture.asset(
        'assets/icons/qr-code.svg',
        height: 45,
        width: 45,
        colorFilter: const ColorFilter.mode(
          AppColors.primary700,
          BlendMode.srcIn,
        ),
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:go_router/go_router.dart';

class BilleteraHeader extends StatelessWidget {
  const BilleteraHeader({
    super.key,
    this.showQrButton = false,
  });

  final bool showQrButton;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        const Center(
          child: Text(
            'Billetera Virtual',
            style: TextStyle(
              fontSize: 22,
              fontWeight: FontWeight.w800,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ),
        if (showQrButton) ...[
          const SizedBox(height: 8),
          GestureDetector(
            onTap: () {
              context.push('/billetera-virtual/afiliacion/compartir-qr');
            },
            child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                const Text(
                  'Ver QR',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    color: AppColors.primary700,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(width: 8),
                SvgPicture.asset(
                  'assets/icons/qr-code.svg',
                  height: 30,
                  colorFilter: const ColorFilter.mode(
                    AppColors.primary700,
                    BlendMode.srcIn,
                  ),
                ),
              ],
            ),
          ),
        ],
      ],
    );
  }
}

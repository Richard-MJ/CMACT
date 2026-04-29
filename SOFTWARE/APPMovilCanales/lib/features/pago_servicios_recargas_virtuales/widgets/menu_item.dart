import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class MenuItem extends StatelessWidget {
  const MenuItem({
    super.key,
    required this.label,
    required this.onPressed,
    required this.icon,
  });

  final String label;
  final void Function() onPressed;
  final String icon;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 60,
      width: double.infinity,
      decoration: const BoxDecoration(
        borderRadius: BorderRadius.all(
          Radius.circular(8),
        ),
        gradient: AppColors.localLinearGrey,
      ),
      child: TextButton(
        style: TextButton.styleFrom(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(8),
          ),
          padding: const EdgeInsets.symmetric(horizontal: 16),
        ),
        onPressed: () {
          onPressed();
        },
        child: Row(
          children: [
            SvgPicture.asset(
              icon,
              height: 24,
              colorFilter: const ColorFilter.mode(
                AppColors.gray900,
                BlendMode.srcIn,
              ),
            ),
            const SizedBox(
              width: 16,
            ),
            Text(
              label,
              style: const TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ],
        ),
      ),
    );
  }
}

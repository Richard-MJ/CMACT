import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class HeaderModal extends StatelessWidget {
  const HeaderModal({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.only(
        top: 10,
        left: 10,
        right: 10,
      ),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.only(
          topLeft: Radius.circular(AppTheme.modalBorderRadius),
          topRight: Radius.circular(AppTheme.modalBorderRadius),
        ),
        color: AppColors.white,
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.end,
        children: [
          Container(
            height: 36,
            width: 36,
            decoration: const BoxDecoration(
              shape: BoxShape.circle,
              color: AppColors.white,
            ),
            child: TextButton(
              style: TextButton.styleFrom(
                shape: const CircleBorder(),
                padding: EdgeInsets.zero,
              ),
              onPressed: () {
                Navigator.pop(context);
              },
              child: SvgPicture.asset(
                'assets/icons/x.svg',
                height: 24,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

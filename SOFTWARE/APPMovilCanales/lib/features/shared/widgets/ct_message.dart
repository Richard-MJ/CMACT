import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtMessage extends StatelessWidget {
  const CtMessage({super.key, required this.child});

  final Widget child;
  @override
  Widget build(BuildContext context) {
    return Container(
      height: 92,
      padding: const EdgeInsets.symmetric(horizontal: 17),
      decoration: BoxDecoration(
        color: AppColors.gray100,
        borderRadius: BorderRadius.circular(8),
        border: Border.all(
          color: AppColors.gray200,
          width: 1,
        ),
      ),
      child: Row(
        children: [
          SvgPicture.asset(
            'assets/icons/bell.svg',
            height: 24,
          ),
          const SizedBox(
            width: 10,
          ),
          child
        ],
      ),
    );
  }
}

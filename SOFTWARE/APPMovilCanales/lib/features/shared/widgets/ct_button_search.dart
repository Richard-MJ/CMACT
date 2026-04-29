import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtButtonSearch extends StatelessWidget {
  const CtButtonSearch({super.key, required this.onPressed});

  final void Function() onPressed;
  @override
  Widget build(BuildContext context) {
    return Container(
      width: 40,
      height: 40,
      decoration: const BoxDecoration(
        shape: BoxShape.circle,
        color: AppColors.primary700,
      ),
      child: TextButton(
        style: TextButton.styleFrom(
          shape: const CircleBorder(),
          padding: EdgeInsets.zero,
        ),
        onPressed: () {
          onPressed();
        },
        child: SvgPicture.asset(
          'assets/icons/search.svg',
          colorFilter: const ColorFilter.mode(
            AppColors.gray25,
            BlendMode.srcIn,
          ),
          width: 20,
          height: 20,
        ),
      ),
    );
  }
}

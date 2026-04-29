import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtAppbar4 extends StatelessWidget {
  const CtAppbar4({
    super.key,
    required this.text,
    required this.iconSvg,
    required this.onBack,
    required this.onTap
  });

  final String text;
  final String iconSvg;
  final void Function() onBack;
  final void Function() onTap;
  @override
  Widget build(BuildContext context) {
    EdgeInsets safeAreaPadding = MediaQuery.of(context).padding;

    return Container(
      height: 64 + safeAreaPadding.top,
      padding: EdgeInsets.only(top: safeAreaPadding.top, left: 16, right: 16),
      decoration: const BoxDecoration(
        color: AppColors.primary700,
        borderRadius: BorderRadius.only(
          bottomLeft: Radius.circular(24),
          bottomRight: Radius.circular(24),
        ),
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Row(
            children: [
              Container(
                width: 32,
                height: 32,
                decoration: const BoxDecoration(
                  shape: BoxShape.circle,
                  color: AppColors.primary600,
                ),
                child: TextButton(
                  style: TextButton.styleFrom(
                    shape: const CircleBorder(),
                    padding: EdgeInsets.zero,
                  ),
                  onPressed: () {
                    onBack();
                  },
                  child: SvgPicture.asset(
                    'assets/icons/shared/chevron-left.svg',
                    colorFilter: const ColorFilter.mode(
                      AppColors.gray25,
                      BlendMode.srcIn,
                    ),
                    width: 24,
                    height: 24,
                  ),
                ),
              ),
              const SizedBox(
                width: 4,
              ),
              Text(
                text,
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w600,
                  color: AppColors.white,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ],
          ),
          Row(
            children: [
              GestureDetector(
                onTap: () {
                  onTap();
                },
                child: SvgPicture.asset(
                  iconSvg,
                  colorFilter: const ColorFilter.mode(
                    AppColors.gray25,
                    BlendMode.srcIn,
                  ),
                  width: 28,
                  height: 28,
                )
              ),
              const SizedBox(
                width: 10,
              ),
            ],
          )
        ],
      ),
    );
  }
}

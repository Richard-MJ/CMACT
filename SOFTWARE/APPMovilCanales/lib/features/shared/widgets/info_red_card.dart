import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

class InfoRedCard extends StatelessWidget {
  const InfoRedCard({
    super.key,
    required this.content,
  });

  final String content;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(
          color: AppColors.primary600,
          width: 1,
        ),
        color: AppColors.primary50,
        borderRadius: BorderRadius.circular(8),
      ),
      width: double.infinity,
      padding: const EdgeInsets.all(15),
      child: Row(
        children: [
          SvgPicture.asset(
            'assets/icons/info.svg',
            height: 24,
            colorFilter: const ColorFilter.mode(
              AppColors.primary600,
              BlendMode.srcIn,
            ),
          ),
          const SizedBox(
            width: 15,
          ),
          Expanded(
            child: Text(
              content,
              style: TextStyle(
                fontSize: 13,
                fontWeight: FontWeight.w400,
                color: AppColors.gray900,
                height: 28 / 18,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

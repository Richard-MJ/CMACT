import 'dart:math';

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_shimmer.dart';
import 'package:flutter/material.dart';

class CtMovimientoSkeleton extends StatelessWidget {
  const CtMovimientoSkeleton({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.only(bottom: 10),
      decoration: const BoxDecoration(
        border: Border(
          bottom: BorderSide(
            color: AppColors.border1,
            width: 1,
          ),
        ),
      ),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              CtShimmer.rectangular(
                width: Random().nextInt(60) + 130,
                height: 16,
                margin: const EdgeInsets.symmetric(vertical: 4),
              ),
              const CtShimmer.rectangular(
                width: 109,
                height: 14,
                margin: EdgeInsets.symmetric(vertical: 4),
              ),
            ],
          ),
          CtShimmer.rectangular(
            width: Random().nextInt(26) + 55,
            height: 16,
            margin: const EdgeInsets.symmetric(vertical: 4),
          ),
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class CtTimer extends ConsumerWidget {
  const CtTimer({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final timerState = ref.watch(timerProvider);
    return Align(
      alignment: Alignment.centerRight,
      child: Text(
        'Vence en ${timerState.timerText}',
        style: const TextStyle(
          fontSize: 14,
          fontWeight: FontWeight.w500,
          color: AppColors.gray600,
          height: 22 / 14,
          leadingDistribution: TextLeadingDistribution.even,
        ),
      ),
    );
  }
}

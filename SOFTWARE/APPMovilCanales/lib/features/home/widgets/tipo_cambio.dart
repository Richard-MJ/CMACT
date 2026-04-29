import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_shimmer.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TipoCambio extends ConsumerWidget {
  const TipoCambio({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final homeState = ref.watch(homeProvider);

    if (homeState.loadingTipoCambio) {
      return const Row(
        mainAxisAlignment: MainAxisAlignment.end,
        children: [
          CtShimmer.rectangular(
            width: 160,
            height: 10,
            margin: EdgeInsets.symmetric(vertical: 4),
          ),
        ],
      );
    }
    return Row(
      mainAxisAlignment: MainAxisAlignment.end,
      children: [
        const Text(
          'T.C.',
          style: TextStyle(
            fontSize: 10,
            fontWeight: FontWeight.w400,
            color: AppColors.gray900,
            height: 1.8,
            leadingDistribution: TextLeadingDistribution.even,
          ),
        ),
        const SizedBox(
          width: 5,
        ),
        RichText(
          text: TextSpan(
            style: const TextStyle(
              fontSize: 10,
              fontWeight: FontWeight.w400,
              height: 1.8,
              color: AppColors.gray900,
              leadingDistribution: TextLeadingDistribution.even,
            ),
            children: <TextSpan>[
              const TextSpan(
                text: 'Compra: ',
              ),
              TextSpan(
                text: '${homeState.tipoCambioCompra} ',
                style: const TextStyle(
                  fontWeight: FontWeight.w500,
                ),
              ),
              const TextSpan(
                text: 'Venta: ',
              ),
              TextSpan(
                text: '${homeState.tipoCambioVenta}',
                style: const TextStyle(
                  fontWeight: FontWeight.w500,
                ),
              ),
            ],
          ),
        ),
      ],
    );
  }
}

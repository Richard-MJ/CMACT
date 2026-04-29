import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/credito/models/credito.dart';
import 'package:caja_tacna_app/features/credito/provider/credito_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/home/widgets/card.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_shimmer.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class MisCreditos extends ConsumerWidget {
  const MisCreditos({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final homeState = ref.watch(homeProvider);

    if (homeState.loadingCreditos) {
      return const _CreditosSkeleton();
    }
    if (!homeState.loadingCreditos && homeState.creditos.isEmpty) {
      return Padding(
        padding: const EdgeInsets.symmetric(horizontal: 24),
        child: Stack(
          children: [
            HomeCard(
              marginBottom: 25,
              type: 2,
              onTap: () {
                ref.read(homeProvider.notifier).abrirWeb();
              },
              child: Column(
                children: [
                  const Text(
                    '!Tenemos tu crédito a medida!',
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w500,
                      height: 1.3,
                      leadingDistribution: TextLeadingDistribution.even,
                      color: AppColors.gray900,
                    ),
                  ),
                  const SizedBox(
                    height: 5,
                  ),
                  RichText(
                    text: const TextSpan(
                      style: TextStyle(
                        fontSize: 12,
                        fontWeight: FontWeight.w400,
                        height: 1.5,
                        leadingDistribution: TextLeadingDistribution.even,
                        color: AppColors.gray900,
                      ),
                      children: <TextSpan>[
                        TextSpan(
                          text: 'Haz clic',
                        ),
                        TextSpan(
                          text: ' AQUÍ ',
                          style: TextStyle(
                            fontWeight: FontWeight.w700,
                          ),
                        ),
                        TextSpan(
                          text: 'para más información.',
                        ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
            Positioned(
              right: 20,
              bottom: 50,
              child: SvgPicture.asset(
                'assets/icons/alert-circle-2.svg',
                height: 40,
                colorFilter: const ColorFilter.mode(
                  AppColors.primary700,
                  BlendMode.srcIn,
                ),
              ),
            ),
          ],
        ),
      );
    }
    return SizedBox(
      height: 135,
      child: ListView.separated(
        itemCount: homeState.creditos.length,
        scrollDirection: Axis.horizontal,
        padding: const EdgeInsets.symmetric(horizontal: 24),
        separatorBuilder: (context, index) => const SizedBox(width: 16),
        itemBuilder: (context, index) {
          return _Credito(
            credito: homeState.creditos[index],
          );
        },
      ),
    );
  }
}

class _Credito extends ConsumerWidget {
  const _Credito({required this.credito});

  final Credito credito;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return HomeCard(
      marginBottom: 25,
      type: 2,
      onTap: () {
        ref.read(creditoProvider.notifier).setCredito(credito);
        context.push('/credito/detalle/${credito.numeroCredito}');
      },
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            credito.descripcionTipoCredito,
            style: const TextStyle(
              fontSize: 10,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
              overflow: TextOverflow.ellipsis,
            ),
          ),
          Text(
            credito.alias,
            style: const TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w700,
              color: AppColors.gray900,
              height: 1,
              leadingDistribution: TextLeadingDistribution.even,
              overflow: TextOverflow.ellipsis,
            ),
          ),
          Text(
            credito.descripcionSubProducto,
            style: TextStyle(
              fontSize: 10,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const Spacer(),
          Text(
            CtUtils.formatCurrency(
              credito.saldoPendiente,
              credito.simboloMoneda,
            ),
            style: const TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.w800,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const Text(
            'Saldo pendiente',
            style: TextStyle(
              fontSize: 10,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 1,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ],
      ),
    );
  }
}

class _CreditosSkeleton extends StatelessWidget {
  const _CreditosSkeleton();

  @override
  Widget build(BuildContext context) {
    return const SingleChildScrollView(
      scrollDirection: Axis.horizontal,
      physics: NeverScrollableScrollPhysics(),
      child: Row(
        children: [
          SizedBox(
            width: 24,
          ),
          _CreditoSkeleton(),
          SizedBox(
            width: 16,
          ),
          _CreditoSkeleton(),
        ],
      ),
    );
  }
}

class _CreditoSkeleton extends StatelessWidget {
  const _CreditoSkeleton();

  @override
  Widget build(BuildContext context) {
    return const HomeCard(
      marginBottom: 25,
      type: 2,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          CtShimmer.rectangular(
            width: 50,
            height: 10,
            margin: EdgeInsets.symmetric(vertical: 2.5),
          ),
          SizedBox(
            height: 3,
          ),
          CtShimmer.rectangular(
            width: 120,
            height: 14,
          ),
          Spacer(),
          CtShimmer.rectangular(
            width: 80,
            height: 18,
            margin: EdgeInsets.symmetric(vertical: 4.5),
          ),
          CtShimmer.rectangular(
            width: 60,
            height: 10,
            margin: EdgeInsets.symmetric(vertical: 2.5),
          ),
        ],
      ),
    );
  }
}

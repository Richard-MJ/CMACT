import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/home/widgets/card.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_shimmer.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';

class MisCuentas extends ConsumerWidget {
  const MisCuentas({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final homeState = ref.watch(homeProvider);

    if (homeState.loadingCuentasAhorro) {
      return const _CuentasSkeleton();
    }

    return SizedBox(
      height: 135,
      child: ListView.separated(
        itemCount: homeState.cuentasAhorro.length,
        scrollDirection: Axis.horizontal,
        padding: const EdgeInsets.symmetric(horizontal: 24),
        separatorBuilder: (context, index) => const SizedBox(width: 16),
        itemBuilder: (context, index) {
          return _CuentaAhorro(
            mostrarSaldo: homeState.mostrarSaldo,
            cuentaAhorro: homeState.cuentasAhorro[index],
          );
        },
      ),
    );
  }
}

class _CuentaAhorro extends ConsumerWidget {
  const _CuentaAhorro({required this.mostrarSaldo, required this.cuentaAhorro});
  final bool mostrarSaldo;
  final CuentaAhorro cuentaAhorro;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return HomeCard(
      marginBottom: 25,
      type: 1,
      onTap: () {
        context.push(
            '/cuenta-ahorro/detalle/${cuentaAhorro.codigoAgencia}/${cuentaAhorro.identificador}');
      },
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            cuentaAhorro.descripcion,
            style: const TextStyle(
              fontSize: 10,
              fontWeight: FontWeight.w400,
              color: AppColors.white,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
              overflow: TextOverflow.ellipsis,
            ),
          ),
          Text(
            cuentaAhorro.alias,
            style: const TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w700,
              color: AppColors.white,
              height: 1,
              leadingDistribution: TextLeadingDistribution.even,
              overflow: TextOverflow.ellipsis,
            ),
          ),
          const Spacer(),
          Text(
            mostrarSaldo
                ? CtUtils.formatCurrency(
                    cuentaAhorro.saldoDisponible,
                    cuentaAhorro.simboloMoneda,
                  )
                : "${cuentaAhorro.simboloMoneda}****",
            style: const TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.w800,
              color: AppColors.white,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const Text(
            'Saldo disponible',
            style: TextStyle(
              fontSize: 10,
              fontWeight: FontWeight.w400,
              color: AppColors.white,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ],
      ),
    );
  }
}

class _CuentasSkeleton extends StatelessWidget {
  const _CuentasSkeleton();

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
          _CuentaSkeleton(),
          SizedBox(
            width: 16,
          ),
          _CuentaSkeleton(),
        ],
      ),
    );
  }
}

class _CuentaSkeleton extends StatelessWidget {
  const _CuentaSkeleton();

  @override
  Widget build(BuildContext context) {
    return const HomeCard(
      marginBottom: 25,
      type: 1,
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

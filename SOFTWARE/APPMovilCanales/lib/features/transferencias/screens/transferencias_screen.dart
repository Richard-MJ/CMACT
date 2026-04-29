import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/transferencia_item.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class TransferenciasScreen extends StatelessWidget {
  const TransferenciasScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Transferencias',
      child: _TransferenciasView(),
    );
  }
}

class _TransferenciasView extends ConsumerWidget {
  const _TransferenciasView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final cuentasAhorro = ref.watch(homeProvider).cuentasAhorro;

    return Container(
      padding: const EdgeInsets.only(top: 24, bottom: 40, left: 24, right: 24),
      width: double.infinity,
      child: Column(
        children: [
          TransferenciaItem(
            label: 'Transferir entre mis cuentas',
            onPressed: () {
              context.push('/transferencias/entre-mis-cuentas/transferir');
            },
            disabled: cuentasAhorro.length < 2,
          ),
          if (cuentasAhorro.length < 2)
            Container(
              padding: const EdgeInsets.only(top: 4, left: 16, right: 16),
              child: Row(
                children: [
                  SvgPicture.asset(
                    'assets/icons/info.svg',
                    height: 16,
                  ),
                  const SizedBox(
                    width: 4,
                  ),
                  const Expanded(
                    child: Text(
                      'Debes tener más de una cuenta activa para activar esta opción.',
                      style: TextStyle(
                        fontSize: 12,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray700,
                        height: 1.5,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ),
                ],
              ),
            ),
          const SizedBox(height: 16),
          TransferenciaItem(
            label: 'Transferir a terceros',
            onPressed: () {
              context.push('/transferencias/terceros/transferir');
            },
          ),
          const SizedBox(height: 16),
          TransferenciaItem(
            label: 'Transferir a otros bancos (CCI)',
            onPressed: () {
              context.push('/transferencias/interbancaria/transferir');
            },
          ),
        ],
      ),
    );
  }
}

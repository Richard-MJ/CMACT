import 'package:caja_tacna_app/features/pago_servicios_recargas_virtuales/widgets/menu_item.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

class PagoServiciosRecargasVirtualesScreen extends StatelessWidget {
  const PagoServiciosRecargasVirtualesScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Pago de servicios y recargas virtuales',
      child: _PagoServiciosRecargasView(),
    );
  }
}

class _PagoServiciosRecargasView extends ConsumerWidget {
  const _PagoServiciosRecargasView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Container(
      padding: const EdgeInsets.only(top: 40, bottom: 40, left: 24, right: 24),
      width: double.infinity,
      child: Column(
        children: [
          MenuItem(
            label: 'Pagar servicios',
            onPressed: () {
              context.push('/pago-servicios/buscar');
            },
            icon: 'assets/icons/shopping-bag.svg',
          ),
          const SizedBox(height: 16),
          MenuItem(
            label: 'Recarga virtual',
            onPressed: () {
              context.push('/recarga-virtual/recargar');
            },
            icon: 'assets/icons/phone.svg',
          ),
        ],
      ),
    );
  }
}

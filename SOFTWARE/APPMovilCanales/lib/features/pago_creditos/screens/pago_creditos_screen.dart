import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/screens/pago_creditos_propios_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/providers/pago_creditos_terceros_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/widgets/pago_credito_item.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class PagoCreditosScreen extends StatelessWidget {
  const PagoCreditosScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Pago de créditos',
      child: _PagoCreditosView(),
    );
  }
}

class _PagoCreditosView extends ConsumerWidget {
  const _PagoCreditosView();

  @override
  Widget build(BuildContext context, ref) {
    final homeState = ref.watch(homeProvider);

    return Container(
      padding: const EdgeInsets.only(top: 48, bottom: 40, left: 24, right: 24),
      width: double.infinity,
      child: Column(
        children: [
          PagoCreditoItem(
            text: 'Pago de créditos propios',
            onPressed: () {
              context.push('/pago-creditos/creditos-propios');
            },
          ),
          const SizedBox(height: 16),
          PagoCreditoItem(
            text: 'Pago de créditos de terceros',
            onPressed: () {
              // context.push('/pago-creditos/creditos-terceros/pagar');
              showPagoCreditoBottomSheet(
                context: context,
                pagoApp: () async {
                  await ref
                      .read(pagoCreditoTercerosProvider.notifier)
                      .setTipoPago(TipoPago.aplicativo);
                  
                  // ref
                  //     .read(pagoCreditoPropioProvider.notifier)
                  //     .setCreditoAbonar(pagoCreditoState.creditos[index]);
                },
                pagoCip: () async {
                  await ref
                      .read(pagoCreditoTercerosProvider.notifier)
                      .setTipoPago(TipoPago.cip);
                  // ref
                  //     .read(pagoCreditoPropioProvider.notifier)
                  //     .setCreditoAbonar(pagoCreditoState.creditos[index]);
                },
                indicadorCip: homeState.configuracion?.indicadorPagoEfectivoPagoCredito ?? false
              );
            },
          ),
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/providers/pago_creditos_propios_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/widgets/credito_item.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/providers/pago_creditos_terceros_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class PagoCreditosPropiosScreen extends ConsumerStatefulWidget {
  const PagoCreditosPropiosScreen({super.key});

  @override
  PagoCreditosPropiosScreenState createState() =>
      PagoCreditosPropiosScreenState();
}

class PagoCreditosPropiosScreenState
    extends ConsumerState<PagoCreditosPropiosScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(pagoCreditoPropioProvider.notifier).initDatosMenu();
      ref.read(pagoCreditoPropioProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Pago de créditos',
      child: _PagoCreditosPropiosView(),
    );
  }
}

class _PagoCreditosPropiosView extends ConsumerWidget {
  const _PagoCreditosPropiosView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoCreditoState = ref.watch(pagoCreditoPropioProvider);
    final homeState = ref.watch(homeProvider);

    return CustomScrollView(
      slivers: [
        SliverPadding(
          padding: const EdgeInsets.only(
            top: 24,
            bottom: 48,
            left: 24,
            right: 24,
          ),
          sliver: SliverList.separated(
            separatorBuilder: (context, index) {
              return const SizedBox(
                height: 16,
              );
            },
            itemCount: pagoCreditoState.creditos.length,
            itemBuilder: (context, index) {
              return CreditoItem(
                credito: pagoCreditoState.creditos[index],
                onPressed: () {
                  showPagoCreditoBottomSheet(
                    context: context,
                    pagoApp: () async {
                      await ref
                          .read(pagoCreditoPropioProvider.notifier)
                          .setTipoPago(TipoPago.aplicativo);
                      ref
                          .read(pagoCreditoPropioProvider.notifier)
                          .setCreditoAbonar(pagoCreditoState.creditos[index]);
                    },
                    pagoCip: () async {
                      await ref
                          .read(pagoCreditoPropioProvider.notifier)
                          .setTipoPago(TipoPago.cip);
                      ref
                          .read(pagoCreditoPropioProvider.notifier)
                          .setCreditoAbonar(pagoCreditoState.creditos[index]);
                    },
                    indicadorCip: homeState.configuracion?.indicadorPagoEfectivoPagoCredito ?? false,
                  );
                },
              );
            },
          ),
        )
      ],
    );
  }
}

void showPagoCreditoBottomSheet({
  required BuildContext context,
  required void Function() pagoApp,
  required void Function() pagoCip,
  required bool indicadorCip,
}) {

  showModalBottomSheet(
    useRootNavigator: true,
    shape: RoundedRectangleBorder(
      borderRadius: BorderRadius.circular(8),
    ),
    context: context,
    isScrollControlled: true,
    backgroundColor: Colors.white,
    enableDrag: true,
    elevation: 10,
    builder: (BuildContext context) {
      return Container(
        height: 276,
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(8),
          color: Colors.white,
        ),
        child: Column(
          children: [
            Container(
              padding: const EdgeInsets.only(
                top: 18,
                left: 24,
                right: 18,
                bottom: 18,
              ),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  SizedBox(
                    height: 36,
                    width: 36,
                    child: TextButton(
                      style: TextButton.styleFrom(
                        shape: const CircleBorder(),
                        padding: EdgeInsets.zero,
                      ),
                      onPressed: () {
                        Navigator.pop(context);
                      },
                      child: SvgPicture.asset(
                        'assets/icons/x.svg',
                        height: 24,
                      ),
                    ),
                  ),
                ],
              ),
            ),
            Container(
              padding: const EdgeInsets.only(
                left: 24,
                right: 24,
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  Container(
                    padding: const EdgeInsets.only(
                      left: 32,
                    ),
                    child: const Text(
                      'Elige la forma de pago',
                      style: TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.w800,
                        height: 22 / 18,
                        leadingDistribution: TextLeadingDistribution.even,
                        color: Colors.black,
                      ),
                    ),
                  ),
                  const SizedBox(
                    height: 24,
                  ),
                  CtButton(
                    text: 'Pago por aplicativo',
                    onPressed: () {
                      Navigator.pop(context);
                      pagoApp();
                    },
                    width: double.infinity,
                    borderRadius: 8,
                    type: ButtonType.plain,
                  ),
                  if(indicadorCip) ... [
                    const SizedBox(
                      height: 24,
                    ),
                    CtButton(
                      text: 'Pago por código CIP (Pago Efectivo)',
                      onPressed: () {
                        Navigator.pop(context);
                        pagoCip();
                      },
                      width: double.infinity,
                      borderRadius: 8,
                      type: ButtonType.plain,
                    ),
                  ]
                ],
              ),
            ),
          ],
        ),
      );
    },
  );
}

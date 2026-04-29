import 'package:caja_tacna_app/features/home/widgets/otras_operaciones_item.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/pop_home_scope.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';

class OtrasOperacionesScreen extends ConsumerStatefulWidget {
  const OtrasOperacionesScreen({super.key});

  @override
  OtrasOperacionesScreenState createState() => OtrasOperacionesScreenState();
}

class OtrasOperacionesScreenState
    extends ConsumerState<OtrasOperacionesScreen> {
  @override
  Widget build(BuildContext context) {
    return PopHomeScope(
      child: CtLayout4(
          title: 'Otros productos',
          child: const _OtrasOperacionesView(),
          onBack: () async {
            context.go('/home');
          }),
    );
  }
}

class _OtrasOperacionesView extends ConsumerWidget {
  const _OtrasOperacionesView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final homeState = ref.watch(homeProvider);

    return ListView(
      padding: const EdgeInsets.only(top: 36, left: 25, right: 24, bottom: 89),
      children: [
        Row(
          children: [
            Expanded(
              child: OtrasOperacionesItem(
                label: 'Abrir cuentas de ahorro y DPF',
                onPressed: () {
                  context.push('/apertura-cuentas/ingreso-datos');
                },
                icon: 'assets/icons/otras_operaciones/abrir_cuentas.svg',
              ),
            ),
            const SizedBox(
              width: 24,
            ),
            Expanded(
              child: OtrasOperacionesItem(
                label: 'Adelanto de\n sueldo',
                onPressed: () {
                  context.push('/adelanto-sueldo/pagar');
                },
                icon: 'assets/icons/otras_operaciones/adelanto_sueldo.svg',
              ),
            )
          ],
        ),
        const SizedBox(
          height: 24,
        ),
        Row(
          children: [
            Expanded(
              child: OtrasOperacionesItem(
                label: 'Pago de tarjetas\n de crédito',
                onPressed: () {
                  context.push('/pago-tarjetas-credito/ingreso-datos-tarjeta');
                },
                icon: 'assets/icons/otras_operaciones/pago_tarjetas.svg',
              ),
            ),
            const SizedBox(
              width: 24,
            ),
            Expanded(
              child: OtrasOperacionesItem(
                label: 'Pago de servicios\ny recargas',
                onPressed: () {
                  context.push('/pago-servicios-recargas-virtuales');
                },
                icon: 'assets/icons/otras_operaciones/pago_servicios.svg',
              ),
            )
          ],
        ),
        const SizedBox(
          height: 24,
        ),
        Row(
          children: [
            Expanded(
              child: OtrasOperacionesItem(
                label: 'Pago con\nSafetyPay',
                onPressed: () {
                  context.push('/pago-safetypay/pagar');
                },
                icon: 'assets/icons/otras_operaciones/pago_safetypay.svg',
              ),
            ),
            const SizedBox(
              width: 24,
            ),
            Expanded(
              child: OtrasOperacionesItem(
                label: 'Emisión\nde giros',
                onPressed: () {
                  context.push('/emision-giros/ingreso-monto');
                },
                icon: 'assets/icons/otras_operaciones/giros.svg',
              ),
            )
          ],
        ),
        const SizedBox(
          height: 24,
        ),
        Row(
          children: [
            Expanded(
              child: OtrasOperacionesItem(
                label: 'Solicitud\nde crédito',
                onPressed: () {
                  context.push('/solicitud-crediticia/ingreso-mensual');
                },
                icon: 'assets/icons/otras_operaciones/solicitud.svg',
              ),
            ),
            const SizedBox(
              width: 24,
            ),
            Expanded(
              child: OtrasOperacionesItem(
                label: 'Billetera\nVirtual',
                onPressed: () {
                  if (homeState.esAfiliadoBilleteraVirtual) {
                    context.push(
                        '/billetera-virtual/transferencia-celular/contactos');
                  } else {
                    context
                        .push('/billetera-virtual/afiliacion/datos-operacion');
                  }
                },
                icon: 'assets/icons/otras_operaciones/billetera.svg',
              ),
            )
          ],
        ),
      ],
    );
  }
}

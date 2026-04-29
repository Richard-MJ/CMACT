import 'package:caja_tacna_app/features/shared/widgets/ct_operacion.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class Operaciones extends StatelessWidget {
  const Operaciones({super.key});

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        CtOperacion(
          label: 'Pagar \ncréditos',
          icon: 'assets/icons/payment.svg',
          onPressed: () {
            context.push('/pago-creditos');
          },
        ),
        CtOperacion(
          label: 'Pagar \nservicios',
          icon: 'assets/icons/receipt.svg',
          onPressed: () {
            context.push('/pago-servicios/buscar');
          },
        ),
        CtOperacion(
          label: 'Transferir \ndinero',
          icon: 'assets/icons/send.svg',
          onPressed: () {
            context.push('/transferencias');
          },
        ),
        CtOperacion(
          label: 'Otros \nproductos',
          icon: 'assets/icons/table.svg',
          onPressed: () {
            context.push('/otras-operaciones');
          },
        ),
      ],
    );
  }
}

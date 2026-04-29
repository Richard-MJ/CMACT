import 'package:caja_tacna_app/features/pago_creditos/models/opcion_pago_credito.dart';
import 'package:caja_tacna_app/features/providers.dart';
import 'package:caja_tacna_app/features/shared/widgets/index.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TipoPagoOpcion extends StatelessWidget {
  const TipoPagoOpcion(
      {super.key, required this.pagoCreditoState, required this.ref});

  final PagoCreditoPropioState pagoCreditoState;
  final WidgetRef ref;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: TipoPagoCredito.values
      .where((tipoPago) {
        if (tipoPago == TipoPagoCredito.adelanto) return false;
        return !tipoPago.esAnticipo() ||
            pagoCreditoState.tipoPago == TipoPago.aplicativo;
      })
      .map((tipoPago) {
        return Padding(
          padding: const EdgeInsets.only(right: 8.0),
          child: CtButton2(
            text: tipoPago.obtenerNombre(),
            onPressed: tipoPago.estaDeshabilitado(
                      montoTotalPago: pagoCreditoState.creditoAbonar?.montoTotalPago ?? 0, 
                      montoSaldoCancelacion: pagoCreditoState.creditoAbonar?.montoSaldoCancelacion ?? 0
                  )
                ? () {}
                : () {
                    ref
                        .read(pagoCreditoPropioProvider.notifier)
                        .changeTipoPagoCredito(tipoPago);
                  },
            type: pagoCreditoState.tipoPagoCredito == tipoPago
                ? ButtonType.solid
                : ButtonType.outline,
            disabled: tipoPago.estaDeshabilitado(
              montoTotalPago: pagoCreditoState.creditoAbonar?.montoTotalPago ?? 0, 
              montoSaldoCancelacion: pagoCreditoState.creditoAbonar?.montoSaldoCancelacion ?? 0
            ),
          ),
        );
      }).toList(),
    );
  }
}

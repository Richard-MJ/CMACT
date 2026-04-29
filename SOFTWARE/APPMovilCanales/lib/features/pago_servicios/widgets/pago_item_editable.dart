import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/obtener_cobro_servicio_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/widgets/input_monto_deuda.dart';
import 'package:caja_tacna_app/features/shared/inputs/monto_deuda_servicio.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';

class PagoItemEditable extends StatelessWidget {
  const PagoItemEditable({
    super.key,
    this.disabled = false,
    required this.cobroServicio,
    required this.montoDeudaServicio,
    required this.onChangeMonto,
  });

  final bool disabled;
  final ObtenerCobroServicioResponse? cobroServicio;
  final MontoDeudaServicio montoDeudaServicio;
  final void Function(MontoDeudaServicio montoDeudaServicio) onChangeMonto;

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(children: [
          CtCheckbox(
            value: true,
            onPressed: () {},
          ),
          const SizedBox(
            width: 8,
          ),
          Text(
            'N° de recibo: ${cobroServicio?.numeroRecibo}\nVence ${CtUtils.formatDate(cobroServicio?.fechaVencimiento)}',
            style: const TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w500,
              color: AppColors.gray700,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ]),
        SizedBox(
          height: 16,
        ),
        InputMontoDeuda(
          onChangeMonto: onChangeMonto,
          simboloMoneda: cobroServicio!.simboloMoneda,
          monto: montoDeudaServicio,
          montoDeuda: cobroServicio!.montoDeuda,
        )
      ],
    );
  }
}

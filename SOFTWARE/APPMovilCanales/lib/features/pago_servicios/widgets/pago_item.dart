import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/obtener_cobro_servicio_response.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';

class PagoItem extends StatelessWidget {
  const PagoItem({
    super.key,
    this.disabled = false,
    required this.cobroServicio,
  });

  final bool disabled;
  final ObtenerCobroServicioResponse? cobroServicio;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 80,
      width: double.infinity,
      decoration: const BoxDecoration(
        color: AppColors.gray100,
        borderRadius: BorderRadius.all(Radius.circular(8)),
      ),
      padding: const EdgeInsets.symmetric(horizontal: 16),
      child: Row(
        children: [
          CtCheckbox(
            value: true,
            onPressed: () {},
          ),
          const SizedBox(
            width: 8,
          ),
          Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
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
            ],
          ),
          const Spacer(),
          Text(
            CtUtils.formatCurrency(
              cobroServicio?.montoDeuda,
              cobroServicio?.simboloMoneda,
            ),
            style: const TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_switch.dart';
import 'package:flutter/material.dart';

/// Card que muestra la información de la cuenta afiliada.
/// Incluye el switch, nombre del producto, número de cuenta y saldo.
class CuentaAfiliacionCard extends StatelessWidget {
  const CuentaAfiliacionCard({
    super.key,
    required this.esAfiliado,
    required this.nombreProducto,
    required this.numeroCuenta,
    required this.saldoDisponible,
    required this.simboloMoneda,
    required this.onPressed,
  });

  final bool esAfiliado;
  final String? nombreProducto;
  final String? numeroCuenta;
  final double? saldoDisponible;
  final String? simboloMoneda;
  final VoidCallback onPressed;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(
          color: esAfiliado ? Colors.transparent : AppColors.gray300,
          width: 1,
        ),
        borderRadius: BorderRadius.circular(8),
        boxShadow: AppColors.shadowSm,
        color: esAfiliado ? AppColors.gray100 : AppColors.gray25,
      ),
      child: TextButton(
        onPressed: onPressed,
        style: TextButton.styleFrom(
          padding: const EdgeInsets.symmetric(
            horizontal: 16,
            vertical: 18,
          ),
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.all(
              Radius.circular(8),
            ),
          ),
        ),
        child: Row(
          children: [
            CtSwitch(
              value: esAfiliado,
              onTap: () {},
            ),
            const SizedBox(width: 16),
            Expanded(
              child: Row(
                children: [
                  Expanded(
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          nombreProducto ?? "",
                          style: const TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray900,
                            height: 22 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                          overflow: TextOverflow.ellipsis,
                        ),
                        Text(
                          CtUtils.formatNumeroCuenta(
                            numeroCuenta: numeroCuenta,
                            hash: true,
                          ),
                          style: const TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w400,
                            color: AppColors.gray900,
                            height: 22 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                      ],
                    ),
                  ),
                  const SizedBox(width: 20),
                  Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      const Text(
                        'Saldo',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray900,
                          height: 22 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      Text(
                        CtUtils.formatCurrency(
                          saldoDisponible,
                          simboloMoneda,
                        ),
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray900,
                          height: 22 / 14,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(width: 16),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}

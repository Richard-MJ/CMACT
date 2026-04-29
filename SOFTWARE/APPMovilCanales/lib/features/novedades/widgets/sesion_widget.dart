import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/canal_origen.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/sesion_canal_electronico.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/svg.dart';

class SesionView extends ConsumerWidget {
  const SesionView({
    super.key,
    required this.sesion,
  });

  final SesionCanalElectronico? sesion;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Container(
      decoration: BoxDecoration(
        color: AppColors.gray100,
        borderRadius: BorderRadius.circular(8),
      ),
      width: double.infinity,
      padding: const EdgeInsets.all(15),
      child: Row(
        children: [
          SvgPicture.asset(
            'assets/icons/info.svg',
            height: 24,
          ),
          const SizedBox(
            width: 15,
          ),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    const Text(
                      'Dirección IP:',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray700,
                        height: 28 / 18,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Text(
                      sesion?.direccionIp ?? '',
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray700,
                        height: 28 / 18,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ],
                ),
                if (sesion?.indicadorCanal == CanalOrigen.appMovil)
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      const Text(
                        'Modelo del equipo:',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray700,
                          height: 28 / 18,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      Text(
                        sesion?.modeloDispositivo ?? '',
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray700,
                          height: 28 / 18,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                    ],
                  ),
                if (sesion?.indicadorCanal != CanalOrigen.appMovil)
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      const Text(
                        'Sistema Operativo:',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray700,
                          height: 28 / 18,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      Text(
                        sesion?.sistemaOperativo ?? '',
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray700,
                          height: 28 / 18,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                    ],
                  ),
                if (sesion?.indicadorCanal != CanalOrigen.appMovil)
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      const Text(
                        'Navegador:',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray700,
                          height: 28 / 18,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                      Text(
                        sesion?.navegador ?? '',
                        style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray700,
                          height: 28 / 18,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                    ],
                  ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    const Text(
                      'Fecha y hora:',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray700,
                        height: 28 / 18,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                    Text(
                      CtUtils.formatDateHorasMinutosSegundos(
                          sesion?.fechaModificacion ?? DateTime.now()),
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.gray700,
                        height: 28 / 18,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

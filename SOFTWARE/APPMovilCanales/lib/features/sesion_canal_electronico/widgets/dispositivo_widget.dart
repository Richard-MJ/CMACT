import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/canal_origen.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/dispositivo_seguro.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/providers/sesion_canal_electronico_provider.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/svg.dart';

class DispositivoView extends ConsumerWidget {
  const DispositivoView({
    super.key,
    required this.dispositivoActual,
    required this.confirmar,
    required this.dispositivoSeguro,
  });

  final bool dispositivoActual;
  final bool confirmar;
  final DispositivoSeguro? dispositivoSeguro;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return GestureDetector(
      onTap: () => dispositivoActual || confirmar
          ? null
          : ref
              .read(sesionCanalElectronicoProvider.notifier)
              .toogleSeleccionarDispositivo(dispositivoSeguro!),
      child: Container(
        decoration: BoxDecoration(
          color: AppColors.white,
          borderRadius: BorderRadius.circular(20),
        ),
        width: double.infinity,
        padding: const EdgeInsets.all(15),
        child: Row(
          children: [
            Row(
              children: [
                Container(
                  width: 55,
                  height: 55,
                  decoration: const BoxDecoration(
                    color: AppColors.primary50,
                    shape: BoxShape.circle,
                  ),
                  alignment: Alignment.center,
                  child: SvgPicture.asset(
                    dispositivoSeguro?.indicadorCanal == CanalOrigen.appMovil
                        ? 'assets/icons/sesiones-canales-electronicos/phone.svg'
                        : 'assets/icons/sesiones-canales-electronicos/display.svg',
                    height: 25,
                    colorFilter: const ColorFilter.mode(
                      AppColors.primary600,
                      BlendMode.srcIn,
                    ),
                  ),
                ),
                const SizedBox(
                  width: 15,
                )
              ],
            ),
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    dispositivoSeguro?.indicadorCanal == CanalOrigen.appMovil
                        ? dispositivoSeguro?.modeloDispositivo ?? ''
                        : dispositivoSeguro?.navegador ?? '',
                    style: const TextStyle(
                      fontSize: 17,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 28 / 18,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  Text(
                    dispositivoSeguro?.direccionIp ?? '',
                    style: const TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray600,
                      height: 28 / 18,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                  if (!confirmar) ...[
                    Text(
                      CtUtils.formatDateHorasMinutosSegundos(
                          dispositivoSeguro?.fechaModificacion ??
                              DateTime.now()),
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray600,
                        height: 28 / 18,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    )
                  ]
                ],
              ),
            ),
            if (!dispositivoActual && !confirmar) ...[
              CtCheckbox(
                onPressed: () => ref
                    .read(sesionCanalElectronicoProvider.notifier)
                    .toogleSeleccionarDispositivo(dispositivoSeguro!),
                value: dispositivoSeguro?.estaSelecionado ?? false,
              ),
              const SizedBox(
                width: 15,
              )
            ],
          ],
        ),
      ),
    );
  }
}

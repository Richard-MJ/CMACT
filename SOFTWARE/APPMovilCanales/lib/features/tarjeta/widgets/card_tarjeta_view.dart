import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/tipo_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/tarjeta/providers/tarjeta_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class TarjetaViewCard extends ConsumerWidget {
  const TarjetaViewCard({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final tarjetaState = ref.watch(tarjetaProvider);
    final timerState = ref.watch(timerProvider);
    const double espaciado = 5;
    const double tamanioLetra = 18;

    return Stack(children: [
      Column(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          const SizedBox(
            height: 50,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                tarjetaState.numeroTarjeta.substring(0, 4),
                style: const TextStyle(
                    letterSpacing: espaciado,
                    fontSize: tamanioLetra,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray300,
                    height: 25 / 16,
                    leadingDistribution: TextLeadingDistribution.even),
              ),
            ],
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                tarjetaState.numeroTarjeta.substring(5, 9),
                style: const TextStyle(
                    letterSpacing: espaciado,
                    fontSize: tamanioLetra,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray300,
                    height: 25 / 16,
                    leadingDistribution: TextLeadingDistribution.even),
              ),
            ],
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                tarjetaState.numeroTarjeta.substring(10, 14),
                style: const TextStyle(
                    letterSpacing: espaciado,
                    fontSize: tamanioLetra,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray300,
                    height: 25 / 16,
                    leadingDistribution: TextLeadingDistribution.even),
              ),
            ],
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                tarjetaState.numeroTarjeta.substring(15, 19),
                style: const TextStyle(
                    letterSpacing: espaciado,
                    fontSize: tamanioLetra,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray300,
                    height: 25 / 16,
                    leadingDistribution: TextLeadingDistribution.even),
              ),
              Text(
                tarjetaState.fechaVencimiento,
                style: const TextStyle(
                    letterSpacing: espaciado,
                    fontSize: tamanioLetra,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray300,
                    height: 25 / 16,
                    leadingDistribution: TextLeadingDistribution.even),
              ),
              Padding(
                padding: const EdgeInsets.only(right: 21),
                child: Text(
                  tarjetaState.cvv,
                  style: const TextStyle(
                      letterSpacing: espaciado,
                      fontSize: tamanioLetra,
                      fontWeight: FontWeight.w600,
                      color: AppColors.gray300,
                      height: 25 / 16,
                      leadingDistribution: TextLeadingDistribution.even),
                ),
              ),
            ],
          ),
        ],
      ),
      Positioned(
        top: 60,
        right: tarjetaState.tipoTarjeta == TipoTarjeta.debitoVisa ? 21 : 5,
        child: tarjetaState.tipoTarjeta == TipoTarjeta.debitoVisa
            ? SvgPicture.asset(
                'assets/icons/visa.svg',
                height: 30,
              )
            : Image.asset(
                'assets/images/logo_blanco.png',
                height: 30,
              ),
      ),
      if (!tarjetaState.datosOfuscados && timerState.timerOn) ...[
        Positioned(
            top: 101,
            right: 0,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                const Text('CVV vence en:',
                    style: TextStyle(
                      fontSize: tamanioLetra - 6,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray500,
                    )),
                Text(
                  timerState.timerText,
                  style: const TextStyle(
                      fontSize: tamanioLetra - 5,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray500),
                )
              ],
            )),
        Positioned(
          top: 49,
          left: 55,
          child: TextButton(
            onPressed: () async =>
                await ref.read(tarjetaProvider.notifier).copiarNumeroTarjeta(),
            style: TextButton.styleFrom(
                textStyle: const TextStyle(color: AppColors.gray300),
                shape: const RoundedRectangleBorder(
                  borderRadius: BorderRadius.all(
                    Radius.circular(8),
                  ),
                ),
                padding: EdgeInsets.zero),
            child: Row(
              children: [
                SvgPicture.asset('assets/icons/copy.svg',
                    height: 30,
                    colorFilter: const ColorFilter.mode(
                        AppColors.gray500, BlendMode.srcIn)),
              ],
            ),
          ),
        ),
      ],
    ]);
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/tipo_tarjeta.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/tarjeta/providers/tarjeta_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/svg.dart';

class TextEncabezadoTarjetaScreen extends ConsumerWidget {
  const TextEncabezadoTarjetaScreen({
    super.key,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final tarjetaState = ref.watch(tarjetaProvider);
    final homeState = ref.watch(homeProvider);

    return Row(
      children: [
        Expanded(
          child: Text(
            tarjetaState.tipoTarjeta == TipoTarjeta.debitoVisa
                ? 'Ver mi tarjeta y CVV dinámico'
                : homeState.datosCliente?.descripcionTipoTarjeta ?? 'Ver mi tarjeta',
            style: const TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w600,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ),
        SizedBox(
          height: 40,
          width: 30,
          child: tarjetaState.afiliacionComprasInternet != null
              ? IconButton(
                  padding: EdgeInsets.zero,
                  constraints: const BoxConstraints(),
                  icon: SvgPicture.asset(
                    tarjetaState.datosOfuscados
                        ? 'assets/icons/eye.svg'
                        : 'assets/icons/eye_closed.svg',
                    height: 30,
                    colorFilter: const ColorFilter.mode(
                        AppColors.gray600, BlendMode.srcIn),
                  ),
                  onPressed: tarjetaState.afiliacionComprasInternet == null ||
                          tarjetaState.mostrarTokenDigital
                      ? null
                      : () {
                          ref
                              .read(tarjetaProvider.notifier)
                              .confirmarMostrarDatos(context);
                        },
                )
              : Container(),
        ),
      ],
    );
  }
}

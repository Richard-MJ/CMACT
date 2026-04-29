import 'package:caja_tacna_app/constants/tipo_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_shimmer.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

class TarjetaSkeleton extends StatelessWidget {
  const TarjetaSkeleton({super.key, required this.tipoTarjeta});
  final String tipoTarjeta;

  @override
  Widget build(BuildContext context) {
    return Stack(children: [
      const Column(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          SizedBox(
            height: 50,
          ),
          Row(
            children: [
              SizedBox(
                width: 65,
                child: CtShimmer.rectangular(
                  width: 10,
                  height: 17,
                ),
              ),
              Spacer(),
            ],
          ),
          Row(
            children: [
              SizedBox(
                width: 65,
                child: CtShimmer.rectangular(
                  width: 10,
                  height: 17,
                ),
              ),
            ],
          ),
          Row(
            children: [
              SizedBox(
                width: 65,
                child: CtShimmer.rectangular(
                  width: 10,
                  height: 17,
                ),
              ),
            ],
          ),
          Row(
            children: [
              SizedBox(
                width: 65,
                child: CtShimmer.rectangular(
                  width: 10,
                  height: 17,
                ),
              ),
              SizedBox(
                width: 40,
              ),
              SizedBox(
                width: 65,
                child: CtShimmer.rectangular(
                  width: 10,
                  height: 17,
                ),
              ),
              SizedBox(
                width: 55,
              ),
              SizedBox(
                width: 40,
                child: CtShimmer.rectangular(
                  width: 10,
                  height: 17,
                ),
              )
            ],
          ),
        ],
      ),
      Positioned(
        top: 60,
        right: tipoTarjeta == TipoTarjeta.debitoVisa ? 21 : 5,
        child: tipoTarjeta == TipoTarjeta.debitoVisa
            ? SvgPicture.asset(
                'assets/icons/visa.svg',
                height: 30,
              )
            : Image.asset(
                'assets/images/logo_blanco.png',
                height: 30,
              ),
      ),
    ]);
  }
}

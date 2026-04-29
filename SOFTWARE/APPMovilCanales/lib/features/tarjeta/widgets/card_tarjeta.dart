import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/tipo_tarjeta.dart';
import 'package:flutter/material.dart';

class TarjetaCard extends StatelessWidget {
  const TarjetaCard(
      {super.key,
      required this.child,
      this.onTap,
      required this.tipoTarjeta,
      required this.width});
  final Widget child;
  final String tipoTarjeta;
  final void Function()? onTap;
  final double width;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: width,
      height: width / 1.61,
      decoration: BoxDecoration(
        color: cardColors[tipoTarjeta],
        borderRadius: BorderRadius.circular(8),
        boxShadow: const [
          BoxShadow(
            color: Color.fromRGBO(97, 84, 170, 0.06),
            offset: Offset(0, 12.52155),
            blurRadius: 10.01724,
          )
        ],
      ),
      child: ClipRRect(
        borderRadius: BorderRadius.circular(8),
        child: Stack(
          children: [
            if (tipoTarjeta == TipoTarjeta.debitoVisa)
              Positioned(
                top: -70,
                left: width / 2.2,
                child: Container(
                  width: width + 20, // Ancho del círculo
                  height: width + 20, // Altura del círculo
                  decoration: const BoxDecoration(
                    shape: BoxShape.circle,
                    color: Color.fromRGBO(255, 255, 255, 0.2),
                    gradient: LinearGradient(
                      begin: Alignment(-0.19438030913, -1),
                      end: Alignment(0.19438030913, 1),
                      colors: [
                        Color.fromRGBO(255, 255, 255, 1),
                        Color.fromRGBO(130, 239, 255, 0),
                      ],
                      stops: [0.081, 0.9503],
                    ),
                  ),
                ),
              ),
            ListTile(
              contentPadding: EdgeInsets.zero,
              dense: true,
              minVerticalPadding: 0,
              onTap: () {
                if (onTap != null) {
                  onTap!();
                }
              },
              title: Container(
                padding: const EdgeInsets.symmetric(
                  horizontal: 22,
                  vertical: 15,
                ),
                child: child,
              ),
            ),
            Positioned(
                top: 6,
                child: Container(
                  width: width,
                  height: width / 6.73,
                  color: AppColors.black,
                )),
          ],
        ),
      ),
    );
  }
}

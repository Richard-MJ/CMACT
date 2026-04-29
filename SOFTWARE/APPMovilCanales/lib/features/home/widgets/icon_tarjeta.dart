import 'dart:math' as math;
import 'package:caja_tacna_app/constants/tipo_tarjeta.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class IconTarjeta extends StatelessWidget {
  const IconTarjeta({
    super.key,
    this.tipoTarjeta = TipoTarjeta.debitoVisa,
    this.width,
    this.girar = false,
  });

  final String tipoTarjeta;
  final double? width;
  final bool girar;

  @override
  Widget build(BuildContext context) {
    String tarjetaImagen = switch (tipoTarjeta) {
      TipoTarjeta.debitoVisa => 'assets/images/servicard_debito.png',
      TipoTarjeta.ahorrosEmpresarial =>
        'assets/images/servicard_empresarial.png',
      TipoTarjeta.primeraServiCard => 'assets/images/servicard_miprimera.png',
      TipoTarjeta.clasica => 'assets/images/servicard_clasica.png',
      _ => 'assets/images/servicard_clasica.png'
    };

    return Transform.rotate(
      angle: girar ? math.pi / -2 : 0,
      child: IconButton(
          padding: EdgeInsets.zero,
          constraints: const BoxConstraints(),
          icon: Image.asset(
            tarjetaImagen,
            width: width ?? 50,
          ),
          onPressed: () {
            context.push('/tarjeta/datos');
          }),
    );
  }
}

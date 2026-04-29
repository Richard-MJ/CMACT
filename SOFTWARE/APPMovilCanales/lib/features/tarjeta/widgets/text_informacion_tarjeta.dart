import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class TextInformacionTarjeta extends StatelessWidget {
  const TextInformacionTarjeta({
    super.key,
    required this.tipoTarjeta,
  });

  final String tipoTarjeta;

  @override
  Widget build(BuildContext context) {
    String respuesta = switch (tipoTarjeta) {
      "3" =>
        'Para ofrecerte mayor seguridad, generamos un CVV diferente para cada una de tus compras por internet. Si el CVV ha vencido, deberás generar uno nuevo.',
      "4" =>
        'Recuerda que con tu tarjeta de ahorro Empresarial solo puedes utilizar Caja Tacna App para consultas.\n\nPara disfrutar de todas las funciones de nuestra app, abre una cuenta de ahorros y solicita una tarjeta Débito Visa en cualquiera de nuestras agencias.',
      "5" =>
        'Recuerda que con tu tarjeta Primera Servicard solo puedes utilizar Caja Tacna App para consultas.\n\nPara disfrutar de todas las funciones de nuestra app, abre una cuenta de ahorros y solicita una tarjeta Débito Visa en cualquiera de nuestras agencias.',
      _ =>
        'Recuerda que con tu tarjeta Clásica solo puedes utilizar Caja Tacna App para consultas.\n\nPara disfrutar de todas las funciones de nuestra app, abre una cuenta de ahorros y solicita una tarjeta Débito Visa en cualquiera de nuestras agencias.'
    };

    return Container(
        decoration: BoxDecoration(
          color: AppColors.gray50,
          borderRadius: BorderRadius.circular(8),
        ),
        width: double.infinity,
        padding: const EdgeInsets.symmetric(
          horizontal: 16,
          vertical: 18,
        ),
        child: Text(
          respuesta,
          textAlign: TextAlign.justify,
          style: const TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w400,
              height: 23 / 15,
              leadingDistribution: TextLeadingDistribution.even,
              color: AppColors.black),
        ));
  }
}

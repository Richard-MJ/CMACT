import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/opcion_pago_credito.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class DialogCondicionesPropio extends StatelessWidget {
  const DialogCondicionesPropio({super.key, required this.tipoPagoCredito});

  final TipoPagoCredito tipoPagoCredito;

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
      elevation: 0,
      backgroundColor: AppColors.white,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24, vertical: 24),
      contentPadding:
          const EdgeInsets.only(top: 18, left: 24, right: 24, bottom: 48),
      titlePadding: const EdgeInsets.only(top: 18, left: 24, right: 18),
      title: _buildCloseButton(context),
      content: SingleChildScrollView(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            _buildTitle(),
            const SizedBox(height: 10),
            _buildConditions(),
            const SizedBox(height: 24),
            CtButton(
              text: 'Aceptar',
              onPressed: () => Navigator.of(context).pop(true),
              borderRadius: 8,
              width: double.infinity,
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildCloseButton(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.end,
      children: [
        SizedBox(
          height: 36,
          width: 36,
          child: TextButton(
            style: TextButton.styleFrom(
                shape: const CircleBorder(), padding: EdgeInsets.zero),
            onPressed: () => Navigator.pop(context),
            child: SvgPicture.asset('assets/icons/x.svg', height: 24),
          ),
        ),
      ],
    );
  }

  Widget _buildTitle() {
    return Text(
      tipoPagoCredito == TipoPagoCredito.anticipo
          ? "Pago Anticipado"
          : "Adelanto de Cuotas",
      style: const TextStyle(
          fontSize: 18,
          fontWeight: FontWeight.w800,
          color: AppColors.black,
          height: 1.5),
      textAlign: TextAlign.center,
    );
  }

  Widget _buildConditions() {
    if (tipoPagoCredito == TipoPagoCredito.anticipo) {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          _buildNumberedListItem(
            number: "1.",
            text: "Se considera pago anticipado a:",
          ),
          _buildIndentedBullet(
              'Los pagos mayores a dos (2) cuotas (que incluye aquella exigible en el periodo). En estos casos, el titular podrá elegir reducir el monto de la cuota o reducir el número de cuotas respectivamente a través de nuestra red de agencias a nivel nacional.'),
          _buildIndentedBullet(
              'Asimismo, el pago realizado trae como consecuencia la aplicación del monto al capital del crédito, con la consiguiente reducción de intereses, comisiones y gastos en favor del titular.'),
          const SizedBox(height: 10),
          _buildNumberedListItem(
            number: "2.",
            text:
                'He sido informado y acepto las implicancias económicas de efectuar un pago anticipado.',
          ),
        ],
      );
    } else {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          _buildNumberedListItem(
            number: "1.",
            text: "Se considera adelanto de cuotas a:",
          ),
          _buildIndentedBullet(
              'Los pagos menores o iguales al equivalente de dos (2) cuotas (que incluyen aquella exigible en el periodo), sin que se produzca una reducción de los intereses, las comisiones y los gastos.'),
          const SizedBox(height: 10),
          _buildNumberedListItem(
            number: "2.",
            text:
                'He sido informado y acepto las implicancias económicas de efectuar un adelanto de cuotas.',
          ),
        ],
      );
    }
  }

  Widget _buildNumberedListItem(
      {required String number, required String text}) {
    return Padding(
      padding: const EdgeInsets.only(top: 8.0),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(number, style: _regularStyle),
          const SizedBox(width: 6),
          Expanded(child: Text(text, style: _regularStyle)),
        ],
      ),
    );
  }

  Widget _buildIndentedBullet(String text) {
    return Padding(
      padding: const EdgeInsets.only(left: 24.0, top: 4.0),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text("• ", style: _boldStyle),
          Expanded(child: Text(text, style: _smallStyle)),
        ],
      ),
    );
  }

  static const TextStyle _boldStyle = TextStyle(
      fontSize: 16, fontWeight: FontWeight.w600, color: AppColors.black);
  static const TextStyle _regularStyle = TextStyle(
      fontSize: 16,
      fontWeight: FontWeight.w400,
      color: AppColors.black,
      height: 1.5);
  static const TextStyle _smallStyle = TextStyle(
      fontSize: 15,
      fontWeight: FontWeight.w400,
      color: AppColors.black,
      height: 1.5);
}

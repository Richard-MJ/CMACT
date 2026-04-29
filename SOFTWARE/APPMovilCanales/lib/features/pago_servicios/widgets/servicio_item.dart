import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class ServicioItem extends StatelessWidget {
  const ServicioItem({
    super.key,
    required this.label,
    required this.onPressed,
  });

  final String label;
  final void Function() onPressed;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: 46,
      decoration: const BoxDecoration(
        border: Border(
          bottom: BorderSide(
            color: AppColors.gray200,
            width: 2,
          ),
        ),
      ),
      child: TextButton(
        style: TextButton.styleFrom(
          minimumSize: Size.zero,
          padding: const EdgeInsets.symmetric(
            horizontal: 16,
            vertical: 0,
          ),
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.all(
              Radius.circular(0),
            ),
          ),
          alignment: Alignment.centerLeft,
        ),
        onPressed: onPressed,
        child: Text(
          label,
          style: const TextStyle(
            color: AppColors.gray900,
            fontSize: 14,
            fontWeight: FontWeight.w500,
          ),
        ),
      ),
    );
  }
}

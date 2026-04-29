import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class HomeCard extends StatelessWidget {
  const HomeCard(
      {super.key,
      required this.child,
      required this.type,
      this.onTap,
      this.marginBottom});
  final Widget child;
  final int type;
  final void Function()? onTap;
  final double? marginBottom;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: 224.0,
      height: 110,
      margin: EdgeInsets.only(bottom: marginBottom ?? 36),
      decoration: BoxDecoration(
        color: type == 1 ? AppColors.primary700 : AppColors.white,
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
            Positioned(
              top: -12,
              left: 127,
              child: Container(
                width: 278, // Ancho del círculo
                height: 278, // Altura del círculo
                decoration: BoxDecoration(
                  shape: BoxShape.circle,
                  color: const Color.fromRGBO(255, 255, 255, 0.2),
                  gradient: LinearGradient(
                    begin: const Alignment(-0.19438030913, -1),
                    end: const Alignment(0.19438030913, 1),
                    colors: [
                      type == 1
                          ? const Color.fromRGBO(255, 255, 255, 1)
                          : const Color.fromRGBO(255, 6, 0, 0.38),
                      type == 1
                          ? const Color.fromRGBO(255, 255, 255, 0)
                          : const Color.fromRGBO(130, 239, 255, 0),
                    ],
                    stops: const [0.081, 0.9503],
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
                  vertical: 14,
                ),
                child: child,
              ),
            )
          ],
        ),
      ),
    );
  }
}

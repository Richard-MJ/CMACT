import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class BgAfiliacionExitosa extends StatelessWidget {
  const BgAfiliacionExitosa({super.key});

  @override
  Widget build(BuildContext context) {
    double deviceWidth = MediaQuery.of(context).size.width;
    double radio1 = (519 / 390) * deviceWidth;
    double radio2 = (278 / 390) * deviceWidth;

    return Positioned(
      top: 256 - radio1,
      left: (deviceWidth - radio1) / 2,
      child: Container(
        width: radio1,
        height: radio1,
        decoration: const BoxDecoration(
          shape: BoxShape.circle,
          color: Color.fromRGBO(255, 255, 255, 0.2),
          gradient: LinearGradient(
            begin: Alignment(0, -1),
            end: Alignment(0, 1),
            colors: [
              AppColors.primary700,
              Color(0xFFF9CDCC),
            ],
            stops: [0.4038, 0.99],
          ),
        ),
        child: ClipRRect(
          borderRadius: BorderRadius.circular(519),
          child: Stack(
            children: [
              Positioned(
                bottom: -116,
                left: (303 / 390) * deviceWidth,
                child: Container(
                  width: radio2,
                  height: radio2,
                  decoration: const BoxDecoration(
                    shape: BoxShape.circle,
                    color: Color.fromRGBO(255, 255, 255, 0.2),
                    gradient: LinearGradient(
                      begin: Alignment(-0.19438030913, -1),
                      end: Alignment(0.19438030913, 1),
                      colors: [
                        Color.fromRGBO(255, 6, 0, 0.38),
                        Color.fromRGBO(130, 239, 255, 0),
                      ],
                      stops: [0.081, 0.9503],
                    ),
                  ),
                ),
              ),
              Positioned(
                bottom: 0,
                left: 0,
                child: Container(
                  width: radio2,
                  height: radio2,
                  decoration: const BoxDecoration(
                    shape: BoxShape.circle,
                    color: Color.fromRGBO(255, 255, 255, 0.2),
                    gradient: LinearGradient(
                      begin: Alignment(-0.19438030913, -1),
                      end: Alignment(0.19438030913, 1),
                      colors: [
                        Color.fromRGBO(255, 6, 0, 0.38),
                        Color.fromRGBO(130, 239, 255, 0),
                      ],
                      stops: [0.081, 0.9503],
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

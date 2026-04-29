import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtAppbar3 extends StatelessWidget {
  const CtAppbar3({
    super.key,
    required this.onBack,
  });

  final void Function() onBack;
  @override
  Widget build(BuildContext context) {
    EdgeInsets safeAreaPadding = MediaQuery.of(context).padding;

    return Container(
      height: 124 + safeAreaPadding.top,
      padding: EdgeInsets.only(top: safeAreaPadding.top, left: 24, right: 24),
      decoration: const BoxDecoration(
        color: AppColors.primary700,
        borderRadius: BorderRadius.only(
          bottomLeft: Radius.circular(24),
          bottomRight: Radius.circular(24),
        ),
      ),
      child: Stack(
        children: [
          Center(
            child: Image.asset(
              'assets/images/logo_blanco.png',
              height: 37,
            ),
          ),
          Positioned(
            child: SizedBox(
              height: double.infinity,
              // color: Colors.white,
              child: Align(
                alignment: Alignment.centerLeft,
                child: GestureDetector(
                  onTap: () {
                    onBack();
                  },
                  child: SvgPicture.asset(
                    'assets/icons/shared/chevron-left.svg',
                    height: 24,
                  ),
                ),
              ),
            ),
          )
        ],
      ),
    );
  }
}

import 'dart:io';

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class CtAppbar extends StatelessWidget implements PreferredSizeWidget {
  @override
  final Size preferredSize = const Size(double.infinity, 48);

  const CtAppbar({super.key});

  @override
  Widget build(BuildContext context) {
    EdgeInsets safeAreaPadding = MediaQuery.of(context).padding;

    return AppBar(
      systemOverlayStyle: Platform.isIOS
          ? const SystemUiOverlayStyle(
              statusBarBrightness: Brightness.dark,
              statusBarIconBrightness: Brightness.dark,
            )
          : const SystemUiOverlayStyle(
              statusBarBrightness: Brightness.light,
              statusBarIconBrightness: Brightness.light,
            ),
      flexibleSpace: Container(
        padding: EdgeInsets.only(top: safeAreaPadding.top, left: 24, right: 24),
        height: preferredSize.height +
            safeAreaPadding.top, // Establece la altura personalizada
        color: AppColors.primary700, // Color de fondo del AppBar personalizado
        alignment: Alignment.center,
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Image.asset(
              'assets/images/logo_blanco.png',
              width: 63,
            ),
            Row(
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                GestureDetector(
                  onTap: () {
                    context.push('/novedades/menu');
                  },
                  child: SvgPicture.asset(
                    'assets/icons/megaphone.svg',
                    height: 24,
                  ),
                ),
                const SizedBox(
                  width: 13,
                ),
                GestureDetector(
                  onTap: () {
                    context.push('/configuracion');
                  },
                  child: SvgPicture.asset(
                    'assets/icons/user.svg',
                    height: 24,
                  ),
                ),
              ],
            )
          ],
        ),
      ),
    );
  }
}

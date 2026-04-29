import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

enum SnackbarType { info, success, error, floating }

class SnackbarService {
  void showSnackbar({
    required BuildContext context,
    required String message,
    SnackbarType type = SnackbarType.info,
    Duration duration = const Duration(milliseconds: 3500),
  }) {
    if (type == SnackbarType.floating) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Container(
            padding: const EdgeInsets.symmetric(
              horizontal: 17,
              vertical: 22,
            ),
            decoration: const BoxDecoration(
              color: AppColors.primary200,
              boxShadow: AppColors.shadowSm,
            ),
            child: Row(
              children: [
                SvgPicture.asset(
                  'assets/icons/send-4.svg',
                  height: 24,
                ),
                const SizedBox(
                  width: 16,
                ),
                Text(
                  message,
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ],
            ),
          ),
          elevation: 0,
          behavior: SnackBarBehavior.floating,
          margin: EdgeInsets.only(
            bottom: MediaQuery.of(context).size.height - 160,
            left: 20,
            right: 20,
          ),
          padding: EdgeInsets.zero,
          backgroundColor: AppColors.primary200,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(
              8,
            ),
          ),
        ),
      );
    } else {
      Color snackbarColor;
      Color textColor;

      switch (type) {
        case SnackbarType.info:
          snackbarColor = Colors.black54;
          textColor = AppColors.white;

          break;
        case SnackbarType.success:
          snackbarColor = Colors.green;
          textColor = AppColors.gray900;

          break;
        case SnackbarType.error:
          snackbarColor = AppColors.primary200;
          textColor = AppColors.gray900;

          break;
        case SnackbarType.floating:
          snackbarColor = AppColors.primary200;
          textColor = AppColors.gray900;

          break;
      }

      ScaffoldMessenger.of(context).hideCurrentSnackBar();
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
        content: Text(
          message,
          style: TextStyle(
            color: textColor,
            fontWeight: FontWeight.w400,
          ),
        ),
        backgroundColor: snackbarColor,
        duration: duration,
      ));
    }
  }
}

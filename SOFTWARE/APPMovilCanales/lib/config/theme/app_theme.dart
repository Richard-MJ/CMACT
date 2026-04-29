import 'package:flutter/material.dart';

class AppTheme {
  ThemeData getTheme() => ThemeData(
        useMaterial3: true,
        colorScheme: ColorScheme.fromSwatch(
          accentColor: Colors.black12,
          backgroundColor: Colors.white,
        ),
        textButtonTheme: TextButtonThemeData(
          style: TextButton.styleFrom(
            foregroundColor: Colors.black12,
          ),
        ),
        scaffoldBackgroundColor: Colors.white,
        textSelectionTheme: TextSelectionThemeData(
          cursorColor: Colors.grey[700],
        ),
        dialogTheme: const DialogThemeData(
          backgroundColor: Colors.white,
          surfaceTintColor: Colors.transparent,
        ),
      );

  static double modalBorderRadius = 16;
  static ShapeBorder modalShape = RoundedRectangleBorder(
    borderRadius: BorderRadius.circular(modalBorderRadius),
  );
  static EdgeInsets modalInsetPadding = const EdgeInsets.symmetric(
    horizontal: 24,
  );
}

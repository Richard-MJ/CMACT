import 'package:flutter/services.dart';

class MontoFormatter extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(
      TextEditingValue oldValue, TextEditingValue newValue) {
    try {
      final text = newValue.text;
      if (text.isEmpty) return newValue;
      double.parse(text); // Valida que sea un número

      if (!text.contains('.') && text.length > 7) {
        return oldValue; // No mas de 7 dígitos
      }

      if (text.contains('.') && text.split('.')[0].length > 7) {
        return oldValue; // No mas de 7 dígitos
      }
      
      if (text.contains('.') && text.split('.')[1].length > 2) {
        return oldValue; // Más de 2 decimales
      }
      return newValue;
    } catch (e) {
      return oldValue; // No se puede analizar como un número
    }
  }
}

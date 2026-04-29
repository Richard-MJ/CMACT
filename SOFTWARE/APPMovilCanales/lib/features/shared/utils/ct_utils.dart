import 'package:intl/intl.dart';
import 'package:url_launcher/url_launcher.dart';

class CtUtils {
  static String hashearCorreo(String? correo) {
    if (correo == null) return '';
    if (correo.contains('@')) {
      final nombre = correo.substring(
          0, correo.indexOf('@')); // Obtiene la parte antes del @
      final dominio = correo
          .substring(correo.indexOf('@')); // Obtiene la parte después del @

      String nombreHasheado = '';
      if (nombre.length > 4) {
        nombreHasheado = nombre.substring(0, 4) +
            '*' * (nombre.length - 4); // Hashea el nombre
      } else {
        nombreHasheado = '${nombre.substring(0, nombre.length - 1)}*';
      }

      return nombreHasheado +
          dominio; // Combina el nombre hasheado con el dominio
    } else {
      // No se encontró el carácter '@', se devuelve el correo original
      return correo;
    }
  }

  static String formatNumeroCuenta({required String? numeroCuenta, bool hash = false}) {
    if (numeroCuenta == null || numeroCuenta.isEmpty) return '';
    return _formatGrupos(numeroCuenta, [3, 4, 4], hash: hash);
  }

  static String formatNumeroCCI({required String? numerocci, bool hash = false}) {
    if (numerocci == null || numerocci.isEmpty) return '';
    return _formatGrupos(numerocci, [3, 4, 4, 9], hash: hash);
  }

  static String _formatGrupos(String numero, List<int> grupos, {bool hash = false}) {
    final buffer = StringBuffer();
    int index = 0;

    final int limiteVisible = numero.length >= 4 ? numero.length - 4 : 0;

    for (final length in grupos) {
      for (int i = 0; i < length && index < numero.length; i++) {
        final char = (hash && index < limiteVisible) ? '*' : numero[index];
        buffer.write(char);
        index++;
      }
      if (index < numero.length) buffer.write(' ');
    }

    while (index < numero.length) {
      final char = (hash && index < limiteVisible) ? '*' : numero[index];
      buffer.write(char);
      index++;
    }

    return buffer.toString();
  }

  static String formatNumeroTarjeta({
    required String? numeroCuenta,
    bool hash = false,
  }) {
    if (numeroCuenta == null) return '';

    final StringBuffer result = StringBuffer();
    int groupLength = 0;

    for (int i = 0; i < numeroCuenta.length; i++) {
      if ([4, 8, 12].contains(groupLength)) {
        // Agrega un espacio después de cada grupo de 3, 4 y 4 dígitos
        result.write(' ');
      }

      if ([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11].contains(groupLength) &&
          hash) {
        result.write('*');
      } else {
        result.write(numeroCuenta[i]);
      }
      groupLength++;
    }

    return result.toString();
  }

  static String formatCurrency(double? amount, String? currencySymbol) {
    NumberFormat currencyFormat = NumberFormat.currency(
      locale: 'en',
      symbol: currencySymbol != null
          ? '${currencySymbol.replaceAll(RegExp(r'\s+'), '')} '
          : '',
    );

    return currencyFormat.format(amount ?? 0);
  }

  static String formatDate(DateTime? dateTime) {
    if (dateTime != null) {
      final formatter = DateFormat('dd MMM y', 'es');
      return formatter.format(dateTime);
    }
    return '';
  }

  static String formatDateHorasMinutosSegundos(DateTime? dateTime) {
    if (dateTime != null) {
      final formatter = DateFormat('dd/MM/yyyy HH:mm:ss', 'es');
      return formatter.format(dateTime);
    }
    return '';
  }

  static String formatTime(DateTime? dateTime) {
    if (dateTime != null) {
      final formatter = DateFormat('HH:mm:ss \'hrs.\'');
      return formatter.format(dateTime);
    }
    return '';
  }

  static String capitalize(String s, {bool allWords = false}) {
    if (s.isEmpty) {
      return '';
    }
    s = s.trim();
    if (allWords) {
      var words = s.split(' ');
      var capitalized = [];
      for (var w in words) {
        capitalized.add(capitalize(w));
      }
      return capitalized.join(' ');
    } else {
      return s.substring(0, 1).toUpperCase() + s.substring(1).toLowerCase();
    }
  }

  static String? encodeQueryParameters(Map<String, String> params) {
    return params.entries
        .map((MapEntry<String, String> e) =>
            '${Uri.encodeComponent(e.key)}=${Uri.encodeComponent(e.value)}')
        .join('&');
  }

  static String removeSpaces(String value) {
    return value.replaceAll(' ', '');
  }

  //formatea el un string de un monto a dos decimales
  static String formatStringWithTwoDecimals(String input) {
    if (input.isEmpty) return input;
    if (num.tryParse(input) == null) return '';

    String formattedString = input;

    if (input.contains('.')) {
      // Si ya contiene un punto decimal, simplemente truncamos o agregamos ceros para obtener dos decimales
      List<String> parts = input.split('.');
      if (parts.length == 2) {
        if (parts[1].length == 1) {
          // Si solo hay un decimal, agregamos un cero
          formattedString = '$input' '0';
        } else if (parts[1].length > 2) {
          // Si hay más de dos decimales, truncamos
          formattedString = '${parts[0]}.${parts[1].substring(0, 2)}';
        }
      }
    } else {
      // Si no tiene punto decimal, agregamos .00 al final
      formattedString = '$input.00';
    }

    return formattedString;
  }

  static bool isSafeInput(String input) {
    // Lista de palabras clave SQL comunes
    List<String> palabrasClaveSQL = [
      "SELECT",
      "INSERT",
      "UPDATE",
      "DELETE",
      "CREATE",
      "ALTER",
      "DROP",
      "TABLE",
      "DATABASE",
      "FROM",
      "WHERE",
      "ORDER BY",
      "GROUP BY",
      "JOIN",
      "AND",
      "OR",
      "HAVING",
      "LIMIT",
      "OFFSET",
      "UNION",
      "INNER",
      "LEFT",
      "RIGHT",
      "OUTER",
      "ON",
      "AS",
      "INTO",
      "VALUES",
      "SET",
      "INDEX",
      "PRIMARY",
      "FOREIGN",
      "KEY",
      "UNIQUE",
      "CHECK",
      "DEFAULT",
      "CASCADE",
      "CONSTRAINT",
      "IF",
      "EXISTS"
    ];

    // Dividir la cadena en palabras
    List<String> palabras = input.toUpperCase().split(RegExp(r'\W+'));

    // Verificar si alguna palabra coincide exactamente con una palabra clave SQL
    for (String palabra in palabras) {
      if (palabrasClaveSQL.contains(palabra)) {
        return false; // La cadena contiene código SQL
      }
    }

    return true; // No se encontró código SQL en la cadena
  }

  static Future<void> abrirWeb({required String url}) async {
    final Uri uri = Uri.parse(url);
    if (await canLaunchUrl(uri)) {
      launchUrl(uri, mode: LaunchMode.externalApplication);
    }
  }

  static String truncateString(String input, int start, int maxLength) {
    if (start < 0 || start >= input.length) {
      return input;
    }

    String truncated = input.substring(start);

    if (truncated.length > maxLength) {
      truncated = '${truncated.substring(0, maxLength - 3)}...';
    }

    return truncated;
  }

  static String formatearVencimientoTarjeta(
      {required DateTime fechaVencimientoTarjeta}) {
    return DateFormat('MM/yy').format(fechaVencimientoTarjeta);
  }

  static String hashNumeroCelular(String? numeroCelular) {
    if (numeroCelular == null || numeroCelular.length < 5) return '';

    final parteOculta = '*' * 5;
    final parteVisible = numeroCelular.substring(5);
    return parteOculta + parteVisible;
  }

  static bool validarEstructuraNumeroCci(String cci) {
    return RegExp(r'^\d{20}$').hasMatch(cci);
  }

  static bool validarEstructuraNumeroTarjeta(String digitos) {
    if (!RegExp(r'^\d+$').hasMatch(digitos) || digitos.length <= 14) {
      return false;
    }

    List<int> numeros = digitos
        .split('')
        .reversed
        .map((c) => int.parse(c))
        .toList();

    int suma = 0;
    for (int i = 0; i < numeros.length; i++) {
      int numero = numeros[i];
      if (i % 2 != 0) {
        numero *= 2;
        if (numero > 9) numero -= 9;
      }
      suma += numero;
    }

    return suma % 10 == 0;
  }
}

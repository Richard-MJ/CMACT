import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_tercero.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputCuentaDestino extends StatefulWidget {
  const InputCuentaDestino({
    super.key,
    required this.numeroCuenta,
    required this.onChanged,
  });

  final NumeroCuentaTercero numeroCuenta;
  final void Function(NumeroCuentaTercero value) onChanged;
  @override
  State<InputCuentaDestino> createState() => _InputCuentaDestinoState();
}

class _InputCuentaDestinoState extends State<InputCuentaDestino> {
  final TextEditingController controller = TextEditingController();
  final _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChanged(NumeroCuentaTercero.dirty(widget.numeroCuenta.value));
      }
    });
  }

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  onChange(String value) {
    String newValue = CtUtils.removeSpaces(value);
    widget.onChanged(
      widget.numeroCuenta.isPure
          ? NumeroCuentaTercero.pure(newValue)
          : NumeroCuentaTercero.dirty(newValue),
    );
  }

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: addSpaces(widget.numeroCuenta.value),
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.numeroCuenta.errorMessage,
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Expanded(
            child: TextFormField(
              onTapOutside: (event) {
                FocusManager.instance.primaryFocus?.unfocus();
              },
              style: const TextStyle(
                color: AppColors.gray800,
                fontSize: 16,
                fontWeight: FontWeight.w400,
              ),
              decoration: const InputDecoration(
                border: InputBorder.none,
                contentPadding: EdgeInsets.zero,
                hintText: '001 5125 9658 5478',
                isDense: true,
                hintStyle: TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
              ),
              keyboardType: TextInputType.number,
              onChanged: (value) {
                onChange(value);
              },
              controller: controller,
              inputFormatters: [
                LengthLimitingTextInputFormatter(18),
                CardFormatter()
              ],
              focusNode: _focusNode,
              enableSuggestions: false,
            ),
          ),
        ],
      ),
    );
  }
}

String addSpaces(String value) {
  final StringBuffer result = StringBuffer();
  int groupLength = 0;
  String stringWithoutSpaces = CtUtils.removeSpaces(value);

  for (int i = 0; i < stringWithoutSpaces.length; i++) {
    result.write(stringWithoutSpaces[i]);
    if ([2, 6, 10].contains(groupLength)) {
      // Agrega un espacio después de cada grupo de 3, 4 y 4 dígitos, al final se obtiene grupos de [3,4,4,4] dígitos
      result.write(' ');
    }
    groupLength++;
  }

  return result.toString();
}

int offset(int selectionEnd) {
  if (selectionEnd >= 0 && selectionEnd <= 2) {
    return 0;
  }
  if (selectionEnd >= 3 && selectionEnd <= 7) {
    return 1;
  }
  if (selectionEnd >= 8 && selectionEnd <= 12) {
    return 2;
  }

  return 3;
}

class CardFormatter extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(
      TextEditingValue oldValue, TextEditingValue newValue) {
    // Rechazar la entrada si ingresan espacio
    if (CtUtils.removeSpaces(oldValue.text) ==
            CtUtils.removeSpaces(newValue.text) &&
        (newValue.text.length > oldValue.text.length)) {
      return oldValue;
    }

    final regex = RegExp(r'^[0-9]*$');
    if (!regex.hasMatch(CtUtils.removeSpaces(newValue.text))) {
      return oldValue; // Rechazar la entrada si no contiene solo números
    }

    var newText = addSpaces(newValue.text);

    //limitar a 18 caracteres, esto es porque al pegar de la papelera
    //el LengthLimitingTextInputFormatter(18) no funcionaba
    if (newText.length > 18) {
      newText = newText.substring(0, 18);
    }

    //variable para saber cuanto hay que aumentarle o disminuirle al puntero
    var addSelection = 0;

    // al agregar caracteres, funciona para cuando se pega del portapapeles
    if (newValue.text.length > oldValue.text.length) {
      addSelection =
          offset(newValue.selection.end) - offset(oldValue.selection.end);
      //al agregar caracter, cuando el puntero esté en las posiciones indicadas se le aumentará una posicion
      if ([3, 8, 13].contains(oldValue.selection.end)) {
        addSelection = addSelection + 1;
      }
    }
    //al remover caracter, cuando el puntero esté en las posiciones indicadas se le restara una posicion
    if (newValue.text.length < oldValue.text.length) {
      if ([4, 9, 14].contains(newValue.selection.end)) {
        addSelection = -1;
      }
    }

    return newValue.copyWith(
      text: newText,
      selection: TextSelection.collapsed(
        offset: newValue.selection.end + addSelection > 18
            ? 18
            : newValue.selection.end + addSelection,
      ),
    );
  }
}

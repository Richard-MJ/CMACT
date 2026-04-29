import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/transferencias/inputs/numero_cuenta_cci.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputCuentaDestinoCci extends StatefulWidget {
  const InputCuentaDestinoCci({
    super.key,
    required this.numeroCuenta,
    required this.onChanged,
    this.readOnly = false,
  });

  final NumeroCuentaCci numeroCuenta;
  final void Function(NumeroCuentaCci value) onChanged;
  final bool readOnly;
  @override
  State<InputCuentaDestinoCci> createState() => _InputCuentaDestinoCciState();
}

class _InputCuentaDestinoCciState extends State<InputCuentaDestinoCci> {
  final TextEditingController controller = TextEditingController();
  final _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChanged(NumeroCuentaCci.dirty(widget.numeroCuenta.value));
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
          ? NumeroCuentaCci.pure(newValue)
          : NumeroCuentaCci.dirty(newValue),
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
              style: TextStyle(
                color: widget.readOnly
                    ? AppColors.inputReadOnly
                    : AppColors.gray800,
                fontSize: 16,
                fontWeight: FontWeight.w400,
              ),
              decoration: const InputDecoration(
                border: InputBorder.none,
                contentPadding: EdgeInsets.zero,
                hintText: '001 123 123456783210 12',
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
              readOnly: widget.readOnly,
              inputFormatters: [
                //20 caracteres del numero de cuenta + 3 espacios
                LengthLimitingTextInputFormatter(23),
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
    if ([2, 5, 17].contains(groupLength)) {
      // Agrega un espacio después de cada grupo de 3, 3 y 12 dígitos, al final se obtiene grupos de [3,3,12,2] dígitos
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
  if (selectionEnd >= 3 && selectionEnd <= 6) {
    return 1;
  }
  if (selectionEnd >= 7 && selectionEnd <= 19) {
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

    //limitar a 23 caracteres, esto es porque al pegar de la papelera
    //el LengthLimitingTextInputFormatter(23) no funcionaba
    if (newText.length > 23) {
      newText = newText.substring(0, 23);
    }

    //variable para saber cuanto hay que aumentarle o disminuirle al puntero
    var addSelection = 0;

    // al agregar caracteres, funciona para cuando se pega del portapapeles
    if (newValue.text.length > oldValue.text.length) {
      addSelection =
          offset(newValue.selection.end) - offset(oldValue.selection.end);
      //al agregar caracter, cuando el puntero esté en las posiciones indicadas se le aumentará una posicion
      if ([3, 7, 20].contains(oldValue.selection.end)) {
        addSelection = addSelection + 1;
      }
    }
    //al remover caracter, cuando el puntero esté en las posiciones indicadas se le restara una posicion
    if (newValue.text.length < oldValue.text.length) {
      if ([4, 8, 21].contains(newValue.selection.end)) {
        addSelection = -1;
      }
    }

    return newValue.copyWith(
      text: newText,
      selection: TextSelection.collapsed(
        offset: newValue.selection.end + addSelection > 23
            ? 23
            : newValue.selection.end + addSelection,
      ),
    );
  }
}

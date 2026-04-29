import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtInputNumeroTarjeta extends StatefulWidget {
  const CtInputNumeroTarjeta({
    super.key,
    required this.numeroTarjeta,
    required this.onChanged,
    this.withObscureText = false,
  });

  @override
  State<CtInputNumeroTarjeta> createState() => _CtInputNumeroTarjetaState();

  final NumeroTarjeta numeroTarjeta;
  final void Function(NumeroTarjeta value) onChanged;
  final bool withObscureText;
}

class _CtInputNumeroTarjetaState extends State<CtInputNumeroTarjeta> {
  @override
  void initState() {
    super.initState();
    setState(() {
      showText = false;
    });
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChanged(NumeroTarjeta.dirty(widget.numeroTarjeta.value));
      }
    });
  }

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  bool showText = false;
  final FocusNode _focusNode = FocusNode();
  final TextEditingController controllerNumeroTarjeta =
      ObscuringTextEditingController();
  @override
  Widget build(BuildContext context) {
    controllerNumeroTarjeta.value = controllerNumeroTarjeta.value.copyWith(
      text: addSpaces(widget.numeroTarjeta.value),
      selection: TextSelection.collapsed(
        offset: controllerNumeroTarjeta.selection.end,
      ),
    );

    return CtTextFieldContainer(
      padding: const EdgeInsets.only(left: 14, right: 4),
      errorMessage: widget.numeroTarjeta.errorMessage,
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          SvgPicture.asset(
            'assets/icons/visa.svg',
            height: 24,
          ),
          const SizedBox(
            width: 8,
          ),
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
                hintText: 'Número de tarjeta',
                isDense: true,
                hintStyle: TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
              ),
              controller: showText ? null : controllerNumeroTarjeta,
              keyboardType: TextInputType.number,
              inputFormatters: [
                CardFormatter(),
                LengthLimitingTextInputFormatter(19)
              ],
              onChanged: (value) {
                String newValue = CtUtils.removeSpaces(value);
                widget.onChanged(
                  widget.numeroTarjeta.isPure
                      ? NumeroTarjeta.pure(newValue)
                      : NumeroTarjeta.dirty(newValue),
                );
              },
              focusNode: _focusNode,
              enableSuggestions: false,
            ),
          ),
          const SizedBox(
            width: 8,
          ),
          widget.withObscureText
              ? SizedBox(
                  width: 36,
                  height: 36,
                  child: TextButton(
                    style: TextButton.styleFrom(
                      shape: const CircleBorder(),
                      padding: EdgeInsets.zero,
                    ),
                    onPressed: () {
                      setState(() {
                        showText = !showText;
                      });
                    },
                    child: SvgPicture.asset(
                      showText
                          ? 'assets/icons/eye_closed.svg'
                          : 'assets/icons/eye.svg',
                      width: 16,
                      height: 16,
                      colorFilter: const ColorFilter.mode(
                          AppColors.gray400, BlendMode.srcIn),
                    ),
                  ),
                )
              : const SizedBox(
                  width: 10,
                ),
        ],
      ),
    );
  }
}

int offset(int selectionEnd) {
  if (selectionEnd >= 0 && selectionEnd <= 3) {
    return 0;
  }
  if (selectionEnd >= 4 && selectionEnd <= 8) {
    return 1;
  }
  if (selectionEnd >= 9 && selectionEnd <= 13) {
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

    //limitar a 19 caracteres, esto es porque al pegar de la papelera
    //el LengthLimitingTextInputFormatter(19) no funcionaba
    if (newText.length > 19) {
      newText = newText.substring(0, 19);
    }

    //variable para saber cuanto hay que aumentarle o disminuirle al puntero
    var addSelection = 0;

    // al agregar caracteres, funciona para cuando se pega del portapapeles
    if (newValue.text.length > oldValue.text.length) {
      addSelection =
          offset(newValue.selection.end) - offset(oldValue.selection.end);
      //al agregar caracter, cuando el puntero esté en las posiciones indicadas se le aumentará una posicion
      if ([4, 9, 14].contains(oldValue.selection.end)) {
        addSelection = addSelection + 1;
      }
    }
    //al remover caracter, cuando el puntero esté en las posiciones indicadas se le restara una posicion
    if (newValue.text.length < oldValue.text.length) {
      if ([5, 10, 15].contains(newValue.selection.end)) {
        addSelection = -1;
      }
    }

    return newValue.copyWith(
      text: newText,
      selection: TextSelection.collapsed(
        offset: newValue.selection.end + addSelection > 19
            ? 19
            : newValue.selection.end + addSelection,
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
    if ([3, 7, 11].contains(groupLength)) {
      // Agrega un espacio después de cada grupo de 4 dígitos, al final se obtiene 4 grupos de 4 dígitos
      result.write(' ');
    }
    groupLength++;
  }

  return result.toString();
}

class ObscuringTextEditingController extends TextEditingController {
  @override
  TextSpan buildTextSpan(
      {required BuildContext context,
      required bool withComposing,
      TextStyle? style}) {
    var displayValue = '';
    for (var i = 0; i < value.text.length; i++) {
      final String char;

      if ((i + 1) % 5 == 0) {
        char = ' ';
      } else {
        if (i + 1 >= value.text.length - 4) {
          char = value.text[i];
        } else {
          char = '*';
        }
      }

      displayValue = '$displayValue$char';
    }

    if (!value.composing.isValid || !withComposing) {
      return TextSpan(style: style, text: displayValue);
    }
    final TextStyle? composingStyle = style?.merge(
      const TextStyle(decoration: TextDecoration.underline),
    );
    return TextSpan(
      style: style,
      children: <TextSpan>[
        TextSpan(text: value.composing.textBefore(displayValue)),
        TextSpan(
          style: composingStyle,
          text: value.composing.textInside(displayValue),
        ),
        TextSpan(text: value.composing.textAfter(displayValue)),
      ],
    );
  }
}

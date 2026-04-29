import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/billetera_virtual/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputCelular extends StatefulWidget {
  const InputCelular({
    super.key,
    required this.onChangeNumeroCelular,
    required this.numeroCelular,
    this.enable = false,
    this.withObscureText = false,
    this.colorOscure = true,
    this.showText = false,
    this.placeholder = 'Número de celular',
  });

  final NumeroCelular numeroCelular;
  final bool enable;
  final bool withObscureText;
  final bool colorOscure;
  final bool showText;
  final String placeholder;
  final void Function(NumeroCelular numeroCelular) onChangeNumeroCelular;

  @override
  State<InputCelular> createState() => _InputCelularState();
}

class _InputCelularState extends State<InputCelular> {
  bool showText = false;
  final _focusNode = FocusNode();
  final TextEditingController controllerNumeroCelular = ObscuringTextEditingController();

  @override

  @override
  void initState() {
    super.initState();
    setState(() {
      showText = false;
    });
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeNumeroCelular(
          NumeroCelular.dirty(
            widget.numeroCelular.value,
          ),
        );
      }
    });
  }

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    controllerNumeroCelular.value = controllerNumeroCelular.value.copyWith(
      text: widget.numeroCelular.value,
      selection: TextSelection.collapsed(
        offset: controllerNumeroCelular.selection.end,
      ),
    );

    return CtTextFieldContainer(
      color: widget.colorOscure ? AppColors.gray100 : AppColors.white,
      errorMessage: widget.numeroCelular.errorMessage,
      padding: EdgeInsets.zero,
      child: Row(
        children: [
          Container(
            height: double.infinity,
            padding: const EdgeInsets.only(
              left: 14,
              right: 8,
            ),
            child: SvgPicture.asset(
              'assets/icons/phone.svg',
              height: 20,
              colorFilter: ColorFilter.mode(
                (widget.numeroCelular.isNotValid &&
                        !widget.numeroCelular.isPure)
                    ? AppColors.error500
                    : AppColors.gray500,
                BlendMode.srcIn,
              ),
            ),
          ),
          Expanded(
            child: TextFormField(
              onTapOutside: (event) {
                FocusManager.instance.primaryFocus?.unfocus();
              },
              style: const TextStyle(
                color: AppColors.gray700,
                fontSize: 16,
                fontWeight: FontWeight.w400,
              ),
              decoration: InputDecoration(
                border: InputBorder.none,
                contentPadding: EdgeInsets.zero,
                hintText: widget.placeholder,
                isDense: true,
                hintStyle: const TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
                enabled: widget.enable,
              ),
              controller: showText || widget.showText ? null : controllerNumeroCelular,
              keyboardType: TextInputType.number,
              inputFormatters: [
                NumeroCelularFormatter(),
                LengthLimitingTextInputFormatter(9)
              ],
              onChanged: (value) {
                String newValue = CtUtils.removeSpaces(value);
                widget.onChangeNumeroCelular(
                  widget.numeroCelular.isPure
                      ? NumeroCelular.pure(newValue)
                      : NumeroCelular.dirty(newValue),
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
                      !showText ? 'assets/icons/eye.svg' : 'assets/icons/eye_closed.svg',
                      width: 16,
                      height: 16,
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

class NumeroCelularFormatter extends TextInputFormatter {
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

    return newValue;
  }
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
        char = '';
      } else {
        if (i + 1 >= value.text.length - 2) {
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

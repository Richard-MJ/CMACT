import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/recarga_virtual/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputCelular extends StatefulWidget {
  const InputCelular({
    super.key,
    required this.onChangeNumeroCelular,
    required this.numeroCelular,
  });

  final NumeroCelular numeroCelular;

  final void Function(NumeroCelular numeroCelular) onChangeNumeroCelular;

  @override
  State<InputCelular> createState() => _InputCelularState();
}

class _InputCelularState extends State<InputCelular> {
  final _focusNode = FocusNode();
  final TextEditingController controllerNumeroCelular = TextEditingController();

  @override
  void initState() {
    super.initState();
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
      errorMessage: widget.numeroCelular.errorMessage,
      padding: EdgeInsets.zero,
      child: Row(
        children: [
          Container(
            height: double.infinity,
            padding: const EdgeInsets.only(
              left: 14,
              right: 12,
            ),
            decoration: BoxDecoration(
              border: Border(
                right: BorderSide(
                  color: (widget.numeroCelular.isNotValid &&
                          !widget.numeroCelular.isPure)
                      ? AppColors.error500
                      : AppColors.gray300,
                ),
              ),
            ),
            child: Center(
              child: Text(
                '+51',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                  color: (widget.numeroCelular.isNotValid &&
                          !widget.numeroCelular.isPure)
                      ? AppColors.error500
                      : AppColors.gray500,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ),
          ),
          Expanded(
            child: Container(
              padding: const EdgeInsets.symmetric(
                horizontal: 14,
              ),
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
                  isDense: true,
                  hintStyle: TextStyle(
                    color: AppColors.gray500,
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                  ),
                ),
                keyboardType: TextInputType.number,
                controller: controllerNumeroCelular,
                inputFormatters: [
                  NumeroCelularFormatter(),
                  LengthLimitingTextInputFormatter(9),
                ],
                onChanged: (value) {
                  widget.onChangeNumeroCelular(
                    widget.numeroCelular.isPure
                        ? NumeroCelular.pure(value)
                        : NumeroCelular.dirty(value),
                  );
                },
                focusNode: _focusNode,
                enableSuggestions: false,
              ),
            ),
          )
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

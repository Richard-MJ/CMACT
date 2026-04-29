import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/perfil/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';

class InputCelular extends StatefulWidget {
  const InputCelular({
    super.key,
    required this.onChangeNumeroCelular,
    required this.numeroCelular,
    required this.hintText,
  });

  final NumeroCelular numeroCelular;
  final String? hintText;
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
            child: Container(
              padding: const EdgeInsets.only(right: 14),
              child: TextFormField(
                onTapOutside: (event) {
                  FocusManager.instance.primaryFocus?.unfocus();
                },
                style: const TextStyle(
                  color: AppColors.gray800,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
                decoration: InputDecoration(
                  border: InputBorder.none,
                  contentPadding: EdgeInsets.zero,
                  isDense: true,
                  hintStyle: const TextStyle(
                    color: AppColors.gray500,
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                  ),
                  hintText: widget.hintText,
                ),
                keyboardType: TextInputType.number,
                controller: controllerNumeroCelular,
                inputFormatters: [
                  FilteringTextInputFormatter.digitsOnly,
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

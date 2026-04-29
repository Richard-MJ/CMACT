import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/perfil/inputs/apodo.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';

class InputApodo extends StatefulWidget {
  const InputApodo({
    super.key,
    required this.onChangeApodo,
    required this.apodo,
    required this.hintText,
  });

  final Apodo apodo;
  final String? hintText;
  final void Function(Apodo apodo) onChangeApodo;

  @override
  State<InputApodo> createState() => _InputApodoState();
}

class _InputApodoState extends State<InputApodo> {
  final _focusNode = FocusNode();
  final TextEditingController controller = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeApodo(
          Apodo.dirty(
            widget.apodo.value,
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
    controller.value = controller.value.copyWith(
      text: widget.apodo.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.apodo.errorMessage,
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
              'assets/icons/user.svg',
              height: 20,
              colorFilter: ColorFilter.mode(
                (widget.apodo.isNotValid && !widget.apodo.isPure)
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
                controller: controller,
                inputFormatters: [
                  //solo debe aceptar 20 caracteres con valores alfanuméricos.
                  FilteringTextInputFormatter.allow(
                      RegExp(r'[a-zA-Z0-9\sáéíóúÁÉÍÓÚüÜñÑ]')),
                  LengthLimitingTextInputFormatter(44),
                ],
                onChanged: (value) {
                  widget.onChangeApodo(
                    widget.apodo.isPure
                        ? Apodo.pure(value)
                        : Apodo.dirty(value),
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

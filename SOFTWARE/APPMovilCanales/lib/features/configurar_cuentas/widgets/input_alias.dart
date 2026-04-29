import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/inputs/alias.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';

class InputAliasCuentaAhorro extends StatefulWidget {
  const InputAliasCuentaAhorro({
    super.key,
    required this.onChangeAlias,
    required this.alias,
    required this.hintText,
  });

  final AliasCuentaAhorro alias;
  final String? hintText;
  final void Function(AliasCuentaAhorro alias) onChangeAlias;

  @override
  State<InputAliasCuentaAhorro> createState() => _InputAliasCuentaAhorroState();
}

class _InputAliasCuentaAhorroState extends State<InputAliasCuentaAhorro> {
  final _focusNode = FocusNode();
  final TextEditingController controller = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeAlias(
          AliasCuentaAhorro.dirty(
            widget.alias.value,
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
      text: widget.alias.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.alias.errorMessage,
      padding: const EdgeInsets.symmetric(horizontal: 14),
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
          border: const OutlineInputBorder(borderSide: BorderSide.none),
          contentPadding: EdgeInsets.zero,
          hintStyle: const TextStyle(
            color: AppColors.gray500,
            fontSize: 16,
            fontWeight: FontWeight.w400,
          ),
          hintText: widget.hintText,
        ),
        controller: controller,
        onChanged: (value) {
          widget.onChangeAlias(
            widget.alias.isPure
                ? AliasCuentaAhorro.pure(value)
                : AliasCuentaAhorro.dirty(value),
          );
        },
        focusNode: _focusNode,
        enableSuggestions: false,
      ),
    );
  }
}

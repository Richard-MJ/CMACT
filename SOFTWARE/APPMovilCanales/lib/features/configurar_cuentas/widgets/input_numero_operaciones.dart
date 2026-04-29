import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputNumeroOperaciones extends StatefulWidget {
  const InputNumeroOperaciones({
    super.key,
    required this.onChange,
    required this.value,
  });

  final String value;
  final void Function(String value) onChange;

  @override
  State<InputNumeroOperaciones> createState() => _InputNumeroOperacionesState();
}

class _InputNumeroOperacionesState extends State<InputNumeroOperaciones> {
  final _focusNode = FocusNode();
  final TextEditingController controller = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChange(
          widget.value,
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
      text: widget.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      padding: const EdgeInsets.symmetric(
        horizontal: 14,
      ),
      width: 74,
      child: TextFormField(
        onTapOutside: (event) {
          FocusManager.instance.primaryFocus?.unfocus();
        },
        style: const TextStyle(
          color: AppColors.gray800,
          fontSize: 14,
          fontWeight: FontWeight.w400,
        ),
        decoration: const InputDecoration(
          border: OutlineInputBorder(borderSide: BorderSide.none),
          contentPadding: EdgeInsets.zero,
        ),
        keyboardType: TextInputType.number,
        controller: controller,
        inputFormatters: [
          FilteringTextInputFormatter.digitsOnly,
        ],
        textAlign: TextAlign.center,
        onChanged: (value) {
          widget.onChange(
            value,
          );
        },
        focusNode: _focusNode,
        enableSuggestions: false,
      ),
    );
  }
}

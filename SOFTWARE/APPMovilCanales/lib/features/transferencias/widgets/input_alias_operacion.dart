import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';

class InputAliasOperacion extends StatefulWidget {
  const InputAliasOperacion({
    super.key,
    required this.alias,
    required this.onChanged,
  });

  final String alias;
  final void Function(String operacionFrecuente) onChanged;

  @override
  State<InputAliasOperacion> createState() => _InputAliasOperacionState();
}

class _InputAliasOperacionState extends State<InputAliasOperacion> {
  final TextEditingController controller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.alias,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
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
          border: OutlineInputBorder(borderSide: BorderSide.none),
          contentPadding: EdgeInsets.zero,
          hintText: 'Alias',
          hintStyle: TextStyle(
            color: AppColors.gray500,
            fontSize: 16,
            fontWeight: FontWeight.w400,
          ),
        ),
        controller: controller,
        onChanged: (value) {
          widget.onChanged(value);
        },
        enableSuggestions: false,
      ),
    );
  }
}

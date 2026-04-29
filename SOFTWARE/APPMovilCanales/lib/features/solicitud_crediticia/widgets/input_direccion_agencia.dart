import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';

class InputDireccionAgencia extends StatefulWidget {
  const InputDireccionAgencia({
    super.key,
    required this.direccionAgencia,
  });

  final String direccionAgencia;

  @override
  State<InputDireccionAgencia> createState() => _InputDireccionAgenciaState();
}

class _InputDireccionAgenciaState extends State<InputDireccionAgencia> {
  final TextEditingController controller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.direccionAgencia,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      height: 90,
      padding: const EdgeInsets.symmetric(
        horizontal: 14,
        vertical: 10,
      ),
      child: TextFormField(
        onTapOutside: (event) {
          FocusManager.instance.primaryFocus?.unfocus();
        },
        maxLines: null,
        style: const TextStyle(
            color: AppColors.inputReadOnly,
            fontSize: 16,
            fontWeight: FontWeight.w400,
            height: 1.5),
        decoration: const InputDecoration(
          border: InputBorder.none,
          contentPadding: EdgeInsets.zero,
          hintText: 'Dirección de agencia',
          isDense: true,
          hintStyle: TextStyle(
            color: AppColors.gray500,
            fontSize: 16,
            fontWeight: FontWeight.w400,
            height: 1.5
          ),
        ),
        controller: controller,
        readOnly: true,
        enableSuggestions: false,
      ),
    );
  }
}

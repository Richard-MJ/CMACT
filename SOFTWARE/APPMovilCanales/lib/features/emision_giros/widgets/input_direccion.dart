import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/inputs/direccion.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';

class InputDireccion extends StatefulWidget {
  const InputDireccion({
    super.key,
    required this.direccion,
    required this.onChanged,
  });

  final Direccion direccion;
  final void Function(Direccion value) onChanged;

  @override
  State<InputDireccion> createState() => _InputDireccionState();
}

class _InputDireccionState extends State<InputDireccion> {
  final TextEditingController controller = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();

    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChanged(
          Direccion.dirty(widget.direccion.value),
        );
      }
    });
  }

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  onChange(String value) {
    widget.onChanged(
      widget.direccion.isPure ? Direccion.pure(value) : Direccion.dirty(value),
    );
  }

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.direccion.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.direccion.errorMessage,
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
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
                border: OutlineInputBorder(borderSide: BorderSide.none),
                contentPadding: EdgeInsets.zero,
                hintText: 'Dirección',
                hintStyle: TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
              ),
              onChanged: (value) {
                onChange(value);
              },
              controller: controller,
              enableSuggestions: false,
              focusNode: _focusNode,
            ),
          ),
        ],
      ),
    );
  }
}

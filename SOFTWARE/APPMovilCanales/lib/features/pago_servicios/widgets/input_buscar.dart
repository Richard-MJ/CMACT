import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button_search.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';

class InputBuscar extends StatefulWidget {
  const InputBuscar({
    super.key,
    required this.value,
    required this.onChanged,
    required this.onSubmitted,
  });

  final String value;
  final void Function(String value) onChanged;
  final void Function() onSubmitted;

  @override
  State<InputBuscar> createState() => _InputBuscarState();
}

class _InputBuscarState extends State<InputBuscar> {
  final TextEditingController controller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return Row(
      children: [
        Expanded(
          child: CtTextFieldContainer(
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
                hintText: 'Empresa',
                hintStyle: TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
              ),
              onChanged: (value) {
                widget.onChanged(value);
              },
              controller: controller,
              onFieldSubmitted: (value) {
                widget.onSubmitted();
              },
              textInputAction: TextInputAction.search,
              enableSuggestions: false,
            ),
          ),
        ),
        const SizedBox(
          width: 8,
        ),
        CtButtonSearch(
          onPressed: () {
            widget.onSubmitted();
          },
        ),
      ],
    );
  }
}

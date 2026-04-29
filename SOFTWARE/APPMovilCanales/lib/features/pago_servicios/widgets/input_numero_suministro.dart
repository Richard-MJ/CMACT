import 'dart:async';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button_search.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputNumeroSuministro extends StatefulWidget {
  const InputNumeroSuministro({
    super.key,
    required this.value,
    required this.onChanged,
    required this.onSubmitted,
  });

  final String value;
  final void Function(String value) onChanged;
  final Future<void> Function() onSubmitted;
  @override
  State<InputNumeroSuministro> createState() => _InputNumeroSuministroState();
}

class _InputNumeroSuministroState extends State<InputNumeroSuministro> {
  final TextEditingController controller = TextEditingController();

  @override
  void initState() {
    super.initState();
  }

  @override
  void dispose() {
    super.dispose();
  }

  onChange(String newValue) {
    widget.onChanged(newValue);
  }

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
                hintText: 'Ej. 123445',
                hintStyle: TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
              ),
              keyboardType: TextInputType.number,
              onChanged: (value) {
                onChange(value);
              },
              controller: controller,
              inputFormatters: [FilteringTextInputFormatter.digitsOnly],
              textInputAction: TextInputAction.search,
              onFieldSubmitted: (value) {
                widget.onSubmitted();
              },
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

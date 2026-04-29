import 'dart:async';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button_search.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputNumeroCredito extends StatefulWidget {
  const InputNumeroCredito({
    super.key,
    required this.value,
    required this.onChange,
    required this.onSubmitted,
  });

  final String value;
  final void Function(String value) onChange;
  final Future<void> Function() onSubmitted;
  @override
  State<InputNumeroCredito> createState() => _InputNumeroCreditoState();
}

class _InputNumeroCreditoState extends State<InputNumeroCredito> {
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
    widget.onChange(newValue);
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
                hintText: '1234567',
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
              inputFormatters: [
                FilteringTextInputFormatter.digitsOnly,
                LengthLimitingTextInputFormatter(10),
              ],
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

import 'dart:async';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button_search.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputCodigoPago extends StatefulWidget {
  const InputCodigoPago(
      {super.key,
      required this.value,
      required this.onChange,
      required this.onSubmitted,
      this.maxLength});

  final String value;
  final void Function(String value) onChange;
  final Future<void> Function() onSubmitted;
  final int? maxLength;
  @override
  State<InputCodigoPago> createState() => _InputCodigoPagoState();
}

class _InputCodigoPagoState extends State<InputCodigoPago> {
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
                    maxLength: widget.maxLength,
                    buildCounter: (BuildContext context,
                            {required int? currentLength,
                            required bool isFocused,
                            required int? maxLength}) =>
                        null,
                    decoration: const InputDecoration(
                      border: InputBorder.none,
                      contentPadding: EdgeInsets.zero,
                      hintText: 'Ej.: 123456',
                      isDense: true,
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
                    onFieldSubmitted: (value) {
                      widget.onSubmitted();
                    },
                    textInputAction: TextInputAction.search,
                    controller: controller,
                    inputFormatters: [FilteringTextInputFormatter.digitsOnly],
                    enableSuggestions: false,
                  ),
                ),
              ],
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

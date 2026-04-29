import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/inputs/dias_dpf.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputDiasDpf extends StatefulWidget {
  const InputDiasDpf({
    super.key,
    required this.diasDpf,
    required this.onChanged,
  });

  final DiasDpf diasDpf;
  final void Function(DiasDpf value) onChanged;

  @override
  State<InputDiasDpf> createState() => _InputDiasDpfState();
}

class _InputDiasDpfState extends State<InputDiasDpf> {
  final TextEditingController controller = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();

    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChanged(
          DiasDpf.dirty(widget.diasDpf.value),
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
      widget.diasDpf.isPure ? DiasDpf.pure(value) : DiasDpf.dirty(value),
    );
  }

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.diasDpf.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.diasDpf.errorMessage,
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
                hintText: '',
                hintStyle: TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
              ),
              keyboardType: TextInputType.number,
              inputFormatters: [FilteringTextInputFormatter.digitsOnly],
              onChanged: (value) {
                onChange(value);
              },
              controller: controller,
              enableSuggestions: false,
            ),
          ),
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/inputs/cuotas.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputCuotas extends StatefulWidget {
  const InputCuotas({
    super.key,
    required this.cuotas,
    required this.onChanged,
  });

  final Cuotas cuotas;
  final void Function(Cuotas value) onChanged;
  @override
  State<InputCuotas> createState() => _InputCuotasState();
}

class _InputCuotasState extends State<InputCuotas> {
  final TextEditingController controller = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChanged(
          Cuotas.dirty(widget.cuotas.value),
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
      text: widget.cuotas.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.cuotas.errorMessage,
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
          hintText: 'Número de cuotas en meses',
          hintStyle: TextStyle(
            color: AppColors.gray500,
            fontSize: 16,
            fontWeight: FontWeight.w400,
          ),
        ),
        keyboardType: TextInputType.number,
        onChanged: (value) {
          widget.onChanged(Cuotas.dirty(value));
        },
        controller: controller,
        inputFormatters: [
          FilteringTextInputFormatter.digitsOnly,
          LengthLimitingTextInputFormatter(2),
        ],
        focusNode: _focusNode,
        enableSuggestions: false,
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputMotivo extends StatefulWidget {
  const InputMotivo({
    super.key,
    required this.motivo,
    required this.onChange,
  });

  final String motivo;
  final void Function(String motivo) onChange;

  @override
  State<InputMotivo> createState() => _InputMotivoState();
}

class _InputMotivoState extends State<InputMotivo> {
  final TextEditingController controllerMotivo = TextEditingController();

  @override
  Widget build(BuildContext context) {
    controllerMotivo.value = controllerMotivo.value.copyWith(
      text: widget.motivo,
      selection: TextSelection.collapsed(
        offset: controllerMotivo.selection.end,
      ),
    );

    return CtTextFieldContainer(
      height: 80,
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
          color: AppColors.gray800,
          fontSize: 16,
          fontWeight: FontWeight.w400,
          height: 1.5,
        ),
        decoration: const InputDecoration(
          border: InputBorder.none,
          contentPadding: EdgeInsets.zero,
          hintText: 'Ingresa el motivo',
          isDense: true,
          hintStyle: TextStyle(
            color: AppColors.gray500,
            fontSize: 16,
            fontWeight: FontWeight.w400,
            height: 1.5,
          ),
        ),
        inputFormatters: [
          //debe ser máximo 40 caracteres, solo debe aceptar números y letras.
          FilteringTextInputFormatter.allow(RegExp(r'[a-zA-Z0-9\s]')),
          LengthLimitingTextInputFormatter(40),
        ],
        controller: controllerMotivo,
        onChanged: (value) {
          widget.onChange(value);
        },
        enableSuggestions: false,
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/inputs/nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputNombreBeneficiario extends StatefulWidget {
  const InputNombreBeneficiario({
    super.key,
    required this.nombreBeneficiario,
    required this.onChanged,
    this.readOnly = false,
  });

  final NombreBeneficiario nombreBeneficiario;
  final void Function(NombreBeneficiario value) onChanged;
  final bool readOnly;

  @override
  State<InputNombreBeneficiario> createState() =>
      _InputNombreBeneficiarioState();
}

class _InputNombreBeneficiarioState extends State<InputNombreBeneficiario> {
  final TextEditingController controller = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();

    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChanged(
          NombreBeneficiario.dirty(widget.nombreBeneficiario.value),
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
      widget.nombreBeneficiario.isPure
          ? NombreBeneficiario.pure(value)
          : NombreBeneficiario.dirty(value),
    );
  }

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.nombreBeneficiario.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.nombreBeneficiario.errorMessage,
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Expanded(
            child: TextFormField(
              onTapOutside: (event) {
                FocusManager.instance.primaryFocus?.unfocus();
              },
              style: TextStyle(
                color: widget.readOnly
                    ? AppColors.inputReadOnly
                    : AppColors.gray800,
                fontSize: 16,
                fontWeight: FontWeight.w400,
              ),
              decoration: const InputDecoration(
                border: OutlineInputBorder(borderSide: BorderSide.none),
                contentPadding: EdgeInsets.zero,
                hintText: 'Nombres y apellidos completos',
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
              readOnly: widget.readOnly,
              inputFormatters: [
                //solo debe aceptar 44 caracteres con valores alfanuméricos (ni Ñ ni tildes).
                FilteringTextInputFormatter.allow(RegExp(r'[a-zA-Z0-9\s]')),
                LengthLimitingTextInputFormatter(44),
              ],
              enableSuggestions: false,
              focusNode: _focusNode,
            ),
          ),
        ],
      ),
    );
  }
}

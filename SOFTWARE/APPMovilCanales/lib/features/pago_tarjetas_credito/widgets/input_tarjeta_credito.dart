import 'package:caja_tacna_app/features/pago_tarjetas_credito/inputs/numero_tarjeta_credito.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputTarjetaCredito extends StatefulWidget {
  const InputTarjetaCredito({
    super.key,
    required this.numeroTarjeta,
    required this.onChanged,
    this.readOnly = false,
  });

  final NumeroTarjetaCredito numeroTarjeta;
  final void Function(NumeroTarjetaCredito value) onChanged;
  final bool readOnly;

  @override
  State<InputTarjetaCredito> createState() => _InputTarjetaCreditoState();
}

class _InputTarjetaCreditoState extends State<InputTarjetaCredito> {
  final TextEditingController controller = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        final cleaned = CtUtils.removeSpaces(controller.text);
        widget.onChanged(NumeroTarjetaCredito.dirty(cleaned));
      }
    });
  }

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  void onChange(String value) {
    final cleaned = CtUtils.removeSpaces(value);
    widget.onChanged(
      widget.numeroTarjeta.isPure
          ? NumeroTarjetaCredito.pure(cleaned)
          : NumeroTarjetaCredito.dirty(cleaned),
    );
  }

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.numeroTarjeta.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.numeroTarjeta.errorMessage,
      child: TextFormField(
        onTapOutside: (_) => FocusManager.instance.primaryFocus?.unfocus(),
        style: TextStyle(
          color: widget.readOnly
              ? AppColors.inputReadOnly
              : AppColors.gray800,
          fontSize: 16,
          fontWeight: FontWeight.w400,
        ),
        decoration: InputDecoration(
          border: OutlineInputBorder(borderSide: BorderSide.none),
          contentPadding: EdgeInsets.zero,
          hintText: '4567698758691234',
          hintStyle: const TextStyle(
            color: AppColors.gray500,
            fontSize: 16,
            fontWeight: FontWeight.w400,
          ),
        ),
        keyboardType: TextInputType.number,
        onChanged: onChange,
        controller: controller,
        readOnly: widget.readOnly,
        inputFormatters: [
          FilteringTextInputFormatter.digitsOnly,
          LengthLimitingTextInputFormatter(19),
        ],
        focusNode: _focusNode,
        enableSuggestions: false,
      ),
    );
  }
}

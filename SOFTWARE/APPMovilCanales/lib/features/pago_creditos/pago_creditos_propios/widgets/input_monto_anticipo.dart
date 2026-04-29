import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/input_formatters/monto_formatter.dart';
import 'package:caja_tacna_app/features/shared/inputs/monto_anticipo.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';

class InputMontoAnticipo extends StatefulWidget {
  const InputMontoAnticipo({
    super.key,
    required this.onChangeMonto,
    required this.simboloMoneda,
    required this.monto,
    this.montoAnticipo,
  });

  final String? simboloMoneda;
  final MontoAnticipo monto;
  final double? montoAnticipo;
  final void Function(MontoAnticipo monto) onChangeMonto;

  @override
  State<InputMontoAnticipo> createState() => _InputMontoAnticipoState();
}

class _InputMontoAnticipoState extends State<InputMontoAnticipo> {
  final _focusNode = FocusNode();
  final TextEditingController controllerMonto = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeMonto(
          widget.monto.copyWith(
            value: CtUtils.formatStringWithTwoDecimals(widget.monto.value),
            isPure: false,
          ),
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
    controllerMonto.value = controllerMonto.value.copyWith(
      text: widget.monto.value,
      selection: TextSelection.collapsed(
        offset: controllerMonto.selection.end,
      ),
    );

    String? helperMessage = '';

    if (widget.montoAnticipo != null &&
        widget.monto.value.isNotEmpty &&
        double.parse(widget.monto.value) < widget.montoAnticipo!) {
      helperMessage = 'Monto anticipo: ${CtUtils.formatCurrency(
        widget.montoAnticipo,
        widget.simboloMoneda,
      )}';
    } else {
      helperMessage = null;
    }

    return CtTextFieldContainer(
      helperMessage: helperMessage,
      errorMessage: widget.monto.errorMessage,
      padding: EdgeInsets.zero,
      child: Row(
        children: [
          if (widget.simboloMoneda != null)
            Container(
              height: double.infinity,
              padding: const EdgeInsets.only(
                left: 14,
                right: 12,
              ),
              decoration: BoxDecoration(
                border: Border(
                  right: BorderSide(
                    color: (widget.monto.isNotValid && !widget.monto.isPure)
                        ? AppColors.error500
                        : AppColors.gray300,
                  ),
                ),
              ),
              child: Center(
                child: Text(
                  '${widget.simboloMoneda}',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: (widget.monto.isNotValid && !widget.monto.isPure)
                        ? AppColors.error500
                        : AppColors.gray500,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ),
            ),
          Expanded(
            child: Container(
              padding: const EdgeInsets.symmetric(
                horizontal: 14,
              ),
              child: TextFormField(
                onTapOutside: (event) {
                  FocusManager.instance.primaryFocus?.unfocus();
                },
                style: const TextStyle(
                  color: AppColors.gray800,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
                decoration: InputDecoration(
                  border: InputBorder.none,
                  contentPadding: EdgeInsets.zero,
                  hintText: CtUtils.formatCurrency(
                    widget.montoAnticipo,
                    '',
                  ),
                  isDense: true,
                  hintStyle: const TextStyle(
                    color: AppColors.gray500,
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                  ),
                ),
                keyboardType:
                    const TextInputType.numberWithOptions(decimal: true),
                controller: controllerMonto,
                inputFormatters: [
                  MontoFormatter(),
                ],
                onChanged: (value) {
                  widget.onChangeMonto(widget.monto.copyWith(value: value));
                },
                focusNode: _focusNode,
                enableSuggestions: false,
              ),
            ),
          )
        ],
      ),
    );
  }
}

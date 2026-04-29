import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/emision_giros/inputs/monto_giro.dart';
import 'package:caja_tacna_app/features/shared/input_formatters/monto_formatter.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';

class InputMontoGiro extends StatefulWidget {
  const InputMontoGiro({
    super.key,
    required this.onChangeMontoGiro,
    required this.montoGiro,
    required this.simboloMoneda,
  });

  final MontoGiro montoGiro;
  final void Function(MontoGiro montoRecarga) onChangeMontoGiro;
  final String? simboloMoneda;

  @override
  State<InputMontoGiro> createState() => _InputMontoGiroState();
}

class _InputMontoGiroState extends State<InputMontoGiro> {
  final _focusNode = FocusNode();
  final TextEditingController controllerMonto = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeMontoGiro(
          MontoGiro.dirty(
              CtUtils.formatStringWithTwoDecimals(widget.montoGiro.value)),
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
      text: widget.montoGiro.value,
      selection: TextSelection.collapsed(
        offset: controllerMonto.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.montoGiro.errorMessage,
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
                    color: (widget.montoGiro.isNotValid &&
                            !widget.montoGiro.isPure)
                        ? AppColors.error500
                        : AppColors.gray300,
                  ),
                ),
              ),
              child: Center(
                child: (widget.simboloMoneda != null)
                    ? Text(
                        '${widget.simboloMoneda}',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: (widget.montoGiro.isNotValid &&
                                  !widget.montoGiro.isPure)
                              ? AppColors.error500
                              : AppColors.gray500,
                          height: 1.5,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      )
                    : const SizedBox(
                        width: 16,
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
                decoration: const InputDecoration(
                  border: InputBorder.none,
                  contentPadding: EdgeInsets.zero,
                  hintText: '0.00',
                  isDense: true,
                  hintStyle: TextStyle(
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
                  widget.onChangeMontoGiro(MontoGiro.dirty(value));
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

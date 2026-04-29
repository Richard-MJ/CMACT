import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/recarga_virtual/inputs/monto_recarga.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class InputMonto extends StatefulWidget {
  const InputMonto(
      {super.key,
      required this.onChangeMontoRecarga,
      required this.montoRecarga,
      required this.simboloMoneda,
      this.maxLength});

  final MontoRecarga montoRecarga;
  final void Function(MontoRecarga montoRecarga) onChangeMontoRecarga;
  final String? simboloMoneda;
  final int? maxLength;

  @override
  State<InputMonto> createState() => _InputMontoState();
}

class _InputMontoState extends State<InputMonto> {
  final _focusNode = FocusNode();
  final TextEditingController controllerMonto = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeMontoRecarga(
          MontoRecarga.dirty(widget.montoRecarga.value),
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
      text: widget.montoRecarga.value,
      selection: TextSelection.collapsed(
        offset: controllerMonto.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.montoRecarga.errorMessage,
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
                    color: (widget.montoRecarga.isNotValid &&
                            !widget.montoRecarga.isPure)
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
                          color: (widget.montoRecarga.isNotValid &&
                                  !widget.montoRecarga.isPure)
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
                maxLength: widget.maxLength,
                buildCounter: (BuildContext context,
                        {required int? currentLength,
                        required bool isFocused,
                        required int? maxLength}) =>
                    null,
                decoration: const InputDecoration(
                  border: InputBorder.none,
                  contentPadding: EdgeInsets.zero,
                  hintText: '0',
                  isDense: true,
                  hintStyle: TextStyle(
                    color: AppColors.gray500,
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                  ),
                ),
                keyboardType: TextInputType.number,
                controller: controllerMonto,
                inputFormatters: [
                  FilteringTextInputFormatter.digitsOnly,
                  LengthLimitingTextInputFormatter(7),
                ],
                onChanged: (value) {
                  widget.onChangeMontoRecarga(
                    widget.montoRecarga.copyWith(value: value),
                  );
                },
                focusNode: _focusNode,
                enableSuggestions: false,
              ),
            ),
          ),
          Container(
            height: double.infinity,
            padding: const EdgeInsets.only(
              left: 14,
              right: 12,
            ),
            decoration: BoxDecoration(
              border: Border(
                left: BorderSide(
                  color: (widget.montoRecarga.isNotValid &&
                          !widget.montoRecarga.isPure)
                      ? AppColors.error500
                      : AppColors.gray300,
                ),
              ),
            ),
            child: Center(
              child: (widget.simboloMoneda != null)
                  ? Text(
                      '.00',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        color: (widget.montoRecarga.isNotValid &&
                                !widget.montoRecarga.isPure)
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
        ],
      ),
    );
  }
}

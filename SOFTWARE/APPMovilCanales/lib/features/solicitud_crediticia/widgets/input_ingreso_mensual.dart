import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/input_formatters/monto_formatter.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/inputs/ingreso_mensual.dart';
import 'package:flutter/material.dart';

class InputIngresoMensual extends StatefulWidget {
  const InputIngresoMensual(
      {super.key,
      required this.onChangeMontoIngresoMensual,
      required this.ingresoMensual,
      required this.simboloMoneda});

  final IngresoMensual ingresoMensual;
  final void Function(IngresoMensual montoRecarga) onChangeMontoIngresoMensual;
  final String? simboloMoneda;

  @override
  State<InputIngresoMensual> createState() => _InputIngresoMensualState();
}

class _InputIngresoMensualState extends State<InputIngresoMensual> {
  final _focusNode = FocusNode();
  final TextEditingController controllerMonto = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeMontoIngresoMensual(
          IngresoMensual.dirty(
              CtUtils.formatStringWithTwoDecimals(widget.ingresoMensual.value)),
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
      text: widget.ingresoMensual.value,
      selection: TextSelection.collapsed(
        offset: controllerMonto.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.ingresoMensual.errorMessage,
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
                    color: (widget.ingresoMensual.isNotValid &&
                            !widget.ingresoMensual.isPure)
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
                          color: (widget.ingresoMensual.isNotValid &&
                                  !widget.ingresoMensual.isPure)
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
                  widget.onChangeMontoIngresoMensual(IngresoMensual.dirty(value));
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

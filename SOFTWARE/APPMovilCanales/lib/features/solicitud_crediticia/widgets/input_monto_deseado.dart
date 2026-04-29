import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/input_formatters/monto_formatter.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/inputs/monto_deseado.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/tipo_moneda.dart';
import 'package:flutter/material.dart';

class InputMontoDeseado extends StatefulWidget {
  const InputMontoDeseado(
      {super.key,
      required this.onChangeMontoDeseado,
      required this.montoDeseado,
      required this.tipoMoneda});

  final MontoDeseado montoDeseado;
  final void Function(MontoDeseado montoDeseado) onChangeMontoDeseado;
  final TipoMoneda? tipoMoneda;

  @override
  State<InputMontoDeseado> createState() => _InputMontoDeseadoState();
}

class _InputMontoDeseadoState extends State<InputMontoDeseado> {
  final _focusNode = FocusNode();
  final TextEditingController controllerMonto = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeMontoDeseado(
          MontoDeseado.dirty(
              CtUtils.formatStringWithTwoDecimals(widget.montoDeseado.value), widget.tipoMoneda),
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
      text: widget.montoDeseado.value,
      selection: TextSelection.collapsed(
        offset: controllerMonto.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.montoDeseado.errorMessage,
      padding: EdgeInsets.zero,
      child: Row(
        children: [
          if (widget.tipoMoneda?.simbolo != null)
            Container(
              height: double.infinity,
              padding: const EdgeInsets.only(
                left: 14,
                right: 12,
              ),
              decoration: BoxDecoration(
                border: Border(
                  right: BorderSide(
                    color: (widget.montoDeseado.isNotValid &&
                            !widget.montoDeseado.isPure)
                        ? AppColors.error500
                        : AppColors.gray300,
                  ),
                ),
              ),
              child: Center(
                child: (widget.tipoMoneda?.simbolo != null)
                    ? Text(
                        '${widget.tipoMoneda?.simbolo}',
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: (widget.montoDeseado.isNotValid &&
                                  !widget.montoDeseado.isPure)
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
                  widget.onChangeMontoDeseado(MontoDeseado.dirty(value, widget.tipoMoneda));
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

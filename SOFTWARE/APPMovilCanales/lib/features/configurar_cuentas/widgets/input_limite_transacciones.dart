import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/input_formatters/monto_formatter.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';

class InputLimiteTransaciones extends StatefulWidget {
  const InputLimiteTransaciones({
    super.key,
    required this.onChange,
    required this.value,
    required this.simboloMoneda,
  });

  final String value;
  final void Function(String value) onChange;
  final String? simboloMoneda;

  @override
  State<InputLimiteTransaciones> createState() =>
      _InputLimiteTransacionesState();
}

class _InputLimiteTransacionesState extends State<InputLimiteTransaciones> {
  final _focusNode = FocusNode();
  final TextEditingController controller = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChange(
          CtUtils.formatStringWithTwoDecimals(widget.value),
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
      text: widget.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      padding: const EdgeInsets.symmetric(
        horizontal: 14,
      ),
      width: 130,
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text(
            widget.simboloMoneda ?? '',
            style: const TextStyle(
              fontSize: 18,
              fontWeight: FontWeight.w500,
              color: AppColors.gray700,
              height: 24 / 18,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            width: 0,
          ),
          Expanded(
            child: TextFormField(
              onTapOutside: (event) {
                FocusManager.instance.primaryFocus?.unfocus();
              },
              style: const TextStyle(
                color: AppColors.gray800,
                fontSize: 14,
                fontWeight: FontWeight.w400,
              ),
              decoration: const InputDecoration(
                border: OutlineInputBorder(borderSide: BorderSide.none),
                contentPadding: EdgeInsets.zero,
              ),
              keyboardType:
                  const TextInputType.numberWithOptions(decimal: true),
              controller: controller,
              inputFormatters: [
                MontoFormatter(),
              ],
              textAlign: TextAlign.center,
              onChanged: (value) {
                widget.onChange(
                  value,
                );
              },
              focusNode: _focusNode,
              enableSuggestions: false,
            ),
          ),
        ],
      ),
    );
  }
}

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class MontoDisponible extends StatefulWidget {
  const MontoDisponible({
    super.key,
    required this.monto,
    required this.simboloMoneda,
    this.enable = false,
    this.withObscureText = true,
    this.colorOscure = true,
    this.showText = false,
  });

  final double monto;
  final String simboloMoneda;
  final bool enable;
  final bool withObscureText;
  final bool colorOscure;
  final bool showText;

  @override
  State<MontoDisponible> createState() => _MontoDisponibleState();
}

class _MontoDisponibleState extends State<MontoDisponible> {
  bool showText = false;
  final TextEditingController controllerMonto = ObscuringTextEditingController();

  @override

  @override
  void initState() {
    super.initState();
    setState(() {
      showText = false;
    });
  }

  @override
  void dispose() {
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    controllerMonto.value = controllerMonto.value.copyWith(
      text: CtUtils.formatCurrency(
        widget.monto,"",
      ),
      selection: TextSelection.collapsed(
        offset: controllerMonto.selection.end,
      ),
    );

    return CtTextFieldContainer(
      color: widget.colorOscure ? AppColors.gray100 : AppColors.white,
      child: Row(
        children: [
          if (widget.simboloMoneda.isNotEmpty)
            Container(
              height: double.infinity,
              padding: const EdgeInsets.only(
                left: 4,
                right: 5,
              ),
              decoration: const BoxDecoration(
                border: Border(
                  right: BorderSide(
                    color: AppColors.gray300,
                  ),
                ),
              ),
              child: Center(
                child: (widget.simboloMoneda.isNotEmpty)
                    ? Text(
                        widget.simboloMoneda,
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w400,
                          color: AppColors.gray500,
                          height: 1.5,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      )
                    : const SizedBox(
                        width: 16,
                      ),
              ),
            ),
          const SizedBox(
            width: 8,
          ),
          Expanded(
            child: TextFormField(
              onTapOutside: (event) {
                FocusManager.instance.primaryFocus?.unfocus();
              },
              style: const TextStyle(
                color: AppColors.gray800,
                fontSize: 17,
                fontWeight: FontWeight.w500,
              ),
              decoration: InputDecoration(
                border: InputBorder.none,
                contentPadding: EdgeInsets.zero,
                isDense: true,
                enabled: widget.enable,
              ),
              controller: showText || widget.showText ? null : controllerMonto,
              keyboardType: TextInputType.number,
              inputFormatters: [
                LengthLimitingTextInputFormatter(19)
              ],
              enableSuggestions: true,
            ),
          ),
          const SizedBox(
            width: 8,
          ),
          widget.withObscureText
              ? SizedBox(
                  width: 36,
                  height: 36,
                  child: TextButton(
                    style: TextButton.styleFrom(
                      shape: const CircleBorder(),
                      padding: EdgeInsets.zero,
                    ),
                    onPressed: () {
                      setState(() {
                        showText = !showText;
                      });
                    },
                    child: SvgPicture.asset(
                      !showText ? 'assets/icons/eye.svg' : 'assets/icons/eye_closed.svg',
                      width: 16,
                      height: 16,
                    ),
                  ),
                )
              : const SizedBox(
                  width: 10,
                ),
        ],
      ),
    );
  }
}

class ObscuringTextEditingController extends TextEditingController {
  @override
  TextSpan buildTextSpan(
      {required BuildContext context,
      required bool withComposing,
      TextStyle? style}) {
    var displayValue = '';
    for (var i = 0; i < value.text.length; i++) {
      final String char;

      if ((i + 1) % 5 == 0) {
        char = '';
      } else {
        if (i + 1 >= value.text.length + 1) {
          char = value.text[i];
        } else {
          char = '*';
        }
      }

      displayValue = '$displayValue$char';
    }

    if (!value.composing.isValid || !withComposing) {
      return TextSpan(style: style, text: displayValue);
    }
    final TextStyle? composingStyle = style?.merge(
      const TextStyle(decoration: TextDecoration.underline),
    );
    return TextSpan(
      style: style,
      children: <TextSpan>[
        TextSpan(text: value.composing.textBefore(displayValue)),
        TextSpan(
          style: composingStyle,
          text: value.composing.textInside(displayValue),
        ),
        TextSpan(text: value.composing.textAfter(displayValue)),
      ],
    );
  }
}

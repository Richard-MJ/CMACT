import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_intenet.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class InputClaveInternet extends ConsumerStatefulWidget {
  const InputClaveInternet({
    super.key,
    required this.value,
    required this.onChanged,
  });

  final ClaveInternet value;
  final void Function(ClaveInternet value) onChanged;

  @override
  InputClaveInternetState createState() => InputClaveInternetState();
}

class InputClaveInternetState extends ConsumerState<InputClaveInternet> {
  @override
  void initState() {
    super.initState();
    setState(() {
      showText = false;
    });
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChanged(ClaveInternet.dirty(widget.value.value));
      }
    });
  }

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  final TextEditingController controller = TextEditingController();
  bool showText = false;
  final FocusNode _focusNode = FocusNode();

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.value.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      padding: const EdgeInsets.only(left: 14, right: 4),
      errorMessage: widget.value.errorMessage,
      child: Row(
        children: [
          SvgPicture.asset(
            'assets/icons/lock.svg',
            height: 20,
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
                fontSize: 16,
                fontWeight: FontWeight.w400,
              ),
              decoration: const InputDecoration(
                border: InputBorder.none,
                contentPadding: EdgeInsets.zero,
                hintText: 'Clave de internet',
                isDense: true,
                hintStyle: TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
              ),
              controller: controller,
              onChanged: (value) {
                widget.onChanged(
                  widget.value.isPure
                      ? ClaveInternet.pure(value)
                      : ClaveInternet.dirty(value),
                );
              },
              keyboardType: TextInputType.number,
              obscureText: !showText,
              obscuringCharacter: '*',
              inputFormatters: [
                LengthLimitingTextInputFormatter(6),
                FilteringTextInputFormatter.digitsOnly
              ],
              focusNode: _focusNode,
              enableSuggestions: false,
            ),
          ),
          const SizedBox(
            width: 8,
          ),
          SizedBox(
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
                showText
                    ? 'assets/icons/eye_closed.svg'
                    : 'assets/icons/eye.svg',
                width: 16,
                height: 16,
                colorFilter:
                    const ColorFilter.mode(AppColors.gray400, BlendMode.srcIn),
              ),
            ),
          ),
        ],
      ),
    );
  }
}

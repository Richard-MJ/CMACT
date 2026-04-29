import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtInputEnviarCorreo extends StatefulWidget {
  const CtInputEnviarCorreo({
    super.key,
    required this.onSend,
    required this.email,
    required this.onChangeEmail,
  });
  final void Function() onSend;
  final Email email;
  final void Function(Email email) onChangeEmail;

  @override
  State<CtInputEnviarCorreo> createState() => _CtInputEnviarCorreoState();
}

class _CtInputEnviarCorreoState extends State<CtInputEnviarCorreo> {
  final TextEditingController controller = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();

    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeEmail(Email.dirty(widget.email.value));
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
      text: widget.email.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );
    return CtTextFieldContainer(
      padding: const EdgeInsets.only(left: 14, right: 9),
      errorMessage: widget.email.errorMessage,
      child: Row(
        children: [
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
                border: OutlineInputBorder(borderSide: BorderSide.none),
                contentPadding: EdgeInsets.zero,
                hintText: 'Enviar a correo electrónico',
                hintStyle: TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
              ),
              keyboardType: TextInputType.emailAddress,
              controller: controller,
              onChanged: (value) {
                widget.onChangeEmail(
                  widget.email.isPure ? Email.pure(value) : Email.dirty(value),
                );
              },
              focusNode: _focusNode,
              enableSuggestions: false,
            ),
          ),
          const SizedBox(
            width: 8,
          ),
          Container(
            width: 36,
            height: 36,
            decoration: const BoxDecoration(
              shape: BoxShape.circle,
              color: AppColors.primary100,
            ),
            child: TextButton(
              style: TextButton.styleFrom(
                shape: const CircleBorder(),
                padding: EdgeInsets.zero,
              ),
              onPressed: () {
                widget.onSend();
              },
              child: SvgPicture.asset(
                'assets/icons/send-3.svg',
                width: 20,
                height: 20,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

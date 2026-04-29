import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/perfil/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';

class InputEmail extends StatefulWidget {
  const InputEmail({
    super.key,
    required this.hintText,
    required this.email,
    required this.onChangeEmail,
  });
  final Email email;
  final void Function(Email email) onChangeEmail;
  final String? hintText;

  @override
  State<InputEmail> createState() => _InputEmailState();
}

class _InputEmailState extends State<InputEmail> {
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
      padding: EdgeInsets.zero,
      errorMessage: widget.email.errorMessage,
      child: Row(
        children: [
          Container(
            height: double.infinity,
            padding: const EdgeInsets.only(
              left: 14,
              right: 8,
            ),
            child: SvgPicture.asset(
              'assets/icons/mail.svg',
              height: 20,
              colorFilter: ColorFilter.mode(
                (widget.email.isNotValid && !widget.email.isPure)
                    ? AppColors.error500
                    : AppColors.gray500,
                BlendMode.srcIn,
              ),
            ),
          ),
          Expanded(
            child: Container(
              padding: const EdgeInsets.only(right: 14),
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
                  border: const OutlineInputBorder(borderSide: BorderSide.none),
                  contentPadding: EdgeInsets.zero,
                  hintText: widget.hintText,
                  hintStyle: const TextStyle(
                    color: AppColors.gray500,
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                  ),
                ),
                keyboardType: TextInputType.emailAddress,
                controller: controller,
                onChanged: (value) {
                  widget.onChangeEmail(
                    widget.email.isPure
                        ? Email.pure(value)
                        : Email.dirty(value),
                  );
                },
                focusNode: _focusNode,
                inputFormatters: [
                  LengthLimitingTextInputFormatter(100),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}

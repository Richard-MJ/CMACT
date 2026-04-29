import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class InputDisabled extends StatefulWidget {
  const InputDisabled({
    super.key,
    required this.content,
    required this.svgIcon,
  });
  final String content;
  final String svgIcon;

  @override
  State<InputDisabled> createState() => _InputDisabledState();
}

class _InputDisabledState extends State<InputDisabled> {
  final TextEditingController controller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.content,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );
    return CtTextFieldContainer(
      color: AppColors.gray50,
      padding: EdgeInsets.zero,
      child: Row(
        children: [
          Container(
            height: double.infinity,
            padding: const EdgeInsets.only(
              left: 14,
              right: 8,
            ),
            child: SvgPicture.asset(
              widget.svgIcon,
              height: 20,
              colorFilter: ColorFilter.mode(
                AppColors.gray500,
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
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
                decoration: InputDecoration(
                  border: const OutlineInputBorder(borderSide: BorderSide.none),
                  contentPadding: EdgeInsets.zero,
                  hintStyle: const TextStyle(
                    color: AppColors.gray500,
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                  ),
                ),
                keyboardType: TextInputType.emailAddress,
                controller: controller,
                enableSuggestions: false,
                enabled: false,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

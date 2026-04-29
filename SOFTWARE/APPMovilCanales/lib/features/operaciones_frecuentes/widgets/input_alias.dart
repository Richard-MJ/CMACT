import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/inputs/alias.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class InputAliasOpFrecuente extends StatefulWidget {
  const InputAliasOpFrecuente({
    super.key,
    required this.onChangeAlias,
    required this.alias,
    required this.hintText,
  });

  final AliasOpFrecuente alias;
  final String? hintText;
  final void Function(AliasOpFrecuente alias) onChangeAlias;

  @override
  State<InputAliasOpFrecuente> createState() => _InputAliasOpFrecuenteState();
}

class _InputAliasOpFrecuenteState extends State<InputAliasOpFrecuente> {
  final _focusNode = FocusNode();
  final TextEditingController controller = TextEditingController();

  @override
  void initState() {
    super.initState();
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeAlias(
          AliasOpFrecuente.dirty(
            widget.alias.value,
          ),
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
      text: widget.alias.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    return CtTextFieldContainer(
      errorMessage: widget.alias.errorMessage,
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
              'assets/icons/user.svg',
              height: 20,
              colorFilter: ColorFilter.mode(
                (widget.alias.isNotValid && !widget.alias.isPure)
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
                  border: InputBorder.none,
                  contentPadding: EdgeInsets.zero,
                  isDense: true,
                  hintStyle: const TextStyle(
                    color: AppColors.gray500,
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                  ),
                  hintText: widget.hintText,
                ),
                controller: controller,
                onChanged: (value) {
                  widget.onChangeAlias(
                    widget.alias.isPure
                        ? AliasOpFrecuente.pure(value)
                        : AliasOpFrecuente.dirty(value),
                  );
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

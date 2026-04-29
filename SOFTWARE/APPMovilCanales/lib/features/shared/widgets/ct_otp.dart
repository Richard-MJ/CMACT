import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class CtOtp extends StatefulWidget {
  const CtOtp({
    super.key,
    required this.onChanged,
    required this.value,
    this.enabled = false,
  });

  final void Function(String value) onChanged;
  final String value;
  final bool enabled;

  @override
  State<CtOtp> createState() => _CtOtpState();
}

class _CtOtpState extends State<CtOtp> {
  List<FocusNode> focusNodeList = List.generate(6, (index) => FocusNode());

  void addCharacter(String character) {
    if (character.isNotEmpty) {
      String newValue = widget.value + character;

      widget.onChanged(newValue);

      if (newValue.length < 6) {
        focusNodeList[newValue.length].requestFocus();
      } else {
        FocusManager.instance.primaryFocus?.unfocus();
      }
    }
  }

  void removeCharacter(position) {
    if (widget.value.isNotEmpty) {
      final newValue = widget.value.substring(0, widget.value.length - 1);
      focusNodeList[newValue.length].requestFocus();
      widget.onChanged(newValue);
    }
  }

  @override
  void didUpdateWidget(CtOtp oldWidget) {
    super.didUpdateWidget(oldWidget);
    //caso que el value sea modificado programaticamente, por ejm desde algun provider
    // Verificar si la propiedad 'value' ha cambiado
    if (widget.value != oldWidget.value) {
      //verifica si el otp tiene focus en alguno de sus inputs
      final currentFocusIndex =
          focusNodeList.indexWhere((node) => node.hasFocus);

      if (currentFocusIndex != -1) {
        if (widget.value.length < 6) {
          focusNodeList[widget.value.length].requestFocus();
        } else {
          FocusManager.instance.primaryFocus?.unfocus();
        }
      }
    }
  }

  @override
  void initState() {
    super.initState();
    // focusNodeList[0].requestFocus();
  }

  setFocus() {
    if (widget.value.length < 6) {
      focusNodeList[widget.value.length].requestFocus();
    } else {
      focusNodeList[5].requestFocus();
    }
  }

  @override
  void dispose() {
    for (var focusNode in focusNodeList) {
      focusNode.dispose();
    }
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        setFocus();
      },
      child: AbsorbPointer(
        absorbing: true,
        child: SizedBox(
          height: 44,
          width: double.infinity,
          child: Wrap(
            alignment: WrapAlignment.spaceBetween,
            children: focusNodeList.asMap().entries.map(
              (entry) {
                final index = entry.key;
                return _TextFiledOTP(
                  enabled: widget.enabled,
                  add: (value) {
                    addCharacter(value);
                  },
                  delete: () {
                    removeCharacter(index);
                  },
                  value: index < widget.value.length ? widget.value[index] : '',
                  focusNode: focusNodeList[index],
                );
              },
            ).toList(),
          ),
        ),
      ),
    );
  }
}

class _TextFiledOTP extends StatefulWidget {
  const _TextFiledOTP({
    required this.add,
    required this.delete,
    required this.value,
    required this.focusNode,
    required this.enabled,
  });

  final void Function(String value) add;
  final void Function() delete;
  final FocusNode focusNode;
  final String value;
  final bool enabled;

  @override
  State<_TextFiledOTP> createState() => _TextFiledOTPState();
}

class _TextFiledOTPState extends State<_TextFiledOTP> {
  final TextEditingController controller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.value,
      selection: TextSelection.collapsed(
        offset: widget.value.length,
      ),
    );

    return SizedBox(
      width: 48,
      child: KeyboardListener(
        focusNode: FocusNode(),
        onKeyEvent: (value) {
          if (value.logicalKey == LogicalKeyboardKey.backspace &&
              value is KeyDownEvent &&
              widget.enabled) {
            widget.delete();
          }
        },
        child: TextField(
          focusNode: widget.focusNode,
          onChanged: (value) {
            widget.add(value);
          },
          controller: controller,
          showCursor: false,
          readOnly: false,
          style: const TextStyle(
            fontSize: 16,
            fontWeight: FontWeight.w400,
            height: 1.5,
            color: AppColors.gray600,
          ),
          maxLength: 1,
          textAlign: TextAlign.center,
          keyboardType: TextInputType.number,
          decoration: InputDecoration(
            contentPadding: const EdgeInsets.only(
              top: 10,
              bottom: 10,
            ),
            isDense: true,
            counter: const Offstage(),
            enabledBorder: OutlineInputBorder(
              borderSide: const BorderSide(width: 1, color: AppColors.gray300),
              borderRadius: BorderRadius.circular(8),
            ),
            focusedBorder: OutlineInputBorder(
              borderSide: const BorderSide(
                width: 1,
                color: AppColors.primary700,
              ),
              borderRadius: BorderRadius.circular(8),
            ),
          ),
          inputFormatters: [FilteringTextInputFormatter.digitsOnly],
        ),
      ),
    );
  }
}

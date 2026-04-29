import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart' hide Orientation;
import 'package:pin_input_text_field/pin_input_text_field.dart';

class InputVerificarIdentidad extends StatefulWidget {
  final void Function(String value) onChanged;

  const InputVerificarIdentidad({super.key, required this.onChanged});

  @override
  State<InputVerificarIdentidad> createState() =>
      _InputVerificarIdentidadState();
}

class _InputVerificarIdentidadState extends State<InputVerificarIdentidad> {
  final int pinLength = 6;
  final TextEditingController pinEditingController =
      TextEditingController(text: '');

  /// Decorate the outside of the Pin.
  final PinDecoration pinDecoration = BoxLooseDecoration(
    strokeColorBuilder:
        PinListenColorBuilder(AppColors.gray300, AppColors.primary700),
    bgColorBuilder: null,
    obscureStyle: ObscureStyle(
      isTextObscure: false,
    ),
    textStyle: const TextStyle(
      fontSize: 16,
      fontWeight: FontWeight.w400,
      height: 1.5,
      color: AppColors.gray700,
    ),
  );

  @override
  void initState() {
    pinEditingController.addListener(() {
      widget.onChanged(pinEditingController.text);
    });

    super.initState();
  }

  @override
  void dispose() {
    pinEditingController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        height: 44,
        child: PinInputTextField(
          pinLength: pinLength,
          decoration: pinDecoration,
          controller: pinEditingController,
          textInputAction: TextInputAction.done,
          enabled: true,
          keyboardType: TextInputType.number,
          textCapitalization: TextCapitalization.characters,
          onSubmit: null,
          onChanged: (value) {
            if (value.length >= 6) {
              FocusManager.instance.primaryFocus?.unfocus();
            }
          },
          enableInteractiveSelection: false,
          cursor: Cursor(
            width: 2,
            color: AppColors.gray500,
            radius: const Radius.circular(1),
            enabled: true,
            orientation: Orientation.vertical,
          ),
        ));
  }
}

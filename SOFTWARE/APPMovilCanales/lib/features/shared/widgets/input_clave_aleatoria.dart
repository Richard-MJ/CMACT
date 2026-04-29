import 'dart:async';

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/svg.dart';

class InputClaveAleatoria extends ConsumerStatefulWidget {
  const InputClaveAleatoria({
    super.key,
    required this.value,
    required this.onChanged,
    required this.length,
    required this.titulo,
    required this.hint,
    this.errorMessage,
  });

  final String value;
  final int length;
  final void Function(String value) onChanged;
  final String? errorMessage;
  final String titulo;
  final String hint;

  @override
  InputClaveAleatoriaState createState() => InputClaveAleatoriaState();
}

class InputClaveAleatoriaState extends ConsumerState<InputClaveAleatoria> {
  @override
  void initState() {
    super.initState();
    setState(() {
      _mostrarClave = false;
    });
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChanged(widget.value);
      }
    });
  }

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  final TextEditingController _controller = TextEditingController();
  bool _mostrarClave = false;
  final FocusNode _focusNode = FocusNode();

  void _mostrarTeclado(BuildContext context) {
    showModalBottomSheet(
      context: context,
      isDismissible: true,
      enableDrag: false,
      backgroundColor: Colors.white,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.vertical(top: Radius.circular(15)),
      ),
      builder: (context) {
        return _TecladoNumericoAleatorio(
          onChanged: widget.onChanged,
          initialPin: widget.value,
          length: widget.length,
          titulo: widget.titulo,
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    if (_controller.text != widget.value) {
      _controller.text = widget.value;
      _controller.selection = TextSelection.collapsed(
        offset: _controller.text.length,
      );
    }

    return CtTextFieldContainer(
      padding: const EdgeInsets.only(left: 14, right: 4),
      errorMessage: widget.errorMessage,
      child: Row(
        children: [
          SvgPicture.asset(
            'assets/icons/lock.svg',
            height: 20,
            colorFilter:
                const ColorFilter.mode(AppColors.gray500, BlendMode.srcIn),
          ),
          const SizedBox(width: 8),
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
              decoration: InputDecoration(
                border: InputBorder.none,
                contentPadding: EdgeInsets.zero,
                hintText: widget.hint,
                isDense: true,
                hintStyle: TextStyle(
                  color: AppColors.gray500,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
              ),
              readOnly: true,
              obscureText: !_mostrarClave,
              obscuringCharacter: '*',
              controller: _controller,
              onTap: () => _mostrarTeclado(context),
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
              child: SvgPicture.asset(
                _mostrarClave
                    ? 'assets/icons/eye_closed.svg'
                    : 'assets/icons/eye.svg',
                width: 16,
                height: 16,
                colorFilter:
                    const ColorFilter.mode(AppColors.gray400, BlendMode.srcIn),
              ),
              onPressed: () {
                setState(() {
                  _mostrarClave = !_mostrarClave;
                });
              },
            ),
          ),
        ],
      ),
    );
  }
}

class _TecladoNumericoAleatorio extends StatefulWidget {
  final Function(String) onChanged;
  final String initialPin;
  final int length;
  final String titulo;

  const _TecladoNumericoAleatorio({
    required this.onChanged,
    required this.initialPin,
    required this.length,
    required this.titulo,
  });

  @override
  State<_TecladoNumericoAleatorio> createState() =>
      _TecladoNumericoAleatorioState();
}

class _TecladoNumericoAleatorioState extends State<_TecladoNumericoAleatorio> {
  late String _pin;
  late List<int> _numerosAleatorios;
  bool _mostrarPin = false;
  int? _ultimoDigitoVisibleIndex;
  Timer? _timerOcultarUltimo;
  List<KeypadButtonConfig> _keypadButtons = [];

  @override
  void initState() {
    super.initState();
    _pin = widget.initialPin;
    _numerosAleatorios = List.generate(10, (index) => index)..shuffle();
    _keypadButtons = [
      ...List.generate(9, (index) {
        final numero = _numerosAleatorios[index];
        return KeypadButtonConfig(
          text: numero.toString(),
          onPressed: () => _onNumberPressed(numero),
        );
      }),
      KeypadButtonConfig(
        icon: Icons.clear,
        onPressed: _onClearPressed,
      ),
      KeypadButtonConfig(
        text: _numerosAleatorios.last.toString(),
        onPressed: () => _onNumberPressed(_numerosAleatorios.last),
      ),
      KeypadButtonConfig(
        icon: Icons.backspace_outlined,
        onPressed: _onBackspacePressed,
      ),
    ];
  }

  @override
  void dispose() {
    _timerOcultarUltimo?.cancel();
    super.dispose();
  }

  void _onNumberPressed(int numero) {
    if (_pin.length < widget.length) {
      setState(() {
        _pin += numero.toString();
      });

      _timerOcultarUltimo?.cancel();
      _ultimoDigitoVisibleIndex = _pin.length - 1;
      _timerOcultarUltimo = Timer(const Duration(milliseconds: 300), () {
        if (mounted) {
          setState(() {
            _ultimoDigitoVisibleIndex = null;
          });
        }
      });

      widget.onChanged(_pin);

      if (_pin.length == widget.length) {
        Navigator.pop(context);
      }
    }
  }

  void _onBackspacePressed() {
    if (_pin.isNotEmpty) {
      setState(() {
        _pin = _pin.substring(0, _pin.length - 1);
      });
      widget.onChanged(_pin);
    }
  }

  void _onClearPressed() {
    if (_pin.isNotEmpty) {
      setState(() {
        _pin = '';
      });
      widget.onChanged(_pin);
    }
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Text(
              widget.titulo,
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.bold,
                color: AppColors.gray900,
              ),
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                PinDisplay(
                  pinLength: widget.length,
                  enteredLength: _pin.length,
                  currentPin: _pin,
                  showDigits: _mostrarPin,
                  showLastDigitIndex: _ultimoDigitoVisibleIndex,
                ),
                TextButton(
                  style: TextButton.styleFrom(
                    shape: const CircleBorder(),
                    padding: EdgeInsets.zero,
                  ),
                  child: SvgPicture.asset(
                    _mostrarPin
                        ? 'assets/icons/eye_closed.svg'
                        : 'assets/icons/eye.svg',
                    width: 16,
                    height: 16,
                    colorFilter: const ColorFilter.mode(
                        AppColors.gray400, BlendMode.srcIn),
                  ),
                  onPressed: () {
                    setState(() {
                      _mostrarPin = !_mostrarPin;
                    });
                  },
                ),
              ],
            ),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 35),
              child: Column(
                children: [
                  GridView.count(
                    childAspectRatio: 1.8,
                    crossAxisCount: 3,
                    shrinkWrap: true,
                    physics: const NeverScrollableScrollPhysics(),
                    mainAxisSpacing: 8,
                    crossAxisSpacing: 8,
                    children: _keypadButtons.map((config) {
                      return _KeypadButton(
                        onPressed: config.onPressed,
                        text: config.text,
                        child: config.icon != null ? Icon(config.icon) : null,
                      );
                    }).toList(),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class _KeypadButton extends StatelessWidget {
  final String? text;
  final Widget? child;
  final VoidCallback onPressed;

  const _KeypadButton({this.text, this.child, required this.onPressed});

  @override
  Widget build(BuildContext context) {
    return Material(
      color: Colors.grey[200],
      borderRadius: BorderRadius.circular(12),
      child: InkWell(
        onTap: onPressed,
        borderRadius: BorderRadius.circular(12),
        child: Center(
          child: child ??
              Text(
                text!,
                style: const TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.w500,
                  color: AppColors.gray800,
                ),
              ),
        ),
      ),
    );
  }
}

class PinDisplay extends StatelessWidget {
  final int pinLength;
  final int enteredLength;
  final String currentPin;
  final bool showDigits;
  final int? showLastDigitIndex;

  const PinDisplay({
    super.key,
    required this.pinLength,
    required this.enteredLength,
    required this.currentPin,
    required this.showDigits,
    required this.showLastDigitIndex,
  });

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: List.generate(pinLength, (index) {
        final filled = index < enteredLength;

        return Container(
          margin: const EdgeInsets.symmetric(horizontal: 6),
          width: 20,
          height: 20,
          alignment: Alignment.center,
          decoration: BoxDecoration(
            shape: BoxShape.circle,
            color: filled && !(showDigits || index == showLastDigitIndex)
                ? AppColors.gray500
                : Colors.transparent,
            border: filled && (showDigits || index == showLastDigitIndex)
                ? null
                : Border.all(color: AppColors.gray500, width: 1.5),
          ),
          child: filled && (showDigits || index == showLastDigitIndex)
              ? Text(
                  currentPin[index],
                  style: const TextStyle(
                    fontSize: 12,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray800,
                  ),
                )
              : null,
        );
      }),
    );
  }
}

class KeypadButtonConfig {
  final String? text;
  final IconData? icon;
  final VoidCallback onPressed;

  KeypadButtonConfig({
    this.text,
    this.icon,
    required this.onPressed,
  });
}

import 'dart:math';

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:uuid/uuid.dart';

class CtTeclado extends StatefulWidget {
  const CtTeclado({
    super.key,
    required this.onChange,
    this.random = false,
    required this.value,
  });

  final String value;
  final void Function(String value) onChange;
  final bool random;

  @override
  State<CtTeclado> createState() => _CtTecladoState();
}

class _CtTecladoState extends State<CtTeclado> {
  void addCharacter(String character) {
    if (widget.value.length < 6) {
      widget.onChange(widget.value + character);
    }
  }

  void removeLastCharacter() {
    if (widget.value.isNotEmpty) {
      widget.onChange(widget.value.substring(0, widget.value.length - 1));
    }
  }

  void clear() {
    widget.onChange('');
  }

  void toggleGlobalShowValue() {
    setState(() {
      globalShowValue = !globalShowValue;
    });
  }

  bool globalShowValue = false;
  List<String> teclas = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0'];

  @override
  void initState() {
    super.initState();
    if (widget.random) {
      setState(() {
        teclas.shuffle(Random());
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    const TextStyle textTecladoStyle = TextStyle(
      color: AppColors.gray900,
      fontSize: 24,
      fontWeight: FontWeight.w600,
    );

    return Column(
      children: [
        Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            _InputItem(
              value: widget.value.isNotEmpty ? widget.value[0] : '',
              globalShowValue: globalShowValue,
              totalValue: widget.value,
              position: 0,
            ),
            const SizedBox(width: 8),
            _InputItem(
              value: widget.value.length > 1 ? widget.value[1] : '',
              globalShowValue: globalShowValue,
              totalValue: widget.value,
              position: 1,
            ),
            const SizedBox(width: 8),
            _InputItem(
              value: widget.value.length > 2 ? widget.value[2] : '',
              globalShowValue: globalShowValue,
              totalValue: widget.value,
              position: 2,
            ),
            const SizedBox(width: 8),
            _InputItem(
              value: widget.value.length > 3 ? widget.value[3] : '',
              globalShowValue: globalShowValue,
              totalValue: widget.value,
              position: 3,
            ),
            const SizedBox(width: 8),
            _InputItem(
              value: widget.value.length > 4 ? widget.value[4] : '',
              globalShowValue: globalShowValue,
              totalValue: widget.value,
              position: 4,
            ),
            const SizedBox(width: 8),
            _InputItem(
              value: widget.value.length > 5 ? widget.value[5] : '',
              globalShowValue: globalShowValue,
              totalValue: widget.value,
              position: 5,
            ),
            SizedBox(
              width: 39,
              height: 39,
              child: Center(
                child: TextButton(
                  style: TextButton.styleFrom(
                    shape: const CircleBorder(),
                    padding: EdgeInsets.zero,
                  ),
                  onPressed: () => toggleGlobalShowValue(),
                  child: SvgPicture.asset(
                    globalShowValue
                        ? 'assets/icons/eye_closed.svg'
                        : 'assets/icons/eye.svg',
                    width: 24,
                    height: 24,
                    colorFilter: const ColorFilter.mode(
                        AppColors.gray900, BlendMode.srcIn),
                  ),
                ),
              ),
            ),
          ],
        ),
        const SizedBox(height: 46),
        Column(
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[0]),
                  child: Text(
                    teclas[0],
                    style: textTecladoStyle,
                  ),
                ),
                const SizedBox(width: 24),
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[1]),
                  child: Text(
                    teclas[1],
                    style: textTecladoStyle,
                  ),
                ),
                const SizedBox(width: 24),
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[2]),
                  child: Text(
                    teclas[2],
                    style: textTecladoStyle,
                  ),
                )
              ],
            ),
            const SizedBox(height: 16),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[3]),
                  child: Text(
                    teclas[3],
                    style: textTecladoStyle,
                  ),
                ),
                const SizedBox(width: 24),
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[4]),
                  child: Text(
                    teclas[4],
                    style: textTecladoStyle,
                  ),
                ),
                const SizedBox(width: 24),
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[5]),
                  child: Text(
                    teclas[5],
                    style: textTecladoStyle,
                  ),
                )
              ],
            ),
            const SizedBox(height: 16),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[6]),
                  child: Text(
                    teclas[6],
                    style: textTecladoStyle,
                  ),
                ),
                const SizedBox(width: 24),
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[7]),
                  child: Text(
                    teclas[7],
                    style: textTecladoStyle,
                  ),
                ),
                const SizedBox(width: 24),
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[8]),
                  child: Text(
                    teclas[8],
                    style: textTecladoStyle,
                  ),
                )
              ],
            ),
            const SizedBox(height: 16),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                _ButtonTeclado(
                  onPressed: () => clear(),
                  child: SvgPicture.asset(
                    'assets/icons/x.svg',
                    height: 24,
                    colorFilter: const ColorFilter.mode(
                      AppColors.gray900,
                      BlendMode.srcIn,
                    ),
                  ),
                ),
                const SizedBox(width: 24),
                _ButtonTeclado(
                  onPressed: () => addCharacter(teclas[9]),
                  child: Text(
                    teclas[9],
                    style: textTecladoStyle,
                  ),
                ),
                const SizedBox(width: 24),
                _ButtonTeclado(
                  onPressed: () => removeLastCharacter(),
                  child: SvgPicture.asset(
                    'assets/icons/delete.svg',
                    height: 24,
                    colorFilter: const ColorFilter.mode(
                      AppColors.gray900,
                      BlendMode.srcIn,
                    ),
                  ),
                )
              ],
            )
          ],
        )
      ],
    );
  }
}

class _InputItem extends StatefulWidget {
  const _InputItem({
    required this.value,
    required this.globalShowValue,
    required this.totalValue,
    required this.position,
  });

  final String value;
  final String totalValue;
  final bool globalShowValue;
  final int position;

  @override
  State<_InputItem> createState() => _InputItemState();
}

class _InputItemState extends State<_InputItem> {
  final uuid = const Uuid();

  @override
  void didUpdateWidget(covariant _InputItem oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.value != widget.value) {
      var temporaryId = uuid.v4();
      internalId = temporaryId;
      setState(() {
        showValue = true;
      });

      Future.delayed(const Duration(milliseconds: 500), () {
        setState(() {
          if (temporaryId == internalId) {
            showValue = false;
          }
        });
      });
    }

    if (widget.totalValue.length > widget.position + 1) {
      setState(() {
        showValue = false;
      });
    }
  }

  bool showValue = false;
  String internalId = '';

  @override
  Widget build(BuildContext context) {
    return Container(
      width: 32,
      height: 40,
      decoration: const BoxDecoration(
        border: Border(
          bottom: BorderSide(
            color: AppColors.gray900,
            width: 1,
          ),
        ),
      ),
      child: ((showValue || widget.globalShowValue) || widget.value.isEmpty)
          ? Text(
              widget.value,
              textAlign: TextAlign.center,
              style: const TextStyle(
                fontSize: 28,
                fontWeight: FontWeight.w700,
                color: AppColors.gray900,
                height: 40 / 28,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            )
          : SizedBox(
              height: 40,
              child: Center(
                child: Container(
                  width: 12,
                  height: 12,
                  decoration: const BoxDecoration(
                    color: AppColors.gray900,
                    shape: BoxShape.circle,
                  ),
                ),
              ),
            ),
    );
  }
}

class _ButtonTeclado extends StatelessWidget {
  const _ButtonTeclado({required this.child, required this.onPressed});

  final Widget child;
  final void Function() onPressed;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 64,
      height: 64,
      child: TextButton(
        style: TextButton.styleFrom(
          backgroundColor: AppColors.primary100,
          shape: const CircleBorder(),
        ),
        onPressed: onPressed,
        child: child,
      ),
    );
  }
}

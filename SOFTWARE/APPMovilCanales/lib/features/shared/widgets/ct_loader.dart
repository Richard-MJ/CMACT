import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtLoader extends StatefulWidget {
  const CtLoader({
    super.key,
    this.withOpacity = false,
  });

  final bool withOpacity;

  @override
  State<CtLoader> createState() => _CtLoaderState();
}

class _CtLoaderState extends State<CtLoader>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(seconds: 1),
    )..repeat(); // Repite la animación indefinidamente
  }

  @override
  void dispose() {
    _controller
        .dispose(); // Asegúrate de llamar a dispose() en AnimationController
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnnotatedRegion(
      value: Platform.isIOS
          ? SystemUiOverlayStyle(
              statusBarBrightness:
                  widget.withOpacity ? Brightness.dark : Brightness.light,
              statusBarIconBrightness:
                  widget.withOpacity ? Brightness.dark : Brightness.light,
            )
          : SystemUiOverlayStyle(
              statusBarBrightness:
                  widget.withOpacity ? Brightness.light : Brightness.dark,
              statusBarIconBrightness:
                  widget.withOpacity ? Brightness.light : Brightness.dark,
            ),
      child: Scaffold(
        backgroundColor:
            widget.withOpacity ? Colors.black.withOpacity(0.5) : Colors.white,
        body: SafeArea(
          child: Center(
            child: AnimatedBuilder(
              animation: _controller,
              builder: (context, child) {
                return Transform.rotate(
                  angle: _controller.value *
                      2 *
                      3.14159265359, // 2π para una rotación completa
                  child: SvgPicture.asset(
                    'assets/icons/shared/loader.svg',
                    height: 94,
                  ),
                );
              },
            ),
          ),
        ),
      ),
    );
  }
}

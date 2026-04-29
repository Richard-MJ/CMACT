import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class WifiBars extends StatefulWidget {
  const WifiBars({super.key});

  @override
  State<WifiBars> createState() => _WifiBarsState();
}

class _WifiBarsState extends State<WifiBars> with TickerProviderStateMixin {
  late final AnimationController _controller;
  late final List<Animation<double>> _animations;

  @override
  void initState() {
    super.initState();

    _controller = AnimationController(
      vsync: this,
      duration: const Duration(seconds: 2),
    )..repeat();

    _animations = List.generate(3, (i) {
      return Tween<double>(begin: 0.0, end: 1.0).animate(
        CurvedAnimation(
          parent: _controller,
          curve: Interval(i * 0.2, 0.4 + i * 0.2, curve: Curves.easeOut),
        ),
      );
    });
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  Widget _buildBar(int index, double size, Animation<double> anim) {
    return AnimatedBuilder(
      animation: anim,
      builder: (context, child) {
        return Opacity(
          opacity: anim.value,
          child: Transform.translate(
            offset: Offset(10 * (1 - anim.value), 0),
            child: CustomPaint(
              size: Size(size, size / 1),
              painter: ArcPainter(),
            ),
          ),
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: List.generate(3, (i) {
        final reversedIndex = 2 - i; // para que salgan de chica a grande
        return _buildBar(reversedIndex, 12.0 + reversedIndex * 6.0,
            _animations[reversedIndex]);
      }),
    );
  }
}

class ArcPainter extends CustomPainter {
  @override
  void paint(Canvas canvas, Size size) {
    final paint = Paint()
      ..color = AppColors.gray800
      ..style = PaintingStyle.stroke
      ..strokeWidth = 2;

    // Curva estilo ")"
    final rect = Rect.fromLTWH(0, 0, size.width * 1.5, size.height);
    final startAngle = -4.00; // -90° (izquierda)
    final sweepAngle = 3.14 / 1.8; // 90° hacia abajo
    canvas.drawArc(rect, startAngle, sweepAngle, false, paint);
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) => false;
}

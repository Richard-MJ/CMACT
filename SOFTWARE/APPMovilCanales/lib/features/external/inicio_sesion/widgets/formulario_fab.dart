import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class FormularioFab extends StatefulWidget {
  const FormularioFab({super.key});

  @override
  State<FormularioFab> createState() => _FormularioFabState();
}

class _FormularioFabState extends State<FormularioFab>
    with SingleTickerProviderStateMixin {
  bool _isSpeedDialOpen = false;
  late AnimationController _animationController;
  late Animation<double> _animation;

  @override
  void initState() {
    _animationController = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 250),
    );
    _animation = CurvedAnimation(
      parent: _animationController,
      curve: Curves.easeInOut,
    );
    super.initState();
  }

  @override
  void dispose() {
    _animationController.dispose();
    super.dispose();
  }

  void _toggleSpeedDial() {
    setState(() {
      _isSpeedDialOpen = !_isSpeedDialOpen;
      if (_isSpeedDialOpen) {
        _animationController.forward();
      } else {
        _animationController.reverse();
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisSize: MainAxisSize.min,
      crossAxisAlignment: CrossAxisAlignment.end,
      children: [
        // FAB principal
        Row(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            AnimatedBuilder(
              animation: _animation,
              builder: (context, child) {
                final backgroundColor = Color.lerp(
                    AppColors.primary700, Colors.white, _animation.value);
                final foregroundColor = Color.lerp(
                    Colors.white, AppColors.primary700, _animation.value);
                final borderColor = Color.lerp(
                    Colors.transparent, AppColors.primary700, _animation.value);

                return SizedBox(
                  width: 45,
                  height: 45,
                  child: FloatingActionButton(
                    shape: CircleBorder(
                        side: BorderSide(
                            color: borderColor ?? Colors.transparent)),
                    backgroundColor: backgroundColor,
                    onPressed: _toggleSpeedDial,
                    child: AnimatedSwitcher(
                      duration: const Duration(milliseconds: 250),
                      child: Icon(
                        _isSpeedDialOpen
                            ? Icons.close
                            : Icons.question_mark_rounded,
                        key: ValueKey<bool>(_isSpeedDialOpen),
                        color: foregroundColor,
                      ),
                    ),
                  ),
                );
              },
            ),
          ],
        ),
        const SizedBox(height: 12),
        // Opciones del Speed Dial
        ScaleTransition(
          scale: _animation,
          alignment: Alignment.topRight,
          child: FadeTransition(
            opacity: _animation,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                _SpeedDialOption(
                  label: 'Contáctanos',
                  icon: Icons.phone_outlined,
                  onTap: () {
                    _toggleSpeedDial();
                    context.push('/contactanos');
                  },
                ),
              ],
            ),
          ),
        ),
      ],
    );
  }
}

/// Widget para cada opción del Speed Dial FAB
class _SpeedDialOption extends StatelessWidget {
  final String label;
  final IconData icon;
  final VoidCallback onTap;

  const _SpeedDialOption({
    required this.label,
    required this.icon,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onTap,
      child: Container(
        padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(30),
          border:
              Border.all(color: AppColors.primary700.withValues(alpha: 0.5)),
          boxShadow: [
            BoxShadow(
              color: AppColors.primary700.withValues(alpha: 0.1),
              blurRadius: 8,
              offset: const Offset(0, 2),
            ),
          ],
        ),
        child: Row(
          mainAxisSize: MainAxisSize.min,
          children: [
            Icon(
              icon,
              color: AppColors.primary700,
              size: 20,
            ),
            const SizedBox(width: 8),
            Text(
              label,
              style: const TextStyle(
                fontSize: 13,
                fontWeight: FontWeight.w600,
                color: AppColors.primary700,
              ),
            ),
          ],
        ),
      ),
    );
  }
}

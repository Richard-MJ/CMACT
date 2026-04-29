import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

class CtCard extends StatelessWidget {
  const CtCard({
    super.key,
    required this.child,
    this.padding,
  });

  final Widget child;
  final EdgeInsets? padding;
  @override
  Widget build(BuildContext context) {
    return Container(
      padding: padding,
      decoration: BoxDecoration(
        color: AppColors.gray100,
        borderRadius: BorderRadius.circular(8),
      ),
      child: child,
    );
  }
}

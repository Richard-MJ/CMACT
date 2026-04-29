import 'package:flutter/material.dart';

class PopNotScope extends StatelessWidget {
  const PopNotScope({
    super.key,
    required this.child,
  });

  final Widget child;

  @override
  Widget build(BuildContext context) {
    return PopScope(
      canPop: false,
      onPopInvoked: (_) {},
      child: child,
    );
  }
}
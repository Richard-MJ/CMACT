import 'package:go_router/go_router.dart';
import 'package:flutter/widgets.dart';

class PopHomeScope extends StatelessWidget {
  const PopHomeScope({
    super.key,
    required this.child,
  });

  final Widget child;

  @override
  Widget build(BuildContext context) {
    return PopScope(
      canPop: false,
      onPopInvoked: (didPop) {
        Future.microtask(() {
          context.go('/home');
        });
      },
      child: child,
    );
  }
}

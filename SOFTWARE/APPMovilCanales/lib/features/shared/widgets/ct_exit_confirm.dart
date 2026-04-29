import 'package:caja_tacna_app/features/shared/providers/exit_confirm_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class ExitConfirm extends ConsumerWidget {
  const ExitConfirm({super.key, required this.child});

  final Widget child;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final exitConfirm = ref.watch(exitConfirmProvider);
    final snackbar = SnackbarService();

    return WillPopScope(
      onWillPop: () async {
        if (!exitConfirm.exit) {
          ref.read(exitConfirmProvider.notifier).firstTap();
          snackbar.showSnackbar(
            context: context,
            message: 'Pulsa de nuevo para salir.',
            type: SnackbarType.info,
            duration: const Duration(milliseconds: 1200),
          );
          return false;
        } else {
          return true;
        }
      },
      child: child,
    );
  }
}

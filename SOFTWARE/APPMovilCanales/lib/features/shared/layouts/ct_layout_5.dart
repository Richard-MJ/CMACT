import 'dart:io';

import 'package:caja_tacna_app/features/shared/widgets/ct_appbar_3.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:go_router/go_router.dart';

class CtLayout5 extends StatelessWidget {
  const CtLayout5({
    super.key,
    required this.child,
    this.onBack,
  });

  final Widget child;
  final void Function()? onBack;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        toolbarHeight: 124,
        automaticallyImplyLeading: false,
        systemOverlayStyle: Platform.isIOS
            ? const SystemUiOverlayStyle(
                statusBarBrightness: Brightness.dark,
                statusBarIconBrightness: Brightness.dark,
              )
            : const SystemUiOverlayStyle(
                statusBarBrightness: Brightness.light,
                statusBarIconBrightness: Brightness.light,
              ),
        flexibleSpace: CtAppbar3(
          onBack: () {
            if (onBack != null) {
              onBack!();
            } else {
              context.pop();
            }
          },
        ),
      ),
      body: child,
    );
  }
}

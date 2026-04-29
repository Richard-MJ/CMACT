import 'dart:io';

import 'package:caja_tacna_app/features/shared/widgets/ct_appbar_2.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:go_router/go_router.dart';

class CtLayout4 extends StatelessWidget {
  const CtLayout4({
    super.key,
    required this.child,
    required this.title,
    this.onBack,
  });

  final Widget child;
  final String title;
  final void Function()? onBack;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        systemOverlayStyle: Platform.isIOS
            ? const SystemUiOverlayStyle(
                statusBarBrightness: Brightness.dark,
                statusBarIconBrightness: Brightness.dark,
              )
            : const SystemUiOverlayStyle(
                statusBarBrightness: Brightness.light,
                statusBarIconBrightness: Brightness.light,
              ),
        toolbarHeight: 64,
        automaticallyImplyLeading: false,
        flexibleSpace: CtAppbar2(
          onBack: () {
            if (onBack != null) {
              onBack!();
            } else {
              context.pop();
            }
          },
          text: title,
        ),
      ),
      body: child,
    );
  }
}

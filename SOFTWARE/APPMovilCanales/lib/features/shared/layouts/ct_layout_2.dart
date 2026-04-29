import 'dart:io';

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class CtLayout2 extends ConsumerStatefulWidget {
  const CtLayout2({
    super.key,
    required this.child,
    required this.title,
    this.onBack,
    this.floatingActionButton
  });

  final Widget child;
  final Widget? floatingActionButton;
  final String title;
  final Future<void> Function()? onBack;

  @override
  CtLayout2State createState() => CtLayout2State();
}

class CtLayout2State extends ConsumerState<CtLayout2> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    EdgeInsets safeAreaPadding = MediaQuery.of(context).padding;

    return Scaffold(
      appBar: AppBar(
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
        toolbarHeight: 50,
        forceMaterialTransparency: true,
        flexibleSpace: Container(
          padding: EdgeInsets.only(
            left: 24,
            right: 24,
            top: safeAreaPadding.top,
          ),
          margin: const EdgeInsets.only(bottom: 0),
          height: 50 + safeAreaPadding.top,
          decoration: const BoxDecoration(
            color: AppColors.primary700,
            boxShadow: [
              BoxShadow(
                offset: Offset(0, 4),
                blurRadius: 4,
                spreadRadius: 0,
                color: Color.fromRGBO(225, 6, 0, 0.13),
              ),
            ],
          ),
          child: Row(
            children: [
              Container(
                width: 32,
                height: 32,
                decoration: const BoxDecoration(
                  shape: BoxShape.circle,
                  color: AppColors.primary600,
                ),
                child: TextButton(
                  style: TextButton.styleFrom(
                    shape: const CircleBorder(),
                    padding: EdgeInsets.zero,
                  ),
                  onPressed: () {
                    if (widget.onBack != null) {
                      widget.onBack!();
                    } else {
                      context.pop();
                    }
                  },
                  child: SvgPicture.asset(
                    'assets/icons/shared/chevron-left-black.svg',
                    colorFilter: const ColorFilter.mode(
                      AppColors.gray25,
                      BlendMode.srcIn,
                    ),
                    width: 24,
                    height: 24,
                  ),
                ),
              ),
              const SizedBox(
                width: 4,
              ),
              Text(
                widget.title,
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w500,
                  color: AppColors.gray25,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ],
          ),
        ),
      ),
      body: widget.child,
      floatingActionButton: widget.floatingActionButton,
    );
  }
}

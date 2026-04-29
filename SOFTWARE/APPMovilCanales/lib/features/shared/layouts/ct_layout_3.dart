import 'dart:io';

import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class CtLayout3 extends ConsumerStatefulWidget {
  const CtLayout3({
    super.key,
    required this.child,
  });

  final Widget child;

  @override
  CtLayout3State createState() => CtLayout3State();
}

class CtLayout3State extends ConsumerState<CtLayout3> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    EdgeInsets safeAreaPadding = MediaQuery.of(context).padding;

    return Scaffold(
      appBar: AppBar(
        elevation: 0,
        automaticallyImplyLeading: false,
        backgroundColor: Colors.white,
        forceMaterialTransparency: true,
        systemOverlayStyle: Platform.isIOS
            ? const SystemUiOverlayStyle(
                statusBarBrightness: Brightness.light,
                statusBarIconBrightness: Brightness.light,
              )
            : const SystemUiOverlayStyle(
                statusBarBrightness: Brightness.dark,
                statusBarIconBrightness: Brightness.dark,
              ),
        flexibleSpace: FlexibleSpaceBar(
          title: Container(
            color: Colors.white,
            padding: EdgeInsets.only(
              top: safeAreaPadding.top,
            ),
            child: Container(
              height: 10,
              decoration: const BoxDecoration(
                gradient: LinearGradient(
                  begin: Alignment.topCenter,
                  end: Alignment.bottomCenter,
                  colors: [
                    AppColors.primary700,
                    Color(0xFFE3130E),
                    AppColors.white,
                  ],
                  stops: [
                    -0.2058,
                    -0.2057,
                    1.0969
                  ], // Ajusta estos valores según tus necesidade
                ),
              ),
            ),
          ),
          titlePadding: EdgeInsets.zero,
          centerTitle: false,
        ),
        toolbarHeight: 10,
      ),
      body: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: widget.child,
          )
        ],
      ),
    );
  }
}

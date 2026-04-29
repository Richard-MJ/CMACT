import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_appbar.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class CtLayout1 extends ConsumerStatefulWidget {
  const CtLayout1({
    super.key,
    required this.child,
  });

  final Widget child;

  @override
  CtLayout1State createState() => CtLayout1State();
}

class CtLayout1State extends ConsumerState<CtLayout1> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppColors.gray100,
      appBar: const CtAppbar(),
      body: SafeArea(
        bottom: false,
        child: CustomScrollView(
          slivers: [
            SliverFillRemaining(
              child: widget.child,
            )
          ],
        ),
      ),
    );
  }
}

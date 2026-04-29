import 'package:flutter/material.dart';
import 'package:shimmer/shimmer.dart';

class CtShimmer extends StatelessWidget {
  const CtShimmer.rectangular({
    super.key,
    required this.width,
    required this.height,
    this.padding,
    this.margin,
  });

  final double width;
  final double height;
  final EdgeInsets? padding;
  final EdgeInsets? margin;

  @override
  Widget build(BuildContext context) {
    return Shimmer.fromColors(
      baseColor: const Color.fromARGB(148, 189, 189, 189),
      highlightColor: const Color.fromARGB(153, 238, 238, 238),
      child: Container(
        height: height,
        width: width,
        padding: padding,
        margin: margin,
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(4),
          color: Colors.red,
        ),
      ),
    );
  }
}

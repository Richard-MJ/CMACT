import 'package:flutter/material.dart';

class AutoMarqueeText extends StatefulWidget {
  final String text;
  final TextStyle style;
  final double height;
  final double velocity;
  final bool activo;

  const AutoMarqueeText({
    super.key,
    required this.text,
    required this.style,
    this.height = 20,
    this.velocity = 100,
    this.activo = true,
  });

  @override
  State<AutoMarqueeText> createState() => _AutoMarqueeTextState();
}

class _AutoMarqueeTextState extends State<AutoMarqueeText> {
  final ScrollController _scrollController = ScrollController();

  double _textWidth = 0;
  double _containerWidth = 0;
  Duration _duration = Duration.zero;
  bool _overflow = false;
  bool _scrolling = false;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) => _startScroll());
  }

  @override
  void didUpdateWidget(covariant AutoMarqueeText oldWidget) {
    super.didUpdateWidget(oldWidget);

    if (oldWidget.activo && !widget.activo) {
      _stopScroll();
      setState(() {
        _overflow = false;
      });
      return;
    }

    if ((!oldWidget.activo && widget.activo) ||
        oldWidget.text != widget.text ||
        oldWidget.velocity != widget.velocity) {
      _stopScroll();
      WidgetsBinding.instance.addPostFrameCallback((_) => _startScroll());
    }
  }

  void _startScroll() {
    if (!mounted || !widget.activo) return;

    final containerWidth = context.size?.width ?? 0;

    if (containerWidth == 0) {
      WidgetsBinding.instance.addPostFrameCallback((_) => _startScroll());
      return;
    }

    final tp = TextPainter(
      text: TextSpan(text: widget.text, style: widget.style),
      maxLines: 1,
      textDirection: TextDirection.ltr,
    )..layout();

    final textWidth = tp.width + 1;
    final overflowNow = textWidth > containerWidth;

    if (!overflowNow) return;

    setState(() {
      _textWidth = textWidth;
      _containerWidth = containerWidth;
      _overflow = true;
    });

    _duration = Duration(
      milliseconds:
          ((_textWidth + _containerWidth) / widget.velocity * 1000).toInt(),
    );

    _scrolling = true;
    _runScrollLoop();
  }

  Future<void> _runScrollLoop() async {
    while (_scrolling && mounted) {
      if (!_scrollController.hasClients) {
        await Future.delayed(const Duration(milliseconds: 50));
        continue;
      }

      final maxScroll = _scrollController.position.maxScrollExtent;

      if (maxScroll <= 0) {
        await Future.delayed(const Duration(milliseconds: 100));
        continue;
      }

      await _scrollController.animateTo(
        maxScroll,
        duration: _duration,
        curve: Curves.linear,
      );

      if (!mounted || !_scrolling) return;

      await Future.delayed(const Duration(milliseconds: 200));

      if (_scrollController.hasClients) {
        _scrollController.jumpTo(0);
      }

      await Future.delayed(const Duration(milliseconds: 200));
    }
  }

  void _stopScroll() {
    _scrolling = false;
    if (_scrollController.hasClients) {
      _scrollController.jumpTo(0);
    }
  }

  @override
  void dispose() {
    _scrolling = false;
    _scrollController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: widget.height,
      child: _overflow
          ? ListView(
              controller: _scrollController,
              scrollDirection: Axis.horizontal,
              physics: const NeverScrollableScrollPhysics(),
              children: [
                Text(widget.text, style: widget.style),
              ],
            )
          : Text(
              widget.text,
              style: widget.style,
              maxLines: 1,
              overflow: TextOverflow.ellipsis,
            ),
    );
  }
}

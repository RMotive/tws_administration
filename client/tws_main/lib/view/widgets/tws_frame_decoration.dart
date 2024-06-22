import 'package:flutter/material.dart';

final class TWSFrameDecoration extends StatelessWidget {
  final Widget child;
  final double topPadding;
  const TWSFrameDecoration({
    super.key,
    this.topPadding = 8,
    required this.child,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(
        top: topPadding,
      ),
      child: DecoratedBox(
        decoration: const BoxDecoration(
          border: Border.fromBorderSide(
            BorderSide(
              color: Colors.white24,
              width: 2,
            ),
          ),
        ),
        child: child,
      ),
    );
  }
}

import 'package:flutter/material.dart';

class TWSButtonChip extends StatelessWidget {
  /// Defines specific size.
  final Size? size;
  final Widget icon;
  final String label;

  const TWSButtonChip({
    super.key,
    this.size,
    required this.icon,
    required this.label,
  });

  @override
  Widget build(BuildContext context) {
    return ColoredBox(
      color: Colors.red,
      child: SizedBox(
        width: size?.width ?? 45,
        height: size?.height ?? 45,
        child: LayoutBuilder(
          builder: (_, BoxConstraints constrains) {
            return Column(
              children: <Widget>[
                icon,
                Text(label),
              ],
            );
          },
        ),
      ),
    );
  }
}

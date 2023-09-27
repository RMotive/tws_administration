import 'package:flutter/material.dart';

class ThemedWidget extends StatelessWidget {
  final Widget Function(BuildContext ctx, ThemeData theme) builder;

  const ThemedWidget({
    super.key,
    required this.builder,
  });

  @override
  Widget build(BuildContext context) {
    final ThemeData theme = Theme.of(context);
    return builder(context, theme);
  }
}

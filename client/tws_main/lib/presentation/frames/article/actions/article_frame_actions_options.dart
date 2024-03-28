import 'package:flutter/material.dart';

class ArticleFrameActionsOptions {
  final String tooltip;
  final Widget Function(double size, Color color) decorator;
  final VoidCallback action;

  const ArticleFrameActionsOptions({
    required this.tooltip,
    required this.decorator,
    required this.action,
  });
}

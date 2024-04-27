import 'package:cosmos_foundation/csm_foundation.dart';
import 'package:flutter/material.dart';

class ArticleOptions {
  final Widget Function(Color? stateColor) icon;
  final String title;
  final CSMRouteOptions? route;

  const ArticleOptions({
    required this.icon,
    required this.title,
    this.route,
  });
}

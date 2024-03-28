import 'package:cosmos_foundation/models/options/route_options.dart';
import 'package:flutter/material.dart';

class ArticleOptions {
  final Widget Function(Color? stateColor) icon;
  final String title;
  final RouteOptions? route;

  const ArticleOptions({
    required this.icon,
    required this.title,
    this.route,
  });
}

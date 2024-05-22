import 'package:flutter/material.dart';

final class TWSArticleTableFieldOptions<T> {
  final String name;
  final bool tip;
  final String Function(T item, int index, BuildContext ctx) factory;

  const TWSArticleTableFieldOptions(this.name, this.factory, [this.tip = false]);
}

import 'package:cosmos_foundation/contracts/cosmos_theme_base.dart';
import 'package:flutter/material.dart';

abstract class ThemeBase extends CosmosThemeBase {
  final Color backgroundColor;

  const ThemeBase(
    String themeIdentifier, {
    this.backgroundColor = Colors.black,
  }) : super(themeIdentifier);
}

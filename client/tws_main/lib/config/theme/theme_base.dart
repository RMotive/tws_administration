import 'package:cosmos_foundation/contracts/cosmos_theme_base.dart';
import 'package:flutter/material.dart';

class ThemeBase extends CosmosThemeBase {
  // --> Primary pallette bundle
  final ColorBundle primaryColor;
  final ColorBundle onPrimaryColorFirstControlColor;
  final ColorBundle onPrimaryColorSecondControlColor;

  const ThemeBase(
    super.themeIdentifier, {
    // --> Primary bundle
    required this.primaryColor,
    required this.onPrimaryColorFirstControlColor,
    required this.onPrimaryColorSecondControlColor,
  });
}

final class ColorBundle {
  final Color mainColor;
  final Color textColor;
  final Color? counterColor;
  const ColorBundle({
    required this.mainColor,
    required this.textColor,
    this.counterColor,
  });
}

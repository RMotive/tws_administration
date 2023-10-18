import 'package:cosmos_foundation/contracts/cosmos_theme_base.dart';
import 'package:flutter/material.dart';

class ThemeBase extends CosmosThemeBase {
  // --> Surface tint colors
  final Color primaryColor;
  // --> Text color over surface tints
  final Color onPrimaryColorFirstTextColor;
  // --> Controls color over surface tints
  final Color onPrimaryColorFirstControlColor;

  const ThemeBase({
    required this.primaryColor,
    required this.onPrimaryColorFirstTextColor,
    required this.onPrimaryColorFirstControlColor,
  });
}

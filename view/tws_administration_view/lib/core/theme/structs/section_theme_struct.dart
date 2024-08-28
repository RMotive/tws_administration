import 'package:flutter/material.dart';

/// Custom theme struct.
///
/// Defined to handle the theme specified to a [SectionComponent]
class SectionThemeStruct {
  /// Defines the style to the title section text.
  final TextStyle? titleStyle;

  /// Defines the border color for the section border.
  final Color? borderColor;

  const SectionThemeStruct([
    this.borderColor,
    this.titleStyle,
  ]);
}

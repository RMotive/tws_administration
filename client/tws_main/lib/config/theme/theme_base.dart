import 'package:cosmos_foundation/contracts/cosmos_theme_base.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';

/// --> General shortcut access for the business theme
ThemeBase get twsTheme => getTheme();

class ThemeBase extends CosmosThemeBase {
  final ColorBundle errorColor;

  // --> Primary pallette bundle
  final ColorBundle primaryColor;
  final ColorBundle onPrimaryColorFirstControlColor;
  final ColorBundle onPrimaryColorSecondControlColor;

  // --> Text theming
  final StyleBundle headerStyle;

  const ThemeBase(
    super.themeIdentifier, {
    required this.errorColor,
    required this.primaryColor,
    required this.onPrimaryColorFirstControlColor,
    required this.onPrimaryColorSecondControlColor,
  }) : headerStyle = const StyleBundle(
          title: TextStyle(
            fontSize: 24,
            fontStyle: FontStyle.italic,
            fontWeight: FontWeight.w600,
          ),
          content: TextStyle(),
        );
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

final class StyleBundle {
  final TextStyle title;
  final TextStyle? subject;
  final TextStyle content;

  const StyleBundle({
    required this.title,
    required this.content,
    this.subject,
  });
}

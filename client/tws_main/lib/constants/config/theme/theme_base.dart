import 'package:cosmos_foundation/contracts/cosmos_theme_base.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';

export './dark_theme.dart';
export './light_theme.dart';

/// --> General shortcut access for the business theme
TWSThemeBase get twsTheme => getTheme();

class TWSThemeBase extends CosmosThemeBase {
  final ColorBundle errorColor;

  // --> Primary pallette bundle
  final ColorBundle primaryColor;
  final ColorBundle onPrimaryColorFirstControlColor;
  final ColorBundle onPrimaryColorSecondControlColor;

  // --> Text theming
  final StyleBundle headerStyle;

  const TWSThemeBase(
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

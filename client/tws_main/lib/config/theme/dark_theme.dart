import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/constants/theme_constants.dart';

class DarkTheme extends ThemeBase {
  static const Color _mainTextExtendedColor = Colors.white70;

  const DarkTheme()
      : super(
          darkThemeIdentifier,
          primaryColor: const ColorBundle(
            mainColor: Color(0xFF363636),
            textColor: _mainTextExtendedColor,
          ),
          onPrimaryColorFirstControlColor: const ColorBundle(
            mainColor: Color(0xff575757),
            textColor: _mainTextExtendedColor,
          ),
          onPrimaryColorSecondControlColor: const ColorBundle(
            mainColor: Colors.blueGrey,
            textColor: _mainTextExtendedColor,
          ),
        );
}

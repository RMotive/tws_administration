import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/constants/theme_constants.dart';

class DarkTheme extends ThemeBase {
  static const Color _mainTextExtendedColor = Colors.white70;

  const DarkTheme()
      : super(
          darkThemeIdentifier,
          errorColor: const ColorBundle(
            mainColor: Color(0xa8a3393d),
            textColor: Colors.red,
          ),
          primaryColor: const ColorBundle(
            mainColor: Color(0xFF363636),
            textColor: _mainTextExtendedColor,
            counterColor: Color(0xFFcfcfcf),
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

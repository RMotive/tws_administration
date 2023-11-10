import 'package:flutter/material.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';
import 'package:tws_main/constants/theme_constants.dart';

class LightTheme extends ThemeBase {
  const LightTheme()
      : super(
          lightThemeIdentifier,
          errorColor: const ColorBundle(
            mainColor: Color(0x16ff0000),
            textColor: Color(0xff800101),
          ),
          primaryColor: const ColorBundle(
            mainColor: Color(0xffe6e3e4),
            textColor: Colors.black87,
            counterColor: Color(0xdd363636),
          ),
          onPrimaryColorFirstControlColor: const ColorBundle(
            mainColor: Color(0xffe8f2ff),
            textColor: Colors.black,
          ),
          onPrimaryColorSecondControlColor: const ColorBundle(
            mainColor: Colors.blue,
            textColor: Colors.black,
          ),
        );
}

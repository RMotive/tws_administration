import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/constants/theme_constants.dart';

class LightTheme extends ThemeBase {
  const LightTheme()
      : super(
          lightThemeIdentifier,
          primaryColor: const ColorBundle(
            mainColor: Color(0xFFcfcfcf),
            textColor: Colors.black,
            counterColor: Color(0xFF363636),
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

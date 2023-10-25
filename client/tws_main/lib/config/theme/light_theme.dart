import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';

class LightTheme extends ThemeBase {
  const LightTheme()
      : super(
          primaryColor: const ColorBundle(
            mainColor: Color(0xFFcfcfcf),
            textColor: Colors.black,
          ),
          onPrimaryColorFirstControlColor: const ColorBundle(
            mainColor: Color(0xffe8f2ff),
            textColor: Colors.white,
          ),
          onPrimaryColorSecondControlColor: const ColorBundle(
            mainColor: Colors.blue,
            textColor: Colors.black38,
          ),
        );
}

import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';

class LightTheme extends ThemeBase {
  const LightTheme()
      : super(
          primaryColor: const Color(0xFFcfcfcf),
          onPrimaryColorFirstTextColor: Colors.black,
          onPrimaryColorFirstControlColor: const Color(0xffe8f2ff),
        );
}

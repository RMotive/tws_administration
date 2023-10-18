import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';

class DarkTheme extends ThemeBase {
  const DarkTheme()
      : super(
          primaryColor: const Color(0xFF363636),
          onPrimaryColorFirstTextColor: Colors.white,
          onPrimaryColorFirstControlColor: const Color(0xffffffff),
        );
}

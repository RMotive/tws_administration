import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/k_colors.dart';
import 'package:tws_main/core/theme/theme_base.dart';

class DarkTheme extends ThemeBase {
  static const String identifier = 'dark-flat-theme';

  const DarkTheme()
      : super(
          identifier,
          frameListenerColor: KColors.warmWhite,
          pageColorStruct: const ThemeColorStruct(
            KColors.deepPurple,
            KColors.warmWhite,
            KColors.oceanBlue,
            onColorAlt: KColors.darkGrey,
          ),
          primaryControlColorStruct: const ThemeColorStruct(
            Colors.transparent,
            KColors.darkGrey,
            KColors.oceanBlue,
            onColorAlt: KColors.warmWhite,
          ),
        );
}
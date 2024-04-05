import 'package:cosmos_foundation/theme/theme_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/twsa_assets.dart';
import 'package:tws_main/core/constants/twsa_colors.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';

class TWSAThemeLight extends TWSAThemeBase {
  static const String kIdentifier = 'light-flat-theme';

  const TWSAThemeLight()
      : super(
          kIdentifier,
          loginLogo: TWSAAssets.fullLogoBlackWebp,
          masterLayoutMenuLogo: TWSAAssets.wideLogoWhiteWebp,
          frame: TWSAColors.deepPurple,
          masterLayoutStruct: const CSMColorThemeOptions(
            Colors.transparent,
            Colors.transparent,
            Colors.transparent,
          ),
          pageColorStruct: const CSMColorThemeOptions(
            TWSAColors.warmWhite,
            TWSAColors.deepPurple,
            TWSAColors.oceanBlue,
            foreAlt: TWSAColors.darkGrey,
          ),
          primaryControlColorStruct: const CSMColorThemeOptions(
            Colors.transparent,
            TWSAColors.darkGrey,
            TWSAColors.oceanBlue,
            foreAlt: TWSAColors.deepPurple,
          ),
          primaryDisabledControlColorStruct: const CSMColorThemeOptions(
            Colors.transparent,
            TWSAColors.darkGrey,
            Colors.red,
          ),
          primaryErrorControlColorStruct: const CSMColorThemeOptions(
            Colors.transparent,
            TWSAColors.darkGrey,
            Colors.red,
          ),
          articlesLayoutSelectorButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(),
          ),
          masterLayoutMenuButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(
              background: Colors.transparent,
              foreground: TWSAColors.warmWhite,
            ),
          ),
          articlesLayoutActionButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(),
          ),
        );
}

import 'package:cosmos_foundation/theme/theme_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/k_assets.dart';
import 'package:tws_main/core/constants/k_colors.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';

class TWSAThemeLight extends TWSAThemeBase {
  static const String kIdentifier = 'light-flat-theme';

  const TWSAThemeLight()
      : super(
          kIdentifier,
          loginLogo: KAssets.fullLogoBlackWebp,
          masterLayoutMenuLogo: KAssets.wideLogoWhiteWebp,
          frame: KColors.deepPurple,
          masterLayoutStruct: const CSMColorThemeOptions(
            Colors.transparent,
            Colors.transparent,
            Colors.transparent,
          ),
          pageColorStruct: const CSMColorThemeOptions(
            KColors.warmWhite,
            KColors.deepPurple,
            KColors.oceanBlue,
            foreAlt: KColors.darkGrey,
          ),
          primaryControlColorStruct: const CSMColorThemeOptions(
            Colors.transparent,
            KColors.darkGrey,
            KColors.oceanBlue,
            foreAlt: KColors.deepPurple,
          ),
          primaryDisabledControlColorStruct: const CSMColorThemeOptions(
            Colors.transparent,
            KColors.darkGrey,
            Colors.red,
          ),
          primaryErrorControlColorStruct: const CSMColorThemeOptions(
            Colors.transparent,
            KColors.darkGrey,
            Colors.red,
          ),
          articlesLayoutSelectorButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(),
          ),
          masterLayoutMenuButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(
              background: Colors.transparent,
              foreground: KColors.warmWhite,
            ),
          ),
          articlesLayoutActionButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(),
          ),
        );
}

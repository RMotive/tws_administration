import 'package:cosmos_foundation/theme/theme_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/k_assets.dart';
import 'package:tws_main/core/constants/k_colors.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';

final class TWSAThemeDark extends TWSAThemeBase {
  static const String kIdentifier = 'dark-flat-theme';

  const TWSAThemeDark()
      : super(
          kIdentifier,
          loginLogo: KAssets.fullLogoWhiteWebp,
          masterLayoutMenuLogo: KAssets.wideLogoBlackWebp,
          frame: KColors.warmWhite,
          masterLayoutStruct: const CSMColorThemeOptions(
            KColors.oceanBlue,
            KColors.warmWhite,
            Colors.transparent,
          ),
          pageColorStruct: const CSMColorThemeOptions(
            KColors.lightDark,
            KColors.warmWhite,
            KColors.oceanBlue,
            foreAlt: KColors.darkGrey,
          ),
          primaryControlColorStruct: const CSMColorThemeOptions(
            KColors.oceanBlue,
            KColors.darkGrey,
            KColors.oceanBlue,
            foreAlt: KColors.warmWhite,
          ),
          primaryDisabledControlColorStruct: const CSMColorThemeOptions(
            KColors.darkGrey,
            KColors.darkGrey,
            KColors.darkGrey,
            foreAlt: KColors.warmWhite,
          ),
          primaryErrorControlColorStruct: const CSMColorThemeOptions(
            Colors.transparent,
            Color.fromARGB(255, 208, 136, 130),
            KColors.smoothWine,
          ),
          articlesLayoutSelectorButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(
              background: KColors.oceanBlue,
              foreground: KColors.warmWhite,
            ),
            hoverStruct: CSMGenericThemeOptions(
              background: KColors.oceanBlueH,
            ),
            selectStruct: CSMGenericThemeOptions(
              background: KColors.oceanBlueH,
            ),
          ),
          masterLayoutMenuButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(
              background: Colors.transparent,
              foreground: KColors.warmWhite,
              textStyle: TextStyle(
                fontSize: 14,
              ),
            ),
            hoverStruct: CSMGenericThemeOptions(
              background: Colors.white10,
            ),
            selectStruct: CSMGenericThemeOptions(
              background: Colors.white10,
              foreground: KColors.warmWhite,
            ),
          ),
          articlesLayoutActionButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(
              background: KColors.oceanBlue,
              foreground: KColors.warmWhite,
            ),
            hoverStruct: CSMGenericThemeOptions(
              background: KColors.oceanBlueH,
              foreground: Colors.white60,
            ),
            selectStruct: CSMGenericThemeOptions(
              background: KColors.oceanBlueH,
            ),
          ),
        );
}

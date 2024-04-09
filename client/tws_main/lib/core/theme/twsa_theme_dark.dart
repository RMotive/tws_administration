import 'package:cosmos_foundation/theme/theme_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/twsa_assets.dart';
import 'package:tws_main/core/constants/twsa_colors.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';

final class TWSAThemeDark extends TWSAThemeBase {
  static const String kIdentifier = 'dark-flat-theme';

  const TWSAThemeDark()
      : super(
          kIdentifier,
          loginLogo: TWSAAssets.fullLogoWhiteWebp,
          masterLayoutMenuLogo: TWSAAssets.wideLogoBlackWebp,
          frame: TWSAColors.warmWhite,
          masterLayoutStruct: const CSMColorThemeOptions(
            TWSAColors.oceanBlue,
            TWSAColors.warmWhite,
            Colors.transparent,
          ),
          pageColorStruct: const CSMColorThemeOptions(
            TWSAColors.lightDark,
            TWSAColors.warmWhite,
            TWSAColors.oceanBlue,
            foreAlt: TWSAColors.darkGrey,
          ),
          primaryControlColorStruct: const CSMColorThemeOptions(
            TWSAColors.oceanBlue,
            TWSAColors.darkGrey,
            TWSAColors.oceanBlue,
            foreAlt: TWSAColors.warmWhite,
          ),
          primaryDisabledControlColorStruct: const CSMColorThemeOptions(
            TWSAColors.darkGrey,
            TWSAColors.darkGrey,
            TWSAColors.darkGrey,
            foreAlt: TWSAColors.warmWhite,
          ),
          primaryErrorControlColorStruct: const CSMColorThemeOptions(
            Colors.transparent,
            Color.fromARGB(255, 208, 136, 130),
            TWSAColors.smoothWine,
          ),
          articlesLayoutSelectorButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(
              background: TWSAColors.oceanBlue,
              foreground: TWSAColors.warmWhite,
            ),
            hoverStruct: CSMGenericThemeOptions(
              background: TWSAColors.oceanBlueH,
            ),
            selectStruct: CSMGenericThemeOptions(
              background: TWSAColors.oceanBlueH,
            ),
          ),
          masterLayoutMenuButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(
              background: Colors.transparent,
              foreground: TWSAColors.warmWhite,
              textStyle: TextStyle(
                fontSize: 14,
              ),
            ),
            hoverStruct: CSMGenericThemeOptions(
              background: Colors.white10,
            ),
            selectStruct: CSMGenericThemeOptions(
              background: Colors.white10,
              foreground: TWSAColors.warmWhite,
            ),
          ),
          articlesLayoutActionButtonStruct: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(
              background: TWSAColors.oceanBlue,
              foreground: TWSAColors.warmWhite,
            ),
            hoverStruct: CSMGenericThemeOptions(
              background: TWSAColors.oceanBlueH,
              foreground: Colors.white60,
            ),
            selectStruct: CSMGenericThemeOptions(
              background: TWSAColors.oceanBlueH,
            ),
          ),
        );
}

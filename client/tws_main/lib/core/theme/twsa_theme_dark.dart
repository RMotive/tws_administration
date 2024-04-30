import 'package:csm_foundation_view/csm_foundation_view.dart';
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
          masterLayout: const CSMColorThemeOptions(
            TWSAColors.oceanBlue,
            TWSAColors.warmWhite,
            Colors.transparent,
          ),
          pageColor: const CSMColorThemeOptions(
            TWSAColors.lightDark,
            TWSAColors.warmWhite,
            TWSAColors.oceanBlue,
            foreAlt: TWSAColors.darkGrey,
          ),
          primaryControlColor: const CSMColorThemeOptions(
            TWSAColors.oceanBlue,
            TWSAColors.darkGrey,
            TWSAColors.oceanBlue,
            foreAlt: TWSAColors.warmWhite,
          ),
          primaryDisabledControlColor: const CSMColorThemeOptions(
            TWSAColors.darkGrey,
            TWSAColors.darkGrey,
            TWSAColors.darkGrey,
            foreAlt: TWSAColors.warmWhite,
          ),
          primaryErrorControlColor: const CSMColorThemeOptions(
            Colors.transparent,
            Color.fromARGB(255, 208, 136, 130),
            TWSAColors.smoothWine,
          ),
          articlesLayoutSelectorButtonState: const CSMStateThemeOptions(
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
          masterLayoutMenuButtonState: const CSMStateThemeOptions(
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
          articlesLayoutActionButtonState: const CSMStateThemeOptions(
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
          primaryControlState: const CSMStateThemeOptions(
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
        );
}

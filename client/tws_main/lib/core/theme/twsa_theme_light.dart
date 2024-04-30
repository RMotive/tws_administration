import 'package:csm_foundation_view/csm_foundation_view.dart';
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
          masterLayout: const CSMColorThemeOptions(
            Colors.transparent,
            Colors.transparent,
            Colors.transparent,
          ),
          pageColor: const CSMColorThemeOptions(
            TWSAColors.warmWhite,
            TWSAColors.deepPurple,
            TWSAColors.oceanBlue,
            foreAlt: TWSAColors.darkGrey,
          ),
          primaryControlColor: const CSMColorThemeOptions(
            Colors.transparent,
            TWSAColors.darkGrey,
            TWSAColors.oceanBlue,
            foreAlt: TWSAColors.deepPurple,
          ),
          primaryDisabledControlColor: const CSMColorThemeOptions(
            Colors.transparent,
            TWSAColors.darkGrey,
            Colors.red,
          ),
          primaryErrorControlColor: const CSMColorThemeOptions(
            Colors.transparent,
            TWSAColors.darkGrey,
            Colors.red,
          ),
          articlesLayoutSelectorButtonState: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(),
          ),
          masterLayoutMenuButtonState: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(
              background: Colors.transparent,
              foreground: TWSAColors.warmWhite,
            ),
          ),
          articlesLayoutActionButtonState: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(),
          ),
          primaryControlState: const CSMStateThemeOptions(
            main: CSMGenericThemeOptions(),
          ),
        );
}

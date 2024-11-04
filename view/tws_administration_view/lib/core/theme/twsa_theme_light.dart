import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/constants/twsa_assets.dart';
import 'package:tws_administration_view/core/constants/twsa_colors.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';

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
            page: const CSMColorThemeOptions(
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
            primaryDisabledControl: const CSMColorThemeOptions(
            Colors.transparent,
            TWSAColors.darkGrey,
            Colors.red,
          ),
            primaryCriticalControl: const CSMColorThemeOptions(
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
            criticalControlState: const CSMStateThemeOptions(
              main: CSMGenericThemeOptions(),
            )
        );
}

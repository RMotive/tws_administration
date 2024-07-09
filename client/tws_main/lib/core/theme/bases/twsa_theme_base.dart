import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:tws_main/core/theme/structs/section_theme_struct.dart';

abstract class TWSAThemeBase extends CSMThemeBase {
  final String loginLogo;
  final String masterLayoutMenuLogo;
  final CSMColorThemeOptions page;
  final CSMColorThemeOptions masterLayout;
  final CSMColorThemeOptions primaryControlColor;
  final CSMColorThemeOptions primaryDisabledControlColor;
  final CSMColorThemeOptions primaryErrorControlColor;
  final CSMStateThemeOptions masterLayoutMenuButtonState;
  final CSMStateThemeOptions articlesLayoutSelectorButtonState;
  final CSMStateThemeOptions articlesLayoutActionButtonState;

  final CSMStateThemeOptions primaryControlState;
  final CSMStateThemeOptions criticalControlState;

  /// [Optional] Custom theme struct for a Section component.
  final SectionThemeStruct? twsSectionStruct;

  const TWSAThemeBase(
    super.identifier, {
    super.frame,
    this.twsSectionStruct,
    required this.loginLogo,
    required this.page,
    required this.masterLayout,
    required this.masterLayoutMenuLogo,
    required this.primaryControlState,
    required this.primaryControlColor,
    required this.criticalControlState,
    required this.masterLayoutMenuButtonState,
    required this.primaryErrorControlColor,
    required this.articlesLayoutActionButtonState,
    required this.primaryDisabledControlColor,
    required this.articlesLayoutSelectorButtonState,
  });
}

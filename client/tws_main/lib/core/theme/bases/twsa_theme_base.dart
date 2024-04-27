import 'package:cosmos_foundation/csm_foundation.dart';
import 'package:tws_main/core/theme/structs/section_theme_struct.dart';

abstract class TWSAThemeBase extends CSMThemeBase {
  final String loginLogo;
  final String masterLayoutMenuLogo;
  final CSMColorThemeOptions pageColor;
  final CSMColorThemeOptions masterLayout;
  final CSMColorThemeOptions primaryControlColor;
  final CSMColorThemeOptions primaryDisabledControlColor;
  final CSMColorThemeOptions primaryErrorControlColor;
  final CSMStateThemeOptions masterLayoutMenuButtonState;
  final CSMStateThemeOptions articlesLayoutSelectorButtonState;
  final CSMStateThemeOptions articlesLayoutActionButtonState;
  final CSMStateThemeOptions primaryControlState;

  /// [Optional] Custom theme struct for a Section component.
  final SectionThemeStruct? twsSectionStruct;

  const TWSAThemeBase(
    super.identifier, {
    super.frame,
    this.twsSectionStruct,
    required this.loginLogo,
    required this.pageColor,
    required this.masterLayout,
    required this.masterLayoutMenuLogo,
    required this.primaryControlState,
    required this.primaryControlColor,
    required this.masterLayoutMenuButtonState,
    required this.primaryErrorControlColor,
    required this.articlesLayoutActionButtonState,
    required this.primaryDisabledControlColor,
    required this.articlesLayoutSelectorButtonState,
  });
}

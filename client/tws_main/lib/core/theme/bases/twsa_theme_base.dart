import 'package:cosmos_foundation/theme/theme_module.dart';
import 'package:tws_main/core/theme/structs/section_theme_struct.dart';

abstract class TWSAThemeBase extends CSMThemeBase {
  final String loginLogo;
  final String masterLayoutMenuLogo;
  final CSMColorThemeOptions pageColorStruct;
  final CSMColorThemeOptions masterLayoutStruct;
  final CSMColorThemeOptions primaryControlColorStruct;
  final CSMColorThemeOptions primaryDisabledControlColorStruct;
  final CSMColorThemeOptions primaryErrorControlColorStruct;
  final CSMStateThemeOptions masterLayoutMenuButtonStruct;
  final CSMStateThemeOptions articlesLayoutSelectorButtonStruct;
  final CSMStateThemeOptions articlesLayoutActionButtonStruct;

  /// [Optional] Custom theme struct for a Section component.
  final SectionThemeStruct? twsSectionStruct;

  const TWSAThemeBase(
    super.identifier, {
    super.frame,
    this.twsSectionStruct,
    required this.loginLogo,
    required this.pageColorStruct,
    required this.masterLayoutStruct,
    required this.masterLayoutMenuLogo,
    required this.primaryControlColorStruct,
    required this.masterLayoutMenuButtonStruct,
    required this.primaryErrorControlColorStruct,
    required this.articlesLayoutActionButtonStruct,
    required this.primaryDisabledControlColorStruct,
    required this.articlesLayoutSelectorButtonStruct,
  });
}

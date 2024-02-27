import 'package:cosmos_foundation/contracts/cosmos_theme_base.dart';
import 'package:cosmos_foundation/models/structs/states_control_theme_struct.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
import 'package:tws_main/core/theme/structs/section_theme_struct.dart';

abstract class ThemeBase extends CosmosThemeBase {
  final String loginLogo;
  final String masterLayoutMenuLogo;
  final StateControlThemeStruct masterLayoutMenuButtonStruct;
  final ThemeColorStruct pageColorStruct;
  final ThemeColorStruct masterLayoutStruct;
  final ThemeColorStruct primaryControlColorStruct;
  final ThemeColorStruct primaryDisabledControlColorStruct;
  final ThemeColorStruct primaryErrorControlColorStruct;

  /// [Optional] Custom theme struct for a Section component.
  final SectionThemeStruct? twsSectionStruct;

  const ThemeBase(
    super.themeIdentifier, {
    super.frameListenerColor,
    this.twsSectionStruct,
    required this.masterLayoutStruct,
    required this.masterLayoutMenuButtonStruct,
    required this.loginLogo,
    required this.masterLayoutMenuLogo,
    required this.pageColorStruct,
    required this.primaryControlColorStruct,
    required this.primaryDisabledControlColorStruct,
    required this.primaryErrorControlColorStruct,
  });
}

import 'package:cosmos_foundation/contracts/cosmos_theme_base.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
abstract class ThemeBase extends CosmosThemeBase {
  final String fullLogoLocation;
  final ThemeColorStruct pageColorStruct;
  final ThemeColorStruct primaryControlColorStruct;
  final ThemeColorStruct primaryDisabledControlColorStruct;
  final ThemeColorStruct primaryErrorControlColorStruct;

  const ThemeBase(
    super.themeIdentifier, {
    super.frameListenerColor,
    required this.fullLogoLocation,
    required this.pageColorStruct,
    required this.primaryControlColorStruct,
    required this.primaryDisabledControlColorStruct,
    required this.primaryErrorControlColorStruct,
  });
}

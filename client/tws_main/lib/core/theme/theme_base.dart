import 'package:cosmos_foundation/contracts/cosmos_theme_base.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
abstract class ThemeBase extends CosmosThemeBase {
  final ThemeColorStruct pageColorStruct;
  final ThemeColorStruct primaryControlColorStruct;

  const ThemeBase(
    super.themeIdentifier, {
    super.frameListenerColor,
    required this.pageColorStruct,
    required this.primaryControlColorStruct,
  });
}

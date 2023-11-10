import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:tws_main/business/contracts/client_service_contract.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';
import 'package:tws_main/constants/theme_constants.dart';

/// Client side service.
///
/// This service allows the user to fetch client-side data
/// related to Theme, and actions related to that stored reference.
class ThemeClientService extends ClientServiceContract {
  /// Stores the private instance reference pointing.
  static ThemeClientService? _instance;

  /// Validates the correct instance of the current singleton object, and returns
  /// the useful correct one.
  static ThemeClientService get i => _instance ??= ThemeClientService._();

  /// Private auto-constructor to avoid the public use of it.
  ThemeClientService._()
      : super(
          themeClientReference,
        );

  /// Fetches the current theme from the client-side storage.
  ///
  /// Return:
  ///   The current theme retrieved from the client-side storage, if the reference
  ///   wasn't found will return with the default theme declared.
  /// Exceptions:
  ///   - This method only can generate uncaught exceptions.
  Future<ThemeBase> fetchCurrentTheme() async {
    ThemeBase? storedTheme = await getThemeFromStore(
      storageReference,
      forcedThemes: themeCollection,
    );
    if (storedTheme != null) return storedTheme;

    ThemeBase themeBase = themeCollection
        .where(
          (ThemeBase element) => element.themeIdentifier == defaultThemeIdentifier,
        )
        .first;
    return themeBase;
  }

  /// Posts and updates the current theme stored in the client-side storage.
  ///
  /// [identifier] The subscribed identifier on the theme class that contracts
  /// with [CosmosThemeBase].
  ///
  /// Return:
  ///   - void.
  ///
  /// Exceptions:
  ///   - This method don't generate internal exceptions.
  Future<void> updateCurrentTheme(String identifier) async {
    updateTheme(
      identifier,
      saveLocalKey: storageReference,
    );
  }
}

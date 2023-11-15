import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:tws_main/business/services/client/theme_client_service.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';
import 'package:tws_main/constants/theme_constants.dart';

void main() {
  // --> Init resources
  initTheme<TWSThemeBase>(
    const TWSDarkTheme(),
    <TWSThemeBase>[
      const TWSDarkTheme(),
      const TWSLightTheme(),
    ],
  );

  // --> TESTS

  /// Internal service reference of [ThemeClientService] to work with testing
  final ThemeClientService service = ThemeClientService.i;
  test(
    'Theme is being stored correctly (client-side)',
    () async {
      await service.updateCurrentTheme(lightThemeIdentifier);
      final TWSThemeBase firstCheck = await service.fetchCurrentTheme();
      await service.updateCurrentTheme(darkThemeIdentifier);
      final TWSThemeBase secondCheck = await service.fetchCurrentTheme();

      expect(firstCheck, isA<TWSLightTheme>(), reason: 'first update storage failed');
      expect(secondCheck, isA<TWSDarkTheme>(), reason: 'second update storage failed');
    },
  );
  test(
    'Theme is being gotten correctly (client-side)',
    () async {
      final TWSThemeBase firstCheck = await service.fetchCurrentTheme();

      final bool assertFirstCheck = (firstCheck is TWSDarkTheme || firstCheck is TWSLightTheme);
      expect(assertFirstCheck, true, reason: 'first check fetch failed');
    },
  );
}

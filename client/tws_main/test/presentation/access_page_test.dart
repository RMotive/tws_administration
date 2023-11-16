import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';
import 'package:tws_main/main.dart';
import 'package:tws_main/presentation/pages/access_page/access_page.dart';

void main() {
  TestWidgetsFlutterBinding.ensureInitialized();
  initTheme(
    const TWSDarkTheme(),
    <TWSThemeBase>[
      const TWSDarkTheme(),
      const TWSLightTheme(),
    ],
  );

  testWidgets(
    'access page loads',
    (WidgetTester widgetTester) async {
      // --> Pumping the entire application context.
      await widgetTester.runAsync(
        () async {
          await widgetTester.pumpWidget(const TWSAdminApp());
          await Future<void>.delayed(5.seconds);
          await widgetTester.pumpAndSettle();

          Finder check = find.byType(AccessPage);

          expect(check, findsOneWidget, reason: 'Access page not found');
        },
      );
    },
  );
}

import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:tws_main/main.dart';

void main() {
  TestWidgetsFlutterBinding.ensureInitialized();

  testWidgets(
    'access page loads',
    (WidgetTester widgetTester) async {
      // --> Pumping the entire application context.
      await widgetTester.runAsync(
        () async {
          await widgetTester.pumpWidget(const TWSAdminApp());
          await widgetTester.pump(120.seconds);
        },
      );
      // --> Finding if the access page loads correctly as initial page.
      Finder pageFinder = find.text('TWS Admin Services');
      expect(
        pageFinder,
        findsOneWidget,
        reason: 'Page not found',
      );
    },
  );
}

import 'package:cosmos_foundation/contracts/cosmos_screen.dart';
import 'package:cosmos_foundation/foundation/conditionals/responsive_view.dart';
import 'package:cosmos_foundation/foundation/simplifiers/separator_column.dart';
import 'package:cosmos_foundation/helpers/route_driver.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/options/route_options.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/routes/tws_routes.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/presentation/pages/mobile_disabled_page/mobile_disabled_page.dart';
import 'package:tws_main/presentation/widgets/tws_drawer_button.dart';

// --> Parts
part 'widgets/main_screen_menu_drawer.dart';
part '../../widgets/widget_options/msmd_button_option.dart';
// --> Helpers
final RouteDriver _router = RouteDriver.i;

/// UI Screen for Main app functionallity wrapper.
/// Draws a complex screen that wraps a collection of pages, this will handle the behavior of drawing and handling all the
/// recurrent content along the display that will still there when the pages are being routed along.
class MainScreen extends CosmosScreen {
  /// A new [MainScreen] UI Screen configuration object.
  const MainScreen({
    super.key,
    required super.page,
  });

  @override
  Widget build(BuildContext context) {
    return ResponsiveView(
      onSmall: const MobileDisabledPage(),
      onLarge: Row(
        children: <Widget>[
          const _MainScreenMenuDrawer(
            buttons: <_MSMDButtonOption>[
              _MSMDButtonOption('Business Dashboard', Icons.dashboard, businessDashboardRoute),
              _MSMDButtonOption('', Icons.security, null),
              _MSMDButtonOption('', Icons.business, null),
              _MSMDButtonOption('Settings', Icons.settings, applicationSettingsRoute),
              _MSMDButtonOption('Log out', Icons.logout, accessRoute),
            ],
          ),
          Expanded(
            child: DecoratedBox(
              decoration: BoxDecoration(
                border: Border.fromBorderSide(
                  BorderSide(
                    color: Colors.teal.shade900,
                    width: 2,
                  ),
                ),
              ),
              child: Padding(
                padding: const EdgeInsets.all(8.0),
                child: page,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

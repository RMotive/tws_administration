import 'package:cosmos_foundation/contracts/cosmos_route.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_base.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_shell.dart';
import 'package:cosmos_foundation/foundation/configurations/cosmos_routing.dart';
import 'package:tws_main/constants/config/routes/tws_routes.dart';
import 'package:tws_main/presentation/layouts/app_layout/app_layout.dart';
import 'package:tws_main/presentation/pages/access_page/access_page.dart';
import 'package:tws_main/presentation/pages/business_dashboard_page/business_dashboard_page.dart';
import 'package:tws_main/presentation/pages/settings_page/settings_page.dart';

class TWSRouting extends CosmosRouting {
  TWSRouting()
      : super(
          routes: <CosmosRouteBase>[
            CosmosRoute(
              accessRoute,
              build: (_, __) => const AccessPage(),
            ),
            CosmosRouteShell(
              routes: <CosmosRouteBase>[
                CosmosRoute(
                  businessDashboardRoute,
                  build: (_, __) => const BusinessDashboardPage(),
                ),
                CosmosRoute(
                  applicationSettingsRoute,
                  build: (_, __) => const SettingsPage(),
                )
              ],
              layoutBuild: (_, __, ___) => AppLayout(page: ___),
            ),
          ],
        );
}

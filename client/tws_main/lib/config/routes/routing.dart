import 'package:cosmos_foundation/contracts/cosmos_route.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_base.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_shell.dart';
import 'package:cosmos_foundation/foundation/configurations/cosmos_routing.dart';
import 'package:tws_main/config/routes/routes.dart';
import 'package:tws_main/presentation/pages/auth_page/auth_page.dart';
import 'package:tws_main/presentation/pages/business_dashboard_page/business_dashboard_page.dart';
import 'package:tws_main/presentation/screens/main_screen/main_screen.dart';

class Routing extends CosmosRouting {
  Routing()
      : super(
          routes: <CosmosRouteBase>[
            CosmosRoute(
              accessRoute,
              build: (_, __) => const AuthPage(),
            ),
            CosmosRouteShell(
              routes: <CosmosRouteBase>[
                CosmosRoute(
                  dashboardRoute,
                  build: (_, __) => const BusinessDashboardPage(),
                ),
              ],
              screenBuild: (_, __, ___) => MainScreen(page: ___),
            ),
          ],
        );
}

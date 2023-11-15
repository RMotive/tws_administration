import 'package:cosmos_foundation/contracts/cosmos_route.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_base.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_shell.dart';
import 'package:cosmos_foundation/foundation/configurations/cosmos_routing.dart';
import 'package:tws_main/business/services/client/session_client_service.dart';
import 'package:tws_main/constants/config/routes/tws_routes.dart';
import 'package:tws_main/presentation/layouts/layouts_library.dart';
import 'package:tws_main/presentation/pages/pages_library.dart';

// --> Services
final SessionClientService _sessionCS = SessionClientService.i;

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
              layoutBuild: (_, __, ___) {
                _sessionCS.fetchStoredSession().then(
                  (SessionClientOutput value) {
                    print(value.toString());
                  },
                );
                return AppLayout(page: ___);
              },
            ),
          ],
        );
}

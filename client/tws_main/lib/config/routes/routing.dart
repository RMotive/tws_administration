import 'package:cosmos_foundation/contracts/cosmos_route.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_base.dart';
import 'package:cosmos_foundation/foundation/configurations/cosmos_routing.dart';
import 'package:tws_main/config/routes/routes.dart';
import 'package:tws_main/views/screens/auth_page.dart';

class Routing extends CosmosRouting {
  Routing()
      : super(
          routes: <CosmosRouteBase>[
            CosmosRoute(
              accessRoute,
              build: (_, __) => const AuthPage(),
            )
          ],
        );
}

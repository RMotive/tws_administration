import 'package:cosmos_foundation/contracts/cosmos_route.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_base.dart';
import 'package:cosmos_foundation/foundation/configurations/cosmos_routing.dart';
import 'package:cosmos_foundation/models/options/route_options.dart';
import 'package:tws_main/presentation/pages/login/login_page.dart';

class RouterBase extends CosmosRouting {
  RouterBase()
      : super(
          routes: <CosmosRouteBase>[
            CosmosRoute(
              const RouteOptions('/'),
              build: (_, __) => const LoginPage(),
            ),
          ],
        );
}

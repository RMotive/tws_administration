import 'package:cosmos_foundation/contracts/cosmos_route_base.dart';
import 'package:cosmos_foundation/widgets/hooks/cosmos_route.dart';
import 'package:cosmos_foundation/widgets/hooks/cosmos_routing.dart';
import 'package:tws_main/config/routes/routes.dart';
import 'package:tws_main/views/screens/auth_page.dart';
import 'package:flutter/material.dart';

class Routing extends CosmosRouting {
  Routing()
      : super(
          routes: <CosmosRouteBase>[
            CosmosRoute(
              accessRoute,
              build: (BuildContext ctx) => const AuthPage(),
            )
          ],
        );
}

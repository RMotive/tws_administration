import 'package:cosmos_foundation/contracts/cosmos_route_base.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_layout.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_node.dart';
import 'package:cosmos_foundation/foundation/configurations/cosmos_routing.dart';
import 'package:cosmos_foundation/models/outputs/route_output.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/k_routes.dart';
import 'package:tws_main/domain/storage/session_storage.dart';
import 'package:tws_main/presentation/layouts/master/master_layout.dart';
import 'package:tws_main/presentation/pages/landing/landing_page.dart';
import 'package:tws_main/presentation/pages/login/login_page.dart';

final SessionStorage _sessionStorage = SessionStorage.instance;

class RoutingOptions extends CosmosRouting {
  RoutingOptions()
      : super(
          redirect: (BuildContext ctx, RouteOutput output) {
            if (!_sessionStorage.isSession) return KRoutes.loginPage;
            return null;
          },
          routes: <CosmosRouteBase>[
            // --> [Login Page]
            CosmosRouteNode(
              KRoutes.loginPage,
              redirect: (BuildContext ctx, RouteOutput output) {
                if (_sessionStorage.isSession) return KRoutes.landingPage;
                return null;
              },
              build: (_, __) => const LoginPage(),
            ),
            // --> [Master Layout]
            //    --> [Landing Page]
            CosmosRouteLayout(
              routes: <CosmosRouteBase>[
                CosmosRouteNode(
                  KRoutes.landingPage,
                  build: (BuildContext _, RouteOutput __) => const LandingPage(),
                ),
              ],
              layoutBuild: (BuildContext _, RouteOutput __, Widget page) => MasterLayout(page: page),
            ),
          ],
        );
}

import 'package:cosmos_foundation/contracts/cosmos_route_base.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_layout.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_node.dart';
import 'package:cosmos_foundation/foundation/configurations/cosmos_routing.dart';
import 'package:cosmos_foundation/models/outputs/route_output.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/k_routes.dart';
import 'package:tws_main/domain/storage/session_storage.dart';
import 'package:tws_main/presentation/articles/features/features_article.dart';
import 'package:tws_main/presentation/layouts/master/master_layout.dart';
import 'package:tws_main/presentation/pages/login/login_page.dart';
import 'package:tws_main/presentation/pages/overview/overview_page.dart';
import 'package:tws_main/presentation/pages/security/security_page.dart';

final SessionStorage _sessionStorage = SessionStorage.instance;

class TWSRouting extends CosmosRouting {
  TWSRouting()
      : super(
          devRoute: KRoutes.securityPageFeaturesArticle,
          redirect: (_, __) async {
            if (!await _sessionStorage.isSession) return KRoutes.loginPage;
            return null;
          },
          routes: <CosmosRouteBase>[
            // --> [Login Page]
            CosmosRouteNode(
              KRoutes.loginPage,
              redirect: (_, __) async {
                if (await _sessionStorage.isSession) return KRoutes.overviewPage;
                return null;
              },
              pageBuild: (_, __) => const LoginPage(),
            ),
            // --> [MasterLayout]
            CosmosRouteLayout(
              layoutBuild: (_, RouteOutput output, Widget page) {
                return MasterLayout(
                  page: page,
                  routeOutput: output,
                );
              },
              routes: <CosmosRouteBase>[
                // --> [Overview Page]
                CosmosRouteNode(
                  KRoutes.overviewPage,
                  pageBuild: (_, __) => const OverviewPage(),
                ),
                // --> [Security Page]
                CosmosRouteNode(
                  KRoutes.securityPage,
                  pageBuild: (_, __) {
                    return const SecurityPage(
                      currentRoute: KRoutes.securityPage,
                    );
                  },
                  routes: <CosmosRouteBase>[
                    // --> [Features]
                    CosmosRouteNode(
                      KRoutes.securityPageFeaturesArticle,
                      pageBuild: (_, __) {
                        return const FeaturesArticle();
                      },
                    ),
                  ],
                )
              ],
            ),
          ],
        );
}

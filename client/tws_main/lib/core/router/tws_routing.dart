import 'package:cosmos_foundation/contracts/cosmos_route_base.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_layout.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_node.dart';
import 'package:cosmos_foundation/foundation/configurations/cosmos_routing.dart';
import 'package:cosmos_foundation/models/outputs/route_output.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:tws_main/core/router/k_routes.dart';
import 'package:tws_main/domain/storage/session_storage.dart';
import 'package:tws_main/presentation/articles/features/features_page.dart';
import 'package:tws_main/presentation/layouts/master/master_layout.dart';
import 'package:tws_main/presentation/pages/login/login_page.dart';
import 'package:tws_main/presentation/pages/overview/overview_page.dart';
import 'package:tws_main/presentation/pages/security/security_page.dart';

final SessionStorage _sessionStorage = SessionStorage.instance;

class TWSRouting extends CosmosRouting {
  TWSRouting()
      : super(
          developmentRoute: KRoutes.securityPage,
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
            //    --> [OverviewPage]
            CosmosRouteLayout(
              routes: <CosmosRouteBase>[
                CosmosRouteNode(
                  KRoutes.overviewPage,
                  pageBuild: (_, __) => const OverviewPage(),
                ),
                CosmosRouteNode(
                  KRoutes.securityPage,
                  redirect: (_, RouteOutput routeOutput) {
                    return null;
                  },
                  pageBuild: (BuildContext ctx, RouteOutput output) => const SecurityPage(currentRoute: KRoutes.securityPage),
                  pageTransitionBuild: (BuildContext ctx, __) {
                    return buildPageWithoutAnimation(
                      context: ctx,
                      child: const SecurityPage(
                        currentRoute: KRoutes.securityPage,
                      ),
                    );
                  },
                  routes: <CosmosRouteBase>[
                    CosmosRouteNode(
                      KRoutes.securityPageFeaturesArticle,
                      pageBuild: (_, __) {
                        return const SecurityPage(
                          currentRoute: KRoutes.securityPageFeaturesArticle,
                          article: FeaturesArticle(),
                        );
                      },
                    )
                  ],
                )
              ],
              layoutBuild: (_, RouteOutput routeOutput, Widget page) => MasterLayout(
                page: page,
                routeOutput: routeOutput,
              ),
              layoutTransitionBuild: (BuildContext ctx, RouteOutput output, Widget page) {
                return buildPageWithoutAnimation(context: ctx, child: page);
              },
            ),
          ],
        );
}

CustomTransitionPage<dynamic> buildPageWithoutAnimation({
  required BuildContext context,
  required Widget child,
}) {
  return CustomTransitionPage<dynamic>(
    child: child,
    transitionsBuilder: (BuildContext context, Animation<double> animation, Animation<double> secondaryAnimation, Widget child) => child,
  );
}

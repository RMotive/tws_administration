import 'package:cosmos_foundation/router/router_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/twsa_k_routes.dart';
import 'package:tws_main/data/storage/session_storage.dart';
import 'package:tws_main/view/articles/features/features_article.dart';
import 'package:tws_main/view/layouts/master/master_layout.dart';
import 'package:tws_main/view/pages/login/login_page.dart';
import 'package:tws_main/view/pages/overview/overview_page.dart';
import 'package:tws_main/view/pages/security/security_page.dart';

final SessionStorage _sessionStorage = SessionStorage.instance;

class TWSRouting extends CSMRouterTreeBase {
  TWSRouting()
      : super(
          redirect: (_, __) async {
            if (!await _sessionStorage.isSession) return TWSAKRoutes.loginPage;
            return null;
          },
          routes: <CSMRouteBase>[
            // --> [Login Page]
            CSMRouteNode(
              TWSAKRoutes.loginPage,
              redirect: (_, __) async {
                if (await _sessionStorage.isSession) return TWSAKRoutes.overviewPage;
                return null;
              },
              pageBuild: (_, __) => const LoginPage(),
            ),
            // --> [MasterLayout]
            CSMRouteLayout(
              layoutBuild: (_, CSMRouterOutput output, Widget page) {
                return MasterLayout(
                  page: page,
                  rOutput: output,
                );
              },
              routes: <CSMRouteBase>[
                // --> [Overview Page]
                CSMRouteNode(
                  TWSAKRoutes.overviewPage,
                  pageBuild: (_, __) => const OverviewPage(),
                ),
                // --> [Security Page]
                CSMRouteNode(
                  TWSAKRoutes.securityPage,
                  pageBuild: (_, __) {
                    return const SecurityPage(
                      currentRoute: TWSAKRoutes.securityPage,
                    );
                  },
                  routes: <CSMRouteBase>[
                    // --> [Features]
                    CSMRouteNode(
                      TWSAKRoutes.securityPageFeaturesArticle,
                      pageBuild: (_, __) {
                        return const FeaturesArticle();
                      },
                      routes: <CSMRouteBase>[],
                    ),
                  ],
                )
              ],
            ),
          ],
        );
}

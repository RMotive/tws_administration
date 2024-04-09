import 'package:cosmos_foundation/router/router_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/twsa_k_routes.dart';
import 'package:tws_main/data/storage/session_storage.dart';
import 'package:tws_main/view/articles/features/features_article.dart';
import 'package:tws_main/view/articles/features/whispers/create/features_create_whisper.dart';
import 'package:tws_main/view/layouts/master/master_layout.dart';
import 'package:tws_main/view/pages/login/login_page.dart';
import 'package:tws_main/view/pages/overview/overview_page.dart';
import 'package:tws_main/view/pages/security/security_page.dart';

typedef Routes = TWSAKRoutes;

final SessionStorage _sessionStorage = SessionStorage.instance;

class TWSARouteTree extends CSMRouterTreeBase {
  TWSARouteTree()
      : super(
          devRoute: Routes.featuresCreateWhisper,
          redirect: (_, __) async {
            if (!await _sessionStorage.isSession) return Routes.loginPage;
            return null;
          },
          routes: <CSMRouteBase>[
            // --> [Login Page]
            CSMRouteNode(
              Routes.loginPage,
              redirect: (_, __) async {
                if (await _sessionStorage.isSession) return Routes.overviewPage;
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
                  Routes.overviewPage,
                  pageBuild: (_, __) => const OverviewPage(),
                ),
                // --> [Security Page]
                CSMRouteNode(
                  Routes.securityPage,
                  pageBuild: (_, __) {
                    return const SecurityPage(
                      currentRoute: Routes.securityPage,
                    );
                  },
                  routes: <CSMRouteBase>[
                    // --> [Features]
                    CSMRouteNode(
                      Routes.featuresArticle,
                      pageBuild: (_, __) {
                        return const FeaturesArticle();
                      },
                      routes: <CSMRouteBase>[
                        CSMRouteWhisper<Object>(
                          Routes.featuresCreateWhisper,
                          whisperOptions: const CSMRouteWhisperOptions(),
                          pageBuild: (BuildContext ctx, CSMRouterOutput output) => const FeaturesCreateWhisper(),
                        ),
                      ],
                    ),
                  ],
                )
              ],
            ),
          ],
        );
}

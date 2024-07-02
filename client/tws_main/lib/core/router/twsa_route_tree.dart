import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/twsa_routes.dart';
import 'package:tws_main/view/articles/features/features_article.dart';
import 'package:tws_main/view/articles/features/whispers/create/features_create_whisper.dart';
import 'package:tws_main/view/articles/insurances/insurances_article.dart';
import 'package:tws_main/view/articles/maintenances/maintenences_article.dart';
import 'package:tws_main/view/articles/manufacturers/manufacturers_article.dart';
import 'package:tws_main/view/articles/manufacturers/whispers/manufactueres_create_whisper.dart';
import 'package:tws_main/view/articles/plates/plates_article.dart';
import 'package:tws_main/view/articles/plates/whispers/plates_create_whisper.dart';
import 'package:tws_main/view/articles/scts/scts_article.dart';
import 'package:tws_main/view/articles/situations/situations_article.dart';
import 'package:tws_main/view/articles/solutions/solutions_article.dart';
import 'package:tws_main/view/articles/solutions/whispers/solutions_create_whisper.dart';
import 'package:tws_main/view/articles/trucks/trucks_article.dart';
import 'package:tws_main/view/articles/trucks/whispers/trucks_create_whisper.dart';
import 'package:tws_main/view/layouts/master/master_layout.dart';
import 'package:tws_main/view/pages/about/about_page.dart';
import 'package:tws_main/view/pages/business/business_page.dart';
import 'package:tws_main/view/pages/login/login_page.dart';
import 'package:tws_main/view/pages/overview/overview_page.dart';
import 'package:tws_main/view/pages/profile/profile_page.dart';
import 'package:tws_main/view/pages/security/security_page.dart';
import 'package:tws_main/view/pages/settings/settings_page.dart';

typedef Routes = TWSARoutes;


class TWSARouteTree extends CSMRouterTreeBase {
  TWSARouteTree()
      : super(
          devRoute: Routes.solutionsArticle,
          redirect: (_, __) {
            return null;
          },
          routes: <CSMRouteBase>[
            // --> [Login Page]
            CSMRouteNode(
              Routes.loginPage,
              redirect: (_, __) async {
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
                // --> [About page]
                 CSMRouteNode(
                  Routes.about,
                  pageBuild: (_, __) => const AboutPage(),
                ),
                // --> [Profile user page]
                 CSMRouteNode(
                  Routes.profile,
                  pageBuild: (_, __) => const ProfilePage(),
                ),
                // --> [User settings page]
                 CSMRouteNode(
                  Routes.settings,
                  pageBuild: (_, __) => const SettingsPage(),
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
                    // --> [Solutions]
                    CSMRouteNode(
                      TWSARoutes.solutionsArticle,
                      pageBuild: (BuildContext ctx, CSMRouterOutput output) => const SolutionsArticle(),
                      routes: <CSMRouteBase>[
                        // --> [Create]
                        CSMRouteWhisper<void>(
                          TWSARoutes.solutionsCreateWhisper,
                          whisperOptions: const CSMRouteWhisperOptions(
                            padding: EdgeInsets.zero,
                          ),
                          pageBuild: (BuildContext ctx, CSMRouterOutput output) => const SolutionsCreateWhisper(),
                        ),
                      ],
                    ),
                  ],
                ),

                // --> [Business Page]
                CSMRouteNode(
                  Routes.businessPage,
                  pageBuild: (_, __) {
                    return const BusinessPage(
                      currentRoute: Routes.businessPage,
                    );
                  },
                  routes: <CSMRouteBase>[
                    // --> [Trucks]
                    CSMRouteNode(
                      Routes.trucksArticle,
                      pageBuild: (_, __) => const TrucksArticle(),
                      routes: <CSMRouteBase>[
                        // -> [Create]
                        CSMRouteWhisper<Object>(
                          Routes.trucksCreateWhisper,
                          whisperOptions: const CSMRouteWhisperOptions(),
                          pageBuild: (BuildContext ctx, CSMRouterOutput output) => const TrucksCreateWhisper(),
                        ),
                      ],
                    ),

                    // --> [manufacturers]
                    CSMRouteNode(
                      Routes.manufacturersArticle,
                      pageBuild: (_, __) => const ManufacturersArticle(),
                      routes: <CSMRouteBase>[
                        // -> [Create]
                        CSMRouteWhisper<Object>(
                          Routes.manufacturersCreateWhisper,
                          whisperOptions: const CSMRouteWhisperOptions(),
                          pageBuild: (BuildContext ctx, CSMRouterOutput output) => const ManufacturersCreateWhisper(),
                        ),
                      ],
                    ),

                    // --> [Plates]
                    CSMRouteNode(
                      Routes.platesArticle,
                      pageBuild: (_, __) => const PlatesArticle(),
                      routes: <CSMRouteBase>[
                        // -> [Create]
                        CSMRouteWhisper<Object>(
                          Routes.platesCreateWhisper,
                          whisperOptions: const CSMRouteWhisperOptions(),
                          pageBuild: (BuildContext ctx, CSMRouterOutput output) => const PlatesCreateWhisper(),
                        ),
                      ],
                    ),

                    // --> [Insurance]
                    CSMRouteNode(
                      Routes.insuranceArticle,
                      pageBuild: (_, __) => const InsurancesArticle(),
                      
                    ),

                    // --> [Maintenece]
                    CSMRouteNode(
                      Routes.maintencesArticle,
                      pageBuild: (_, __) => const MaintenencesArticle(),   
                    ),

                    // --> [SCTS]
                    CSMRouteNode(
                      Routes.sctsArticle,
                      pageBuild: (_, __) => const SCTsArticle(),
                    ),

                    // --> [Situations]
                    CSMRouteNode(
                      Routes.situationsArticle,
                      pageBuild: (_, __) => const SituationsArticle(),
                    ),
                  ],
                ),
              ],
            ),
          ],
        );
}

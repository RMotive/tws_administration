import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/twsa_k_routes.dart';
import 'package:tws_main/view/frames/article/action_ribbon_options.dart';
import 'package:tws_main/view/frames/article/article_frame.dart';
import 'package:tws_main/view/frames/article/article_options.dart';

class SecurityFrame extends StatelessWidget {
  final Widget? article;
  final CSMRouteOptions currentRoute;
  final ActionRibbonOptions? actionsOptions;

  const SecurityFrame({
    super.key,
    this.article,
    this.actionsOptions,
    required this.currentRoute,
  });

  /// Stores all the articles configured for Security.
  static final List<ArticleOptions> securityArticles = <ArticleOptions>[
    // --> Features article
    ArticleOptions(
      icon: (Color? stateColor) {
        return Icon(
          Icons.call_to_action_sharp,
          color: stateColor,
        );
      },
      route: TWSARoutes.featuresArticle,
      title: 'Features',
    ),
    ArticleOptions(
      icon: (Color? stateColor) {
        return Icon(
          Icons.business,
          color: stateColor,
        );
      },
      title: 'Solutions',
    ),
  ];

  @override
  Widget build(BuildContext context) {
    return ArticleFrame(
      articlesOptions: securityArticles,
      actionsOptions: actionsOptions,
      currentRoute: currentRoute,
      article: article,
    );
  }
}

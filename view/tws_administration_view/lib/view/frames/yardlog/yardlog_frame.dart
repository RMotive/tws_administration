import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/router/twsa_routes.dart';
import 'package:tws_administration_view/view/frames/article/action_ribbon_options.dart';
import 'package:tws_administration_view/view/frames/article/article_frame.dart';
import 'package:tws_administration_view/view/frames/article/article_options.dart';

class YardlogFrame extends StatelessWidget {
  final Widget? article;
  final CSMRouteOptions currentRoute;
  final ActionRibbonOptions? actionsOptions;

  const YardlogFrame({
    super.key,
    this.article,
    this.actionsOptions,
    required this.currentRoute,
  });

  /// Stores all the articles configured for Business.
  static final List<ArticleOptions> yardlogArticles = <ArticleOptions>[
    // --> Yard log article
    ArticleOptions(
      icon: (Color? stateColor) {
        return Icon(
          Icons.call_to_action_sharp,
          color: stateColor,
        );
      },
      route: TWSARoutes.yardlogPage,
      title: 'Yard Logs',
    ),
    ArticleOptions(
      icon: (Color? stateColor) {
        return Icon(
          Icons.inventory_2,
          color: stateColor,
        );
      },
      route: TWSARoutes.yardlogsTruckInventoryArticle,
      title: 'Truck Inventory',
    )
  ];

  @override
  Widget build(BuildContext context) {
    return ArticleFrame(
      articlesOptions: yardlogArticles,
      actionsOptions: actionsOptions,
      currentRoute: currentRoute,
      article: article,
    );
  }
}
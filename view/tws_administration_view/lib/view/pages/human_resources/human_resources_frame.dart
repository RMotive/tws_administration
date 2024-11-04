import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/router/twsa_routes.dart';
import 'package:tws_administration_view/view/frames/article/action_ribbon_options.dart';
import 'package:tws_administration_view/view/frames/article/article_frame.dart';
import 'package:tws_administration_view/view/frames/article/article_options.dart';

final class HumanResourcesFrame extends StatelessWidget {
  final Widget? article;

  final CSMRouteOptions currentRoute;

  final ActionRibbonOptions? actionsOptions;

  const HumanResourcesFrame({
    super.key,
    this.article,
    this.actionsOptions,
    required this.currentRoute,
  });

  static final List<ArticleOptions> humanResourcesArticles = <ArticleOptions>[
    // --> Features article
    ArticleOptions(
      icon: (Color? stateColor) {
        return Icon(
          Icons.contact_phone,
          color: stateColor,
        );
      },
      route: TWSARoutes.contactsArticle,
      title: 'Contacts',
    ),
  ];

  @override
  Widget build(BuildContext context) {
    return ArticleFrame(
      articlesOptions: humanResourcesArticles,
      actionsOptions: actionsOptions,
      currentRoute: currentRoute,
      article: article,
    );
  }
}

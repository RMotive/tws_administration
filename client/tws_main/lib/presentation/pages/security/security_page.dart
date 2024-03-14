import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/models/options/route_options.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/k_routes.dart';
import 'package:tws_main/presentation/layouts/articles/article_options.dart';
import 'package:tws_main/presentation/layouts/articles/articles_layout.dart';

class SecurityPage extends CosmosPage {
  final CosmosPage? article;
  final RouteOptions currentRoute;

  const SecurityPage({
    super.key,
    this.article,
    required this.currentRoute,
  });

  @override
  Widget compose(BuildContext ctx, Size window) {
    return ArticlesLayout(
      article: article,
      currentRoute: currentRoute,
      articles: <ArticleOptions>[
        // --> Features management page
        ArticleOptions(
          icon: (Color? stateColor) {
            return Icon(
              Icons.call_to_action_sharp,
              color: stateColor,
            );
          },
          route: KRoutes.securityPageFeaturesArticle,
          title: 'Features',
        ),
      ],
    );
  }
}

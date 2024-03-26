import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/presentation/components/tws_article_table.dart';

class FeaturesArticle extends CosmosPage {
  const FeaturesArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return const TWSArticleTable<void>(
      fields: <String>[
        'Name',
      ],
    );
  }
}

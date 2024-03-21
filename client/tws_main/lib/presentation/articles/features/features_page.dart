import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/presentation/components/tws_article_table.dart';

class FeaturesArticle extends CosmosPage {
  const FeaturesArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return LayoutBuilder(
      builder: (_, BoxConstraints constraints) {
        BoxConstraints pageBounds = constraints.tighten(height: constraints.minHeight);

        return Row(
          children: <Widget>[
            Expanded(
              child: SizedBox(
                height: pageBounds.maxHeight,
                child: const TWSArticleTable<void>(
                  fields: <String>[
                    'Name',
                  ],
                ),
              ),
            ),
            ColoredBox(
              color: Colors.green.shade900.withOpacity(.7),
              child: SizedBox(
                height: pageBounds.maxHeight,
                width: 400,
              ),
            ),
          ],
        );
      },
    );
  }
}

import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/router/twsa_routes.dart';
import 'package:tws_administration_view/data/services/sources.dart';
import 'package:tws_administration_view/data/storages/session_storage.dart';
import 'package:tws_administration_view/view/frames/business/business_frame.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_adapter.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_field_options.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

part 'options/situations_article_table_adapter.dart';

class SituationsArticle extends CSMPageBase {
  const SituationsArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return BusinessFrame(
      currentRoute: TWSARoutes.situationsArticle,
      article:TWSArticleTable<Situation>(
        adapter: const _TableAdapter(),
        fields: <TWSArticleTableFieldOptions<Situation>>[
          TWSArticleTableFieldOptions<Situation>(
            'Name',
            (Situation item, int index, BuildContext ctx) => item.name,
          ),
          TWSArticleTableFieldOptions<Situation>(
            'Description',
            (Situation item, int index, BuildContext ctx) => item.description ?? '---',
            true,
          ),
        ],
        page: 1,
        size: 25,
        sizes: const <int>[25, 50, 75, 100],
      ),
    );
  }
}

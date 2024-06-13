import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/router/twsa_routes.dart';
import 'package:tws_main/data/services/sources.dart';
import 'package:tws_main/data/storages/session_storage.dart';

import 'package:tws_main/view/frames/business/business_frame.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_data_adapter.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_field_options.dart';

part 'options/scts_article_table_adapter.dart';

class SCTsArticle extends CSMPageBase {
  const SCTsArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return BusinessFrame(
      currentRoute: TWSARoutes.sctsArticle,
      article: TWSArticleTable<SCT>(
        adapter: const _TableAdapter(),
        fields: <TWSArticleTableFieldOptions<SCT>>[
          TWSArticleTableFieldOptions<SCT>(
            'Type',
            (SCT item, int index, BuildContext ctx) => item.type,
          ),
          TWSArticleTableFieldOptions<SCT>(
            'Number',
            (SCT item, int index, BuildContext ctx) => item.number,
          ),
          TWSArticleTableFieldOptions<SCT>(
            'Configuration',
            (SCT item, int index, BuildContext ctx) => item.configuration,
            true,
          ),
           TWSArticleTableFieldOptions<SCT>(
            'Truck VIN',
            (SCT item, int index, BuildContext ctx) {
              String vins = '';
              int cont = 0;
              for (Truck truck in item.trucks) {
                cont++;
                vins += truck.vin;
                if(item.trucks.length > cont) vins += '\n';
              }

              return vins != '' ? vins : '---';
            } ,
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

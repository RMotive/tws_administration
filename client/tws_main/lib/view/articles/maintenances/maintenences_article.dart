import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/router/twsa_routes.dart';
import 'package:tws_main/data/services/sources.dart';
import 'package:tws_main/data/storages/session_storage.dart';

import 'package:tws_main/view/frames/business/business_frame.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_adapter.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_field_options.dart';
part 'options/maintenences_article_table_adapter.dart';

class MaintenencesArticle extends CSMPageBase {
  const MaintenencesArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return BusinessFrame(
      currentRoute: TWSARoutes.maintencesArticle,
      article:  TWSArticleTable<Maintenance>(
        adapter: const _TableAdapter(),
        fields: <TWSArticleTableFieldOptions<Maintenance>>[
          TWSArticleTableFieldOptions<Maintenance>(
            'Anual',
            (Maintenance item, int index, BuildContext ctx) => item.anual.toString(),
          ),
          TWSArticleTableFieldOptions<Maintenance>(
            'Trimestral',
            (Maintenance item, int index, BuildContext ctx) => item.trimestral.toString(),
          ),
           TWSArticleTableFieldOptions<Maintenance>(
            'Truck VIN',
            (Maintenance item, int index, BuildContext ctx) {
             String vins = '---';
              if(item.trucks.isNotEmpty){
                vins = '';
                for(int cont = 0; cont < item.trucks.length; cont++) {
                  vins += item.trucks[cont].vin;
                  if(item.trucks.length > cont) vins += '\n';
                }
              }
              return vins;
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

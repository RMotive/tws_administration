import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/router/twsa_routes.dart';
import 'package:tws_main/data/services/sources.dart';
import 'package:tws_main/data/storages/session_storage.dart';
import 'package:tws_main/view/frames/article/action_ribbon_options.dart';
import 'package:tws_main/view/frames/article/actions/maintenance_group_options.dart';

import 'package:tws_main/view/frames/business/business_frame.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_data_adapter.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_field_options.dart';
part 'options/plates_article_table_adapter.dart';

class PlatesArticle extends CSMPageBase {
  const PlatesArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return BusinessFrame(
      currentRoute: TWSARoutes.platesArticle,
      article:  TWSArticleTable<Plate>(
        adapter: const _TableAdapter(),
        fields: <TWSArticleTableFieldOptions<Plate>>[
          TWSArticleTableFieldOptions<Plate>(
            'Identifier',
            (Plate item, int index, BuildContext ctx) => item.identifier,
          ),
          TWSArticleTableFieldOptions<Plate>(
            'State',
            (Plate item, int index, BuildContext ctx) => item.state,
          ),
          TWSArticleTableFieldOptions<Plate>(
            'Country',
            (Plate item, int index, BuildContext ctx) => item.country,
            true,
          ),
          TWSArticleTableFieldOptions<Plate>(
            'Expiration',
            (Plate item, int index, BuildContext ctx) => item.expiration.toString(),
            true,
          ),
          TWSArticleTableFieldOptions<Plate>(
            'Truck ID',
            (Plate item, int index, BuildContext ctx) => item.truck.toString(),
            true,
          ),
          TWSArticleTableFieldOptions<Plate>(
            'Truck VIN',
            (Plate item, int index, BuildContext ctx) => item.truckNavigation?.vin ?? '---',
            true,
          ),
          
        ],
        page: 1,
        size: 25,
        sizes: const <int>[25, 50, 75, 100],
      ),
       actionsOptions: ActionRibbonOptions(
        maintenanceGroupConfig: MaintenanceGroupOptions(
          onCreate: () {
            
          },
        ),
      ),
    );
  }
}

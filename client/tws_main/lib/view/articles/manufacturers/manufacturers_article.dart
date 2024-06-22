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
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_adapter.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_field_options.dart';
part 'options/manufacturers_article_table_adapter.dart';

class ManufacturersArticle extends CSMPageBase {
  const ManufacturersArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return BusinessFrame(
      currentRoute: TWSARoutes.manufacturersArticle,
      article:  TWSArticleTable<Manufacturer>(
        adapter: const _TableAdapter(),
        fields: <TWSArticleTableFieldOptions<Manufacturer>>[
          TWSArticleTableFieldOptions<Manufacturer>(
            'Model',
            (Manufacturer item, int index, BuildContext ctx) => item.model,
          ),
          TWSArticleTableFieldOptions<Manufacturer>(
            'Brand',
            (Manufacturer item, int index, BuildContext ctx) => item.brand,
          ),
          TWSArticleTableFieldOptions<Manufacturer>(
            'Year',
            (Manufacturer item, int index, BuildContext ctx) => item.year.toString(),
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

import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/router/twsa_routes.dart';
import 'package:tws_administration_view/data/services/sources.dart';
import 'package:tws_administration_view/data/storages/session_storage.dart';
import 'package:tws_administration_view/view/frames/article/action_ribbon_options.dart';
import 'package:tws_administration_view/view/frames/article/actions/maintenance_group_options.dart';
import 'package:tws_administration_view/view/frames/business/business_frame.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_adapter.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_field_options.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

part 'options/manufacturers_article_table_adapter.dart';

class ManufacturersArticle extends CSMPageBase {
  const ManufacturersArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return BusinessFrame(
      currentRoute: TWSARoutes.manufacturersArticle,
      article: const TWSArticleTable<Manufacturer>(
        adapter: _TableAdapter(),
        fields: <TWSArticleTableFieldOptions<Manufacturer>>[],
        page: 1,
        size: 25,
        sizes: <int>[25, 50, 75, 100],
      ),
      actionsOptions: ActionRibbonOptions(
        maintenanceGroupConfig: MaintenanceGroupOptions(
          onCreate: () {},
        ),
      ),
    );
  }
}

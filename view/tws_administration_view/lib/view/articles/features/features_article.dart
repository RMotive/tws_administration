import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/router/twsa_routes.dart';
import 'package:tws_administration_view/view/components/tws_article_table/tws_article_table.dart';
import 'package:tws_administration_view/view/components/tws_article_table/tws_article_table_adapter.dart';
import 'package:tws_administration_view/view/components/tws_article_table/tws_article_table_field_options.dart';
import 'package:tws_administration_view/view/frames/article/action_ribbon_options.dart';
import 'package:tws_administration_view/view/frames/article/actions/maintenance_group_options.dart';
import 'package:tws_administration_view/view/pages/security/security_frame.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

part './options/features_article_table_adapter.dart';

final class FeaturesArticle extends CSMPageBase {
  const FeaturesArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return SecurityFrame(
      currentRoute: TWSARoutes.featuresArticle,
      actionsOptions: ActionRibbonOptions(
        maintenanceGroupConfig: MaintenanceGroupOptions(
          onCreate: () {
            
          },
        ),
      ),
      article: const TWSArticleTable<Feature>(
        adapter: _TableAdapter(),
        fields: <TWSArticleTableFieldOptions<Feature>>[],
        page: 1,
        size: 25,
        sizes: <int>[25, 50, 75, 100],
      ),
    );
  }
}

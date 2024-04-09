import 'package:cosmos_foundation/router/router_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/twsa_k_routes.dart';
import 'package:tws_main/view/frames/article/action_ribbon_options.dart';
import 'package:tws_main/view/frames/article/actions/maintenance_group_options.dart';
import 'package:tws_main/view/frames/security/security_frame.dart';
import 'package:tws_main/view/widgets/tws_article_table.dart';

class FeaturesArticle extends CSMPageBase {
  const FeaturesArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return SecurityFrame(
      currentRoute: TWSAKRoutes.featuresArticle,
      actionsOptions: ActionRibbonOptions(
        maintenanceGroupConfig: MaintenanceGroupOptions(
          onCreate: () {
            
          },
        ),
      ),
      article: const TWSArticleTable<void>(
        fields: <String>[
          'Name',
        ],
      ),
    );
  }
}

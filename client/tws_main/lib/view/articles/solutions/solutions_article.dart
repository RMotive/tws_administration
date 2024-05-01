import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/router/twsa_k_routes.dart';
import 'package:tws_main/data/sources/sources.dart';
import 'package:tws_main/view/frames/article/action_ribbon_options.dart';
import 'package:tws_main/view/frames/article/actions/maintenance_group_options.dart';
import 'package:tws_main/view/frames/security/security_frame.dart';
import 'package:tws_main/view/widgets/options/bases/tws_article_table_data_adapter.dart';
import 'package:tws_main/view/widgets/tws_article_table.dart';

part 'options/solutions_article_table_adapter.dart';

class SolutionsArticle extends CSMPageBase {
  const SolutionsArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return SecurityFrame(
      currentRoute: TWSARoutes.solutionsArticle,
      actionsOptions: ActionRibbonOptions(
        maintenanceGroupConfig: MaintenanceGroupOptions(
          onCreate: () {},
        ),
      ),
      article: const TWSArticleTable<Solution>(
        adapter: _TableAdapter(),
        fields: <String>[
          'Name',
          'Sign',
        ],
        page: 1,
        size: 25,
        sizes: <int>[25, 50, 75, 100],
      ),
    );
  }
}

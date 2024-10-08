import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/router/twsa_routes.dart';
import 'package:tws_main/data/services/sources.dart';
import 'package:tws_main/data/storages/session_storage.dart';
import 'package:tws_main/view/frames/article/action_ribbon_options.dart';
import 'package:tws_main/view/frames/article/actions/maintenance_group_options.dart';
import 'package:tws_main/view/frames/security/security_frame.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_agent.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_data_adapter.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_field_options.dart';

part 'options/solutions_article_table_adapter.dart';



class SolutionsArticle extends CSMPageBase {
  
  static final TWSArticleTableAgent tableAgent = TWSArticleTableAgent();
  const SolutionsArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {

    return SecurityFrame(
      currentRoute: TWSARoutes.solutionsArticle,

      actionsOptions: ActionRibbonOptions(
        refresher: tableAgent.refresh,
        maintenanceGroupConfig: MaintenanceGroupOptions(
          onCreate: () => CSMRouter.i.drive(TWSARoutes.solutionsCreateWhisper),
        ),
      ),
      article: TWSArticleTable<Solution>(
        adapter: const _TableAdapter(),
        agent: tableAgent,
        fields: <TWSArticleTableFieldOptions<Solution>>[
          TWSArticleTableFieldOptions<Solution>(
            'Name',
            (Solution item, int index, BuildContext ctx) => item.name,
          ),
          TWSArticleTableFieldOptions<Solution>(
            'Sign',
            (Solution item, int index, BuildContext ctx) => item.sign,
          ),
          TWSArticleTableFieldOptions<Solution>(
            'Description',
            (Solution item, int index, BuildContext ctx) => item.description ?? '---',
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

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
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_agent.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_field_options.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';
part 'options/trucks_article_table_adapter.dart';

class TrucksArticle extends CSMPageBase {
  static final TWSArticleTableAgent tableAgent = TWSArticleTableAgent();
  const TrucksArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return BusinessFrame(
      currentRoute: TWSARoutes.trucksArticle,
      actionsOptions: ActionRibbonOptions(
        maintenanceGroupConfig: MaintenanceGroupOptions(
          onCreate: () => CSMRouter.i.drive(TWSARoutes.trucksCreateWhisper),
        ),
      ),
      article: TWSArticleTable<Truck>(
        adapter: const _TableAdapter(),
        fields: <TWSArticleTableFieldOptions<Truck>>[
          TWSArticleTableFieldOptions<Truck>(
            'VIN',
            (Truck item, int index, BuildContext ctx) => item.vin,
          ),
          TWSArticleTableFieldOptions<Truck>(
            'Manufacturer',
            (Truck item, int index, BuildContext ctx) => item.manufacturerNavigation?.brand ?? '---',
          ),
          TWSArticleTableFieldOptions<Truck>(
            'Motor',
            (Truck item, int index, BuildContext ctx) => item.motor,
            true,
          ),
          TWSArticleTableFieldOptions<Truck>(
            'SCT Number',
            (Truck item, int index, BuildContext ctx) =>  '---',
            true,
          ),
          TWSArticleTableFieldOptions<Truck>(
            'Trimestral Maintenance',
            (Truck item, int index, BuildContext ctx) => item.maintenanceNavigation?.trimestral.toString() ?? '---',
            true,
          ),
          TWSArticleTableFieldOptions<Truck>(
            'Anual Maintenance',
            (Truck item, int index, BuildContext ctx) => item.maintenanceNavigation?.anual.toString() ?? '---',
            true,
          ),
          TWSArticleTableFieldOptions<Truck>(
            'Situation',
            (Truck item, int index, BuildContext ctx) => '---',
            true,
          ),
          TWSArticleTableFieldOptions<Truck>(
            'Insurance',
            (Truck item, int index, BuildContext ctx) => item.insuranceNavigation?.policy ?? '---',
            true,
          ),
          TWSArticleTableFieldOptions<Truck>(
            'Plates',
            (Truck item, int index, BuildContext ctx) {
              String plates = '---';
              if (item.plates.isNotEmpty) {
                plates = '';
                for (int cont = 0; cont < item.plates.length; cont++) {
                  plates += item.plates[cont].identifier;
                  if (item.plates.length > cont) plates += '\n';
                }
              }
              return plates;
            },
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

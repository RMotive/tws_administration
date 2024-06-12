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
part 'options/insurances_article_table_adapter.dart';

class InsurancesArticle extends CSMPageBase {
  const InsurancesArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return BusinessFrame(
      currentRoute: TWSARoutes.insuranceArticle,
      article: TWSArticleTable<Insurance>(
        adapter: const _TableAdapter(),
        fields: <TWSArticleTableFieldOptions<Insurance>>[
          TWSArticleTableFieldOptions<Insurance>(
            'Policy',
            (Insurance item, int index, BuildContext ctx) => item.policy,
          ),
          TWSArticleTableFieldOptions<Insurance>(
            'Expiration',
            (Insurance item, int index, BuildContext ctx) => item.expiration.toString(),
          ),
          TWSArticleTableFieldOptions<Insurance>(
            'Country',
            (Insurance item, int index, BuildContext ctx) => item.country,
            true,
          ),
            TWSArticleTableFieldOptions<Insurance>(
            'Truck VIN',
            (Insurance item, int index, BuildContext ctx) {
              String vins = '';
              int cont = 0;
              item.trucks?.forEach((Truck truck) {
                cont++;
                vins += truck.vin;
                if(item.trucks!.length > cont) vins += '\n';
              });


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
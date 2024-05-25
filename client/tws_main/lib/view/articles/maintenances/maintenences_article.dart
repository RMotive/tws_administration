import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/router/twsa_routes.dart';
import 'package:tws_main/data/services/sources.dart';
import 'package:tws_main/data/storages/session_storage.dart';

import 'package:tws_main/view/frames/business/business_frame.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_data_adapter.dart';
part 'options/maintenences_article_table_adapter.dart';

class MaintenencesArticle extends CSMPageBase {
  const MaintenencesArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return const BusinessFrame(
      currentRoute: TWSARoutes.maintencesArticle,
      article: SizedBox(
        child: Text("Maintenences view"),

      ),
    );
  }
}

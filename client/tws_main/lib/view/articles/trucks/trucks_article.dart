import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/twsa_k_routes.dart';
import 'package:tws_main/view/frames/article/action_ribbon_options.dart';
import 'package:tws_main/view/frames/article/actions/maintenance_group_options.dart';
import 'package:tws_main/view/frames/business/business_frame.dart';

class TrucksArticle extends CSMPageBase {
  const TrucksArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return BusinessFrame(
      currentRoute: TWSARoutes.trucksArticle,
      actionsOptions: ActionRibbonOptions(
        maintenanceGroupConfig: MaintenanceGroupOptions(
          onCreate: () {
            
          },
          onRemove: (){

          },
          onEdit:  (){
            
          }
        ),
      ),
      article: const SizedBox(),
    );
  }
}

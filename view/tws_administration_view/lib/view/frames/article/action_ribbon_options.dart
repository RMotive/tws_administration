import 'package:flutter/material.dart';
import 'package:tws_administration_view/view/frames/article/actions/maintenance_group_options.dart';

class ActionRibbonOptions {
  final VoidCallback? refresher;
  final MaintenanceGroupOptions? maintenanceGroupConfig;

  const ActionRibbonOptions({
    this.refresher,
    this.maintenanceGroupConfig,
  });

  bool evalDraw() {
    bool initVal = false;
    if (refresher != null) return true;
    if (maintenanceGroupConfig != null) return true;

    return initVal;
  }
}

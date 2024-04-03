import 'package:tws_main/view/frames/article/actions/maintenance_group_options.dart';

class ActionRibbonOptions {
  final MaintenanceGroupOptions? maintenanceGroupConfig;

  const ActionRibbonOptions({
    this.maintenanceGroupConfig,
  });

  bool evalDraw() {
    bool initVal = false;
    if (maintenanceGroupConfig != null) return true;

    return initVal;
  }
}

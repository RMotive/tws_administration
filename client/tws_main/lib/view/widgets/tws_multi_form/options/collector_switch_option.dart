import 'package:tws_main/view/widgets/tws_multi_form/options/collector_options.dart';

class CollectorSwitchOption extends CollectorOptions {
  final bool defaultvalue;
  const CollectorSwitchOption({required super.label, super.height, super.width, this.defaultvalue = false});
}

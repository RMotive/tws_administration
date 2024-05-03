
import 'package:tws_main/view/frames/collector/options/collector_options.dart';

class CollectorToggleOption extends CollectorOptions{
  final String hint;
  final String errorText;  

  const CollectorToggleOption({
    required super.label,
    required this.hint,
    required this.errorText,
  });
}
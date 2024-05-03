
import 'package:tws_main/view/frames/collector/options/collector_options.dart';

class CollectorTextOption extends CollectorOptions{
  final String hint;
  final String? Function(String? value) validator;

  const CollectorTextOption({
    super.required,
    super.width,
    required super.label,
    required this.hint,
    required this.validator, 
  });
}
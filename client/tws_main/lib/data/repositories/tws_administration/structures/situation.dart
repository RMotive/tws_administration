import 'package:cosmos_foundation/common/common_module.dart';

const String _kCodePropBinder = "code";
const String _kDisplayPropBinder = "display";
const String _kDefaultDisplay = "default";
const int _kDefaultCode = 0;

class Situation {
  final int code;
  final String display;

  const Situation.def() : this(_kDefaultCode, _kDefaultDisplay);
  const Situation(this.code, this.display);
  factory Situation.fromJson(JObject json) {
    int code = json.bindProp(_kCodePropBinder, _kDefaultCode);
    String display = json.bindProp(_kDisplayPropBinder, _kDefaultDisplay);
    return Situation(code, display);
  }
}

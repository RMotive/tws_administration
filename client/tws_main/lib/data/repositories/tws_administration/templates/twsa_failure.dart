import 'package:cosmos_foundation/common/common_module.dart';
import 'package:tws_main/data/repositories/tws_administration/structures/situation.dart';

class TWSAFailure {
  final String message;
  final JObject failure;
  final Situation situation;

  const TWSAFailure(this.message, this.failure, this.situation);
  TWSAFailure.def() : this('', JUtils.empty, const Situation.def());

  factory TWSAFailure.fromJson(JObject json) {
    String message = json.bindProp('message', '');
    JObject failure = json.bindProp('failure', JUtils.empty);
    JObject situationJObject = json.bindProp('situation', JUtils.empty);
    Situation situation = Situation.fromJson(situationJObject);

    return TWSAFailure(message, failure, situation);
  }

  Map<String, dynamic> toJson() {
    return <String, dynamic>{};
  }
}

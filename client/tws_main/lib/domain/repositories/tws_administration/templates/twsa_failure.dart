import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:cosmos_foundation/contracts/bases/model_base.dart';
import 'package:cosmos_foundation/extensions/jobject.dart';
import 'package:tws_main/domain/repositories/tws_administration/structures/situation.dart';

class TWSAFailure extends ModelBase {
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

  @override
  Map<String, dynamic> toJson() {
    return <String, dynamic>{};
  }
}

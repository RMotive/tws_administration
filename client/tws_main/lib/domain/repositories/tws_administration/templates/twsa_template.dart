import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:cosmos_foundation/contracts/bases/model_base.dart';
import 'package:cosmos_foundation/extensions/jobject.dart';

class TWSATemplate<T> extends ModelBase {
  final String tracer;
  final T estela;

  const TWSATemplate(this.tracer, this.estela);
  factory TWSATemplate.fromJson(JObject json, T defaultVal, T Function(JObject json) factory) {
    final String tracer = json.bindProp('tracer', '');
    final JObject estela = json.bindProp('estela', JUtils.empty);

    return TWSATemplate<T>(tracer, factory(estela));
  }

  @override
  Map<String, dynamic> toJson() {
    return <String, dynamic>{};
  }
}

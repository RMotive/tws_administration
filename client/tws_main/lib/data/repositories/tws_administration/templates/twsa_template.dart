import 'package:cosmos_foundation/common/common_module.dart';

class TWSATemplate<T> {
  final String tracer;
  final T estela;

  const TWSATemplate(this.tracer, this.estela);
  factory TWSATemplate.fromJson(JObject json, T defaultVal, T Function(JObject json) factory) {
    final String tracer = json.bindProp('tracer', '');
    final JObject estela = json.bindProp('estela', JUtils.empty);

    return TWSATemplate<T>(tracer, factory(estela));
  }

  Map<String, dynamic> toJson() {
    return <String, dynamic>{};
  }
}

import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:cosmos_foundation/extensions/jobject.dart';

class InitSessionOutput {
  final String token;
  final bool wildcard;
  final List<int> features;

  InitSessionOutput.def() : this('', false, <int>[]);
  const InitSessionOutput(this.token, this.wildcard, this.features);
  factory InitSessionOutput.fromJson(JObject json) {
    List<dynamic> dynamicList = json.bindProp('features', <int>[]);
    List<int> featuresList = dynamicList.isEmpty ? <int>[] : (dynamicList.cast<int>());

    String token = json.bindProp('token', '');
    bool wildcard = json.bindProp('wildcard', false);
    List<int> features = featuresList;
    return InitSessionOutput(token, wildcard, features);
  }
}

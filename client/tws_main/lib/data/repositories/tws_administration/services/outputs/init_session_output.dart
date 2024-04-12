import 'package:cosmos_foundation/common/common_module.dart';

class InitSessionOutput {
  final String token;
  final bool wildcard;
  final List<int> features;
  final DateTime expiration;
  final DateTime expirationLocal;

  InitSessionOutput.def() : this('', false, <int>[], DateTime(0).toUtc(), DateTime(0).toLocal());
  const InitSessionOutput(this.token, this.wildcard, this.features, this.expiration, this.expirationLocal);
  factory InitSessionOutput.fromJson(JObject json) {
    List<dynamic> dynamicList = json.bindProp('features', <int>[]);
    List<int> featuresList = dynamicList.isEmpty ? <int>[] : (dynamicList.cast<int>());

    List<int> features = featuresList;
    String token = json.bindProp('token', '');
    bool wildcard = json.bindProp('wildcard', false);
    String decodedExpiration = json.bindProp('expiration', DateTime(0).toString());
    DateTime expiration = DateTime.parse(decodedExpiration);
    DateTime localExpiration = expiration.toLocal();
    return InitSessionOutput(token, wildcard, features, expiration, localExpiration);
  }
}

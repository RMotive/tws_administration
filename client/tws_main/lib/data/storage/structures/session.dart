import 'package:cosmos_foundation/common/common_module.dart';
import 'package:tws_main/data/repositories/tws_administration/services/outputs/init_session_output.dart';

class Session {
  final String token;
  final bool wildcard;
  final List<int> features;
  final DateTime expiration;
  final DateTime expirationLocal;

  const Session(this.token, this.wildcard, this.features, this.expiration, this.expirationLocal);
  factory Session.fromJson(JObject json) {
    String token = json.bindProp('token', '');
    bool wildcard = json.bindProp('wildcard', false);
    List<dynamic> encodedFeatures = json.bindProp('features', <dynamic>[]);
    List<int> features = encodedFeatures.cast();
    String encodedExpiration = json.bindProp('expiration', '');
    DateTime expiration = DateTime.parse(encodedExpiration);
    DateTime expirationLocal = expiration.toLocal();
    return Session(token, wildcard, features, expiration, expirationLocal);
  }
  factory Session.fromOutput(InitSessionOutput output) {
    String token = output.token;
    bool wildcard = output.wildcard;
    List<int> features = output.features;
    DateTime expiration = output.expiration;
    return Session(token, wildcard, features, expiration, output.expirationLocal);
  }
  
  JObject toJson() {
    return <String, dynamic>{
      'token': token,
      'wildcard': wildcard,
      'features': features,
      'expiration': expiration.toString(),
      'expirationLocal': expirationLocal.toString(),
    };
  }
}

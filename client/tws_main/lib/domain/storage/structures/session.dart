import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:cosmos_foundation/extensions/jobject.dart';

class Session {
  final String token;
  final bool wildcard;
  final List<int> features;
  final DateTime expiration;

  const Session(this.token, this.wildcard, this.features, this.expiration);
  factory Session.fromJson(JObject json) {
    String token = json.bindProp('token', '');
    bool wildcard = json.bindProp('wildcard', false);
    List<int> features = json.bindProp('features', <int>[]);
    DateTime expiration = json.bindProp('expiration', DateTime(0));
    return Session(token, wildcard, features, expiration);
  }
}

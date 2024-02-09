import 'package:cosmos_foundation/alias/aliases.dart';

class ForeignSessionModel {
  late final String token;
  late final bool wildcard;
  late final List<int> features;

  ForeignSessionModel(JObject json) {
    token = json['Token'];
    wildcard = json['Wildcard'];
    features = json['Features'];
  }
}

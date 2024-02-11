import 'package:cosmos_foundation/alias/aliases.dart';

class ForeignSessionModel {
  late final String token;
  late final bool wildcard;
  late final List<int> features;

  ForeignSessionModel(JObject json) {
    List<dynamic> dynamicList = json['Features'];
    List<int> featuresList = dynamicList.isEmpty ? <int>[] : (dynamicList.cast<int>());

    token = json['Token'];
    wildcard = json['Wildcard'];
    features = featuresList;
  }
}

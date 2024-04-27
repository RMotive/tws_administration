import 'package:csm_foundation_services/csm_foundation_services.dart';

final class Solution implements CSMEncodeInterface {
  final int id;
  final String name;
  final String sign;
  final String? description;

  const Solution(this.id, this.name, this.sign, this.description);
  factory Solution.des(JObject json) {
    int id = json.get('id');
    String name = json.get('name');
    String sign = json.get('sign');
    String? description = json.getDefault('description', null);

    return Solution(id, name, sign, description);
  }

  @override
  JObject encode() {
    return <String, dynamic>{
      'id': id,
      'name': name,
      'sign': sign,
      'description': description,
    };
  }
}

final class SolutionDecode implements CSMDecodeInterface<Solution> {
  const SolutionDecode();

  @override
  Solution decode(JObject json) {
    return Solution.des(json);
  }
}

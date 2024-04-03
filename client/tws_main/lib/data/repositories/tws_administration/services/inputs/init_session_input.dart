import 'dart:convert';
import 'dart:typed_data';

import 'package:cosmos_foundation/contracts/bases/model_base.dart';

typedef JSON = Map<String, dynamic>;

class InitSessionInput extends ModelBase {
  final String identity;
  final Uint8List password;

  const InitSessionInput(this.identity, this.password);
  factory InitSessionInput.fromJson(JSON json) {
    return InitSessionInput(json['Identity'], json['Password']);
  }

  @override
  JSON toJson() {
    return <String, dynamic>{
      'Identity': identity,
      'Password': base64.encode(password),
    };
  }
}

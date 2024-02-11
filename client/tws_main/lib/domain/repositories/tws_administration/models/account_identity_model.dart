import 'dart:convert';
import 'dart:typed_data';

import 'package:cosmos_foundation/contracts/interfaces/i_model.dart';

typedef JSON = Map<String, dynamic>;

class AccountIdentityModel implements IModel {
  final String identity;
  final Uint8List password;

  const AccountIdentityModel(this.identity, this.password);
  factory AccountIdentityModel.fromJson(JSON json) {
    return AccountIdentityModel(json['Identity'], json['Password']);
  }

  @override
  JSON toJson() {
    return <String, dynamic>{
      'Identity': identity,
      'Password': base64.encode(password),
    };
  }
}

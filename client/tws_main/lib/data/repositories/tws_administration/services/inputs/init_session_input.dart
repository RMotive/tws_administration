import 'dart:convert';
import 'dart:typed_data';


typedef JSON = Map<String, dynamic>;

class InitSessionInput {
  final String identity;
  final Uint8List password;

  const InitSessionInput(this.identity, this.password);
  factory InitSessionInput.fromJson(JSON json) {
    return InitSessionInput(json['Identity'], json['Password']);
  }

  JSON toJson() {
    return <String, dynamic>{
      'Identity': identity,
      'Password': base64.encode(password),
    };
  }
}

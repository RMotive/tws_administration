import 'dart:typed_data';

import 'package:tws_administration_service/tws_administration_service.dart';

final class DevelopmentSecrets {
  static final Credentials credentials = Credentials(identity, password);

  static const String identity = "quality_account";
  static final Uint8List password = Uint8List.fromList("quality".codeUnits);
}

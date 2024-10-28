import 'package:flutter/foundation.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

class Sources {
  static final TWSFoundationSource administration = TWSFoundationSource(
    kDebugMode,
    development: const CSMUri(
      '127.0.0.1',
      '',
      port: 5196,
      protocol: CSMProtocols.http,
    ),
  );
}

class TWSAdministrationSource {
}

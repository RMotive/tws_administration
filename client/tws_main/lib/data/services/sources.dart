import 'package:flutter/foundation.dart';
import 'package:tws_administration_service/tws_administration_service.dart';


class Sources {
  static final TWSAdministrationSource administration = TWSAdministrationSource(
  kDebugMode,
  development: const CSMUri(
      '192.168.100.32',
    '',
    port: 5196,
    protocol: CSMProtocols.http,
  ),
);
}


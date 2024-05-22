import 'package:flutter/foundation.dart';
import 'package:tws_administration_service/tws_administration_service.dart';

final TWSAdministrationSource administration = TWSAdministrationSource(
  kDebugMode,
  development: const CSMUri(
    '192.168.100.35',
    '',
    port: 5196,
    protocol: CSMProtocols.http,
  ),
);

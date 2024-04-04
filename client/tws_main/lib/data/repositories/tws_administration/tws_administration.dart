import 'package:cosmos_foundation/server/bases/server_bases_module.dart';
import 'package:cosmos_foundation/server/enums/csm_protocols.dart';
import 'package:cosmos_foundation/server/models/records/server_models_records_module.dart';
import 'package:tws_main/data/repositories/tws_administration/contracts/twsa_security_service_base.dart';
import 'package:tws_main/data/repositories/tws_administration/services/twsa_security_service.dart';

class TWSAdministrationRepository extends CSMRepositoryBase {
  // Avoid self instance
  TWSAdministrationRepository({
    TWSASecurityServiceBase? securityServiceImpl,
  }) : super(
          const CSMUriRecord(
            '192.168.100.38',
            '',
            port: 5196,
            protocol: CSMProtocols.http,
          ),
        ) {
    securityService = securityServiceImpl ?? TWSASecurityService(host);
  }

  // --> Services
  late final TWSASecurityServiceBase securityService;
}

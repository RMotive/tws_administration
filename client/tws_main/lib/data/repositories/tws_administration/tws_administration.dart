import 'package:cosmos_foundation/contracts/cosmos_repository.dart';
import 'package:cosmos_foundation/enumerators/cosmos_protocols.dart';
import 'package:cosmos_foundation/models/structs/cosmos_uri_struct.dart';
import 'package:tws_main/data/repositories/tws_administration/contracts/twsa_security_service_base.dart';
import 'package:tws_main/data/repositories/tws_administration/services/twsa_security_service.dart';

class TWSAdministrationRepository extends CosmosRepository {
  // Avoid self instance
  TWSAdministrationRepository({
    TWSASecurityServiceBase? securityServiceImpl,
  }) : super(
          const CosmosUriStruct(
            '192.168.100.38',
            '',
            port: 5196,
            protocol: CosmosProtocols.http,
          ),
        ) {
    securityService = securityServiceImpl ?? TWSASecurityService(host);
  }

  // --> Services
  late final TWSASecurityServiceBase securityService;
}

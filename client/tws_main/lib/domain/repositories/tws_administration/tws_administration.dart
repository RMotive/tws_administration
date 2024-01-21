import 'package:cosmos_foundation/contracts/cosmos_repository.dart';
import 'package:cosmos_foundation/models/structs/cosmos_uri_struct.dart';
import 'package:tws_main/domain/repositories/tws_administration/services/twsa_security_service.dart';

class TWSAdministrationRepository extends CosmosRepository {
  static TWSAdministrationRepository? _instance;
  // Avoid self instance
  TWSAdministrationRepository._()
      : super(
          const CosmosUriStruct(
            'localhost',
            '',
            port: null,
          ),
        ) {
    securityService = TWSASecurityService(host);
  }

  static TWSAdministrationRepository get instance => _instance ??= TWSAdministrationRepository._();

  // --> Services
  late final TWSASecurityService securityService;
}

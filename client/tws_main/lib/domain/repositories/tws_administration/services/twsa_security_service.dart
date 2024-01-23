import 'package:cosmos_foundation/models/structs/cosmos_uri_struct.dart';
import 'package:tws_main/domain/repositories/tws_administration/contracts/twsa_security_service_base.dart';

class TWSASecurityService extends TWSASecurityServiceBase {
  TWSASecurityService(CosmosUriStruct host)
      : super(
          host,
          'Security',
        );
        
  @override
  void initSesion() {
    // TODO: implement initSesion
  }
}

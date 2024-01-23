import 'package:cosmos_foundation/contracts/cosmos_service.dart';

abstract class TWSASecurityServiceBase extends CosmosService {
  TWSASecurityServiceBase(super.host, super.servicePath);

  void initSesion();
}

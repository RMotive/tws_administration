import 'package:cosmos_foundation/contracts/cosmos_service.dart';
import 'package:tws_main/domain/repositories/tws_administration/services/inputs/init_session_input.dart';
import 'package:tws_main/domain/repositories/tws_administration/services/outputs/init_session_output.dart';
import 'package:tws_main/domain/resolvers/twsa_resolver.dart';

typedef SResult<TSuccess> = TWSAResolver<TSuccess>;

abstract class TWSASecurityServiceBase extends CosmosService {
  TWSASecurityServiceBase(super.host, super.servicePath);

  Future<SResult<InitSessionOutput>> initSession(InitSessionInput account);
}

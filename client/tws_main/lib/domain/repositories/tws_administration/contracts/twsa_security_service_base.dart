import 'package:cosmos_foundation/contracts/cosmos_service.dart';
import 'package:cosmos_foundation/foundation/services/service_result.dart';
import 'package:tws_main/domain/repositories/tws_administration/models/account_identity_model.dart';
import 'package:tws_main/domain/repositories/tws_administration/models/foreign_session_model.dart';

abstract class TWSASecurityServiceBase extends CosmosService {
  TWSASecurityServiceBase(super.host, super.servicePath);

  Future<ServiceResult<ForeignSessionModel>> initSession(AccountIdentityModel account);
}

import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:cosmos_foundation/foundation/services/operation_result.dart';
import 'package:cosmos_foundation/foundation/services/service_result.dart';
import 'package:cosmos_foundation/models/structs/cosmos_uri_struct.dart';
import 'package:tws_main/domain/repositories/tws_administration/contracts/twsa_security_service_base.dart';
import 'package:tws_main/domain/repositories/tws_administration/models/account_identity_model.dart';
import 'package:tws_main/domain/repositories/tws_administration/models/foreign_session_model.dart';

typedef SResult<T> = Future<ServiceResult<T>>;

class TWSASecurityService extends TWSASecurityServiceBase {
  TWSASecurityService(CosmosUriStruct host)
      : super(
          host,
          'security',
        );
        
  @override
  SResult<ForeignSessionModel> initSession(
    AccountIdentityModel account,
  ) async {
    OperationResult oResult = await post('initSession', account);
    return ServiceResult<ForeignSessionModel>(
      oResult,
      (JObject json) => ForeignSessionModel(json),
    );
  }
}

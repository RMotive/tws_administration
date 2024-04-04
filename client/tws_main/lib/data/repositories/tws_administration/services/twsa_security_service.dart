import 'package:cosmos_foundation/common/common_module.dart';
import 'package:cosmos_foundation/server/server_module.dart';
import 'package:tws_main/data/repositories/tws_administration/contracts/twsa_security_service_base.dart';
import 'package:tws_main/data/repositories/tws_administration/services/inputs/init_session_input.dart';
import 'package:tws_main/data/repositories/tws_administration/services/outputs/init_session_output.dart';
import 'package:tws_main/data/resolvers/twsa_resolver.dart';

typedef SResult<T> = Future<TWSAResolver<T>>;

class TWSASecurityService extends TWSASecurityServiceBase {
  TWSASecurityService(CSMUriRecord host)
      : super(
          host,
          'security',
        );

  @override
  SResult<InitSessionOutput> initSession(
    InitSessionInput account,
  ) async {
    CSMActEffect oResult = await post('initSession', account);
    return TWSAResolver<InitSessionOutput>(
      oResult,
      factory: (JObject json) => InitSessionOutput.fromJson(json),
    );
  }
}

import 'package:cosmos_foundation/common/common_module.dart';
import 'package:cosmos_foundation/server/bases/csm_service_resolver_base.dart';
import 'package:http/http.dart';
import 'package:tws_main/data/repositories/tws_administration/templates/twsa_failure.dart';
import 'package:tws_main/data/repositories/tws_administration/templates/twsa_template.dart';

class TWSAResolver<TSuccess> extends CSMServiceResolverBase<TSuccess> {
  TWSAResolver(super.operationResult, {required super.factory});

  void resolve(
    TSuccess modelDefault, {
    required void Function() onConnectionFailure,
    void Function(Object exception, StackTrace trace)? onException,
    required void Function(TWSATemplate<TWSAFailure> failure, int status) onFailure,
    required void Function(TWSATemplate<TSuccess> success) onSuccess,
    void Function()? onFinally,
  }) {
    const CSMAdvisor advisor = CSMAdvisor("twsa resolver");

    result.resolve(
      (JObject jSuccess) {
        advisor.success(
          'Communication success',
          info: jSuccess,
        );
        final TWSATemplate<TSuccess> templateWithSuccess = TWSATemplate<TSuccess>.fromJson(
          jSuccess,
          modelDefault,
          (JObject json) => super.factory(json),
        );
        onSuccess(templateWithSuccess);
      },
      (JObject jFailure, int statusCode) {
        advisor.warning(
          'Communication failure',
          info: jFailure,
        );
        final TWSATemplate<TWSAFailure> templateWithFailure = TWSATemplate<TWSAFailure>.fromJson(
          jFailure,
          TWSAFailure.def(),
          (JObject json) => TWSAFailure.fromJson(json),
        );
        onFailure(templateWithFailure, statusCode);
      },
      (Object exception, StackTrace trace) {
        advisor.exception(
          'Communication critical exception',
          exception as Exception,
          trace,
        );
        if (exception is ClientException) {
          onConnectionFailure.call();
        } else {
          onException?.call(exception, trace);
        }
      },
    );
    onFinally?.call();
  }
}

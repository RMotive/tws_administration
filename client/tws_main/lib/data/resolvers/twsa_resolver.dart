import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:cosmos_foundation/contracts/bases/service_resolver_base.dart';
import 'package:cosmos_foundation/helpers/advisor.dart';
import 'package:http/http.dart';
import 'package:tws_main/data/repositories/tws_administration/templates/twsa_failure.dart';
import 'package:tws_main/data/repositories/tws_administration/templates/twsa_template.dart';

class TWSAResolver<TSuccess> extends ServiceResolverBase<TSuccess> {
  TWSAResolver(super.operationResult, {required super.factory});

  void resolve(
    TSuccess modelDefault, {
    required void Function() onConnectionFailure,
    void Function(Object exception, StackTrace trace)? onException,
    required void Function(TWSATemplate<TWSAFailure> failure, int status) onFailure,
    required void Function(TWSATemplate<TSuccess> success) onSuccess,
    void Function()? onFinally,
  }) {
    const Advisor advisor = Advisor("twsa resolver");

    operationResult.resolve(
      (JObject jSuccess) {
        advisor.adviseSuccess(
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
        advisor.adviseWarning(
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
        advisor.adviseException(
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

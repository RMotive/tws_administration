import 'package:csm_foundation_services/csm_foundation_services.dart';
import 'package:tws_administration/tws_administration.dart';

class MainResolver<TSuccess extends CSMEncodeInterface> extends CSMServiceResolverBase<TSuccess> {
  MainResolver(super.operationResult);

  void resolve({
    required void Function() onConnectionFailure,
    void Function(Object exception, StackTrace trace)? onException,
    required void Function(FailureFrame failure, int status) onFailure,
    required void Function(SuccessFrame<TSuccess> success) onSuccess,
    void Function()? onFinally,
    CSMDecodeInterface<TSuccess>? decoder,
  }) {
    result.resolve(
      (JObject jSuccess) {
        if (decoder == null) {
          throw 'DependencyException: decoder is mandatory due to framing is generic abstracted';
        }

        CSMDecodeInterface<SuccessFrame<TSuccess>> frameDecoder = SuccessFrameDecode<TSuccess>(decoder);
        final SuccessFrame<TSuccess> templateWithSuccess = deserealize(jSuccess, decode: frameDecoder);
        onSuccess(templateWithSuccess);
      },
      (JObject jFailure, int statusCode) {
        final FailureFrame templateWithFailure = FailureFrame.des(jFailure);
        onFailure(templateWithFailure, statusCode);
      },
      (Object exception, StackTrace trace) {
        if (exception.toString().contains('ClientException')) {
          onConnectionFailure.call();
        } else {
          onException?.call(exception, trace);
        }
      },
    );
    onFinally?.call();
  }
}

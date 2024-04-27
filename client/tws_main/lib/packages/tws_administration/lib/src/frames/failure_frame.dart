import 'package:csm_foundation_services/csm_foundation_services.dart';
import 'package:tws_administration/src/models/server_exception_publish.dart';

/// Represents a standarized information for failures responses from the server source.
final class FailureFrame {
  /// Unique server transaction identification.
  final String tracer;

  /// Server exception reflection information.
  final ServerExceptionPublish estela;

  /// Generates a new failure frame response object.
  const FailureFrame(this.tracer, this.estela);

  factory FailureFrame.des(JObject json) {
    return FailureFrame(
      '',
      ServerExceptionPublish(1, '', '', '', <String, dynamic>{}),
    );
  }
}

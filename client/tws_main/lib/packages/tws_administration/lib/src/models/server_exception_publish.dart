import 'package:csm_foundation_services/csm_foundation_services.dart';

/// Represents a server exception exposed information.
final class ServerExceptionPublish {
  /// Identification of situation.
  final int situation;

  /// StackTrace from the exception source at server context.
  final String trace;

  /// User friendly advise message to display (recommended).
  final String advise;

  /// A system reflected type with message from the exception thrown at server context.
  final String system;

  /// A custom collection of data, this is custom per exception definition.
  final JObject factors;

  /// Generates a new server representation information object.
  const ServerExceptionPublish(this.situation, this.trace, this.advise, this.system, this.factors);
}

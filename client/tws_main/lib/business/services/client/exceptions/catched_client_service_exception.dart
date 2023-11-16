/// Defines a custom [Exception] to cases
/// where [ClientServices] catches unknown exceptions in their proccesses.
class CatchedClientServiceException implements Exception {
  /// Defines the catched exception object.
  final Object catched;

  /// Defines the stacktrace fired from the catched exception object.
  final StackTrace stackTrace;

  /// Instances a new exception object that represents an unknown exception
  /// catched in [ClientServices] proccesses.
  const CatchedClientServiceException(this.catched, this.stackTrace);

  @override
  String toString() {
    return 'Critical exception catched in Client Services'
        '\n catched: $catched'
        '\n trace: stackTrace';
  }
}

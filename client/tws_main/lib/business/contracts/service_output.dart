import 'package:tws_main/constants/defs_constants.dart';

/// Contract class.
///
/// Declares a contractual indications for all classes
/// that will work as service call output when it is resolved.
abstract class ServiceOutput {
  const ServiceOutput();

  /// Converts the current object into a JSON object.
  JSON toJson();

  @override
  String toString() {
    return toJson().toString();
  }
}

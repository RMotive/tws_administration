/// Contract class.
///
/// Declares a contractual indications for all classes
/// that will work as service call output when it is resolved.
abstract class ServiceOutput {
  Map<String, dynamic> toJson();

  @override
  String toString() {
    return toJson().toString();
  }
}

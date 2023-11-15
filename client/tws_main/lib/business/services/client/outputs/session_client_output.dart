import 'package:tws_main/business/contracts/service_output.dart';

/// Output class (client-side).
///
/// Declares an output class to store information retrieved and resolved
/// by [SessionClientService].
class SessionClientOutput extends ServiceOutput {
  @override
  Map<String, dynamic> toJson() {
    return <String, dynamic>{};
  }
}

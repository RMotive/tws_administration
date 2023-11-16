import 'package:tws_main/business/contracts/service_output.dart';
import 'package:tws_main/constants/defs_constants.dart';

/// Output class (client-side).
///
/// Declares an output class to store information retrieved and resolved
/// by [SessionClientService].
class SessionClientOutput extends ServiceOutput {
  final List<DateTime> timeMarks;
  final Duration lifeTime;
  final String identifier;

  bool get valid {
    return timeMarks.isNotEmpty;
  }

  /// Instances a new empty session object, commonly used to invalidate the object.
  const SessionClientOutput()
      : identifier = '',
        timeMarks = const <DateTime>[],
        lifeTime = const Duration();

  /// Instances a new generated session object, commonly used to generate custom objects with
  /// specific desired values.
  const SessionClientOutput.generate(this.identifier, this.timeMarks, this.lifeTime);

  /// Instances a new populated object from a JSON object.
  factory SessionClientOutput.fromJson(JSON json) {
    String identifier = (json['identifier'] as String?) ?? '';
    List<DateTime> timeMarks = (json['timeMarks'] as List<DateTime>?) ?? <DateTime>[];
    Duration lifeTime = Duration(microseconds: ((json['lifeTime'] as int?) ?? 0));

    return SessionClientOutput.generate(identifier, timeMarks, lifeTime);
  }

  @override
  JSON toJson() {
    return <String, dynamic>{
      'identifier': identifier,
      'lifeTime': lifeTime.inMicroseconds,
      'timeMarks': timeMarks,
    };
  }
}

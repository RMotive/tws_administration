import 'package:cosmos_foundation/helpers/advisor.dart';
import 'package:flutter/foundation.dart';
import 'package:tws_main/business/contracts/client_service_contract.dart';
import 'package:tws_main/business/services/client/outputs/session_client_output.dart';

export './outputs/session_client_output.dart';

// --> Helpers
const Advisor _advisor = Advisor('session-csvc');

/// Client side service.
///
/// This service allows the user to fetch client-side data
/// related to the current session, and actions related to that stored reference.
class SessionClientService extends ClientServiceContract {
  /// Stores the private instance reference pointing.
  static SessionClientService? _instance;

  /// Validates the correct instance of the current singleton object, and returns
  /// the useful correct one.
  static SessionClientService get i => _instance ??= SessionClientService._();

  /// Private auto-constructor to avoid the public use of it.
  SessionClientService._()
      : super(
          'session-reference',
        );

  /// Fetch the current session stored in the clinet-side storage service.
  ///
  /// Returns:
  ///   - [SessionClientOutput] validated and resolved by fetching the data from the client-side storage.
  ///
  /// Exceptions:
  ///   - This method only can throw uncaught exceptions.
  Future<SessionClientOutput> fetchStoredSession() async {
    try {
      _advisor.adviseMessage('Starting to fetch the current session (client-side storage)');
      dynamic absItem = storage.getItem(storageReference);
      if (absItem == null) {
        _advisor.adviseWarning('Unable to retrieve a correct session from client-side storage');
        return SessionClientOutput();
      }

      return absItem as SessionClientOutput;
    } catch (_) {
      debugPrint('error');
      return SessionClientOutput();
    }
  }

  Future<SessionClientOutput> updateStoredSession(SessionClientOutput session) async {
    _advisor.adviseMessage(
      'Starting to update the stored session (client-side storage)',
      info: <String, dynamic>{
        'newSession': session.toString(),
      },
    );
    storage.setItem(storageReference, session);
    return session;
  }
}

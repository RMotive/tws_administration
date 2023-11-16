import 'package:cosmos_foundation/helpers/advisor.dart';
import 'package:flutter/foundation.dart';
import 'package:tws_main/business/contracts/client_service_contract.dart';
import 'package:tws_main/business/services/client/exceptions/catched_client_service_exception.dart';
import 'package:tws_main/business/services/client/exceptions/empty_sotred_session_exception.dart';
import 'package:tws_main/business/services/client/outputs/session_client_output.dart';

export './outputs/session_client_output.dart';
export './exceptions/empty_sotred_session_exception.dart';

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
  Future<SessionClientOutput> fetchStoredSession() async {
    _advisor.adviseMessage('Starting to fetch the current session (client-side storage)');
    try {
      dynamic absItem = storage.getItem(storageReference);
      if (absItem == null) throw const EmptySotredSessionException();

      return SessionClientOutput.fromJson(absItem);
    } catch (catched, stackTrace) {
      debugPrint('error');
      throw CatchedClientServiceException(catched, stackTrace);
    }
  }

  /// Updates the current session stored in the clinet-side storage device.
  ///
  /// Returns:
  ///   - [SessionClientOutput] validated and resolved by fetching the data from the client-side storage.
  Future<SessionClientOutput> updateStoredSession(SessionClientOutput session) async {
    _advisor.adviseMessage(
      'Starting to update the stored session (client-side storage)',
      info: <String, dynamic>{
        'newSession': session.toString(),
      },
    );
    try {
      await storage.setItem(storageReference, session);
      return session;
    } catch (catched, stackTrace) {
      throw CatchedClientServiceException(catched, stackTrace);
    }
  }
}

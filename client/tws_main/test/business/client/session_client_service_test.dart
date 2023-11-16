import 'package:flutter_test/flutter_test.dart';
import 'package:tws_main/business/services/client/session_client_service.dart';

void main() {
  // --> Init resources
  /// Internal service reference [SessionClientService] for internal testing.
  final SessionClientService service = SessionClientService.i;

  /// Session object identifier reference to use for matching expectations.
  const String arrangeSessionIdentifier = 'session-service-test-identifier';

  /// Session object to store and handle testing matching expectations.
  const SessionClientOutput arrangeSession = SessionClientOutput.generate(arrangeSessionIdentifier, <DateTime>[], Duration.zero);

  // --> TESTS
  test(
    'Session update stores correctly (client-side)',
    () async {
      final SessionClientOutput check = await service.updateStoredSession(arrangeSession);
      final SessionClientOutput secondCheck = await service.fetchStoredSession();

      expect(check.identifier, arrangeSessionIdentifier, reason: 'Session update failed');
      expect(secondCheck.identifier, arrangeSessionIdentifier, reason: 'Second check session update failed');
    },
  );
  test(
    'Session fetch from store correctly (client-side)',
    () async {
      await service.updateStoredSession(arrangeSession);

      final SessionClientOutput check = await service.fetchStoredSession();

      expect(check.identifier, arrangeSession.identifier, reason: 'Session fetch failed');
    },
  );
}

import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:cosmos_foundation/helpers/advisor.dart';
import 'package:localstorage/localstorage.dart';
import 'package:tws_main/data/storage/structures/session.dart';

/// Specifies a static service.
///
/// Works as a singleton service to manage everything related to the session local storage functions.
class SessionStorage {
  //* --> SINGLETON PATTER HANDLER <--
  static SessionStorage? _instance;
  // Avoid self instance
  static SessionStorage get instance => _instance ??= SessionStorage._();

  /// Indicates the key reference for the session related storage.
  static const String _kStorageKey = "session_store.json";

  /// Indicates the key reference for the specific session item in the storage.
  static const String _kSessionItemStoreKey = "session";

  /// Stores the reference for the specific storage used to store [Session]
  late final LocalStorage _storage;

  /// Service to print advises in console.
  late final Advisor _advisor;

  /// Stores the current session that the manager works in.
  Session? _session;

  /// Current session.
  Session? get session => _session;

  /// Wheter the application context has an active session
  late Future<bool> isSession;

  /// When the singleton is created it first will look if the browser already has a stored session.
  SessionStorage._() {
    _advisor = const Advisor('session-storage');
    _storage = LocalStorage(_kStorageKey);
    _advisor.adviseMessage('Starting engines for [SessionStorage]');

    isSession = Future<bool>(
      () async {
        if (_session != null) return true;
        bool result = await _storage.ready.then(
          (bool value) {
            if (!value) throw 'local storage manager session isn\'t ready';
            JObject? stored = _storage.getItem(_kSessionItemStoreKey);
            if (stored == null) {
              _advisor.adviseWarning('No session found');
              return false;
            }
            Session sessionObject = Session.fromJson(stored);
            DateTime expiration = sessionObject.expiration;
            if (expiration.isBefore(DateTime.now())) {
              _advisor.adviseWarning('Session expired', info: stored);
              _storage.deleteItem(_kSessionItemStoreKey);
            }
            _advisor.adviseSuccess('Session currently valid', info: stored);
            _session = sessionObject;
            return true;
          },
        );
        return result;
      },
    );
  }

  void storeSession(Session session) async {
    DateTime expiration = session.expiration;
    if (expiration.isBefore(DateTime.now().toLocal())) {
      _advisor.adviseWarning(
        'Unable to store the given session',
        info: <String, dynamic>{
          'reason': 'Session twsalready expired',
          'session': session.toJson(),
        },
      );
      return;
    }
    _session = session;
    try {
      if (await _storage.ready) {
        _storage.setItem(_kSessionItemStoreKey, _session);
      } else {
        throw 'local storage manager isn\'t ready to operate.';
      }
    } catch (_) {
      rethrow;
    }
  }
}

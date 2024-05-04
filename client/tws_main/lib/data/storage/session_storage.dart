import 'dart:convert';

import 'package:csm_foundation_view/csm_foundation_view.dart';
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

  /// Indicates the key reference for the specific session item in the storage.
  static const String _kSessionItemStoreKey = "session";

  /// Stores the reference for the specific storage used to store [Session]
  late final LocalStorage _storage;

  /// Service to print advises in console.
  late final CSMAdvisor _advisor;

  /// Stores the current session that the manager works in.
  Session? _session;

  /// Current session.
  Session? get session => _session;

  /// Wheter the application context has an active session
  late Future<bool> isSession;

  /// When the singleton is created it first will look if the browser already has a stored session.
  SessionStorage._() {
    _advisor = const CSMAdvisor('session-storage');
    _storage = localStorage;
    _advisor.message('Starting engines for [SessionStorage]');

    isSession = Future<bool>(
      () async {
        if (_session != null) return true;
        String? storedValue = _storage.getItem(_kSessionItemStoreKey);
        if (storedValue == null) return false;

        JObject? stored = jsonDecode(storedValue);
        if (stored == null) {
          _advisor.warning('No session found');
          return false;
        }
        Session sessionObject = Session.fromJson(stored);
        DateTime expiration = sessionObject.expiration;
        if (expiration.isBefore(DateTime.now())) {
          _advisor.warning('Session expired', info: stored);
          _storage.removeItem(_kSessionItemStoreKey);
        }
        _advisor.warning('Session currently valid', info: stored);
        _session = sessionObject;
        return true;
      },
    );
  }

  void storeSession(Session session) async {
    DateTime expiration = session.expiration;
    if (expiration.isBefore(DateTime.now().toLocal())) {
      _advisor.warning(
        'Unable to store the given session',
        info: <String, dynamic>{
          'reason': 'Session twsalready expired',
          'session': session.toJson(),
        },
      );
      return;
    }
    _session = session;
    _storage.setItem(_kSessionItemStoreKey, jsonEncode(_session?.toJson()));
  }
}

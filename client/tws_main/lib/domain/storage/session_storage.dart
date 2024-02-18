import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:localstorage/localstorage.dart';
import 'package:tws_main/domain/storage/structures/session.dart';

class SessionStorage {
  //* --> SINGLETON PATTER HANDLER <--
  static SessionStorage? _instance;
  // Avoid self instance
  static SessionStorage get instance => _instance ??= SessionStorage._();

  /// Indicates the key reference for the session related storage.
  static const String _kStorageKey = "session-store";

  /// Indicates the key reference for the specific session item in the storage.
  static const String _kSessionItemStoreKey = "session";

  /// Stores the reference for the specific storage used to store [Session]
  late final LocalStorage _storage;

  /// Stores the current session that the manager works in.
  late Session _session;

  /// When the singleton is created it first will look if the browser already has a stored session.
  SessionStorage._() {
    _storage = LocalStorage(_kStorageKey);
    JObject? stored = _storage.getItem(_kSessionItemStoreKey);
    if (stored != null) _session = Session.fromJson(stored);
  }

  void storeSession(Session session) {
    _session = session;
    _storage.setItem(_kSessionItemStoreKey, _session);
  }
}

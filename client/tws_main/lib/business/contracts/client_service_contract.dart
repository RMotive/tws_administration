import 'package:localstorage/localstorage.dart';

/// Contract class.
///
/// Indicates a contract to all classes that expect work as
/// client side services (local-data/client-side-data fetching).
///
/// Some examples:
///   - Local persistive theme stored.
///   - Local persistive session stored.
///   - Local persistive application settings stored.
abstract class ClientServiceContract {
  /// The main client-side storage reference for the current
  /// service context.
  final String storageReference;

  /// Client-side storage manager object, to apply
  /// storage operations.
  late final LocalStorage storage;

  /// Creates an instance of the contract to handle
  /// the shared main client-side storage reference.
  ClientServiceContract(this.storageReference) {
    storage = LocalStorage(storageReference);
  }
}

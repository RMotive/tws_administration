class EmptySotredSessionException implements Exception {
  const EmptySotredSessionException();

  @override
  String toString() {
    return 'The current web local storage context doesn\'t have a reference to a session object';
  }
}

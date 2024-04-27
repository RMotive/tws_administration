abstract interface class TWSArticleTableDataAdapter<TEntries> {
  Future<List<TEntries>> update(int page, int range);
}

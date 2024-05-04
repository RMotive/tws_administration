part of '../solutions_article.dart';

final class _TableAdapter implements TWSArticleTableDataAdapter<Solution> {
  const _TableAdapter();

  @override
  Future<List<Solution>> update(int page, int range) {
    throw UnimplementedError();
  }
}

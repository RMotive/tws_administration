part of '../features_article.dart';

final class _TableAdapter implements TWSArticleTableDataAdapter<Feature> {
  const _TableAdapter();

  @override
  Future<List<Feature>> update(int page, int range) {
    throw UnimplementedError();
  }
}

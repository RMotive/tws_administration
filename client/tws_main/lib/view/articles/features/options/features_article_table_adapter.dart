part of '../features_article.dart';

final class _TableAdapter implements TWSArticleTableDataAdapter<Feature> {
  const _TableAdapter();

  @override
  Future<MigrationView<Feature>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) {
    throw UnimplementedError();
  }
}

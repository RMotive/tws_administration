part of '../features_article.dart';

final class _TableAdapter implements TWSArticleTableAdapter<Feature> {
  const _TableAdapter();
  
  @override
  Future<MigrationView<Feature>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) {
    throw UnimplementedError();
  }
  
  @override
  Widget? composeEditor(Feature set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(Feature set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(Feature set, BuildContext context) {}
}

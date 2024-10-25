part of '../features_article.dart';

final class _TableAdapter implements TWSArticleTableAdapter<Feature> {
  const _TableAdapter();
  
  @override
  Future<SetViewOut<Feature>> consume(int page, int range, List<SetViewOptions> orderings) {
    throw UnimplementedError();
  }
  
  @override
  TWSArticleTableEditor? composeEditor(Feature set, Function closeReinvoke, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(Feature set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(Feature set, BuildContext context) {}
}

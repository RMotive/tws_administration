part of 'tws_article_creation.dart';

final class _TWSArticleCreationState<TModel> extends CSMStateBase {
  late List<_TWSArticleCreationItemState<TModel>> states;
  late TModel Function() modelFactory;

  int current = 0;

  _TWSArticleCreationState(this.modelFactory) {
    TModel model = modelFactory();

    states = <_TWSArticleCreationItemState<TModel>>[
      _TWSArticleCreationItemState<TModel>(model),
    ];
  }
}

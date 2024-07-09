part of 'tws_article_creator.dart';

final class _TWSArticleCreationState<TModel> extends CSMStateBase {
  late List<TWSArticleCreatorItemState<TModel>> states;
  late TModel Function() modelFactory;
  int current = 0;

  _TWSArticleCreationState(this.modelFactory) {
    TModel model = modelFactory();

    states = <TWSArticleCreatorItemState<TModel>>[
      TWSArticleCreatorItemState<TModel>(model),
    ];
  }

  void removeItem(int index) {
    states.removeAt(index);

    current = 0;
    effect();
  }

  void addItem() {
    final TModel modelFactoried = modelFactory();

    TWSArticleCreatorItemState<TModel> newState = TWSArticleCreatorItemState<TModel>(modelFactoried);
    states.insert(0, newState);
    current = 0;
    effect();
  }

  void changeSelection(int index) {
    current = index;
    effect();
  }
}

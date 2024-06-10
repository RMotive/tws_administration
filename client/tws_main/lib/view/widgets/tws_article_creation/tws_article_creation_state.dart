part of 'tws_article_creation.dart';

final class _TWSArticleCreationState<TModel> extends CSMStateBase {
  late List<TWSArticleCreationItemState<TModel>> states;
  late TModel Function() modelFactory;
  int current = 0;

  _TWSArticleCreationState(this.modelFactory) {
    TModel model = modelFactory();

    states = <TWSArticleCreationItemState<TModel>>[
      TWSArticleCreationItemState<TModel>(model),
    ];
  }



  void removeItem(int index) {
    states.removeAt(index);

    current = 0;
    effect();
  }

  void addItem() {
    final TModel modelFactoried = modelFactory();

    TWSArticleCreationItemState<TModel> newState = TWSArticleCreationItemState<TModel>(modelFactoried);
    states.insert(0, newState);
    current = 0;
    effect();
  }

  void changeSelection(int index) {
    current = index;
    effect();
  }
}

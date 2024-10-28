part of 'tws_article_creator.dart';

final class _TWSArticleCreationState<TModel> extends CSMStateBase {
  /// List that stores all sets states added to the form.
  late List<TWSArticleCreatorItemState<TModel>> states;
  /// Facotory to generate models with defaults properties.
  late TModel Function() modelFactory;
  /// Currently selected item in form list.
  int current = 0;

  _TWSArticleCreationState(this.modelFactory) {
    TModel model = modelFactory();

    states = <TWSArticleCreatorItemState<TModel>>[
      TWSArticleCreatorItemState<TModel>(model),
    ];
  }
  /// Remove method.
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

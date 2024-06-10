

import 'package:csm_foundation_view/csm_foundation_view.dart';

final class TWSArticleCreationItemState<TModel> extends CSMStateBase {
  late TModel _model;

  TWSArticleCreationItemState(this._model);


  TModel get model => _model;
  void updateModel(TModel newModel) => _model = newModel;
  void updateModelRedrawing(TModel newModel) {
    _model = newModel;
    effect();
  } 
}

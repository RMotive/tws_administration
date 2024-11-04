

import 'package:csm_view/csm_view.dart';

final class TWSArticleCreatorItemState<TModel> extends CSMStateBase {
  late TModel _model;
  late bool _valid;

  TWSArticleCreatorItemState(this._model) : _valid = true;


  TModel get model => _model;
  bool get valid => _valid;

  void updateModel(TModel newModel) => _model = newModel;
  void updateModelRedrawing(TModel newModel) {
    _model = newModel;
    effect();
  }

  void updateInvalid(bool newInvalid) => _valid = newInvalid;
  void updateInvalidRedrawing(bool newInvalid) {
    _valid = newInvalid;
    effect();
  }

  void updateFactory(TModel Function() useFactory){
    _model = useFactory();
    effect();
  }
}

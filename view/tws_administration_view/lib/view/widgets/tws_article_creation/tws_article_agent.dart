import 'package:flutter/foundation.dart';

final class TWSArticleCreatorAgent<TModel> extends ChangeNotifier {
  TWSArticleCreatorAgent();

  void create() {
    notifyListeners();
  }
}

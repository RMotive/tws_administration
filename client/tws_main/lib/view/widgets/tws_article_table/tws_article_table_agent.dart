import 'package:flutter/material.dart';

final class TWSArticleTableAgent with ChangeNotifier {
  void refresh() {
    notifyListeners();
  }
}

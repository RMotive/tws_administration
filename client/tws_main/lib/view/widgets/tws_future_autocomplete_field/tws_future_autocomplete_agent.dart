import 'package:flutter/material.dart';

final class TWSFutureAutoCompleteAgent with ChangeNotifier {
  void refresh() {
    notifyListeners();
  }
}

import 'package:flutter/material.dart';

final MasterLayoutMenuState masterLayoutMenuState = MasterLayoutMenuState();

class MasterLayoutMenuState with ChangeNotifier {
  bool _menuClosed = true;
  bool _drawerActive = false;

  bool get drawerClosed => _menuClosed;
  bool get drawerActive => _drawerActive;

  void restore() {
    _menuClosed = true;
    _drawerActive = true;
    notifyListeners();
  }

  void deactivateDrawer() {
    _drawerActive = false;
    notifyListeners();
  }

  void openMenu() {
    _menuClosed = false;
    notifyListeners();
  }

  void closeMenu() {
    _menuClosed = true;
    notifyListeners();
  }
}

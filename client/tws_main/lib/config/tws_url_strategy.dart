import 'dart:ui';

import 'package:flutter_web_plugins/url_strategy.dart';
import 'package:tws_main/config/routes/tws_routes.dart';

class TWSUrlStrategy extends PathUrlStrategy {
  @override
  void pushState(Object? state, String title, String url) {
    String currentPath = getPath();
    String maybe = state.toString();

    if (currentPath == accessRoute.path || url == accessRoute.path) {
      super.replaceState(state, title, url);
      
      return;
    }
    super.pushState(state, title, url);
  }
}

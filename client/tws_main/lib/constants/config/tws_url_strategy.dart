import 'package:flutter_web_plugins/url_strategy.dart';
import 'package:tws_main/constants/config/routes/tws_routes.dart';

class TWSUrlStrategy extends PathUrlStrategy {
  @override
  void pushState(Object? state, String title, String url) {
    String currentPath = getPath();

    if (currentPath == accessRoute.path || url == accessRoute.path) {
      super.replaceState(state, title, url);
      return;
    }
    super.pushState(state, title, url);
  }
}

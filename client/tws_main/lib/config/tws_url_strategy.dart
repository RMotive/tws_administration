import 'package:flutter_web_plugins/url_strategy.dart';

class TWSUrlStrategy extends PathUrlStrategy {
  @override
  void pushState(Object? state, String title, String url) {
    replaceState(state, title, url);
  }
}

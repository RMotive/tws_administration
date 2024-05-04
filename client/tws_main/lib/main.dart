import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:flutter_web_plugins/url_strategy.dart';
import 'package:localstorage/localstorage.dart';
import 'package:tws_main/core/router/twsa_route_tree.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/core/theme/twsa_theme_dark.dart';
import 'package:tws_main/data/storage/session_storage.dart';

/// --> Flutter entry point.
void main() async {
  usePathUrlStrategy();  
  /// --> Service initialize
  await initLocalStorage();
  SessionStorage.instance;
  runApp(
    const TWSAdministration(),
  );
}

class TWSAdministration extends StatelessWidget {
  const TWSAdministration({super.key});

  @override
  Widget build(BuildContext context) {
    return CSMApplication<CSMThemeBase>(
      listenFrame: false,
      defaultTheme: const TWSAThemeDark(),
      routerConfig: TWSARouteTree(),
      builder: (BuildContext context, Widget? home) {
        CSMColorThemeOptions theme = getTheme<TWSAThemeBase>().page;
        
        return DefaultTextStyle(
          style: const TextStyle(
            color: Colors.white60,
            fontSize: 16,
          ),
          child: ColoredBox(
            color: theme.main,
            child: home,
          ),
        );
      },
    );
  }
}

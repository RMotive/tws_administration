import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_web_plugins/url_strategy.dart';
import 'package:localstorage/localstorage.dart';
import 'package:tws_main/core/router/twsa_route_tree.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/core/theme/twsa_theme_dark.dart';
import 'package:tws_main/core/tools/development_configurator.dart';

/// --> Flutter entry point.
void main() async {
  usePathUrlStrategy();  
  /// --> Initializing local storage
  await initLocalStorage();
  /// --> Configuring user on development mode.
  if (kDebugMode) {
    try {
      await DevelopmentConfigurator.configure();
    } catch (x, s) {
      final Exception excep = Exception(x.toString());
      const CSMAdvisor('development-configurer').exception('Development configurer exception', excep, s);
      runApp(
        MaterialApp(
          home: ColoredBox(
            color: Colors.black,
            child: Center(
              child: Text(
                'Development pre configuration failure\n$x',
                style: const TextStyle(
                  color: Colors.red,
                  fontStyle: FontStyle.normal,
                  decoration: TextDecoration.none,
                  fontSize: 16,
                ),
              ),
            ),
          ),
        ),
      );
      return;
    }
  }
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

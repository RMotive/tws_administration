import 'package:cosmos_foundation/csm_application.dart';
import 'package:cosmos_foundation/theme/theme_module.dart';
import 'package:flutter/material.dart';
import 'package:flutter_web_plugins/url_strategy.dart';
import 'package:tws_main/core/router/tws_routing.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/core/theme/twsa_theme_dark.dart';
import 'package:tws_main/data/storage/session_storage.dart';

/// --> Flutter entry point.
void main() {
  usePathUrlStrategy();  
  /// --> Service initialize
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
      routerConfig: TWSRouting(),
      builder: (BuildContext context, Widget? home) {
        CSMColorThemeOptions theme = getTheme<TWSAThemeBase>().pageColorStruct;
        
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

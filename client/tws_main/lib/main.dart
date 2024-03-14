import 'package:cosmos_foundation/foundation/hooks/cosmos_app.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
import 'package:flutter/material.dart';
import 'package:flutter_web_plugins/url_strategy.dart';
import 'package:tws_main/core/router/tws_routing.dart';
import 'package:tws_main/core/theme/dark_theme.dart';
import 'package:tws_main/core/theme/theme_base.dart';
import 'package:tws_main/domain/storage/session_storage.dart';

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
    return CosmosApp<ThemeBase>(
      listenFrameSize: false,
      defaultTheme: const DarkTheme(),
      routerConfig: TWSRouting(),
      generalBuilder: (BuildContext context, Widget? home) {
        ThemeColorStruct theme = getTheme<ThemeBase>().pageColorStruct;
        
        return DefaultTextStyle(
          style: const TextStyle(
            color: Colors.white60,
            fontSize: 16,
          ),
          child: ColoredBox(
            color: theme.mainColor,
            child: home,
          ),
        );
      },
    );
  }
}

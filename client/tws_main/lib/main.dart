import 'package:cosmos_foundation/foundation/hooks/cosmos_app.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/router_base.dart';
import 'package:tws_main/core/theme/theme_base.dart';
import 'package:tws_main/core/theme/theme_dark.dart';

/// --> Flutter entry point.
void main() {
  runApp(
    const TWSAdministration(),
  );
}

class TWSAdministration extends StatelessWidget {
  const TWSAdministration({super.key});

  @override
  Widget build(BuildContext context) {
    return CosmosApp<ThemeBase>(
      defaultTheme: const ThemeDark(),
      routerConfig: RouterBase(),
      generalBuilder: (BuildContext context, Widget? home) {
        ThemeBase theme = getTheme();

        return DefaultTextStyle(
          style: const TextStyle(
            color: Colors.white60,
            fontSize: 16,
          ),
          child: ColoredBox(
            color: theme.backgroundColor,
            child: home,
          ),
        );
      },
    );
  }
}

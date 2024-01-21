import 'package:cosmos_foundation/foundation/hooks/cosmos_app.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/router_base.dart';
import 'package:tws_main/core/theme/theme_base.dart';
import 'package:tws_main/core/theme/dark_theme.dart';

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
      defaultTheme: const DarkTheme(),
      routerConfig: RouterBase(),
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

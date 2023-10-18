import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/widgets/hooks/cosmos_app.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/routing.dart';
import 'package:tws_main/config/theme/dark_theme.dart';
import 'package:tws_main/config/theme/light_theme.dart';
import 'package:tws_main/config/theme/theme_base.dart';

void main() {
  runApp(
    const MainApp(),
  );
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return CosmosApp<ThemeBase>.router(
      defaultTheme: const DarkTheme(),
      routerConfig: Routing(),
      listenFrameSize: true,
      generalBuilder: (BuildContext context, Widget? home) {
        ThemeBase theme = getTheme();

        return DefaultTextStyle(
          style: const TextStyle(
            decoration: TextDecoration.none,
          ),
          child: ColoredBox(
            color: theme.primaryColor,
            child: home,
          ),
        );
      },
    );
  }
}

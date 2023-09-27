import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/routing.dart';
import 'package:tws_main/widgets/themed_widget.dart';

void main() {
  runApp(
    MainApp(),
  );
}

class MainApp extends StatelessWidget {
  MainApp({super.key});

  final ValueListenable themeHandler = ValueNotifier(ThemeMode.dark);

  @override
  Widget build(BuildContext context) {
    return ValueListenableBuilder(
      valueListenable: themeHandler,
      builder: (context, value, child) {
        return MaterialApp.router(
          theme: ThemeData.light(),
          darkTheme: ThemeData.dark(),
          themeMode: value,
          routerConfig: routingConfig,
          builder: (context, child) {
            if (child == null) return Container();

            return ThemedWidget(
              builder: (ctx, theme) {
                return ColoredBox(
                  color: theme.colorScheme.background,
                  child: child,
                );
              },
            );
          },
        );
      },
    );
  }
}

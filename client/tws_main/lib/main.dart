import 'package:cosmos_foundation/widgets/hooks/themed_widget.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/routing.dart';

void main() {
  runApp(
    MainApp(),
  );
}

class MainApp extends StatelessWidget {
  MainApp({super.key});

  final ValueListenable<ThemeMode> themeHandler = ValueNotifier<ThemeMode>(ThemeMode.dark);

  @override
  Widget build(BuildContext context) {
    return ValueListenableBuilder<ThemeMode>(
      valueListenable: themeHandler,
      builder: (BuildContext context, ThemeMode value, Widget? child) {
        return MaterialApp.router(
          theme: ThemeData.light(
            useMaterial3: true,
          ),
          darkTheme: ThemeData.dark(
            useMaterial3: true,
          ),
          themeMode: value,
          routerConfig: Routing(),
          builder: (BuildContext context, Widget? child) {
            if (child == null) return Container();

            return ThemedWidget(
              builder: (BuildContext ctx, ThemeData theme) {
                return DefaultTextStyle(
                  style: theme.textTheme.labelLarge ?? const TextStyle(),
                  child: ColoredBox(
                    color: theme.colorScheme.background,
                    child: child,
                  ),
                );
              },
            );
          },
        );
      },
    );
  }
}

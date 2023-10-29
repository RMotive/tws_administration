import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/widgets/hooks/cosmos_app.dart';
import 'package:cosmos_foundation/widgets/hooks/future_widget.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/routing.dart';
import 'package:tws_main/config/theme/dark_theme.dart';
import 'package:tws_main/config/theme/theme_base.dart';

void main() {
  runApp(
    const MainApp(),
  );
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  Future<ThemeBase> storeThemedCatcher() async {
    return (await getThemeFromStore<ThemeBase>('theme-no-user')) ?? const DarkTheme();
  }

  @override
  Widget build(BuildContext context) {
    return Directionality(
      textDirection: TextDirection.ltr,
      child: FutureWidget<ThemeBase>(
        future: storeThemedCatcher(),
        emptyAsError: false,
        successBuilder: (BuildContext context, BoxConstraints constraints, ThemeBase themeBase) {

          return CosmosApp<ThemeBase>.router(
            defaultTheme: themeBase,
            routerConfig: Routing(),
            listenFrameSize: true,
            generalBuilder: (BuildContext context, Widget? home) {
              ThemeBase theme = getTheme();

              return Title(
                color: Colors.white,
                title: 'TWS Admin Services',
                child: DefaultTextStyle(
                  style: const TextStyle(
                    decoration: TextDecoration.none,
                  ),
                  child: ColoredBox(
                    color: theme.primaryColor.mainColor,
                    child: home,
                  ),
                ),
              );
            },
          );
        },
      ),
    );
  }
}

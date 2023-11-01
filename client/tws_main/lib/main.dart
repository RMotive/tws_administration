import 'package:cosmos_foundation/helpers/advisor.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/widgets/hooks/cosmos_app.dart';
import 'package:cosmos_foundation/widgets/hooks/future_widget.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/routes/routing.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/constants/theme_constants.dart';

// --> Helpers
Advisor _advisor = Advisor.instance;

void main() {
  runApp(
    const MainApp(),
  );
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  Future<ThemeBase> storeThemedCatcher() async {
    ThemeBase? storedTheme = await getThemeFromStore(
      themeNoUserStoreKey,
      forcedThemes: themeCollection,
    );
    if (storedTheme != null) return storedTheme;

    ThemeBase themeBase = themeCollection
        .where(
          (ThemeBase element) => element.themeIdentifier == defaultThemeIdentifier,
        )
        .first;
    _advisor.adviseSuccess('Stored theme gathered (${themeBase.runtimeType})');
    return themeBase;
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
            themes: themeCollection,
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

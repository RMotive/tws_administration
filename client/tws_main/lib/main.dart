import 'package:cosmos_foundation/foundation/hooks/cosmos_app.dart';
import 'package:cosmos_foundation/foundation/hooks/future_widget.dart';
import 'package:cosmos_foundation/helpers/advisor.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:flutter_web_plugins/url_strategy.dart';
import 'package:tws_main/config/routes/routing.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/config/tws_url_strategy.dart';
import 'package:tws_main/constants/theme_constants.dart';

// --> Helpers
Advisor _advisor = Advisor.instance;

void main() {
  setUrlStrategy(TWSUrlStrategy());
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
        successBuilder: (BuildContext context, ThemeBase themeBase) {
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
                  style: TextStyle(
                    decoration: TextDecoration.none,
                    color: theme.primaryColor.textColor
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

import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:cosmos_foundation/foundation/hooks/cosmos_app.dart';
import 'package:cosmos_foundation/foundation/hooks/future_widget.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:flutter_web_plugins/url_strategy.dart';
import 'package:tws_main/business/services/client/theme_client_service.dart';
import 'package:tws_main/constants/config/routes/tws_routing.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';
import 'package:tws_main/constants/config/tws_url_strategy.dart';
import 'package:tws_main/constants/theme_constants.dart';

/// --> Services
final ThemeClientService _themeSVC = ThemeClientService.i;

/// This method represents the initial renderization and building for the
/// application since the framework is instructed to generate all of it.
///
/// Here we can set pre-renderization instruction and another complex
/// pre-render validations and complex behavior listeners.
void main() {
  /// Setting a custom url strategy to handle over a better way
  /// the stack remotion of the access page after a success
  /// credential validation.
  setUrlStrategy(TWSUrlStrategy());
  runApp(
    const TWSAdminApp(),
  );
}

/// Represents the initial application configuration.
class TWSAdminApp extends StatelessWidget {
  const TWSAdminApp({super.key});

  @override
  Widget build(BuildContext context) {
    return Directionality(
      textDirection: TextDirection.ltr,
      child: FutureWidget<TWSThemeBase>(
        future: _themeSVC.fetchCurrentTheme(),
        successBuilder: (BuildContext context, TWSThemeBase themeBase) {
          return CosmosApp<TWSThemeBase>.router(
            defaultTheme: themeBase,
            routerConfig: TWSRouting(),
            themes: themeCollection,
            listenFrameSize: true,
            generalBuilder: (BuildContext context, Widget? home) {
              TWSThemeBase theme = getTheme();

              return Title(
                color: Colors.white,
                title: 'TWS Admin Services',
                child: DefaultTextStyle(
                  style: TextStyle(decoration: TextDecoration.none, color: theme.primaryColor.textColor),
                  child: AnimatedContainer(
                    duration: 300.miliseconds,
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

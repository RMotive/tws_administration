import 'package:tws_main/config/theme/dark_theme.dart';
import 'package:tws_main/config/theme/light_theme.dart';
import 'package:tws_main/config/theme/theme_base.dart';

const String themeNoUserStoreKey = 'no-user-theme-storing';

const String defaultThemeIdentifier = lightThemeIdentifier;
const String darkThemeIdentifier = 'classic-dark-theme';
const String lightThemeIdentifier = 'classic-light-theme';
const List<ThemeBase> themeCollection = <ThemeBase>[
  DarkTheme(),
  LightTheme(),
];

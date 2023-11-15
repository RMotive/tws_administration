import 'package:tws_main/constants/config/theme/dark_theme.dart';
import 'package:tws_main/constants/config/theme/light_theme.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';

const String themeClientReference = 'no-user-theme-storing';

const String defaultThemeIdentifier = lightThemeIdentifier;
const String darkThemeIdentifier = 'classic-dark-theme';
const String lightThemeIdentifier = 'classic-light-theme';
const List<TWSThemeBase> themeCollection = <TWSThemeBase>[
  TWSDarkTheme(),
  TWSLightTheme(),
];

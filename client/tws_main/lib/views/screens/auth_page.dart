import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/contracts/cosmos_screen.dart';
import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/widgets/simplifiers/separator_row.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/routes/routes.dart';
import 'package:tws_main/config/theme/light_theme.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/constants/theme_constants.dart';
import 'package:tws_main/views/widgets/tws_button.dart';
import 'package:tws_main/views/widgets/tws_textfield.dart';

class AuthPage extends CosmosPage {
  const AuthPage({super.key})
      : super(
          accessRoute,
        );

  @override
  Widget build(BuildContext context) => const _AuthPageState();
}

class _AuthPageState extends StatefulWidget {
  const _AuthPageState();

  @override
  State<_AuthPageState> createState() => _AuthPageStateState();
}

class _AuthPageStateState extends State<_AuthPageState> {
  // --> Init resources
  late final TextEditingController identityCtrl;
  late final TextEditingController securityCtrl;

  // --> State
  late String? identityError;
  late String? passwordError;
  late bool tipEnabled;
  late ThemeBase theme;

  @override
  void initState() {
    super.initState();
    theme = getTheme();
    identityCtrl = TextEditingController();
    securityCtrl = TextEditingController();
    identityError = null;
    passwordError = null;
    tipEnabled = false;
    listenTheme.addListener(
      () {
        if (mounted) setState(() => theme = getTheme());
      },
    );
  }

  @override
  void didUpdateWidget(covariant _AuthPageState oldWidget) {
    super.didUpdateWidget(oldWidget);
  }

  void checkAccess(String identity, String password) {
    if (identity.isEmpty) setState(() => identityError = 'Cannot be empty');
    if (password.isEmpty) setState(() => passwordError = 'Cannot be empty');
    if (identity.isEmpty || password.isEmpty) return;
    if (!(identity == 'twtms-admin' && password == 'tws2023&')) return;
  }

  String ifToolTip(String tip) {
    if (!tipEnabled) return '';
    return tip;
  }

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: <Widget>[
        Align(
          alignment: Alignment.topRight,
          child: _OptionsRibbon(
            onTipChange: (bool isEnable) {
              setState(() {
                tipEnabled = isEnable;
              });
            },
          ),
        ),
        Center(
          child: AutofillGroup(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: <Widget>[
                // --> Welcome title
                Padding(
                  padding: const EdgeInsets.only(
                    bottom: 20,
                  ),
                  child: Text(
                    "TWS Admin services",
                    style: TextStyle(
                      color: theme.primaryColor.textColor,
                      fontWeight: FontWeight.w600,
                      fontSize: 22,
                    ),
                  ),
                ),
                // --> Identity text input
                TWSTextField(
                  onWritting: () {
                    if (identityError != null) {
                      setState(() {
                        identityError = null;
                      });
                    }
                  },
                  autofillHints: const <String>[
                    AutofillHints.username,
                  ],
                  padding: const EdgeInsets.only(
                    top: 12,
                  ),
                  toolTip: ifToolTip("Input your user identifier to validate your identity"),
                  editorController: identityCtrl,
                  errorHint: identityError,
                  textfieldLabel: 'Identity',
                ),
                // --> Password text input
                TWSTextField(
                  onWritting: () {
                    if (passwordError != null) {
                      setState(() {
                        passwordError = null;
                      });
                    }
                  },
                  autofillHints: const <String>[
                    AutofillHints.password,
                  ],
                  padding: const EdgeInsets.only(
                    top: 12,
                  ),
                  toolTip: ifToolTip("Input your security password to validate your identity"),
                  errorHint: passwordError,
                  editorController: securityCtrl,
                  textfieldLabel: 'Password',
                  isSecret: true,
                ),
                // --> Access action button
                TWSButton(
                  padding: const EdgeInsets.only(
                    top: 12,
                  ),
                  toolTip: ifToolTip('Tap to access into the platform after identity validation'),
                  buttonSize: const Size(80, 35),
                  action: () => checkAccess(identityCtrl.text, securityCtrl.text),
                ),
              ],
            ),
          ),
        ),
      ],
    );
  }
}

class _OptionsRibbon extends StatefulWidget {
  final void Function(bool isEnable) onTipChange;
  const _OptionsRibbon({
    required this.onTipChange,
  });

  @override
  State<_OptionsRibbon> createState() => _OptionsRibbonState();
}

class _OptionsRibbonState extends State<_OptionsRibbon> {
  // --> State
  late bool currentIsLight;
  late bool tipEnabled;

  @override
  void initState() {
    super.initState();
    currentIsLight = getTheme().runtimeType == LightTheme;
    tipEnabled = false;
  }

  void switchThemeOption() {
    setState(() {
      currentIsLight = !currentIsLight;
    });
    updateTheme(
      !currentIsLight ? darkThemeIdentifier : lightThemeIdentifier,
      saveLocalKey: themeNoUserStoreKey,
    );
  }

  void switchTipOption() async {
    /// --> Forcing the await for the Fade Out tooltip animation duration.
    /// This prevents when the user taps fastly the tooltip enable/disable button
    /// doesn't got messed up showing a half-faded grey tooltip.
    await Future<void>.delayed(150.miliseconds);
    if (mounted) setState(() => tipEnabled = !tipEnabled);
  }

  String onTip(String tip) {
    if (tipEnabled) return tip;
    return '';
  }

  @override
  Widget build(BuildContext context) {
    return FittedBox(
      child: Padding(
        padding: const EdgeInsets.all(12.0),
        child: SeparatorRow(
          spacing: 12,
          children: <Widget>[
            // --> Tooltips toogle
            _ToogleRoundedButton(
              icon: !currentIsLight ? Icons.light_mode : Icons.dark_mode,
              onFire: switchThemeOption,
              toolTip: onTip('Tap to enable ${currentIsLight ? 'dark' : 'light'} theme mode'),
            ),
            // --> Theming toogle.
            _ToogleRoundedButton(
              icon: Icons.disabled_visible,
              onFire: switchTipOption,
              toolTip: onTip('Tap to disable the tooltips'),
            ),
          ],
        ),
      ),
    );
  }
}

class _ToogleRoundedButton extends StatelessWidget {
  final double _controlReferenceSize;
  final IconData icon;
  final void Function() onFire;
  final String toolTip;

  const _ToogleRoundedButton({
    required this.onFire,
    required this.icon,
    this.toolTip = '',
  }) : _controlReferenceSize = 45;

  @override
  Widget build(BuildContext context) {
    ThemeBase theme = getTheme();

    return Tooltip(
      message: toolTip,
      child: MouseRegion(
        cursor: SystemMouseCursors.click,
        child: GestureDetector(
          onTap: onFire,
          child: ClipRRect(
            borderRadius: BorderRadius.circular(_controlReferenceSize),
            clipBehavior: Clip.hardEdge,
            child: ColoredBox(
              color: theme.onPrimaryColorFirstControlColor.mainColor,
              child: SizedBox.square(
                dimension: _controlReferenceSize,
                child: Icon(
                  icon,
                  color: theme.onPrimaryColorFirstControlColor.textColor,
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}

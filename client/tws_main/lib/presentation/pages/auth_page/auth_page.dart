import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:cosmos_foundation/foundation/simplifiers/separator_row.dart';
import 'package:cosmos_foundation/helpers/route_driver.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/routes/routes.dart';
import 'package:tws_main/config/theme/light_theme.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/constants/theme_constants.dart';
import 'package:tws_main/presentation/widgets/tws_button.dart';
import 'package:tws_main/presentation/widgets/tws_textfield.dart';
import 'package:tws_main/presentation/widgets/tws_toogle_rounded_button.dart';

part 'widgets/options_ribbon.dart';

// --> Helpers
final RouteDriver _router = RouteDriver.i;

/// UI Page for Auth functionallity.
/// This UI Page shows the auth functionallity view, where the user can firm his credentials.
class AuthPage extends CosmosPage {
  /// New instance of [AuthPage] UI Page configuration.
  const AuthPage({super.key});

  @override
  Widget build(BuildContext context) => const _AuthPageState();
}

// --> Here is handled the UI Page state control.

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

    // --> All validations went succesful. (redirecting to the Main Screen wrapper)
    _router.driveTo(dashboardRoute);
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

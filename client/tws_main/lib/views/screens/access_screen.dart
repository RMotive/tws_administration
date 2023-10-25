import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/views/widgets/tws_button.dart';
import 'package:tws_main/views/widgets/tws_textfield.dart';

class AccessScreen extends StatefulWidget {
  const AccessScreen({super.key});

  @override
  State<AccessScreen> createState() => _AccessScreenState();
}

class _AccessScreenState extends State<AccessScreen> {
  // --> Init resources
  late final ThemeBase theme;
  late final TextEditingController identityCtrl;
  late final TextEditingController securityCtrl;

  // --> State variables
  String? identityError;
  String? passwordError;

  @override
  void initState() {
    super.initState();
    theme = getTheme();
    identityCtrl = TextEditingController();
    securityCtrl = TextEditingController();
  }

  Future<bool> checkHealth() async {
    return true;
  }

  void checkAccess(String identity, String password) {
    if (identity.isEmpty) setState(() => identityError = 'Cannot be empty');
    if (password.isEmpty) setState(() => passwordError = 'Cannot be empty');
  }

  @override
  Widget build(BuildContext context) {
    return AutofillGroup(
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
            buttonSize: const Size(80, 35),
            action: () => checkAccess(identityCtrl.text, securityCtrl.text),
          ),
        ],
      ),
    );
  }
}

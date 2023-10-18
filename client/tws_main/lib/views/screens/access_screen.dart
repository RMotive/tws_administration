import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/widgets/hooks/future_widget.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/views/widgets/tws_button.dart';
import 'package:tws_main/views/widgets/tws_textfield.dart';

class AccessScreen extends StatelessWidget {
  const AccessScreen({super.key});

  Future<bool> checkHealth() async {
    return true;
  }

  void checkAccess(String identity, String password) {
    debugPrint('$identity $password');
  }

  @override
  Widget build(BuildContext context) {
    // --> Retrieving the current theme info
    final ThemeBase theme = getTheme();
    // --> Creating the text controllers
    final TextEditingController identityCtrl = TextEditingController();
    final TextEditingController securityCtrl = TextEditingController();

    return FutureWidget<bool>(
      future: checkHealth(),
      successBuilder: (BuildContext ctx, BoxConstraints boxSurface, bool data) {
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
                    color: theme.onPrimaryColorFirstTextColor,
                    fontWeight: FontWeight.w600,
                    fontSize: 22,
                  ),
                ),
              ),
              // --> Identity text input
              TWSTextField(
                autofillHints: const <String>[
                  AutofillHints.username,
                ],
                padding: const EdgeInsets.only(
                  top: 12,
                ),
                editorController: identityCtrl,
                textfieldLabel: 'Identity',
              ),
              // --> Password text input
              TWSTextField(
                autofillHints: const <String>[
                  AutofillHints.password,
                ],
                padding: const EdgeInsets.only(
                  top: 12,
                ),
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
      },
    );
  }
}

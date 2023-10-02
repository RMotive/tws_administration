import 'package:cosmos_foundation/widgets/hooks/future_widget.dart';
import 'package:cosmos_foundation/widgets/hooks/themed_widget.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/views/widgets/tws_button.dart';
import 'package:tws_main/views/widgets/tws_textfield.dart';

class AccessScreen extends StatelessWidget {
  const AccessScreen({super.key});

  Future<bool> checkHealth() async {
    return true;
  }

  void checkAccess() {}

  @override
  Widget build(BuildContext context) {
    return FutureWidget<bool>(
      future: checkHealth(),
      successBuilder: (BuildContext ctx, BoxConstraints boxSurface, bool data) {
        return ThemedWidget(
          builder: (BuildContext context, ThemeData theme) {
            return AutofillGroup(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: <Widget>[
                  // --> Welcome title
                  Text(
                    "Welcome to TWS Admin services!",
                    style: theme.textTheme.titleMedium,
                  ),
                  // --> Identity text input
                  const TWSTextField(
                    autofillHints: <String>[
                      AutofillHints.username,
                    ],
                    textfieldLabel: 'Identity',
                    padding: EdgeInsets.only(
                      top: 12,
                    ),
                  ),
                  // --> Password text input
                  const TWSTextField(
                    autofillHints: <String>[
                      AutofillHints.password,
                    ],
                    textfieldLabel: 'Password',
                    isSecret: true,
                    padding: EdgeInsets.only(
                      top: 12,
                    ),
                  ),
                  // --> Access action button
                  TWSButton(
                    padding: const EdgeInsets.only(
                      top: 12,
                    ),
                    buttonSize: const Size(80, 35),
                    action: checkAccess,
                  ),
                ],
              ),
            );
          },
        );
      },
    );
  }
}

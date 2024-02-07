import 'dart:typed_data';

import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:cosmos_foundation/foundation/services/service_result.dart';
import 'package:cosmos_foundation/foundation/simplifiers/separator_column.dart';
import 'package:cosmos_foundation/helpers/focuser.dart';
import 'package:tws_main/domain/repositories/repositories.dart';
import 'package:tws_main/domain/repositories/tws_administration/contracts/twsa_security_service_base.dart';
import 'package:tws_main/domain/repositories/tws_administration/models/account_identity_model.dart';
import 'package:tws_main/domain/repositories/tws_administration/models/foreign_session_model.dart';
import 'package:tws_main/presentation/components/tws_button_flat.dart';
import 'package:tws_main/presentation/components/tws_display_flat.dart';
import 'package:tws_main/presentation/components/tws_input_text.dart';
import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/foundation/simplifiers/colored_sizedbox.dart';
import 'package:flutter/material.dart';

part './business_decorator.dart';
part './login_form.dart';

class LoginPage extends CosmosPage {
  const LoginPage({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    const double rowSize = 400;
    const double separatorHeight = rowSize * .55;

    return const Row(
      children: <Widget>[
        Expanded(
          child: _BusinessDecorator(),
        ),
        CosmosColorBox(
          size: Size(1.5, separatorHeight),
          color: Colors.grey,
        ),
        Expanded(
          child: _LoginForm(),
        ),
      ],
    );
  }
}

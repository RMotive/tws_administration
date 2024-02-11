import 'dart:typed_data';

import 'package:cosmos_foundation/alias/aliases.dart';
import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:cosmos_foundation/foundation/services/service_result.dart';
import 'package:cosmos_foundation/foundation/simplifiers/separator_column.dart';
import 'package:cosmos_foundation/helpers/focuser.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:tws_main/core/theme/theme_base.dart';
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
    const double maxWidthAllowedFullView = 740;
    const double separatorDecoratorWidth = 1.5;
    const double itemSeparation = 20;
    const double rowSize = 400;
    const double separatorHeight = rowSize * .55;
    const double offsetTransaltionAboveCenterForm = 100;
    const double maxHeightAllowedToTranslateForm = 850;
    final Size screenSize = MediaQuery.sizeOf(ctx);
    final double screenWidth = screenSize.width;

    final bool isFullView = screenWidth >= maxWidthAllowedFullView;
    final double translation = screenSize.height <= maxHeightAllowedToTranslateForm ? 0 : -offsetTransaltionAboveCenterForm;

    return Padding(
      padding: const EdgeInsets.all(16),
      child: TweenAnimationBuilder<double>(
        tween: Tween<double>(
          begin: 0,
          end: translation,
        ),
        duration: 600.miliseconds,
        builder: (BuildContext context, double value, Widget? child) {
          return Transform.translate(
            offset: Offset(0, value),
            child: Center(
              child: SizedBox(
                width: screenWidth,
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 16,
                  ),
                  child: Wrap(
                    runSpacing: itemSeparation * 2,
                    alignment: isFullView ? WrapAlignment.spaceEvenly : WrapAlignment.center,
                    crossAxisAlignment: WrapCrossAlignment.center,
                    children: <Widget>[
                      Padding(
                        padding: EdgeInsets.symmetric(
                          horizontal: isFullView ? 0 : (itemSeparation + separatorDecoratorWidth),
                        ),
                        child: const FittedBox(
                          child: _BusinessDecorator(),
                        ),
                      ),
                      Visibility(
                        visible: isFullView,
                        child: const CosmosColorBox(
                          size: Size(separatorDecoratorWidth, separatorHeight),
                          color: Colors.grey,
                        ),
                      ),
                      Padding(
                        padding: EdgeInsets.symmetric(
                          horizontal: isFullView ? 0 : (itemSeparation + separatorDecoratorWidth),
                        ),
                        child: const _LoginForm(),
                      ),
                    ],
                  ),
                ),
              ),
            ),
          );
        },
      ),
    );
  }
}

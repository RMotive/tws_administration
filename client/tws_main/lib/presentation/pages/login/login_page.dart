import 'dart:typed_data';

import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:cosmos_foundation/extensions/string_extension.dart';
import 'package:cosmos_foundation/foundation/simplifiers/colored_sizedbox.dart';
import 'package:cosmos_foundation/foundation/simplifiers/spacing_column.dart';
import 'package:cosmos_foundation/helpers/route_driver.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/structs/control_controller.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/k_common_displays.dart';
import 'package:tws_main/core/router/k_routes.dart';
import 'package:tws_main/core/theme/theme_base.dart';
import 'package:tws_main/domain/repositories/repositories.dart';
import 'package:tws_main/domain/repositories/tws_administration/contracts/twsa_security_service_base.dart';
import 'package:tws_main/domain/repositories/tws_administration/services/inputs/init_session_input.dart';
import 'package:tws_main/domain/repositories/tws_administration/services/outputs/init_session_output.dart';
import 'package:tws_main/domain/repositories/tws_administration/structures/situation.dart';
import 'package:tws_main/domain/repositories/tws_administration/templates/twsa_failure.dart';
import 'package:tws_main/domain/repositories/tws_administration/templates/twsa_template.dart';
import 'package:tws_main/domain/resolvers/twsa_resolver.dart';
import 'package:tws_main/domain/storage/session_storage.dart';
import 'package:tws_main/domain/storage/structures/session.dart';
import 'package:tws_main/presentation/components/tws_button_flat.dart';
import 'package:tws_main/presentation/components/tws_display_flat.dart';
import 'package:tws_main/presentation/components/tws_input_text.dart';

part 'business_decorator.dart';
part 'login_form/login_form.dart';
part 'login_form/login_form_state.dart';

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
                      // --> Business decorator.
                      Padding(
                        padding: EdgeInsets.symmetric(
                          horizontal: isFullView ? 0 : (itemSeparation + separatorDecoratorWidth),
                        ),
                        child: const FittedBox(
                          child: _BusinessDecorator(),
                        ),
                      ),
                      // --> Separator bar.
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

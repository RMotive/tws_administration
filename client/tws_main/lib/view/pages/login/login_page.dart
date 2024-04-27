
import 'package:cosmos_foundation/csm_foundation.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/twsa_common_displays.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_button_flat.dart';
import 'package:tws_main/view/widgets/tws_display_flat.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';

part 'business_decorator.dart';
part 'login_form/login_form.dart';
part 'login_form/login_form_state.dart';

class LoginPage extends CSMPageBase {
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
                        child: const CSMColorBox(
                          size: Size(separatorDecoratorWidth, separatorHeight),
                          background: Colors.grey,
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

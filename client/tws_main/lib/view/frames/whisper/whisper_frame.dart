import 'dart:ui';

import 'package:cosmos_foundation/common/common_module.dart';
import 'package:cosmos_foundation/common/tools/csm_responsive.dart';
import 'package:cosmos_foundation/router/csm_router.dart';
import 'package:cosmos_foundation/theme/theme_module.dart';
import 'package:cosmos_foundation/widgets/csm_spacing_row.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/frames/whisper/options/whisper_frame_action_options.dart';
import 'package:tws_main/view/widgets/tws_button_flat.dart';

part 'whisper_header.dart';
part 'whisper_footer.dart';

typedef _ActionOptions = WhisperFrameActionOptions;

class WhisperFrame extends StatelessWidget {
  final String title;
  final Widget child;
  final bool trigger;
  final bool closer;
  final VoidCallback? onClose;
  final List<WhisperFrameActionOptions> actions;

  const WhisperFrame({
    super.key,
    this.onClose,
    required this.title,
    required this.child,
    this.closer = true,
    this.trigger = true,
    this.actions = const <_ActionOptions>[],
  });

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions pageTheme = getTheme<TWSAThemeBase>().pageColorStruct;

    return BackdropFilter(
      filter: ImageFilter.blur(
        sigmaX: 5,
        sigmaY: 5,
      ),
      child: Padding(
        padding: EdgeInsets.all(
          CSMResponsive.clampRatio(
            MediaQuery.sizeOf(context).width,
            const CSMClampRatioOptions(
              minValue: 12,
              minBreak: 500,
              maxValue: 25,
              maxBreak: 1200,
            ),
          ),
        ),
        child: ColoredBox(
          color: pageTheme.main,
          child: LayoutBuilder(
            builder: (_, BoxConstraints constrains) {
              return ConstrainedBox(
                constraints: constrains.tighten(
                  height: constrains.minHeight,
                ),
                child: Column(
                  children: <Widget>[
                    // --> Whisper header
                    Expanded(
                      child: Column(
                        children: <Widget>[
                          _WhisperHeader(
                            title: title,
                            pageTheme: pageTheme,
                          ),
                        ],
                      ),
                    ),
                    // --> Whisper content

                    // --> Whisper footer
                    if (actions.isNotEmpty || closer || !trigger)
                      _WhisperFooter(
                        closer: closer,
                        trigger: trigger,
                        onClose: onClose,
                        pageTheme: pageTheme,
                      )
                  ],
                ),
              );
            },
          ),
        ),
      ),
    );
  }
}

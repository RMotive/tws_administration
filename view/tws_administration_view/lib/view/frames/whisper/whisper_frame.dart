import 'dart:ui';

import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_administration_view/view/components/tws_button_flat.dart';
import 'package:tws_administration_view/view/frames/whisper/options/whisper_frame_action_options.dart';

part 'whisper_footer.dart';
part 'whisper_header.dart';

typedef _ActionOptions = WhisperFrameActionOptions;

class WhisperFrame extends StatelessWidget {
  final String title;
  final Widget child;
  final void Function()? trigger;
  final bool closer;
  final VoidCallback? onClose;
  final List<WhisperFrameActionOptions> actions;

  const WhisperFrame({
    super.key,
    this.onClose,
    this.trigger,
    required this.title,
    required this.child,
    this.closer = true,
    this.actions = const <_ActionOptions>[],
  });

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions pageTheme = getTheme<TWSAThemeBase>().page;

    return ClipPath(
      clipBehavior: Clip.antiAlias,
      child: BackdropFilter(
        filter: ImageFilter.blur(
          sigmaX: 5,
          sigmaY: 5,
        ),
        child: Padding(
          padding: EdgeInsets.all(
            CSMResponsive.clampRatio(
              MediaQuery.sizeOf(context).width,
              const CSMClampRatioOptions(
                minValue: 10,
                minBreak: 400,
                maxValue: 20,
                maxBreak: 1000,
              ),
            ),
          ),
          child: ColoredBox(
            color: pageTheme.main,
            child: LayoutBuilder(
              builder: (_, BoxConstraints constrains) {
                BoxConstraints frameCts = constrains.tighten(
                  height: constrains.minHeight,
                );
                return ConstrainedBox(
                  constraints: frameCts,
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
                            // --> Whisper content
                            Expanded(child: child),
                          ],
                        ),
                      ),
                      // --> Whisper footer
                      if (actions.isNotEmpty || closer || (trigger != null))
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
      ),
    );
  }
}

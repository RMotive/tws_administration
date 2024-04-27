import 'package:cosmos_foundation/csm_foundation.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';

part 'features_create_whisper_state.dart';

typedef _State = _FeaturesCreateWhisperState;

class FeaturesCreateWhisper extends CSMPageBase {
  const FeaturesCreateWhisper({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return CSMDynamicWidget<_State>(
      state: _State(),
      designer: (BuildContext ctx, CSMStateBase state) {
        return const WhisperFrame(
          title: 'Feature creation',
          child: CSMForm(
            options: CSMFormOptions(
              name: 'feature-creation',
            ),
            child: SizedBox(
              width: double.maxFinite,
              child: Padding(
                padding: EdgeInsets.symmetric(
                  vertical: 16,
                  horizontal: 32,
                ),
                child: Wrap(
                  children: <Widget>[
                    /// --> Input for [Feature Name]
                    TWSInputText(
                      label: 'Name',
                      hint: 'Identification name for the feature',
                      width: 325,
                    ),
                  ],
                ),
              ),
            ),
          ),
        );
      },
    );
  }
}

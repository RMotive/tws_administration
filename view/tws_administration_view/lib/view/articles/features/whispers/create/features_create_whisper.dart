import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/view/frames/whisper/whisper_frame.dart';
import 'package:tws_administration_view/view/widgets/tws_input_text.dart';

part 'features_create_whisper_state.dart';

typedef _State = _FeaturesCreateWhisperState;

class FeaturesCreateWhisper extends PageB {
  const FeaturesCreateWhisper({super.key});

  @override
  Widget compose(BuildContext ctx, Size windowSize, Size pageSize) {
    return ReactiveWidget<_State>(
      reactor: _State(),
      builder: (BuildContext ctx, _State state) {
        return WhisperFrame(
          title: 'Feature creation',
          child: FormWidget(
            controller: FormWidgetController(
              name: 'feature-creation',
            ),
            child: const SizedBox(
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

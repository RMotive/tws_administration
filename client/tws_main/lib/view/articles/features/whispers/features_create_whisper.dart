import 'package:cosmos_foundation/router/router_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';

class FeaturesCreateWhisper extends CSMPageBase {
  const FeaturesCreateWhisper({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return const WhisperFrame(
      title: 'Feature creation',
      child: Column(),
    );
  }
}

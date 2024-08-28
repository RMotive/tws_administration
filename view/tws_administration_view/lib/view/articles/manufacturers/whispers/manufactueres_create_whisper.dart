import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/view/frames/whisper/whisper_frame.dart';

class ManufacturersCreateWhisper extends CSMPageBase{
  const ManufacturersCreateWhisper({super.key});

  @override 
  Widget compose(BuildContext ctx, Size window){

    return WhisperFrame(
      title: 'Create manufacturers',
      child: const SizedBox(),
      trigger: () {
      },
    );
  }
}
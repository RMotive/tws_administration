import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';


class TrucksCreateWhisper extends CSMPageBase{
  const TrucksCreateWhisper({super.key});

  @override 
  Widget compose(BuildContext ctx, Size window){
    
    return WhisperFrame(
      title: 'Create trucks',
      trigger: () {
      },
      child:  Container(
        color: Colors.red,
      )
     
    );
  }
}
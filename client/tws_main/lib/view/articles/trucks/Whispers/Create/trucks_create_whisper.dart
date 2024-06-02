import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_creation.dart';

class TrucksCreateWhisper extends CSMPageBase{
  const TrucksCreateWhisper({super.key});

  @override 
  Widget compose(BuildContext ctx, Size window) {
    return WhisperFrame(
      title: 'Create trucks',
      child: TWSArticleCreator<Solution>(
        displayDesigner: (Solution context) {
          return const SizedBox();
        },
      ),
    );
  }
}
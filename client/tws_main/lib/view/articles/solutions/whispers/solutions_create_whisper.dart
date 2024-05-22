import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_creation.dart';

final class SolutionsCreateWhisper extends CSMPageBase {
  const SolutionsCreateWhisper({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return WhisperFrame(
      title: 'Create solutions',
      trigger: () {},
      child: const TWSArticleCreator<Solution>(),
    );
  }
}

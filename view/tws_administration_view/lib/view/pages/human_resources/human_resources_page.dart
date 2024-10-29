import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/view/frames/introduction/introduction_frame.dart';
import 'package:tws_administration_view/view/pages/human_resources/human_resources_frame.dart';

final class HumanResourcesPage extends CSMPageBase {
  const HumanResourcesPage({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return IntroductionFrame(
      articles: HumanResourcesFrame.humanResourcesArticles,
    );
  }
}

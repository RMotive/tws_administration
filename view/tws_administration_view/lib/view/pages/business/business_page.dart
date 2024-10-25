import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/view/frames/business/business_frame.dart';
import 'package:tws_administration_view/view/frames/introduction/introduction_frame.dart';

class BusinessPage extends CSMPageBase {
  final CSMRouteOptions currentRoute;

  const BusinessPage({
    super.key,
    required this.currentRoute,
  });

  @override
  Widget compose(BuildContext ctx, Size window) {
    return IntroductionFrame(
      articles: BusinessFrame.businessArticles,
    );
  }
}

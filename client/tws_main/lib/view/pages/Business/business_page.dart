import 'package:cosmos_foundation/router/router_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/view/frames/business/business_frame.dart';
import 'package:tws_main/view/frames/introduction/introduction_frame.dart';

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

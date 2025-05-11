import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/view/frames/introduction/introduction_frame.dart';
import 'package:tws_administration_view/view/pages/security/security_frame.dart';

class SecurityPage extends PageB {
  final RouteData currentRoute;

  const SecurityPage({
    super.key,
    required this.currentRoute,
  });

  @override
  Widget compose(BuildContext ctx, Size windowSize, Size pageSize) {
    return IntroductionFrame(
      articles: SecurityFrame.securityArticles,
    );
  }
}

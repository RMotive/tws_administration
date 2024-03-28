import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/models/options/route_options.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/presentation/frames/introduction/introduction_frame.dart';
import 'package:tws_main/presentation/frames/security/security_frame.dart';

class SecurityPage extends CosmosPage {
  final RouteOptions currentRoute;

  const SecurityPage({
    super.key,
    required this.currentRoute,
  });

  @override
  Widget compose(BuildContext ctx, Size window) {
    return IntroductionFrame(
      articles: SecurityFrame.securityArticles,
    );
  }
}

import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/twsa_routes.dart';

import 'package:tws_main/view/frames/business/business_frame.dart';

class SCTsArticle extends CSMPageBase {
  const SCTsArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return const BusinessFrame(
      currentRoute: TWSARoutes.sctsArticle,
      article: SizedBox(
        child: Text("SCTs view"),

      ),
    );
  }
}

import 'package:cosmos_foundation/router/router_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/view/frames/article/article_frame.dart';
import 'package:tws_main/view/frames/article/article_options.dart';

class IntroductionFrame extends StatelessWidget {
  final List<ArticleOptions> articles;
  final Widget? content;
  const IntroductionFrame({
    super.key,
    this.content,
    required this.articles,
  });

  @override
  Widget build(BuildContext context) {
    return ArticleFrame(
      articlesOptions: articles,
      currentRoute: const CSMRouteOptions(''),
      article: content,
    );
  }
}

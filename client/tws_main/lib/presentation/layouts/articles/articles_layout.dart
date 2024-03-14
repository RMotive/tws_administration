import 'package:cosmos_foundation/enumerators/cosmos_control_states.dart';
import 'package:cosmos_foundation/enumerators/evaluators/cosmos_control_states_evaluator.dart';
import 'package:cosmos_foundation/foundation/simplifiers/colored_sizedbox.dart';
import 'package:cosmos_foundation/foundation/simplifiers/spacing_row.dart';
import 'package:cosmos_foundation/helpers/route_driver.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/options/route_options.dart';
import 'package:cosmos_foundation/models/structs/standard_theme_struct.dart';
import 'package:cosmos_foundation/models/structs/states_control_theme_struct.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/k_assets.dart';
import 'package:tws_main/core/theme/theme_base.dart';
import 'package:tws_main/presentation/layouts/articles/article_options.dart';

part 'selector/articles_layout_selector.dart';
part 'selector/articles_layout_selector_button.dart';

part 'articles_layout_display.dart';

typedef _CStruct = ThemeColorStruct;
typedef _SCStruct = StateControlThemeStruct;

final RouteDriver _routeDriver = RouteDriver.i;

class ArticlesLayout extends StatelessWidget {
  final Widget? article;
  final RouteOptions currentRoute;
  final List<ArticleOptions> articles;

  const ArticlesLayout({
    super.key,
    this.article,
    required this.articles,
    required this.currentRoute,
  });

  /// Calculates the correct height to bound up the current layout.
  double calAvailableHeight(BoxConstraints constrains) {
    double avbHeight = constrains.minHeight;
    if (constrains.hasBoundedHeight) {
      avbHeight = constrains.maxHeight;
    }
    return avbHeight;
  }

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        final double height = calAvailableHeight(constrains);

        return SizedBox(
          height: height,
          child: Padding(
            padding: const EdgeInsets.all(8.0),
            child: Column(
              children: <Widget>[
                /// --> Articles selection ribbon
                _ArticlesSelector(articles, currentRoute),

                /// --> Articles display section.
                Expanded(
                  child: _ArticlesDisplay(article),
                ),
              ],
            ),
          ),
        );
      },
    );
  }
}

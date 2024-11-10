import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/constants/twsa_assets.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_administration_view/view/frames/article/action_ribbon_options.dart';
import 'package:tws_administration_view/view/frames/article/actions/article_frame_actions_options.dart';
import 'package:tws_administration_view/view/frames/article/actions/maintenance_group_options.dart';
import 'package:tws_administration_view/view/frames/article/article_options.dart';
import 'package:tws_administration_view/view/components/tws_frame_decoration.dart';

part 'selector/articles_layout_selector.dart';
part 'selector/articles_layout_selector_button.dart';

part 'actions/article_frame_actions.dart';
part 'actions/article_frame_actions_group.dart';
part 'actions/article_frame_actions_button.dart';

part 'articles_layout_display.dart';

typedef _CStruct = CSMColorThemeOptions;
typedef _SCStruct = CSMStateThemeOptions;
typedef _DrawEval = ({bool bar, bool actions, bool selector});

final CSMRouter _routeDriver = CSMRouter.i;

class ArticleFrame extends StatelessWidget {
  final Widget? article;
  final CSMRouteOptions currentRoute;
  final List<ArticleOptions> articlesOptions;
  final ActionRibbonOptions? actionsOptions;

  const ArticleFrame({
    super.key,
    this.article,
    this.actionsOptions,
    required this.articlesOptions,
    required this.currentRoute,
  });

  /// Calculates the correct height to bound up the current layout.
  double _calAvailableHeight(BoxConstraints constrains) {
    double avbHeight = constrains.minHeight;
    if (constrains.hasBoundedHeight) {
      avbHeight = constrains.maxHeight;
    }
    return avbHeight;
  }

  /// Evaluates if some features should be drawn.
  ///
  /// [$1]: Actions ribbon evaluation
  ///
  /// [$2]: Selector ribbon evaluation
  _DrawEval _evalDraw() {
    _DrawEval initVal = (bar: false, actions: false, selector: false);
    if (actionsOptions != null && actionsOptions!.evalDraw()) {
      initVal = (bar: true, actions: true, selector: initVal.selector);
    }
    if (articlesOptions.isNotEmpty) {
      initVal = (bar: true, actions: initVal.actions, selector: true);
    }

    return initVal;
  }

  @override
  Widget build(BuildContext context) {
    _DrawEval drawEvaluation = _evalDraw();

    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        final double height = _calAvailableHeight(constrains);

        return SizedBox(
          height: height,
          child: Padding(
            padding: const EdgeInsets.all(8.0),
            child: Column(
              children: <Widget>[
                Visibility(
                  visible: drawEvaluation.bar,
                  child: SizedBox(
                    height: 75,
                    child: CSMSpacingRow(
                      spacing: 8,
                      children: <Widget>[
                        if (drawEvaluation.actions)
                          Expanded(
                            child: _ArticleActions(
                              options: actionsOptions as ActionRibbonOptions,
                            ),
                          ),
                        if (drawEvaluation.selector)
                          Expanded(
                            child: _ArticlesSelector(articlesOptions, currentRoute),
                          ),
                      ],
                    ),
                  ),
                ),

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

part of '../article_frame.dart';

/// [Private] component.
///
/// Draws the article selection ribbon, that allows the user to select another article
/// to work on.
class _ArticlesSelector extends StatelessWidget {
  final CSMRouteOptions currentRoute;
  final List<ArticleOptions> articles;

  /// [Private] component.
  ///
  /// Draws the article selection ribbon, that allows the user to select another article
  /// to work on.
  ///
  /// Depends on:
  ///   Theme: [primaryDisableColorStruct]
  ///   - Used when the article selection button doesn't have a specified route options.
  ///     - mainColor: Specifies the disabled button background color.
  ///     -* onColorAlt: Specifies the disabled button foreground color.
  ///   Theme: [articlesLayoutSelectorButtonStruct]
  ///   - Used when the article selection button is enabled to handle its own states.
  ///     - background: uses the background property calculation to draw the button background color;
  ///       if is giving null will use [Colors.blueGrey.shade700]
  ///     -
  const _ArticlesSelector(this.articles, this.currentRoute);

  /// Asserts the disabled control theme color struct to ensure dependencies.
  _CStruct asrDisabledStruct(TWSAThemeBase theme) {
    _CStruct struct = theme.primaryDisabledControl;

    assert(struct.foreAlt != null, 'required theme prop');
    return struct;
  }

  /// Asserts the primary control theme color struct to ensure dependencies.
  _SCStruct asrStruct(TWSAThemeBase theme) {
    _SCStruct struct = theme.articlesLayoutSelectorButtonState;

    return struct;
  }

  /// Looks if the given article contains route options to be enrouted.
  bool hasRoute(ArticleOptions article) {
    return article.route != null;
  }

  @override
  Widget build(BuildContext context) {
    final TWSAThemeBase theme = getTheme();
    final _SCStruct struct = asrStruct(theme);
    final _CStruct disableStruct = asrDisabledStruct(theme);

    return SingleChildScrollView(
      scrollDirection: Axis.horizontal,
      child: CSMSpacingRow(
        spacing: 8,
        children: <Widget>[
          for (ArticleOptions article in articles)
            _ArticlesSelectorButton(
              enabled: hasRoute(article),
              isCurrent: article.route == currentRoute,
              options: article,
              disabledStruct: disableStruct,
              stateStruct: struct,
            ),
        ],
      ),
    );
  }
}

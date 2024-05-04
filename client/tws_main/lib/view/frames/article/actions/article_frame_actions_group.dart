part of '../article_frame.dart';

class _ArticleActionsGroup extends StatelessWidget {
  final String? name;
  final List<ArticleFrameActionsOptions> actions;

  const _ArticleActionsGroup({
    this.name,
    required this.actions,
  }) : assert(actions.length > 0, 'An article frame actions ribbon action group should have at least an action');

  @override
  Widget build(BuildContext context) {
    final CSMStateThemeOptions actionStruct = getTheme<TWSAThemeBase>().articlesLayoutActionButtonState;

    return Column(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: <Widget>[
        Expanded(
          child: LayoutBuilder(
            builder: (_, BoxConstraints constrains) {
              BoxConstraints actionBounds = constrains;
              if (!constrains.hasBoundedHeight) {
                actionBounds = constrains.tighten(height: constrains.minHeight);
              }

              final double actionSize = actionBounds.maxHeight - 4;
              return CSMSpacingRow(
                spacing: 8,
                mainAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  for (ArticleFrameActionsOptions action in actions)
                    _ArticleFrameActionsButton(
                      size: actionSize,
                      options: action,
                      struct: actionStruct,
                    ),
                ],
              );
            },
          ),
        ),

        // --> Just displays the action group name when it's available.
        if (name != null)
          Text(
            name as String,
            style: const TextStyle(
              fontSize: 12,
            ),
          ),
      ],
    );
  }
}

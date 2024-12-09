part of '../tws_article_table.dart';

final class _TWSArticleTableDetails<TArticle extends CSMEncodeInterface> extends StatelessWidget {
  final TWSArticleTableAdapter<TArticle> adapter;
  final VoidCallback closeAction;
  final TArticle record;
  final String viewerTitle;
  final bool editable;
  final bool removable;


  const _TWSArticleTableDetails({
    required this.closeAction,
    required this.adapter,
    required this.record,
    required this.editable,
    required this.removable,
    required this.viewerTitle
  });

  void _closeDetails(_TWSArticleTableDetailsState state) {
    state.editing = false;
  }

  @override
  Widget build(BuildContext context) {
    final _TWSArticleTableDetailsState state = _TWSArticleTableDetailsState();

    final TWSAThemeBase themeBase = getTheme<TWSAThemeBase>();

    final CSMColorThemeOptions tPage = themeBase.page;
    final CSMStateThemeOptions tCritical = themeBase.criticalControlState;

    return ColoredBox(
      color: tPage.main,
      child: TWSFrameDecoration(
        topPadding: 0,
        child: Padding(
          padding: const EdgeInsets.all(12.0),
          child: CSMDynamicWidget<_TWSArticleTableDetailsState>(
            state: state,
            designer: (BuildContext ctx, _TWSArticleTableDetailsState state) {
              final Widget? editionForm = adapter.composeEditor(record, () => _closeDetails(state), context);
              if (state._editing && editionForm != null) {
                return editionForm;
              }

              return Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: <Widget>[
                  // --> Details section actions
                  CSMSpacingRow(
                    spacing: 8,
                    mainAlignment: MainAxisAlignment.end,
                    children: <Widget>[
                      // --> Title
                      Expanded(
                        child: Text(
                          viewerTitle,
                          style: const TextStyle(
                            fontSize: 19,
                            fontWeight: FontWeight.bold,
                            fontStyle: FontStyle.italic,
                          ),  
                        ),
                      ),
                      // --> Close details action
                      _TWSArticleTableDetailsAction(
                        hint: 'Close record details',
                        icon: Icons.close,
                        action: closeAction,
                      ),
                      // --> Remove action.
                      if(removable)
                      _TWSArticleTableDetailsAction(
                        hint: 'Remove record',
                        icon: Icons.remove,
                        fore: tCritical.main.background,
                        action: () => adapter.onRemoveRequest(record, context),
                      ),
                      
                      if(editable)
                      if(editionForm != null)
                        _TWSArticleTableDetailsAction(
                          hint: 'Edit record',
                          icon: Icons.edit,
                          action: () {
                            state.editing = true;
                          },
                        ),
                    ],
                  ),
                  // --> Details custom content
                  Expanded(
                    child: Padding(
                      padding: const EdgeInsets.only(
                        top: 12,
                      ),
                      child: adapter.composeViewer(record, context),
                    ),
                  ),
                ],
              );
            },
          ),
        ),
      ),
    );
  }
}

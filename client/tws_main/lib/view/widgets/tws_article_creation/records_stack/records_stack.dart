part of '../tws_article_creation.dart';

class _RecordsStack<TModel> extends StatelessWidget {
  final List<TWSArticleCreationItemState<TModel>> states;
  final CSMColorThemeOptions pageTheme;
  final double creatorWidth;
  final Widget Function(TModel actualModel, bool isSelected) itemDesigner;
  final int currentItemIndex;
  final void Function(int index) changeItem;

  final void Function(int currentItem) remove;
  final void Function() add;

  const _RecordsStack({
    required this.pageTheme,
    required this.states,
    required this.creatorWidth,
    required this.itemDesigner,
    required this.currentItemIndex,
    required this.changeItem,
    required this.remove,
    required this.add,
  });

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions dangerTheme = getTheme<TWSAThemeBase>().primaryErrorControlColor;

    return CSMSpacingColumn(
      spacing: 12,
      includeEnd: true,
      children: <Widget>[
        // --> Actions
        CSMSpacingRow(
          mainAlignment: MainAxisAlignment.end,
          spacing: 8,
          includeEnd: true,
          includeStart: true,
          children: <Widget>[
            Expanded(
              child: Text(
                'Records: (${states.length})',
                style: TextStyle(
                  color: pageTheme.fore,
                ),
              ),
            ),
            // --> Add item action
            CSMPointerHandler(
              onClick: add,
              cursor: SystemMouseCursors.click,
              child: Icon(
                Icons.add_circle,
                size: 24,
                color: pageTheme.fore,
              ),
            ),
            // --> Remove selection
            CSMPointerHandler(
              onClick: () => remove(currentItemIndex),
              cursor: SystemMouseCursors.click,
              child: Icon(
                Icons.remove_circle,
                size: 24,
                color: dangerTheme.highlight,
              ),
            ),
          ],
        ),
        // --> Stack Display Content
        Expanded(
          child: LayoutBuilder(
            builder: (_, BoxConstraints constrains) {
              final ScrollController ctrl = ScrollController();
              WidgetsBinding.instance.addPostFrameCallback(
                (Duration timestamp) {
                  ctrl.animateTo(0, duration: 300.miliseconds, curve: Curves.easeOut);
                },
              );

              return ConstrainedBox(
                constraints: BoxConstraints(
                  minHeight: 0,
                  maxHeight: constrains.maxHeight,
                ),
                child: ListView.builder(
                  itemCount: states.length,
                  controller: ctrl,
                  itemBuilder: (BuildContext context, int index) {
                    final bool currentActive = currentItemIndex == index;

                    return CSMPointerHandler(
                      cursor: currentActive ? MouseCursor.defer : SystemMouseCursors.click,
                      onClick: () => changeItem(index),
                      child: CSMDynamicWidget<TWSArticleCreationItemState<TModel>>(
                        state: states[index],
                        designer: (BuildContext ctx, TWSArticleCreationItemState<TModel> state) {
                          return Padding(
                            padding: const EdgeInsets.only(
                              top: 3,
                            ),
                            child: SizedBox(
                              width: creatorWidth,
                              child: itemDesigner(state.model, currentActive),
                            ),
                          );
                        },
                      ),
                    );
                  },
                ),
              );
            },
          ),
        ),
      ],
    );
  }
}

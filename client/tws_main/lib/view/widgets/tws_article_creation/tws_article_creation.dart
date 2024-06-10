import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_creation_item_state.dart';
import 'package:tws_main/view/widgets/tws_section.dart';

part 'tws_article_creation_state.dart';

part 'creation_form/creation_form.dart';

part 'records_stack/records_stack.dart';

const double _kPadding = 8;
const double _kColWidthLimit = 300;

final class TWSArticleCreator<TModel> extends StatelessWidget {
  final TModel Function() factory;
  final Widget Function(TModel actualModel, bool isSelected) itemDesigner;
  final Widget Function(TWSArticleCreationItemState<TModel>? itemState) formDesigner;

  const TWSArticleCreator({
    super.key,
    required this.factory,
    required this.itemDesigner,
    required this.formDesigner,
  });

  @override
  Widget build(BuildContext context) {
    final TWSAThemeBase theme = getTheme();

    return CSMDynamicWidget<_TWSArticleCreationState<TModel>>(
      state: _TWSArticleCreationState<TModel>(factory),
      designer: (BuildContext context, _TWSArticleCreationState<TModel> state) {
        return LayoutBuilder(
          builder: (_, BoxConstraints cts) {
            final double calcWidth = ((cts.maxWidth / 2) - _kPadding);
            Size sizeFactor = const BoxConstraints(
              minWidth: _kColWidthLimit,
            ).constrain(Size(calcWidth, cts.maxHeight));
            if (sizeFactor.width <= _kColWidthLimit) {
              sizeFactor = Size(cts.maxWidth, sizeFactor.height);
            }

            return Padding(
              padding: const EdgeInsets.all(_kPadding),
              child: Wrap(
                children: <Widget>[
                  SizedBox.fromSize(
                    size: sizeFactor,
                    child: TWSSection(
                      title: 'Properties',
                      content: formDesigner(state.states.isEmpty ? null : state.states[state.current]),
                    ),
                  ),
                  SizedBox.fromSize(
                    size: sizeFactor,
                    child: _RecordsStack<TModel>(
                      add: state.addItem,
                      remove: state.removeItem,
                      pageTheme: theme.page,
                      states: state.states,
                      creatorWidth: sizeFactor.width,
                      itemDesigner: itemDesigner,
                      changeItem: state.changeSelection,
                      currentItemIndex: state.current,
                    ),
                  ),
                ],
              ),
            );
          },
        );
      },
    );
  }
}

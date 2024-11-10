import 'dart:async';

import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_administration_view/view/components/tws_article_creation/tws_article_agent.dart';
import 'package:tws_administration_view/view/components/tws_article_creation/tws_article_creation_item_state.dart';
import 'package:tws_administration_view/view/components/tws_article_creation/tws_article_creator_feedback.dart';
import 'package:tws_administration_view/view/components/tws_section.dart';

part 'tws_article_creator_state.dart';

part 'records_stack/tes_article_creator_records_stack.dart';

final CSMRouter _router = CSMRouter.i;

const double _kPadding = 8;
const double _kColWidthLimit = 300;

final class TWSArticleCreator<TModel> extends StatefulWidget {
  final TModel Function() factory;
  final bool Function(TModel model)? modelValidator;
  final FutureOr<List<TWSArticleCreatorFeedback>> Function(List<TModel> records)? onCreate;
  final Widget Function(TModel actualModel, bool selected, bool valid) itemDesigner;
  final Widget Function(TWSArticleCreatorItemState<TModel>? itemState) formDesigner;
  final TWSArticleCreatorAgent<TModel>? agent;
  final VoidCallback? afterClose;

  const TWSArticleCreator({
    super.key,
    this.agent,
    this.onCreate,
    this.modelValidator,
    this.afterClose,
    required this.factory,
    required this.itemDesigner,
    required this.formDesigner,
  });

  @override
  State<TWSArticleCreator<TModel>> createState() => _TWSArticleCreatorState<TModel>();
}

class _TWSArticleCreatorState<TModel> extends State<TWSArticleCreator<TModel>> {
  late _TWSArticleCreationState<TModel> mainState;

  @override
  void initState() {
    super.initState();
    mainState = _TWSArticleCreationState<TModel>(widget.factory);
    widget.agent?.addListener(submitRecords);
  }

  @override
  void didUpdateWidget(covariant TWSArticleCreator<TModel> oldWidget) {
    super.didUpdateWidget(oldWidget);
    widget.agent?.addListener(submitRecords);
  }

  void submitRecords() async {
    FutureOr<List<TWSArticleCreatorFeedback>> Function(List<TModel> models)? creator = widget.onCreate;
    bool Function(TModel)? validator = widget.modelValidator;
    if (creator == null) return;
    List<TModel> models = <TModel>[];

    if (validator != null) {
      bool error = false;

      for (TWSArticleCreatorItemState<TModel> state in mainState.states) {
        TModel model = state.model;
        models.add(model);

        bool isValid = validator(model);
        state.updateInvalid(isValid);
        if (isValid) continue;
        error = true;
      }

      mainState.effect();
      if (error) {
        return;
      }
    } else {
      models = mainState.states.map((TWSArticleCreatorItemState<TModel> i) => i.model).toList();
    }

    List<TWSArticleCreatorFeedback> feedbacks = await creator(models);
    if (feedbacks.isEmpty) {
      _router.pop();
      widget.afterClose?.call();
    }
  }

  @override
  Widget build(BuildContext context) {
    final TWSAThemeBase theme = getTheme();

    return CSMDynamicWidget<_TWSArticleCreationState<TModel>>(
      state: mainState,
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
                      content: widget.formDesigner(state.states.isEmpty ? null : state.states[state.current]),
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
                      itemDesigner: widget.itemDesigner,
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

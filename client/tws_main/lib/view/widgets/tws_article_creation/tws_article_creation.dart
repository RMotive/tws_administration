import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_article_creation/interfaces/tws_article_creation_property_interface.dart';

part 'tws_article_creation_state.dart';
part 'tws_article_creation_item_state.dart';

part 'creation_form/creation_form.dart';

part 'records_stack/records_stack.dart';

const double _kPadding = 8;
const double _kColWidthLimit = 300;

final class TWSArticleCreator<TModel> extends StatelessWidget {
  final TModel Function() factory;
  final List<TWSArticleCreationPropertyInterface> properties;

  const TWSArticleCreator({
    super.key,
    required this.factory,
    required this.properties,
  }) : assert(properties.length > 0, 'The form must contain at least one [properties]');

  @override
  Widget build(BuildContext context) {
    final TWSAThemeBase theme = getTheme();

    return CSMDynamicWidget<_TWSArticleCreationState<TModel>>(
        state: _TWSArticleCreationState<TModel>(factory),
        designer: (BuildContext context, _TWSArticleCreationState<TModel> state) {
          return LayoutBuilder(
            builder: (_, BoxConstraints cts) {
              final double calcWidth = ((cts.maxWidth / 2) - _kPadding);
              double widthFactor = const BoxConstraints(
                minWidth: _kColWidthLimit,
              ).constrainWidth(calcWidth);
              if (widthFactor <= _kColWidthLimit) {
                widthFactor = cts.maxWidth;
              }

              return Padding(
                padding: const EdgeInsets.all(_kPadding),
                child: Wrap(
                  children: <Widget>[
                    SizedBox(
                      height: 200,
                      width: widthFactor,
                      child: _RecordsStack(
                        pageTheme: theme.page,
                        recordsCount: state.states.length,
                      ),
                    ),
                    ColoredBox(
                      color: Colors.green,
                      child: SizedBox(
                        width: widthFactor,
                        height: 200,
                        child: _CreationForm<TModel>(state.states[state.current]),
                      ),
                    ),
                  ],
                ),
              );
            },
          );
        }
    );
  }
}

import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';

part 'tws_article_creation_state.dart';

part 'creation_form/creation_form.dart';

part 'records_stack/records_stack.dart';

const double _padding = 8;

final class TWSArticleCreator<TModel> extends StatelessWidget {
  const TWSArticleCreator({super.key});

  @override
  Widget build(BuildContext context) {
    return CSMDynamicWidget<_TWSArticleCreationState>(
      state: _TWSArticleCreationState(),
      designer: (_, CSMStateBase state) {
        return LayoutBuilder(
          builder: (_, BoxConstraints cts) {
            final double calcWidth = ((cts.maxWidth / 2) - _padding);
            final double widthFactor = const BoxConstraints(minWidth: 500).constrainWidth(calcWidth);

            return Padding(
              padding: const EdgeInsets.all(_padding),
              child: Wrap(
                children: <Widget>[
                  ColoredBox(
                    color: Colors.red,
                    child: SizedBox(
                      height: 200,
                      width: widthFactor,
                      child: const _RecordsStack(),
                    ),
                  ),
                  ColoredBox(
                    color: Colors.green,
                    child: SizedBox(
                      width: widthFactor,
                      height: 200,
                      child: const _CreationForm(),
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

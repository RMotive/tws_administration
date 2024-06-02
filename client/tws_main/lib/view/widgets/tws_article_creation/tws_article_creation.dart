import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';

part 'creation_form/creation_form.dart';

part 'records_stack/records_stack.dart';

const double _kPadding = 8;
const double _kColWidthLimit = 300;

final class TWSArticleCreator<TModel> extends StatelessWidget {
  final Widget Function(TModel context) displayDesigner;

  const TWSArticleCreator({
    super.key,
    required this.displayDesigner,
  });

  @override
  Widget build(BuildContext context) {
    final TWSAThemeBase theme = getTheme();

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
                  recordsCount: 0,
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
  }
}

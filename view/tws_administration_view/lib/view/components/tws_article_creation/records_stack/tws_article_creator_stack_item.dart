import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_administration_view/view/components/tws_article_creation/records_stack/tws_article_creator_stack_item_property.dart';

final class TWSArticleCreationStackItem extends StatelessWidget {
  final bool selected;
  final List<TwsArticleCreationStackItemProperty> properties;
  final bool expand;
  final bool valid;

  const TWSArticleCreationStackItem({
    super.key,
    this.expand = false,
    this.valid = true,
    required this.properties,
    required this.selected,
  });

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions pageTheme = getTheme<TWSAThemeBase>().page;
    final CSMColorThemeOptions dangerTheme = getTheme<TWSAThemeBase>().primaryCriticalControl;

    return DecoratedBox(
      position: DecorationPosition.foreground,
      decoration: BoxDecoration(
        border: Border.fromBorderSide(
          BorderSide(
            width: valid ? 0 : 1.5,
            color: valid ? Colors.transparent : dangerTheme.highlight,
          ),
        ),
      ),
      child: ColoredBox(
        color: pageTheme.fore.withAlpha(selected ? 126 : 64),
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: DefaultTextStyle(
            style: TextStyle(
              color: pageTheme.hightlightAlt ?? Colors.black,
              fontSize: 14,
              fontWeight: FontWeight.w600,
            ),
            child: IntrinsicWidth(
              child: Wrap(
                runSpacing: 10,
                children: properties,
              ),
            ),
          ),
        ),
      ),
    );
  }
}

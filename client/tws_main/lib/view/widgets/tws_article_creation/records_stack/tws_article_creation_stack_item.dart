import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_article_creation/records_stack/tws_article_creation_stack_item_property.dart';

final class TWSArticleCreationStackItem extends StatelessWidget {
  final bool isSelected;
  final List<TwsArticleCreationStackItemProperty> properties;
  final bool expand;

  const TWSArticleCreationStackItem({
    super.key,
    this.expand = false,
    required this.properties,
    required this.isSelected,
  });

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions pageTheme = getTheme<TWSAThemeBase>().page;

    return ColoredBox(
      color: pageTheme.fore.withAlpha(isSelected ? 126 : 64),
      child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: DefaultTextStyle(
          style: TextStyle(
            color: pageTheme.main,
            fontSize: 14,
            fontWeight: FontWeight.w600,
          ),
          child: IntrinsicWidth(
            child: Wrap(
              children: properties,
            ),
          ),
        ),
      ),
    );
  }
}

import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';

/// [TWSSectionDivider] a custom divider component to divide the creation sections in 
/// Trucks, Trailers and drivers sections.
/// This component shows horizontal line with a centered section name, ideal for dividing sections in a column.
class TWSSectionDivider extends StatelessWidget {
  final Color? color;
  final String text;
  const TWSSectionDivider({
    super.key,
    this.color,
    required this.text,
  });

  @override
  Widget build(BuildContext context) {
    Color? mainColor = color; 
    if(color == null){
      CSMColorThemeOptions pageColorTheme = getTheme<TWSAThemeBase>().page;
      mainColor = pageColorTheme.fore;
    }

    return Padding(
      padding: const EdgeInsets.all(5.0),
      child: Row(
        children: <Widget>[
          Expanded(
            child: Divider(
              color: mainColor,
            )
          ),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 8.0),
            child: Text(
              text,
              style: TextStyle(
                fontWeight: FontWeight.w100,
                fontStyle: FontStyle.italic,
                color: mainColor
              ),
            ),
          ),
          Expanded(
            child: Divider(
              color: mainColor,
            )
          )
        ],
      ),
    );
  }
}
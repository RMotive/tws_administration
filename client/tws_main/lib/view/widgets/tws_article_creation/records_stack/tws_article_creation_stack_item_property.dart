import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';

final class TwsArticleCreationStackItemProperty extends StatelessWidget {
  final String label;
  final String? value;
  final double? minWidth;
  final double? maxWidth;

  const TwsArticleCreationStackItemProperty({
    super.key,
    this.minWidth,
    this.maxWidth,
    required this.label,
    required this.value,
  });

  @override
  Widget build(BuildContext context) {
    String val = (value?.isEmpty ?? true) ? '---' : value!;

    return ConstrainedBox(
      constraints: BoxConstraints(
        minWidth: minWidth ?? maxWidth ?? 0,
        maxWidth: maxWidth ?? double.maxFinite,
      ),
      child: CSMSpacingColumn(
        crossAlignment: CrossAxisAlignment.start,
        spacing: 6,
        children: <Widget>[
          Text('$label:'),
          Text(val),
        ],
      ),
    );
  }
}

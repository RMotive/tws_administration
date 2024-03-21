import 'package:flutter/material.dart';
import 'package:tws_main/presentation/components/tws_display_flat.dart';

class TWSArticleTable<TArticle> extends StatelessWidget {
  final List<String> fields;

  const TWSArticleTable({
    super.key,
    required this.fields,
  });

  @override
  Widget build(BuildContext context) {
    return DecoratedBox(
      decoration: const BoxDecoration(
        border: Border.fromBorderSide(
          BorderSide(
            width: 3,
            color: Colors.blueGrey,
          ),
        ),
      ),
      child: IntrinsicWidth(
        child: Column(
          children: <Widget>[
            // --> Table header fields
            DecoratedBox(
              decoration: const BoxDecoration(
                border: Border.fromBorderSide(
                  BorderSide(
                    width: 3,
                    color: Colors.blueGrey,
                  ),
                ),
              ),
              child: Row(
                children: <Widget>[
                  for (String header in fields)
                    Padding(
                      padding: const EdgeInsets.symmetric(
                        vertical: 12,
                        horizontal: 8,
                      ),
                      child: Text(
                        header,
                      ),
                    ),
                ],
              ),
            ),
            const Visibility(
              child: Padding(
                padding: EdgeInsets.only(
                  top: 20,
                ),
                child: TWSDisplayFlat(
                  width: 400,
                  verticalPadding: 10,
                  display: 'No features yet',
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

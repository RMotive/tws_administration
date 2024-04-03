import 'package:cosmos_foundation/foundation/simplifiers/colored_sizedbox.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/view/widgets/tws_display_flat.dart';

class TWSArticleTable<TArticle> extends StatefulWidget {
  final List<String> fields;

  const TWSArticleTable({
    super.key,
    required this.fields,
  });

  @override
  State<TWSArticleTable<TArticle>> createState() => _TWSArticleTableState<TArticle>();
}

class _TWSArticleTableState<TArticle> extends State<TWSArticleTable<TArticle>> {
  // --> State resources
  TArticle? sltItem;

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        final double detailsWidth = sltItem != null ? 400 : 0;
        BoxConstraints pageBounds = constrains;
        if (!constrains.hasBoundedHeight) {
          pageBounds = constrains.tighten(
            height: constrains.minHeight,
          );
        }

        return Stack(
          children: <Widget>[
            // --> Table draw
            DecoratedBox(
              decoration: const BoxDecoration(
                border: Border.fromBorderSide(
                  BorderSide(
                    width: 2,
                    color: Colors.blueGrey,
                  ),
                ),
              ),
              child: SizedBox(
                width: pageBounds.maxWidth - detailsWidth,
                height: pageBounds.maxHeight,
                child: Column(
                  children: <Widget>[
                    // --> Table
                    DecoratedBox(
                      decoration: const BoxDecoration(
                        border: Border.fromBorderSide(
                          BorderSide(
                            width: 1,
                            color: Colors.blueGrey,
                          ),
                        ),
                      ),
                      child: Row(
                        children: <Widget>[
                          for (String header in widget.fields)
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
                    // --> Details drawer
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
            ),
            // --> Item detail
            Positioned(
              left: pageBounds.maxWidth - detailsWidth,
              child: Visibility(
                child: CosmosColorBox(
                  size: Size(detailsWidth, pageBounds.maxHeight),
                  color: Colors.purple.withOpacity(.2),
                ),
              ),
            ),
          ],
        );
      },
    );
  }
}

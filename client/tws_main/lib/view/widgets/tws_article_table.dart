import 'dart:math';

import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/twsa_colors.dart';
import 'package:tws_main/view/widgets/options/bases/tws_article_table_data_adapter.dart';
import 'package:tws_main/view/widgets/tws_paging_selector.dart';

class TWSArticleTable<TArticle> extends StatefulWidget {
  final List<String> fields;
  final TWSArticleTableDataAdapter<TArticle> adapter;

  const TWSArticleTable({
    super.key,
    required this.fields,
    required this.adapter,
  });

  @override
  State<TWSArticleTable<TArticle>> createState() => _TWSArticleTableState<TArticle>();
}

class _TWSArticleTableState<TArticle> extends State<TWSArticleTable<TArticle>> {
  // --> State resources
  TArticle? sltItem;

  Future<List<int>> call() async {
    Random random = Random.secure();

    return <int>[random.nextInt(120)];
  }

  @override
  Widget build(BuildContext context) {
    final CSMConsumerAgent agent = CSMConsumerAgent();

    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        final double detailsWidth = sltItem != null ? 400 : 0;
        const double pagingHeight = 50;
        BoxConstraints pageBounds = constrains;
        if (!constrains.hasBoundedHeight) {
          pageBounds = constrains.tighten(
            height: constrains.minHeight,
          );
        }      
        return Stack(
          children: <Widget>[
            // --> Table draw
            Column(
              children: <Widget>[
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
                    height: pageBounds.maxHeight - pagingHeight,
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
                        CSMConsumer<List<int>>(
                          consume: call,
                          delay: 1.seconds,
                          agent: agent,
                          emptyCheck: (List<int> data) => data.isEmpty,
                          loadingBuilder: (_) {
                            return const Padding(
                              padding: EdgeInsets.only(
                                top: 50,
                              ),
                              child: SizedBox(
                                width: 50,
                                height: 50,
                                child: CircularProgressIndicator(
                                  backgroundColor: TWSAColors.oceanBlueH,
                                  color: TWSAColors.oceanBlue,
                                  strokeWidth: 3,
                                ),
                              ),
                            );
                          },
                          errorBuilder: (BuildContext ctx, Object? error, List<int>? data) {
                            return Column(
                              children: <Widget>[
                                FloatingActionButton(
                                  onPressed: () {
                                    agent.refresh();
                                  },
                                ),
                                const Text('FAILURE'),
                              ],
                            );
                          },
                          successBuilder: (_, List<int> data) {
                            return Column(
                              children: <Widget>[
                                FloatingActionButton(
                                  onPressed: () {
                                    agent.refresh();
                                  },
                                ),
                                Text('SUCCESSES $data'),
                              ],
                            );
                          },
                        ),
                      ],
                    ),
                  ),
                ),
                const SizedBox(
                  height: pagingHeight,
                  child: Padding(
                    padding: EdgeInsets.symmetric(
                      horizontal: 12,
                    ),
                    child: TWSPagingSelector(
                      pages: 12,
                    ),
                  ),
                ),
              ],
            ),
            // --> Item detail
            Positioned(
              left: pageBounds.maxWidth - detailsWidth,
              child: Visibility(
                child: CSMColorBox(
                  size: Size(detailsWidth, pageBounds.maxHeight),
                  background: Colors.purple.withOpacity(.2),
                ),
              ),
            ),
          ],
        );
      },
    );
  }
}

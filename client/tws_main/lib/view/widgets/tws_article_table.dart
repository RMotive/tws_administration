import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/constants/twsa_colors.dart';
import 'package:tws_main/view/widgets/options/bases/tws_article_table_data_adapter.dart';
import 'package:tws_main/view/widgets/tws_display_flat.dart';
import 'package:tws_main/view/widgets/tws_paging_selector.dart';

class TWSArticleTable<TArticle extends CSMEncodeInterface> extends StatefulWidget {
  final List<String> fields;
  final TWSArticleTableDataAdapter<TArticle> adapter;
  final int page;
  final int size;
  final List<int> sizes;

  const TWSArticleTable({
    super.key,
    this.page = 1,
    required this.size,
    required this.sizes,
    required this.fields,
    required this.adapter,
  });

  @override
  State<TWSArticleTable<TArticle>> createState() => _TWSArticleTableState<TArticle>();
}

class _TWSArticleTableState<TArticle extends CSMEncodeInterface> extends State<TWSArticleTable<TArticle>> {
  late final TWSArticleTableDataAdapter<TArticle> adapter;
  late Future<MigrationView<TArticle>> Function() consume;

  // --> State resources
  late (int index, TArticle item)? selected;
  late List<TArticle> records;
  late int items;
  late int page;
  late int pages;
  late int size;
  late final List<int> sizes;

  void updatePaging(int page, int size) {
    setState(() {
      this.page = page;
      this.size = size;
      consume = () => adapter.consume(page, size, <MigrationViewOrderOptions>[]);
    });
  }

  @override
  void initState() {
    selected = null;
    page = widget.page;
    sizes = widget.sizes;
    pages = page;
    size = widget.size;
    items = 0;
    adapter = widget.adapter;
    records = <TArticle>[];
    consume = () => adapter.consume(page, size, <MigrationViewOrderOptions>[]);
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    final CSMConsumerAgent agent = CSMConsumerAgent();

    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        final double detailsWidth = selected != null ? 400 : 0;
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
                // --> Table content
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
                        // --> Table header draw
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
                        // --> Table items draw
                        CSMConsumer<MigrationView<TArticle>>(
                          consume: consume,
                          agent: agent,
                          emptyCheck: (MigrationView<TArticle> data) => data.sets.isEmpty,
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
                          errorBuilder: (BuildContext ctx, Object? error, MigrationView<TArticle>? data) {
                            return const Padding(
                              padding: EdgeInsets.only(
                                top: 50,
                                left: 20,
                                right: 20,
                              ),
                              child: TWSDisplayFlat(
                                width: 300,
                                display: 'Critical error reading view',
                              ),
                            );
                          },
                          successBuilder: (_, MigrationView<TArticle> data) {
                            setState(() {
                              items = data.amount;
                              pages = data.pages;
                            });

                            return ListView.builder(
                              itemCount: data.sets.length,
                              itemBuilder: (BuildContext context, int index) {
                                return const Text('Success');
                              },
                            );
                          },
                        ),
                      ],
                    ),
                  ),
                ),
                // --> Paging selection
                SizedBox(
                  height: pagingHeight,
                  child: Padding(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 12,
                    ),
                    child: TWSPagingSelector(
                      pages: pages,
                      size: size,
                      items: items,
                      sizes: sizes,
                      onChange: updatePaging,
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

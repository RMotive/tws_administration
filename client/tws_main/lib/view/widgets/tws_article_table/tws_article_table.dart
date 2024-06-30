import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/constants/twsa_colors.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_agent.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_data_adapter.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_field_options.dart';
import 'package:tws_main/view/widgets/tws_display_flat.dart';
import 'package:tws_main/view/widgets/tws_paging_selector.dart';

part 'tws_article_table_details/tws_article_table_details.dart';

class TWSArticleTable<TArticle extends CSMEncodeInterface> extends StatefulWidget {
  final List<TWSArticleTableFieldOptions<TArticle>> fields;
  final TWSArticleTableDataAdapter<TArticle> adapter;
  final TWSArticleTableAgent? agent;
  final int page;
  final int size;
  final List<int> sizes;

  const TWSArticleTable({
    super.key,
    this.page = 1,
    this.agent,
    required this.size,
    required this.sizes,
    required this.fields,
    required this.adapter,
  });

  @override
  State<TWSArticleTable<TArticle>> createState() => _TWSArticleTableState<TArticle>();
}

class _TWSArticleTableState<TArticle extends CSMEncodeInterface> extends State<TWSArticleTable<TArticle>> with SingleTickerProviderStateMixin {
  late Future<MigrationView<TArticle>> Function() consume;
  late AnimationController detailsAnimationController;
  late Animation<double> detailsAnimation;

  final CSMConsumerAgent agent = CSMConsumerAgent();
  late final TWSArticleTableDataAdapter<TArticle> adapter;

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
      this.consume = () => adapter.consume(page, size, <MigrationViewOrderOptions>[]);
    });
    agent.refresh();
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
    detailsAnimationController = AnimationController(vsync: this, duration: 400.miliseconds);
    detailsAnimation = Tween<double>(
      begin: 0,
      end: 400,
    ).animate(detailsAnimationController);
    widget.agent?.addListener(agent.refresh);
    super.initState();
  }

  @override
  void dispose() {
    widget.agent?.removeListener(agent.refresh);
    detailsAnimationController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        const double pagingHeight = 50;
        final double detailsWidth = selected != null ? 400 : 0;
        BoxConstraints pageBounds = constrains;
        if (!constrains.hasBoundedHeight) {
          pageBounds = constrains.tighten(
            height: constrains.minHeight,
          );
        }

        final double viewWidth = pageBounds.maxWidth;
        final double tableWidth = viewWidth - detailsWidth;
        final double cellWidth = tableWidth / widget.fields.length;
        
        return SizedBox(
          width: viewWidth,
          child: AnimatedBuilder(
            animation: detailsAnimation,
            builder: (_, __) {
              final double animationComputationValue = tableWidth - detailsAnimation.value;

              return Stack(
                children: <Widget>[
                  // --> Table draw
                  SizedBox(
                    width: animationComputationValue,
                    child: Column(
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
                                      for (TWSArticleTableFieldOptions<TArticle> header in widget.fields)
                                        ConstrainedBox(
                                          constraints: const BoxConstraints(
                                            minWidth: 150,
                                          ),
                                          child: SizedBox(
                                            width: cellWidth,
                                            child: Padding(
                                              padding: const EdgeInsets.symmetric(
                                                vertical: 12,
                                                horizontal: 8,
                                              ),
                                              child: Text(
                                                header.name,
                                                style: const TextStyle(
                                                  color: Colors.white,
                                                  fontWeight: FontWeight.w900,
                                                ),
                                              ),
                                            ),
                                          ),
                                        ),
                                    ],
                                  ),
                                ),
                                // --> Table items
                                Expanded(
                                  child: CSMConsumer<MigrationView<TArticle>>(
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
                                      if (items != data.amount || pages != data.pages || records != data.sets) {
                                        WidgetsBinding.instance.addPostFrameCallback(
                                          (Duration timeStamp) {
                                            setState(() {
                                              records = data.sets;
                                              items = data.amount;
                                              pages = data.pages;
                                            });
                                          },
                                        );
                                      }

                                      return SizedBox(
                                        height: pageBounds.maxHeight - 100,
                                        child: ListView.builder(
                                          itemCount: data.sets.length,
                                          itemBuilder: (BuildContext context, int index) {
                                            return CSMPointerHandler(
                                              cursor: SystemMouseCursors.click,
                                              onClick: () {
                                                setState(() {
                                                  if (selected?.$1 == index) {
                                                    selected = null;
                                                  } else {
                                                    selected = (index, data.sets[index]);
                                                  }
                                                });
                                              },
                                              child: DecoratedBox(
                                                decoration: BoxDecoration(
                                                  color: selected != null ? Colors.blueGrey : Colors.transparent,
                                                ),
                                                child: Row(
                                                  children: <Widget>[
                                                    for (TWSArticleTableFieldOptions<TArticle> field in widget.fields)
                                                      ConstrainedBox(
                                                        constraints: const BoxConstraints(
                                                          minWidth: 150,
                                                        ),
                                                        child: SizedBox(
                                                          width: cellWidth,
                                                          child: Padding(
                                                            padding: const EdgeInsets.symmetric(
                                                              vertical: 6,
                                                              horizontal: 8,
                                                            ),
                                                            child: Builder(builder: (BuildContext context) {
                                                              final String cellValue = field.factory(data.sets[index], index, context);
                                                              final Widget textWidget = Text(
                                                                cellValue,
                                                                maxLines: 2,
                                                                overflow: TextOverflow.ellipsis,
                                                              );

                                                              if (!field.tip) {
                                                                return textWidget;
                                                              }

                                                              return Tooltip(
                                                                message: cellValue,
                                                                child: textWidget,
                                                              );
                                                            }),
                                                          ),
                                                        ),
                                                      ),
                                                  ],
                                                ),
                                              ),
                                            );
                                          },
                                        ),
                                      );
                                    },
                                  ),
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
                              items: records.length,
                              sizes: sizes,
                              total: items,
                              onChange: updatePaging,
                            ),
                          ),
                        ),
                      ],
                    ),
                  ),
                  // --> Item detail
                  Positioned(
                    right: -detailsAnimation.value,
                    child: Visibility(
                      visible: selected != null,
                      child: const _TWSArticleTableDetails(),
                    ),
                  ),
                ],
              );
            },
          ),
        );
      },
    );
  }
}

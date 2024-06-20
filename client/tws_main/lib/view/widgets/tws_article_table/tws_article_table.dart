
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
part 'tws_article_table_header/tws_article_table_header.dart';
part 'tws_article_table_loading.dart';
part 'tws_article_table_error.dart';

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
  static const double _pagingHeight = 50;
  static const double _minFieldWidth = 200;
  static const double _detailsWidth = 400;

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
    detailsAnimationController = AnimationController(vsync: this, duration: 200.miliseconds);
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

  void _checkPagingChanges(MigrationView<TArticle> data) {
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
  }

  void _onRecordClicked(int index, TArticle set) {
    setState(() {
      if (selected?.$1 == index) {
        selected = null;
        detailsAnimationController.reverse();
      } else {
        selected = (index, set);
        detailsAnimationController.forward();
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        BoxConstraints pageBounds = constrains;
        if (!constrains.hasBoundedHeight) {
          pageBounds = constrains.tighten(
            height: constrains.minHeight,
          );
        }
        final Size viewSize = pageBounds.biggest;
        final bool detailsFullDisplay = viewSize.width <= (_detailsWidth * 2);

        return SizedBox(
          width: viewSize.width,
          child: AnimatedBuilder(
            animation: detailsAnimation,
            builder: (_, __) {
              final double animationComputationValue = viewSize.width - detailsAnimation.value;
              final double cellWidth = animationComputationValue / widget.fields.length;

              return Stack(
                children: <Widget>[
                  // --> Table
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: <Widget>[
                      // --> Table content
                      Expanded(
                        child: DecoratedBox(
                          decoration: const BoxDecoration(
                            border: Border.fromBorderSide(
                              BorderSide(
                                width: 2,
                                color: Colors.blueGrey,
                              ),
                            ),
                          ),
                          child: SingleChildScrollView(
                            scrollDirection: Axis.horizontal,
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: <Widget>[
                                // Table header draw
                                _TWSArticleTableHeader<TArticle>(
                                  fields: widget.fields,
                                  minFieldWidth: _minFieldWidth,
                                  fieldWidth: cellWidth,
                                ),
                                // --> Table items
                                Expanded(
                                  child: CSMConsumer<MigrationView<TArticle>>(
                                    consume: consume,
                                    agent: agent,
                                    emptyCheck: (MigrationView<TArticle> data) => data.sets.isEmpty,
                                    loadingBuilder: (_) => const _TWSArticleTableLoading(),
                                    errorBuilder: (_, __, ___) => const _TWSArticleTableError(),
                                    successBuilder: (_, MigrationView<TArticle> data) {
                                      _checkPagingChanges(data);
                  
                                      return SizedBox(
                                        height: pageBounds.maxHeight - 100,
                                        child: Column(
                                          children: List<Widget>.generate(
                                            data.sets.length,
                                            (int index) {
                                              return CSMPointerHandler(
                                                cursor: SystemMouseCursors.click,
                                                onClick: () => _onRecordClicked(index, data.sets[index]),
                                                child: DecoratedBox(
                                                  decoration: BoxDecoration(
                                                    color: selected?.$1 == index ? Colors.blueGrey : Colors.transparent,
                                                  ),
                                                  child: Row(
                                                    children: <Widget>[
                                                      for (TWSArticleTableFieldOptions<TArticle> field in widget.fields)
                                                        ConstrainedBox(
                                                          constraints: const BoxConstraints(
                                                            minWidth: _minFieldWidth,
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
                                        ),
                                      );
                                    },
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),
                      // --> Paging selection
                      ConstrainedBox(
                        constraints: BoxConstraints(
                          minWidth: animationComputationValue,
                        ),
                        child: ColoredBox(
                          color: Colors.red,
                          child: SizedBox(
                            height: _pagingHeight,
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
                        ),
                      ),
                    ],
                  ),
                  // --> Item detail
                  Positioned(
                    left: animationComputationValue,
                    width: detailsFullDisplay ? viewSize.width : _detailsWidth,
                    height: viewSize.height,
                    child: const Visibility(
                      visible: true,
                      child: _TWSArticleTableDetails(),
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

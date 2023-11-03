import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';

/// Public Widget to draw a themed data informative table.
///
/// This draws a complex themed informative table for TWS information.
class TWSInfoTable extends StatelessWidget {
  /// Indicates the link between the table header and the property that will be displayed at that cell from the object.
  final List<TableHeaderPropertyOption> headersLinks;

  /// Indicates the text style bundle that the table will use to stylish the texts.
  ///
  /// Will use the next properties to draw each section.
  /// [style] to style the table title.
  /// [subject] to style the table headers.
  /// [content] to style the table cells.
  final StyleBundle? themedStyleBundle;

  /// Indicates the collection of data objets that will be used to draw the table content.
  final List<Map<String, dynamic>> data;

  /// Draws a TWS themed informative table.
  const TWSInfoTable({
    super.key,
    required this.headersLinks,
    required this.data,
    this.themedStyleBundle,
  });

  @override
  Widget build(BuildContext context) {
    final ScrollController controler = ScrollController();
    controler.addListener(() {});

    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        return SingleChildScrollView(
          controller: controler,
          scrollDirection: Axis.horizontal,
          child: ConstrainedBox(
            constraints: BoxConstraints(
              minWidth: constrains.maxWidth,
            ),
            child: SingleChildScrollView(
              child: IntrinsicWidth(
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: <Widget>[
                    for (int headerPointer = 0; headerPointer < headersLinks.length; headerPointer++)
                      Expanded(
                        child: Column(
                          children: <Widget>[
                            // --> Header draw
                            ConstrainedBox(
                              constraints: const BoxConstraints(
                                minWidth: 100,
                              ),
                              child: Center(
                                child: Padding(
                                  padding: const EdgeInsets.symmetric(
                                    horizontal: 20,
                                  ),
                                  child: Text(
                                    headersLinks[headerPointer].tableHeader,
                                    style: themedStyleBundle?.subject ??
                                        const TextStyle(
                                          fontWeight: FontWeight.w600,
                                          fontSize: 16,
                                        ),
                                  ),
                                ),
                              ),
                            ),
                            // --> Spacer between header and data table content
                            const SizedBox.square(
                              dimension: 20,
                            ),
                            // --> Data table content drawing
                            for (int dataPointer = 0; dataPointer < data.length; dataPointer++)
                              Padding(
                                padding: EdgeInsets.only(
                                  top: dataPointer == 0 ? 0 : 15,
                                ),
                                child: DecoratedBox(
                                  decoration: BoxDecoration(
                                    color: const Color(0xffF2F3FE),
                                    borderRadius: BorderRadius.horizontal(
                                      left: Radius.circular(
                                        headerPointer == 0 ? 12 : 0,
                                      ),
                                      right: Radius.circular(
                                        headerPointer == headersLinks.length - 1 ? 12 : 0,
                                      ),
                                    ),
                                  ),
                                  child: ConstrainedBox(
                                    constraints: const BoxConstraints(
                                      minWidth: 100,
                                    ),
                                    child: SizedBox(
                                      child: Padding(
                                        padding: const EdgeInsets.symmetric(
                                          vertical: 12,
                                        ),
                                        child: Center(
                                          child: Padding(
                                            padding: const EdgeInsets.symmetric(
                                              horizontal: 20,
                                            ),
                                            child: ConstrainedBox(
                                              constraints: const BoxConstraints(
                                                maxWidth: 200,
                                              ),
                                              child: Text(
                                                ((data[dataPointer][headersLinks[headerPointer].objectProperty] as dynamic) ?? '- - -').toString(),
                                                maxLines: 1,
                                                style: themedStyleBundle?.content ??
                                                    const TextStyle(
                                                      fontSize: 13,
                                                      overflow: TextOverflow.ellipsis,
                                                    ),
                                              ),
                                            ),
                                          ),
                                        ),
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                          ],
                        ),
                      ),
                  ],
                ),
              ),
            ),
          ),
        );
      },
    );
  }
}

final class TableHeaderPropertyOption {
  final String tableHeader;
  final String objectProperty;

  const TableHeaderPropertyOption(
    this.tableHeader,
    this.objectProperty,
  );
}

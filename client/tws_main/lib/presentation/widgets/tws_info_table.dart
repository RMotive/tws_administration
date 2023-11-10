import 'package:flutter/material.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';

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

  bool isLargestEntry(Map<String, dynamic> currentItem, TableHeaderPropertyOption header) {
    final String currentGreatestEntry = currentItem.entries.reduce(
      (MapEntry<String, dynamic> a, MapEntry<String, dynamic> b) {
        final int aValueLength = (a.value ?? '---').toString().length;
        final int bValueLength = (b.value ?? '---').toString().length;
        return (aValueLength > bValueLength) ? a : b;
      },
    ).value;
    final String currentItemValue = (currentItem[header.objectProperty] ?? '---').toString();
    return currentGreatestEntry == currentItemValue;
  }

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (_, BoxConstraints parentTableContainerConstraints) {
        return SingleChildScrollView(
          scrollDirection: Axis.horizontal,
          child: ConstrainedBox(
            constraints: BoxConstraints(
              minWidth: parentTableContainerConstraints.maxWidth,
            ),
            child: Table(
              textBaseline: TextBaseline.alphabetic,
              defaultColumnWidth: const IntrinsicColumnWidth(),
              children: <TableRow>[
                // --> This row draws the column headers
                TableRow(
                  children: <Widget>[
                    for (TableHeaderPropertyOption header in headersLinks)
                      ConstrainedBox(
                        constraints: const BoxConstraints(
                          minWidth: 100,
                        ),
                        child: Center(
                          child: Text(
                            header.tableHeader,
                            style: themedStyleBundle?.subject ??
                                TextStyle(
                                  color: twsTheme.primaryColor.textColor,
                                  fontWeight: FontWeight.w600,
                                  fontSize: 18,
                                ),
                          ),
                        ),
                      ),
                  ],
                ),
                // --> Here we start drawing the items rows.
                for (Map<String, dynamic> item in data)
                  TableRow(
                    children: <Widget>[
                      for (TableHeaderPropertyOption header in headersLinks)
                        TableCell(
                          verticalAlignment: isLargestEntry(item, header) ? TableCellVerticalAlignment.top : TableCellVerticalAlignment.fill,
                          child: Padding(
                            padding: const EdgeInsets.only(
                              top: 12,
                            ),
                            child: DecoratedBox(
                              decoration: BoxDecoration(
                                color: const Color(0x4fd5c9f2),
                                borderRadius: BorderRadius.horizontal(
                                  left: Radius.circular(header == headersLinks.first ? 12 : 0),
                                  right: Radius.circular(header == headersLinks.last ? 12 : 0),
                                ),
                              ),
                              child: Padding(
                                padding: const EdgeInsets.symmetric(
                                  horizontal: 10,
                                  vertical: 12,
                                ),
                                child: ConstrainedBox(
                                  constraints: const BoxConstraints(
                                    maxWidth: 250,
                                  ),
                                  child: Center(
                                    child: Text(
                                      item[header.objectProperty].toString(),
                                      textAlign: TextAlign.center,
                                      maxLines: 3,
                                      overflow: TextOverflow.fade,
                                      style: themedStyleBundle?.content ??
                                          TextStyle(
                                            fontSize: 14,
                                            color: twsTheme.onPrimaryColorFirstControlColor.textColor,
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
              ],
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
